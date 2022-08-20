using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using System.Transactions;

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
    }
}
