using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Scraper.Extensions;
using MyWallet.Scraper.Interfaces;

namespace MyWallet.Domain.Services
{
    public class RecommendedWalletService : IRecommendedWalletService
    {
        private readonly ITickerRepository _tickerRepository;
        private readonly IScraperStrategyResolver _scraperStrategyResolver;
        private readonly IRecommendedWalletRepository _recommendedWalletRepository;

        public RecommendedWalletService(ITickerRepository tickerRepository, IScraperStrategyResolver scraperStrategyResolver, IRecommendedWalletRepository recommendedWalletRepository)
        {
            _tickerRepository = tickerRepository;
            _scraperStrategyResolver = scraperStrategyResolver;
            _recommendedWalletRepository = recommendedWalletRepository;
        }

        public async Task Create(RecommendedWalletDto recommendedWalletDto)
        {
            var recommendedWallet = new RecommendedWallet(recommendedWalletDto);

            await _recommendedWalletRepository.Add(recommendedWallet);
        }

        public async Task AddRecommendation(long recommendedWalletId, RecommendationDto recommendationDto)
        {
            var recommendedWallet = await _recommendedWalletRepository.GetById(recommendedWalletId);

            var ticker = (await _tickerRepository.FilterBy(x => x.Code == recommendationDto.TickerCode)).FirstOrDefault();

            if (recommendedWallet is null || ticker is null)
            {
                return;
            }

            var recommendation = new Recommendation(recommendationDto);

            recommendation.AddTicker(ticker);

            recommendedWallet.AddRecommendation(recommendation);

            await _recommendedWalletRepository.Update(recommendedWallet);
        }

        public async Task UpdatePrices(long recommendedWalletId)
        {
            var recommendedWallet = (await _recommendedWalletRepository.FilterBy(x => x.Active && x.Id == recommendedWalletId)).FirstOrDefault();

            if (recommendedWallet is null)
            {
                return;
            }

            var tickersId = recommendedWallet.Recommendations.Select(x => x.TickerId);

            var tickers = (await _tickerRepository.FilterBy(x => tickersId.Contains(x.Id))).ToList();

            foreach (var ticker in tickers)
            {
                var scraperStrategyResponse = await _scraperStrategyResolver.FindStrategy(ticker.Company.Category.GetHashCode())
                    .Execute(ticker.Code);

                if (!scraperStrategyResponse.IsValid())
                {
                    continue;
                }

                ticker.Update(scraperStrategyResponse.GetPrice());
            }

            await _tickerRepository.Update(tickers);
        }

        public async Task<IList<RecommendationBuy>> RecommendationsBuy()
        {
            var recommendedWallets = await _recommendedWalletRepository.GetAll();

            var recommendations = new List<Recommendation>();

            foreach (var wallet in recommendedWallets)
            {
                recommendations.AddRange(wallet.Recommendations);
            }

            var recommendationsBuy = new List<RecommendationBuy>();

            foreach (var recommendation in recommendations)
            {
                if (!recommendation.HasBuyRecommendation())
                {
                    continue;
                }

                recommendationsBuy.Add(new RecommendationBuy(
                    recommendation.Ticker.Company.Category.ToString(),
                    recommendation.Ticker.Code,
                    recommendation.Discount(),
                    0,
                    recommendation.Ticker.Price,
                    0
                ));
            }

            return recommendationsBuy
                .OrderByDescending(x => x.Discount)
                .ToList();
        }
    }
}
