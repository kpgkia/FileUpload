namespace FileUploadAPI.Models
{
    public class TransactionData
    {
        public long Id { get; set; }

        public string? TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string? CurrencyCode { get; set; }

        public DateTime TransactionDatetime { get; set; }
        public enum Status
        {
            Approved, Rejected, Done
        }
    }
}