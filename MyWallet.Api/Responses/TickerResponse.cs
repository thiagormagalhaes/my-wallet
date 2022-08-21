namespace MyWallet.Api.Responses
{
    public class TickerResponse
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public DateTime? UpdateDate { get; private set; }
    }
}
