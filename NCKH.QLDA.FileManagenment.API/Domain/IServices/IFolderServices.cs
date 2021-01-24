using NCKH.Infrastruture.Binding.Models;
using NCKH.QLDA.FileManagenment.API.Domain.ModelMetas;
using NCKH.QLDA.FileManagenment.API.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.IServices
{
    public interface IFolderServices
	{
		//Task<FolderSearchViewModel> SearchAsync(string tenantId, string userId, string keyword, int page, int pageSize);
		Task<ActionResultReponese<string>> InsertAsync(string FolderName, int FolderId,FolderMeta folderMeta);
		//Task<ActionResultReponese<string>> UpdateAsync(string tenantId, string lastUpdateUserId, string lastUpdateFullName, string lastUpdateAvatar, int id, FolderMeta folderMeta);
		//Task<ActionResultReponese> DeleteAsync(string tenantId, string deleteUserId, string deleteFullName, string deleteAvatar, int id);
		//Task<ActionResultReponese<FolderDetailViewModel>> GetDetailAsync(string tenantId, string userId, int id);
		//Task<List<FolderViewModel>> GetsAll(string tenantId, string userId, int? parentId);
		//Task<ActionResultReponese> UpdateName(string tenantId, string lastUpdateUserId, string lastUpdateFullName,
		//   int folderId, string concurrencyStamp, string name);
		//Task<List<Breadcrumb>> GetBreadcrumbs(string tenantId, string userId, int? folderId);
		//Task<List<TreeData>> GetFullTreeAsync(string tenantId, string userId);
	}
}