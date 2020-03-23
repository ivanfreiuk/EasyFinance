using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.Constans;
using EasyFinance.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Extensions.Options;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using EasyFinance.Models;

namespace EasyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly CustomVisionSecrets _cvSecrets;
        private readonly IReceiptPhotoService _receiptPhotoSvc;
        private readonly IReceiptHelper _receiptHelper;
        private readonly IOCRProcessor _ocrProcessor;
        private readonly IReceiptObjectBuilder _receiptBuilder;

        public ReceiptsController(IReceiptPhotoService receiptPhotoSvc, IReceiptHelper receiptHelper, IOCRProcessor ocrProcessor, IReceiptObjectBuilder receiptBuilder, IOptions<CustomVisionSecrets> options)
        {
            _receiptPhotoSvc = receiptPhotoSvc;
            _receiptHelper = receiptHelper;
            _ocrProcessor = ocrProcessor;
            _receiptBuilder = receiptBuilder;
            _cvSecrets = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> StartReceiptProcessing([FromBody]int id)
        {
            // BUILD TEMPLETE
            var receiptPhoto = _receiptPhotoSvc.GetReceiptPhoto(id);
            var imagePrediction = await GetPredictionAsync(receiptPhoto.FileBytes);           
            var receiptSections = _receiptHelper.ExtractSections(imagePrediction.Predictions);
            var receiptTemplate = _receiptHelper.CreateReceiptTemplate(receiptSections);

            // EXTRACT TEXT
            var image = ByteArrayToImage(receiptPhoto.FileBytes);
            var section =  _receiptHelper.CropReceiptSection(image, receiptTemplate.FooterSection.BoundingBox);
            string text = _ocrProcessor.GetText(section);

            var receipt = _receiptBuilder.BuildPurchaseDate(text)
                .BuildTotalAmount(text)
                .GetReceipt();
            //section.Save($"C:\\Users\\Ivan_Freiuk\\Desktop\\DIPLOMA\\CroppedReceipts\\{Guid.NewGuid()}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return Ok(receipt);
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
        
        public Image ByteArrayToImage(byte[] file)
        {
            var stream = new MemoryStream(file);

            return Image.FromStream(stream);
        }
    }
}
