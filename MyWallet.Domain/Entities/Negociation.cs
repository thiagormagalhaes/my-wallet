using MyWallet.Domain.Dto;
using MyWallet.Domain.Enums;
using MyWallet.Domain.ValueObjects;

namespace MyWallet.Domain.Entities
{
    public class Negociation : Entity
    {
        public DateTime DateOperation { get; private set; }
        public long CompanyId { get; private set; }
        public virtual Company Company { get; private set; }
        public OperationType Operation { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        protected Negociation() { }
        public Negociation(NegociationDto negociationDto)
        {
            DateOperation = negociationDto.DateOperation;
            CompanyId = negociationDto.CompanyId;
            Operation = negociationDto.Operation;
            Quantity = negociationDto.Quantity;
            UnitPrice = negociationDto.UnitPrice;
        }
    }
}
