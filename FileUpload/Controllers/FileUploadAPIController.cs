using FileUploadAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadAPIController : ControllerBase
    {  

        public FileUploadAPIController(ILogger<FileUploadAPIController> logger)
        { 
        }

        [HttpGet]
        public IEnumerable<TransactionData> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new TransactionData
            { 
                TransactionId = "asd"
            })
            .ToArray();
        }
    }
}