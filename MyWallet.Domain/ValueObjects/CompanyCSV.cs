using ExcelDataReader;

namespace MyWallet.Domain.ValueObjects
{
    public class CompanyCSV
    {
        public string Company { get; }
        public string Ticker { get; }
        public string Cnpj { get; }

        public CompanyCSV(IExcelDataReader reader)
        {
            Company = reader.GetString(0);
            Ticker = reader.GetString(1);
            Cnpj = reader.GetString(2);
        }
    }
}
