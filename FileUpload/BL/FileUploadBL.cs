using FileUploadAPI.DTO;
using FileUploadAPI.Models;
using FileUploadAPI.DAL;

namespace FileUploadAPI.BL
{
    public class FileUploadBL
    {
        public (bool, string) InsertTransactionDatas(List<TransactionDatasDTO> transactionDatasDTOs, string FileFormat)
        {
            var transactionDatas = new List<TransactionData>();
            bool success = false;
            string message = string.Empty;
            var fileUploadDAL = new FileUploadDAL();
            try
            {
                foreach (var dto in transactionDatasDTOs)
                {
                    var TransactionData = new TransactionData();
                    TransactionData.TransactionId = dto.TransactionId;
                    TransactionData.CurrencyCode = dto.CurrencyCode;
                    TransactionData.TransactionDatetime = dto.TransactionDatetime;

                    switch (FileFormat.ToLower())
                    {
                        case "csv":
                            switch (dto.Status.ToLower())
                            {
                                case "approved":
                                    TransactionData.Status = "A";
                                    break;
                                case "failed":
                                    TransactionData.Status = "R";
                                    break;
                                case "finished":
                                    TransactionData.Status = "D";
                                    break;
                            }
                            break;
                        case "xml":
                            switch (dto.Status.ToLower())
                            {
                                case "approved":
                                    TransactionData.Status = "A";
                                    break;
                                case "rejected":
                                    TransactionData.Status = "R";
                                    break;
                                case "done":
                                    TransactionData.Status = "D";
                                    break;
                            }
                            break;
                    }
                    transactionDatas.Add(TransactionData);
                }

                if (transactionDatas.Count > 0)
                {
                    success = fileUploadDAL.InsertTransaction(transactionDatas);
                }
            }
            catch (Exception ex) { message = ex.Message; }

            return (success, message);
        }

        public List<TransactionDatasDTO> GetTransactionDatas(string? CurrencyCode, DateTime? DateTimeFilter, string? Status)
        {
            var QueryResults = new List<TransactionDatasDTO>();
            var fileUploadDAL = new FileUploadDAL();
            var trxDatas = fileUploadDAL.GetTransaction(CurrencyCode, DateTimeFilter, Status);
 
            foreach(var trxData in trxDatas)
            {
                var dto = new TransactionDatasDTO();
                dto.TransactionId = trxData.TransactionId;
                dto.CurrencyCode = trxData.CurrencyCode;
                dto.TransactionDatetime = trxData.TransactionDatetime;
                dto.Status = trxData.Status;

                QueryResults.Add(dto);
            } 

            return QueryResults;
        }
    }
}
