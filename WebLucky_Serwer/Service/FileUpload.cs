﻿using Tangy_Common;
using Microsoft.AspNetCore.Components.Forms;

namespace WebLucky_Serwer.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteFile(string filePath)
        {
            if (File.Exists(_webHostEnvironment.WebRootPath + filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath + filePath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;

            var folderDirectory = $"{_webHostEnvironment.WebRootPath}{SD.PRODUCTIMG}";

            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            var filePath = Path.Combine(folderDirectory, fileName);
            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);

            return fileName;
        }
    }

}
