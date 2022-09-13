using MyWallet.Domain.Enums;

namespace MyWallet.Api.Responses
{
    public class BalancingResponse
    {
        public string Category { get; set; }
        public string Ticker { get; set; }
        public decimal? Discount { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public string Operation { get; set; }
    }
}
