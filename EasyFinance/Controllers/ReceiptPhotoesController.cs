using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptPhotoesController : ControllerBase
    {
        //// GET: api/ReceiptPhotoes
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/ReceiptPhotoes/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpPost]
        //public async Task<IActionResult> UploadReceipt(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest();
        //    }

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);

        //        using (var image = Image.FromStream(stream))
        //        {
        //            // TODO : Save image to DATABASE
        //        }
        //    }

        //    return Ok();
        //}

        //// PUT: api/ReceiptPhotoes/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
