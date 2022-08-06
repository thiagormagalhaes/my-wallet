namespace MyWallet.Domain.Dto
{
    public class EarningDto
    {
        public int TickerId { get; set; }
        public int DividendType { get; set; }
        public DateTime DateCom { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}
