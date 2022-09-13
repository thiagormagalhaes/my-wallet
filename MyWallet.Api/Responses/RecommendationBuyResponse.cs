using MyWallet.Domain.Entities;

namespace MyWallet.Api.Responses
{
    public class RecommendationBuyResponse
    {
        public RecommendationBuyResponse(Recommendation recommendation)
        {
            Category = recommendation.Ticker.Company.Category.ToString();
            TickerCode = recommendation.Ticker.Code;
            Discount = recommendation.Discount();
            Price = recommendation.Ticker.Price;
        }

        public string Category { get; set; }
        public string TickerCode { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Price { get; set; }
    }
}
