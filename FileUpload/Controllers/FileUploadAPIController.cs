using FileUploadAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FileUploadAPI.BL;
using System.Buffers.Text;
using System.Xml;
using FileUploadAPI.DTO;
using Microsoft.Extensions.Configuration;

namespace FileUploadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadAPIController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public FileUploadAPIController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpPost]
        public IActionResult UploadFile([FromBody] FileUploadDTO model)
        {
            if (!string.IsNullOrEmpty(model.FileData))
            {
                if (model.FileName.EndsWith(".csv"))
                {
                    // Parse CSV data
                    string[] csvLines = model.FileData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    List<string[]> parsedCsv = new List<string[]>();
                    foreach (var line in csvLines)
                    {
                        string[] values = line.Split(',');
                        parsedCsv.Add(values);
                    }

                    // Process parsedCsv as needed
                }
                else if (model.FileName.EndsWith(".xml"))
                {
                    // Parse XML data
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(model.FileData);

                    // Process xmlDoc as needed
                }

                return Ok(new { Message = "File uploaded and parsed successfully." });
            }

            return BadRequest(new { Message = "No file data was provided." });
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