namespace MyWallet.Domain.Dto
{
    public class TickerDto
    {
        public string Code { get; set; }
        public int CompanyId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
