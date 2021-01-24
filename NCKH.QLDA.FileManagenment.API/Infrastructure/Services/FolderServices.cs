using NCKH.Infrastruture.Binding.Models;
using NCKH.QLDA.FileManagenment.API.Domain.IRepository;
using NCKH.QLDA.FileManagenment.API.Domain.IServices;
using NCKH.QLDA.FileManagenment.API.Domain.ModelMetas;
using NCKH.QLDA.FileManagenment.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.Services
{
    public class FolderService : IFolderServices
    {
        private readonly IFolderRepository _iFolderRepository;
        public FolderService(IFolderRepository iFolderRepository)
        {
            _iFolderRepository = iFolderRepository;
        }

        public async Task<ActionResultReponese<string>> InsertAsync(string FolderName,int folderId, FolderMeta folderMeta)
        {
            var isFolderID = await _iFolderRepository.CheckExitsFolder(folderId);
            if (isFolderID)
                return new ActionResultReponese<string>(-21, "FolderId already exists", "Folder", null);
            var folder = new Folder()
            {
                FolderId = folderId,
                FolderName = FolderName,
                NamePath = folderMeta.NamePath?.Trim(),
                Level = folderMeta.Level,
                ChildCount = folderMeta.ChildCount,
                Description = folderMeta.Description?.Trim(),
                CreateTime = DateTime.Now,
                DeleteTime = null,
                LastUpdate = null,
                IsActive = true,
                IsDelete = false,
            };
            var result = await _iFolderRepository.InsertAsync(FolderName, folderId, folder);
            if (result <= 0)
                return new ActionResultReponese<string>(-1, "Insert False", "Folder", null);
            return new ActionResultReponese<string>(result, "Insert Success", "Folder", null);
        }
    }
}
