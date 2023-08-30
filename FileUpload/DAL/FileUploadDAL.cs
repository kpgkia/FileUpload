using FileUploadAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace FileUploadAPI.DAL
{
    public class FileUploadDAL
    {
        public bool InsertTransaction(List<TransactionData> transactionDatas)
        {
            bool success = false;
            if (transactionDatas != null && transactionDatas.Count >0)
            {
                using (var context = new FileUploadContext())
                {
                    context.AddRange(transactionDatas);
                    context.SaveChanges();
                    success = true;
                }
            }
            return success;
        }

        public List<TransactionData> GetTransaction(string? CurrencyCode, DateTime? DateTimeFilter, string? Status)
        {
            var transactionDatas = new List<TransactionData>();
            using (var context = new FileUploadContext())
            {
                transactionDatas = context.TransactionData
                                .Where(s => string.IsNullOrEmpty(s.CurrencyCode) || s.CurrencyCode == CurrencyCode)
                                .Where(s => !DateTimeFilter.HasValue || s.TransactionDatetime >= DateTimeFilter)
                                .Where(s => !DateTimeFilter.HasValue || s.TransactionDatetime <= DateTimeFilter)
                                .Where(s => string.IsNullOrEmpty(s.Status) || s.Status == Status)
                                .ToList();

            }
            return transactionDatas;
        }

    }
}
