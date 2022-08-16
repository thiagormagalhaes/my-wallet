using Microsoft.AspNetCore.Http;
using MyWallet.Domain.Dto;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Enums;
using MyWallet.Domain.Extensions;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Domain.Interfaces.Services;
using MyWallet.Domain.Notifications;
using MyWallet.Domain.ValueObjects;
using MyWallet.Globalization;

namespace MyWallet.Domain.Services
{
    public class NegociationService : INegociationService
    {
        private readonly INotifier _notifier;
        private readonly INegociationRepository _negociationRepository;
        private readonly ITickerRepository _tickerRepository;

        public NegociationService(INotifier notifier, INegociationRepository negociationRepository, ITickerRepository tickerRepository)
        {
            _notifier = notifier;
            _negociationRepository = negociationRepository;
            _tickerRepository = tickerRepository;
        }

        // TODO: Ainda não foi testado :D
        public async Task Import(IFormFile file)
        {
            var tickers = await _tickerRepository.GetAll();

            var negociationsCSV = file.ConvertCSV<NegociationCSV>();

            var recordedNegociations = await _negociationRepository.GetAll();

            var newNegociations = BuildNegociations(tickers, negociationsCSV)
                .OrderBy(x => x.DateOperation)
                .Skip(recordedNegociations.Count)
                .ToList();

            if (!_notifier.HaveNotification())
            {
                await _negociationRepository.AddRange(newNegociations);
            }
        }

        private List<Negociation> BuildNegociations(IList<Ticker> tickers, IList<NegociationCSV> negociationsCSV)
        {
            var negociations = new List<Negociation>();

            foreach (var negociationCSV in negociationsCSV)
            {
                var tickerId = GetTickerByCode(tickers, negociationCSV.Ticker);

                if (tickerId is null)
                {
                    // TODO: Exibir apenas uma mensagem por Ticker
                    _notifier.NotifyError(string.Format(Resources.TickerNotFound, negociationCSV.Ticker));
                    continue;
                }

                var negociationDto = BuildNegociationDto(negociationCSV, tickerId.Value);

                negociations.Add(new Negociation(negociationDto));
            }

            return negociations;
        }

        private NegociationDto BuildNegociationDto(NegociationCSV negociationCSV, long tickerId)
        {
            var operationType = GetOperationType(negociationCSV.Operation);

            return new NegociationDto
            (
                negociationCSV.DateOperation,
                tickerId,
                operationType,
                negociationCSV.Quantity,
                negociationCSV.UnitPrice
            );
        }

        private OperationType GetOperationType(string operationType)
        {
            if (operationType == "C")
            {
                return OperationType.Buy;
            }

            return OperationType.Sell;
        }

        private long? GetTickerByCode(IList<Ticker> tickers, string code)
        {
            return tickers.Where(x => x.Code == code).FirstOrDefault()?.Id;
        }
    }
}
