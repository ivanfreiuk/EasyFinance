using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EasyFinance.OCR.Helpers
{
    public class LocalBuffer
    {
        private readonly string _path;
        private readonly List<string> _savedFiles;

        public string LastSavedFile { get; private set; }

        public LocalBuffer(string path = null)
        {
            _path = path ?? CreateDefaultDirectory();
            _savedFiles = new List<string>();
        }

        public string SaveImage(Image image)
        {
            var fileName = $"{Guid.NewGuid()}.jpeg";
            var fullPath = $"{_path}\\{fileName}";


            image.Save(fullPath, ImageFormat.Jpeg);
            LastSavedFile = fullPath;
            _savedFiles.Add(fullPath);

            return LastSavedFile;
        }

        public void Clear()
        {
            foreach (var fileName in _savedFiles)
            {
                File.Delete(fileName);
            }
        }

        private string CreateDefaultDirectory()
        {
            var path = ".\\Buffer";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
