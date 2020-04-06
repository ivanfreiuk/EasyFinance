using System;
using System.IO;
using System.Threading.Tasks;
using EasyFinance.Builders.Interfaces;
using EasyFinance.BusinessLogic.Builders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.Constans;
using EasyFinance.DataAccess.Entities;
using EasyFinance.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Extensions.Options;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;


namespace EasyFinance.Controllers
{
    [Route("api/receipts")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly CustomVisionSecrets _cvSecrets;
        private readonly IReceiptPhotoService _receiptPhotoSvc;
        private readonly IReceiptHelper _receiptHelper;
        private readonly IReceiptObjectDirector _receiptDirector;
        private readonly IReceiptScanTextDirector _scanTextDirector;
        private readonly IReceiptService _receiptService;
        private readonly IFileHelper _fileHelper;

        public ReceiptsController(IReceiptPhotoService receiptPhotoSvc,
            IReceiptHelper receiptHelper,
            IReceiptObjectDirector receiptDirector,
            IReceiptScanTextDirector scanTextDirector,
            IReceiptService receiptService,
            IFileHelper fileHelper,
            IOptions<CustomVisionSecrets> options)
        {
            _receiptPhotoSvc = receiptPhotoSvc;
            _receiptHelper = receiptHelper;
            _receiptDirector = receiptDirector;
            _scanTextDirector = scanTextDirector;
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
            try
            {
                await _receiptService.AddReceiptAsync(receipt);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(receipt.Id);
        }

        [HttpPost]
        public async Task<IActionResult> StartReceiptProcessing([FromBody]int id)
        {
            var receiptPhoto = await _receiptPhotoSvc.GetReceiptPhotoAsync(id);

            if (receiptPhoto == null)
            {
                return BadRequest();
            }

            var imagePrediction = await GetPredictionAsync(receiptPhoto.FileBytes);           
            var receiptSections = _receiptHelper.ExtractSections(imagePrediction.Predictions);
            var receiptTemplate = _receiptHelper.CreateReceiptTemplate(receiptSections);


            var image = _fileHelper.ByteArrayToImage(receiptPhoto.FileBytes);
            var scanText = _scanTextDirector.ConstructScanText(image, receiptTemplate);
            var receipt = _receiptDirector.ConstructReceipt(scanText);

            receipt.ReceiptPhotoId = id;
            await _receiptService.AddReceiptAsync(receipt);

            var receiptModel = await _receiptService.GetReceiptAsync(receipt.Id);

            return Ok(receiptModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditReceipt(int id, Receipt receipt)
        {
            var receiptFromDb = await _receiptService.GetReceiptAsync(id);

            if (receiptFromDb == null)
            {
                return BadRequest();
            }

            receiptFromDb.Merchant = receipt.Merchant;
            receiptFromDb.ReceiptPhotoId = receipt.ReceiptPhotoId;
            receiptFromDb.CategoryId = receipt.CategoryId;
            receiptFromDb.CurrencyId = receipt.CurrencyId;
            receiptFromDb.PaymentMethodId = receipt.PaymentMethodId;
            receiptFromDb.TotalAmount = receipt.TotalAmount;
            receiptFromDb.PurchaseDate = receipt.PurchaseDate;
            receiptFromDb.Description = receipt.Description;

            await _receiptService.UpdateReceiptAsync(receiptFromDb);

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
