using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;
using EasyFinance.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptPhotosController : ControllerBase
    {
        private readonly IReceiptPhotoService _receiptPhotoService;
        private readonly IFileHelper _fileHelper;

        public ReceiptPhotosController(IReceiptPhotoService receiptPhotoService, IFileHelper fileHelper)
        {
            _receiptPhotoService = receiptPhotoService;
            _fileHelper = fileHelper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiptPhoto(int id)
        {
            var photo = await _receiptPhotoService.GetReceiptPhotoAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        [HttpGet]
        public async Task<IActionResult> GetReceiptPhotos()
        {
            var photos = await _receiptPhotoService.GetReceiptPhotosAsync();
            
            return Ok(photos);
        }


        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var image = Image.FromStream(stream))
                {
                    var receiptPhoto = new ReceiptPhoto
                    {
                        FileName = file.FileName,
                        FileBytes = _fileHelper.ImageToByteArray(image)
                    };

                    await _receiptPhotoService.AddReceiptPhotoAsync(receiptPhoto);

                    return Ok(new {receiptPhoto.Id});
                }
            }
        }

        public byte[] ImageToByteArray(Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            return stream.ToArray();
        }

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
