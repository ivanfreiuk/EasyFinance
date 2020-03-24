using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.Constans;
using EasyFinance.DataAccess.Entities;
using EasyFinance.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Extensions.Options;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using EasyFinance.OCR.Interfaces;

namespace EasyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly CustomVisionSecrets _cvSecrets;
        private readonly IReceiptPhotoService _receiptPhotoSvc;
        private readonly IReceiptHelper _receiptHelper;
        private readonly IOCRService _ocrService;
        private readonly IReceiptObjectBuilder _receiptBuilder;
        private readonly IReceiptService _receiptService;
        private readonly IFileHelper _fileHelper;

        public ReceiptsController(IReceiptPhotoService receiptPhotoSvc,
            IReceiptHelper receiptHelper,
            IOCRService ocrService,
            IReceiptObjectBuilder receiptBuilder,
            IReceiptService receiptService,
            IFileHelper fileHelper,
            IOptions<CustomVisionSecrets> options)
        {
            _receiptPhotoSvc = receiptPhotoSvc;
            _receiptHelper = receiptHelper;
            _ocrService = ocrService;
            _receiptBuilder = receiptBuilder;
            _receiptService = receiptService;
            _fileHelper = fileHelper;
            _cvSecrets = options.Value;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceipt(int id)
        {
           var receipt = await _receiptService.GetReceiptAsync(id);

           if (receipt == null)
           {
               return NotFound();
           }

           return Ok(receipt);
        }

        [HttpGet]
        public async Task<IActionResult> GetReceipts()
        {
            var receipts = await _receiptService.GetReceiptsAsync();

            if (receipts == null)
            {
                return NoContent();
            }

            return Ok(receipts);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReceipt(Receipt receipt)
        {
            await _receiptService.AddReceiptAsync(receipt);

            return Ok(new { receipt.Id });
        }

        [HttpPost]
        public async Task<IActionResult> StartReceiptProcessing([FromBody]int id)
        {
            // BUILD TEMPLETE
            var receiptPhoto = await _receiptPhotoSvc.GetReceiptPhotoAsync(id);
            var imagePrediction = await GetPredictionAsync(receiptPhoto.FileBytes);           
            var receiptSections = _receiptHelper.ExtractSections(imagePrediction.Predictions);
            var receiptTemplate = _receiptHelper.CreateReceiptTemplate(receiptSections);

            // EXTRACT TEXT
            var image = _fileHelper.ByteArrayToImage(receiptPhoto.FileBytes);
            var section =  _receiptHelper.CropReceiptSection(image, receiptTemplate.FooterSection.BoundingBox);
            string text = _ocrService.GetText(section);

            var receipt = _receiptBuilder.BuildPurchaseDate(text)
                .BuildTotalAmount(text)
                .GetReceipt();
            //section.Save($"C:\\Users\\Ivan_Freiuk\\Desktop\\DIPLOMA\\CroppedReceipts\\{Guid.NewGuid()}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return Ok(receipt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditReceipt(int id, Receipt receipt)
        {
            var receiptFromDb = await _receiptService.GetReceiptAsync(id);

            if (receiptFromDb == null)
            {
                return BadRequest();
            }

            await _receiptService.UpdateReceiptAsync(receipt);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            var receiptFromDb = await _receiptService.GetReceiptAsync(id);

            if (receiptFromDb == null)
            {
                return NotFound();
            }

            await _receiptService.RemoveReceiptAsync(receiptFromDb);

            return Ok();
        }



        private async Task<ImagePrediction> GetPredictionAsync(byte[] imageFile)
        {
            var predictionClient = new CustomVisionPredictionClient
            {
                ApiKey = _cvSecrets.PredictionKey,
                Endpoint = _cvSecrets.PredictionUrl
            };
            var stream = new MemoryStream(imageFile);
            return await predictionClient.DetectImageAsync(_cvSecrets.ProjectId, _cvSecrets.PublishedName, stream);
        }
    }
}
