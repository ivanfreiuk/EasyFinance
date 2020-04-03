﻿using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;
using EasyFinance.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.Controllers
{
    [Route("api/receiptphotos")]
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

        [HttpGet("blob/{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await _receiptPhotoService.GetReceiptPhotoAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            var extension = photo.FileName.Substring(photo.FileName.IndexOf(".", StringComparison.Ordinal) + 1);

            return File(photo.FileBytes, $"image/{extension}");
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

                    return Ok(receiptPhoto.Id);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _receiptPhotoService.GetReceiptPhotoAsync(id);

            if (photo == null)
            {
                return BadRequest();
            }

            await _receiptPhotoService.RemoveReceiptPhotoAsync(photo);

            return NoContent();
        }
    }
}
