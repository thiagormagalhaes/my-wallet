namespace MyWallet.Api.Responses
{
    public class CompanyResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Category { get; set; }
        public AdministratorResponse? Administrator { get; set; }
        public IList<TickerResponse> Tickers { get; set; }
    }
}
