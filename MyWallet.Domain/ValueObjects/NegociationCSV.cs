using ExcelDataReader;

namespace MyWallet.Domain.ValueObjects;

public record NegociationCSV
{
    public DateTime DateOperation { get; }
    public string Category { get; }
    public string Ticker { get; }
    public string Operation { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public NegociationCSV(IExcelDataReader reader)
    {
        DateOperation = Convert.ToDateTime(reader.GetString(0));
        Category = reader.GetString(1);
        Ticker = reader.GetString(2);
        Operation = reader.GetString(3);
        Quantity = Convert.ToInt32(Convert.ToDecimal(reader.GetString(4)));
        UnitPrice = Convert.ToDecimal(reader.GetString(5));
    }
}