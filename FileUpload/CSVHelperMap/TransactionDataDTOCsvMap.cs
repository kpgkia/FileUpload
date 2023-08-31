using CsvHelper.Configuration;
using FileUploadAPI.DTO;
using System.Globalization;

namespace FileUploadAPI.CSVHelperMap
{
    public sealed class TransactionDataDTOCsvMap : ClassMap<TransactionDataDTO>
    {
        public TransactionDataDTOCsvMap()
        {
            string format = "dd/MM/yyyy hh:mm:ss";
            var msMY = CultureInfo.GetCultureInfo("ms-MY");

            Map(m => m.TransactionId);
            Map(m => m.Amount);
            Map(m => m.CurrencyCode);
            Map(m => m.TransactionDatetime).TypeConverterOption.Format(format)
              .TypeConverterOption.CultureInfo(msMY).Index(3);
            Map(m => m.Status);
        }
    }
}
