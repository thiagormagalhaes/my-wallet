using ExcelDataReader;

namespace MyWallet.Domain.ValueObjects
{
    public class CompanyCSV
    {
        public string Company { get; }
        public string Ticker { get; }
        public string Cnpj { get; }
        public string Administrator { get; }
        public string AdministratorCnpj { get; }

        public CompanyCSV(IExcelDataReader reader)
        {
            Company = reader.GetString(0);
            Ticker = reader.GetString(1);
            Cnpj = reader.GetString(2);

            if (reader.FieldCount > 3)
            {
                Administrator = reader.GetString(3);
                AdministratorCnpj = reader.GetString(4);
            }
        }
    }
}
