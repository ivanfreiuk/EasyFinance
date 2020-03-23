using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using EasyFinance.Constans;
using EasyFinance.Interfaces;
using EasyFinance.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace EasyFinance.Helpers
{
    public class ReceiptHelper: IReceiptHelper
    {
        public IEnumerable<Section> ExtractSections(IEnumerable<PredictionModel> predictions)
        {
            return predictions.OrderByDescending(p => p.TagName)
            .ThenByDescending(p => p.Probability)
            .GroupBy(p => p.TagName)
            .Select(i =>
            {
                var section = i.First();
                return new Section
                {
                    SectionName = section.TagName,
                    Probability = section.Probability,
                    BoundingBox = new Models.BoundingBox
                    {
                        Left = section.BoundingBox.Left,
                        Top = section.BoundingBox.Top,
                        Width = section.BoundingBox.Width,
                        Height = section.BoundingBox.Height
                    }
                };
            }).ToList();
        }

        public ReceiptTemplate CreateReceiptTemplate(IEnumerable<Section> sections)
        {
            return new ReceiptTemplate
            {
                HeaderSection = sections.FirstOrDefault(s => s.SectionName == SectionName.Header),
                ProductListSection = sections.FirstOrDefault(s => s.SectionName == SectionName.ProductList),
                FooterSection = sections.FirstOrDefault(s => s.SectionName == SectionName.Footer)
            };
        }

        public Image CropReceiptSection(Image source, Models.BoundingBox section)
        {
            var bitmap = new Bitmap(source);
            var rectangle = ConvertToRectangle(section, source.Width, source.Height);
            var croppedImage = bitmap.Clone(rectangle, bitmap.PixelFormat);

            return croppedImage;
        }

        private Rectangle ConvertToRectangle(Models.BoundingBox source, int imageWidth, int imageHeight)
        {
            return new Rectangle
            {
                X = (int)(source.Left * imageWidth),
                Y = (int)(source.Top * imageHeight),
                Width = (int)(source.Width * imageWidth),
                Height = (int)(source.Height * imageHeight)
            };
        }
    }
}
