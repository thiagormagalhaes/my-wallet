using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Entities
{
    public class Earning : Entity
    {
        public int TickerId { get; private set; }
        public virtual Ticker Ticker { get; private set; }
        public int DividendType { get; private set; }
        public DateTime DateCom { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public decimal TotalValue { get; private set; }

        protected Earning() { }

        public Earning(EarningDto earningDto)
        {
            TickerId = earningDto.TickerId;
            DividendType = earningDto.DividendType;
            DateCom = earningDto.DateCom;
            PaymentDate = earningDto.PaymentDate;
            Quantity = earningDto.Quantity;
            UnitValue = earningDto.UnitValue;
            TotalValue = earningDto.TotalValue;
        }
    }
}
