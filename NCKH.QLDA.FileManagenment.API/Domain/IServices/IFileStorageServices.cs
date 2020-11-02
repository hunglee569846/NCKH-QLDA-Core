using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.IServices
{
    public interface IFileStorageServices
    {
        string GetFileUrl(string fileName);
        Task<int> CopyFileToServer(Stream mediaBinaryStream, string fileName);
        Task<int> CopyFileToServer(IFormFile file);
        Task<string> SaveFile(IFormFile file);
        Task<int> DeleteFileAsync(string fileName);
    }
}
