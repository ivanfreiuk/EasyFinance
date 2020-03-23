using System.Collections.Generic;
using System.Drawing;
using EasyFinance.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace EasyFinance.Interfaces
{
    public interface IReceiptHelper
    {
        IEnumerable<Section> ExtractSections(IEnumerable<PredictionModel> predictions);

        ReceiptTemplate CreateReceiptTemplate(IEnumerable<Section> sections);

        Image CropReceiptSection(Image source, Models.BoundingBox section);
    }
}
