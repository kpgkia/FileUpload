namespace FileUploadAPI.DTO
{
    public class TransactionDatasDTO
    {
        public string TransactionId { get; set; } = null!;

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; } = null!;

        public DateTime TransactionDatetime { get; set; }

        public string Status { get; set; } = null!;
    }
}
