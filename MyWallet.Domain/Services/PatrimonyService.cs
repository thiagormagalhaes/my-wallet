using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;

namespace MyWallet.Domain.Services
{
    public class PatrimonyService : IPatrimonyService
    {
        private readonly IPatrimonyRepository _patrimonyRepository;
        private readonly INegociationRepository _negociationRepository;

        public PatrimonyService(IPatrimonyRepository patrimonyRepository, INegociationRepository negociationRepository)
        {
            _patrimonyRepository = patrimonyRepository;
            _negociationRepository = negociationRepository;
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
    }
}
