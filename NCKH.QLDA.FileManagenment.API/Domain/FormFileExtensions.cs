using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain
{
    public static class FormFileExtensions
    {
        public static string GetFilename(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(
                file.ContentDisposition).FileName.ToString().Trim('"');
        }

        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }

        public static long GetFileSize(this IFormFile file)
        {
            var sizeFile = file.Length;
            return sizeFile;
        }

        public static string GetExtensionFile(this IFormFile file)
        {
            var exten = Path.GetExtension(file.FileName).Split(".");
            if (exten.Length > 1)
                return exten[1];
            else
                return string.Empty;
        }

        public static string GetNameNotExtensionFile(this IFormFile file)
        {
            var exten = Path.GetFileNameWithoutExtension(file.FileName);
            if (exten.Length > 1)
                return exten;
            else
                return string.Empty;
        }

        public static string GetTypeFile(this IFormFile file)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return types[ext];
        }



        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".swf", "graphics/vector"}
            };
        }
    }
}
