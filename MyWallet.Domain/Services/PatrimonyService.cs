using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;
using MyWallet.Globalization;
using MyWallet.Scraper.Enums;
using MyWallet.Scraper.Extensions;
using MyWallet.Scraper.Interfaces;

namespace MyWallet.Domain.Services
{
    public class PatrimonyService : IPatrimonyService
    {
        private readonly INotifier _notifier;
        private readonly IPatrimonyRepository _patrimonyRepository;
        private readonly INegociationRepository _negociationRepository;
        private readonly IScraperStrategyResolver _scraperStrategyResolver;
        private readonly IRecommendedWalletRepository _recommendedWalletRepository;

        public PatrimonyService(INotifier notifier,
            IPatrimonyRepository patrimonyRepository, 
            INegociationRepository negociationRepository, 
            IScraperStrategyResolver scraperStrategyResolver, 
            IRecommendedWalletRepository recommendedWalletRepository)
        {
            _notifier = notifier;
            _patrimonyRepository = patrimonyRepository;
            _negociationRepository = negociationRepository;
            _scraperStrategyResolver = scraperStrategyResolver;
            _recommendedWalletRepository = recommendedWalletRepository;
        }

        public async Task Consolidate()
        {
            var negociations = await _negociationRepository.FilterBy(x => true);

            negociations = negociations.OrderBy(x => x.DateOperation);

            var patrimonies = BuildPatrimonies(negociations);

            var validPatrimonies = patrimonies.Where(x => x.Quantity > 0)
                .ToList();

            await _patrimonyRepository.RemoveAll();
            await _patrimonyRepository.AddRange(validPatrimonies);
        }

        private IList<Patrimony> BuildPatrimonies(IQueryable<Negociation> negociations)
        {
            var consolidation = new Dictionary<long, Patrimony>();

            foreach (var negociation in negociations)
            {
                BuildPatrimonyInDictonary(consolidation, negociation);

                UpdateConsolidationValues(consolidation, negociation);
            }

            return consolidation.Values.ToList();
        }

        private void BuildPatrimonyInDictonary(Dictionary<long, Patrimony> consolidation, Negociation negociation)
        {
            if (!consolidation.ContainsKey(negociation.TickerId))
            {
                consolidation.Add(negociation.TickerId, new Patrimony(negociation.TickerId));
            }
        }

        private void UpdateConsolidationValues(Dictionary<long, Patrimony> consolidation, Negociation negociation)
        {
            if (negociation.Operation == OperationType.Buy || negociation.Operation == OperationType.Bonus)
            {
                consolidation[negociation.TickerId].Increase(negociation.UnitPrice, negociation.Quantity);
            }
            else
            {
                consolidation[negociation.TickerId].Decrease(negociation.UnitPrice, negociation.Quantity);
            }
        }

        public async Task UpdatePrices()
        {
            var patrimonies = await _patrimonyRepository.GetAll();

            foreach (var patrimony in patrimonies)
            {
                var scraperStrategyResponse = await _scraperStrategyResolver.FindStrategy(patrimony.Ticker.Company.Category.GetHashCode())
                    .Execute(patrimony.Ticker.Code);

                if (!scraperStrategyResponse.IsValid())
                {
                    continue;
                }

                patrimony.Ticker.Update(scraperStrategyResponse.GetPrice());
            }

            await _patrimonyRepository.Update(patrimonies);
        }

        public async Task<IList<BalancingDto>> Balancing()
        {
            var goal = 36000;

            var patrimonies = await _patrimonyRepository.GetAll();

            var recommendations = await BuildRecommendationsActives();

            var balances = new List<BalancingDto>();

            AddSellInBalance(balances, patrimonies, recommendations);

            AddBuyInBalance(balances, patrimonies, recommendations, goal);

            return balances
                .OrderByDescending(x => x.Discount)
                .ToList();
        }

        private async Task<IList<Recommendation>> BuildRecommendationsActives()
        {
            var recommendedWallets = await _recommendedWalletRepository.GetAll();

            recommendedWallets = recommendedWallets.Where(x => x.Active).ToList();

            var recommendations = new List<Recommendation>();

            foreach (var wallet in recommendedWallets)
            {
                recommendations.AddRange(wallet.Recommendations.Where(x => x.Active));
            }

            return recommendations;
        }

        private void AddSellInBalance(List<BalancingDto> balances, IList<Patrimony> patrimonies, IList<Recommendation> recommendations)
        {
            var tickersId = recommendations.Select(x => x.Ticker.Id);

            var patrimoniesSell = patrimonies.Where(x => !tickersId.Contains(x.TickerId))
                .Select(x => BalancingWithSell(x));

            balances.AddRange(patrimoniesSell);
        }

        private void AddBuyInBalance(List<BalancingDto> balances, IList<Patrimony> patrimonies, IList<Recommendation> recommendations, int goal)
        {
            var recommedationWithGoodPrice = recommendations
                .Where(x => x.Discount() >= 0)
                .ToList();

            foreach (var recommendation in recommedationWithGoodPrice)
            {
                var patrimony = patrimonies.FirstOrDefault(x => x.TickerId == recommendation.TickerId);

                if (!PriceIsValid(recommendation.Ticker))
                {
                    continue;
                }

                var currentPatrimony = patrimony is not null
                    ? patrimony.CurrentPatrimony()
                    : 0;

                var quantityForBalancing = QuantityForBalancing(recommendation, 
                    recommendations, 
                    recommendation.Ticker.Price.Value, 
                    currentPatrimony, 
                    goal);

                if (quantityForBalancing < 1)
                {
                    continue;
                }

                balances.Add(BalancingWithBuy(recommendation.Ticker, recommendation, quantityForBalancing));
            }
        }

        private BalancingDto BalancingWithSell(Patrimony patrimony) => 
            new BalancingDto
            {
                Category = patrimony.Ticker.Company.Category.ToString(),
                Ticker = patrimony.Ticker.Code,
                Quantity = patrimony.Quantity,
                Price = patrimony.Price,
                Amount = patrimony.CurrentPatrimony(),
                Operation = OperationType.Sell.ToString(),
            };

        private BalancingDto BalancingWithBuy(Ticker ticker, Recommendation recommendation, int quantityForBalancing) =>
            new BalancingDto
            {
                Category = ticker.Company.Category.ToString(),
                Ticker = recommendation.Ticker.Code,
                Discount = recommendation.Discount(),
                Quantity = quantityForBalancing,
                Price = ticker.Price,
                Amount = ticker.Price * quantityForBalancing,
                Operation = OperationType.Buy.ToString()
            };

        private int QuantityForBalancing(Recommendation recommendation, IList<Recommendation> recommendations, decimal currentPrice, decimal currentPatrimony, int goal)
        {
            var amountForRecommendation = Math.Round(recommendation.Weight * goal / recommendations.Sum(x => x.Weight), 2);

            var quantity = (amountForRecommendation - currentPatrimony) / currentPrice;

            quantity = Math.Ceiling(quantity);

            return Decimal.ToInt32(quantity);
        }

        private bool PriceIsValid(Ticker ticker)
        {
            if (ticker.Price is null)
            {
                _notifier.NotifyError(String.Format(Resources.PriceNotFound, ticker.Code));
                return false;
            }

            return true;
        }
    }
}
