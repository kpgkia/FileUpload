using FileUploadAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FileUploadAPI.BL;
using System.Buffers.Text;
using System.Xml;
using FileUploadAPI.DTO;
using Microsoft.Extensions.Configuration;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using FileUploadAPI.CSVHelperMap;
using System.Xml.Serialization;

namespace FileUploadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadAPIController : ControllerBase
    {

        public FileUploadAPIController(IConfiguration configuration)
        {
        }

        [HttpPost]
        public IActionResult UploadFile()
        {
            IFormFile file = Request.Form.Files.FirstOrDefault();

            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);

                    if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = false,
                            Delimiter = ",",
                            Quote = '"'
                        }))
                        {
                            csv.Context.RegisterClassMap<TransactionDataDTOCsvMap>();
                            var records = csv.GetRecords<TransactionDataDTO>().ToList();

                            var TransactionDataDTOs = new List<TransactionDataDTO>();
                            foreach (var record in records)
                            {
                                var transactionDataDTO = new TransactionDataDTO();
                                transactionDataDTO.TransactionId = record.TransactionId;
                                transactionDataDTO.Amount = record.Amount.Remove(',');
                                transactionDataDTO.TransactionDatetime = record.TransactionDatetime;
                                transactionDataDTO.Status = record.Status;

                                TransactionDataDTOs.Add(transactionDataDTO);
                            }

                            (bool success, string msg) = new FileUploadBL().InsertTransactionDatas(TransactionDataDTOs, "csv");
                            if(!success)
                                return BadRequest(new { Message = msg });
                        }
                    }
                    else if (fileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        // XML file
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            var serializer = new XmlSerializer(typeof(List<TransactionDataDTO>));
                            var transactionDataList = (List<TransactionDataDTO>)serializer.Deserialize(reader);

                            // Process the deserialized transactionDataList 

                            (bool success, string msg) = new FileUploadBL().InsertTransactionDatas(transactionDataList, "xml");
                            if (!success)
                                return BadRequest(new { Message = msg });
                        }


                        return Ok(new { Message = "XML file uploaded and processed successfully." });
                    }
                    else
                    {
                        // Unsupported file type
                        return BadRequest(new { Message = "Unknown Format" });
                    }

                    return Ok(new { Message = "File uploaded successfully." });
                }

                return BadRequest(new { Message = "No file data was provided." });
            }
            catch (Exception ex) {  return BadRequest(ex); }
        }


        [HttpGet]
        public IActionResult Get(string? CurrencyCode, DateTime? DateTimeFilter, string? Status)
        {
            var fileUploadBL = new FileUploadBL();
            try
            {
                var result = fileUploadBL.GetTransactionDatas(CurrencyCode, DateTimeFilter, Status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}