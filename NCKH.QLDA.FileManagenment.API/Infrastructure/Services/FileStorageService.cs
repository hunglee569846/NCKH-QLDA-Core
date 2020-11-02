using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NCKH.QLDA.FileManagenment.API.Domain.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.Services
{
    public class FileStorageService :  IFileStorageServices
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "uploads";

        public FileStorageService(IWebHostEnvironment hostingEnvironment)
        {
            _userContentFolder = Path.Combine(hostingEnvironment.ContentRootPath, USER_CONTENT_FOLDER_NAME);
        }
        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task<int> CopyFileToServer(Stream mediaBinaryStream, string fileName)
        {
            if (!Directory.Exists(_userContentFolder))
                Directory.CreateDirectory(_userContentFolder);

            if (File.Exists(_userContentFolder))
                return -1;

            var filePath = Path.Combine(_userContentFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(stream);
            }
            return 1;
        }


        public async Task<int> CopyFileToServer(IFormFile file)
        {
            if (!Directory.Exists(_userContentFolder))
                Directory.CreateDirectory(_userContentFolder);

            if (File.Exists(_userContentFolder))
                return -1;

            using (var stream = new FileStream(_userContentFolder, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return 1;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await CopyFileToServer(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> DeleteFileAsync(string fileName)
        {

            var filePath = Path.Combine(_userContentFolder, fileName);
            if (!File.Exists(filePath))
                return -1;

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
            return 1;
        }
    }
}
