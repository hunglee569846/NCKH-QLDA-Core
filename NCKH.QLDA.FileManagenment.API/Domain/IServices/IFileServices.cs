using Microsoft.AspNetCore.Http;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using NCKH.QLDA.FileManagenment.API.Domain.ModelMetas;
using NCKH.QLDA.FileManagenment.API.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.IServices
{
    public interface IFileService
    {
        //Task<ActionResultReponese<List<FileViewModel>>> UploadFiles(string tenantId, string creatorId, string creatorFullName, string creatorAvatar,
        //   int? folderId, IFormFileCollection formFileCollection);
        Task<SearchResult<FileViewModel>> SearchAsync(string IdFile, string FileName, int FolderId);
        //Task<ActionResultReponese<string>> UpdateAsync(string tenantId, string lastUpdateUserId, string lastUpdateFullName, string lastUpdateAvatar, string id, FileMeta fileMeta);
        //Task<ActionResultReponese> DeleteAsync(string tenantId, string deleteUserId, string deleteFullName, string deleteAvatar, string id);
        //Task<ActionResultReponese<FileDetailViewModel>> GetDetailAsync(string tenantId, string userId, string id);
        Task<List<FileViewModel>> GetsAll(string FileName, int FolderId);
        //Task<ActionResultReponese<string>> DownloadAsync(string tenantId, string userId, string id);
    }
}