using MyWallet.Domain.Dto;
using MyWallet.Domain.Enums;
using MyWallet.Domain.ValueObjects;

namespace MyWallet.Domain.Entities
{
    public class Negociation : Entity
    {
        public DateTime DateOperation { get; private set; }
        public long TickerId { get; private set; }
        public virtual Ticker Ticker { get; private set; }
        public OperationType Operation { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        protected Negociation() { }
        public Negociation(NegociationDto negociationDto)
        {
            DateOperation = negociationDto.DateOperation;
            TickerId = negociationDto.TickerId;
            Operation = negociationDto.Operation;
            Quantity = negociationDto.Quantity;
            UnitPrice = negociationDto.UnitPrice;
        }
    }
}
