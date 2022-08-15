using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;

namespace MyWallet.Infra.Repositories
{
    public class NegociationRepository : Repository<Negociation>, INegociationRepository
    {
        public NegociationRepository(MyWalletContext context) : base(context) { }
    }
}
