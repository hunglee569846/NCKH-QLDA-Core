using NCKH.QLDA.FileManagenment.API.Domain.Models;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.IRepository
{
    public interface IFolderRepository
    {
        Task<int> InsertAsync(string FolderName,int FolderId,Folder folder);
        Task<bool> CheckExitsFolder(int FolderId);
        Task<Folder> GetInfoAsync(int FolderId);
       // Task<bool> CheckExistsByFolderIdName(string FolderId, )
    }
}
