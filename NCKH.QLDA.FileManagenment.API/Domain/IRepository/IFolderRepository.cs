using NCKH.QLDA.FileManagenment.API.Domain.Models;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.IRepository
{
    public interface IFolderRepository
    {
        Task<int> InsertAsync(string IdPath,string FolderName,string FolderId,Folder folder);
        Task<bool> CheckExitsFolder(string FolderId);
    }
}
