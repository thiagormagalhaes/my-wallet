using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using MyWallet.Domain.ValueObjects;

namespace MyWallet.Domain.Extensions
{
    public static class FormFileExtensions
    {
        public static IList<T> ConvertCSV<T>(this IFormFile file) where T : class
        {
            var result = new List<T>();

            using (var stream = file.OpenReadStream())
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);

                while (reader.Read())
                {
                    result.Add((T)Activator.CreateInstance(typeof(T), new object[] { reader } ));
                }
            }

            return result;
        }
    }
}
