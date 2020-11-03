using NCKH.Infrastruture.Binding.ViewModel;
using NCKH.QLDA.FileManagenment.API.Domain.Models;
using NCKH.QLDA.FileManagenment.API.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.IRepository
{
    public interface IFileRepository
    {
        Task<SearchResult<FileViewModel>> SearchAsync(string IdFile, string FileName, int FolderId);
        Task<List<FileViewModel>> SelectAllAsync(string FileName, int FolderId);
        Task<int> InsertAsync(Files file);
        Task<bool> CheckExistsByFolderIdName(string IdFile, int? folderId, string FileName);

    }
}
