using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NCKH.Infrastruture.Binding.Extensions;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using NCKH.QLDA.FileManagenment.API.Domain;
using NCKH.QLDA.FileManagenment.API.Domain.IRepository;
using NCKH.QLDA.FileManagenment.API.Domain.IServices;
using NCKH.QLDA.FileManagenment.API.Domain.Models;
using NCKH.QLDA.FileManagenment.API.Domain.ViewModels;
using NCKH.QLDA.FileManagenment.API.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFolderRepository _folderRepository;
        public FileService(IFileRepository fileRepository, IFolderRepository folderRepository, IWebHostEnvironment WebHostEnvironment)
        {
            _fileRepository = fileRepository;
            _folderRepository = folderRepository;
            _webHostEnvironment = WebHostEnvironment;
        }
        public async Task<SearchResult<FileViewModel>> SearchAsync(string IdFile, string FileName, int FolderId)
        {
            return await _fileRepository.SearchAsync(IdFile, FileName, FolderId);
        }
        public async Task<List<FileViewModel>> GetsAll(string FileName, int FolderId)
        {
           return await _fileRepository.SelectAllAsync(FileName, FolderId);
        }

        public async Task<ActionResultReponese<List<FileViewModel>>> UploadFiles(string IdFile,string FileName, string creatorId, string FolderName, int? folderId, IFormFileCollection formFileCollection)
        {
            List<Files> listFiles = new List<Files>();
            string uploadUrl = string.Format("/uploads/" + FolderName + "/{0:yyyy/MM/dd}/", DateTime.Now);
            Folder folderInfo = null;
            if (folderId.HasValue)
            {
                folderInfo = await _folderRepository.GetInfoAsync(FolderName, folderId.Value);
                if (folderInfo == null)
                    return new ActionResultReponese<List<FileViewModel>>(-1, "Folder does not exists. You can not update file to this folder.");
                //_ghmFileResource.GetString("Folder does not exists. You can not update file to this folder."));
            }
          

            foreach (IFormFile formFile in formFileCollection)
            {
                var id = Guid.NewGuid().ToString("n");
                string urlOutPut = $"{uploadUrl}{id}.{formFile.GetExtensionFile()}";
                string uploadPath = $"{CreateFolder()}{id}.{formFile.GetExtensionFile()}";
                string uploadPathBackup = $"{CreateFolderBackUp()}{id}.{formFile.GetExtensionFile()}";
                var type = formFile.GetTypeFile();
                var isImage = type.Contains("image/");

                var isNameExit = await _fileRepository.CheckExistsByFolderIdName(IdFile,folderId,formFile.FileName?.Trim());
                if (isNameExit)
                    continue;

                await CopyFileToServer(formFile, uploadPathBackup, isImage);
                var resultCopyFile = await CopyFileToServer(formFile, uploadPath, isImage);
                if (resultCopyFile == -1)
                    continue;

                var file = new Files
                {
                    IdFile = id,
                    FileName = formFile.FileName?.Trim().StripVietnameseChars().ToUpper(),
                    Type = formFile.GetTypeFile(),
                    Size = formFile.GetFileSize(),
                    Url = urlOutPut,
                    CreatorId = creatorId,
                    Folderld = folderInfo.FolderId,
                    CreateDate = DateTime.Now,
                    DeleteTime = null,
                    LastUpdate = null,
                    IsActive = true,
                    IsDelete = false

                };

                // Add file info to list for insert into database.
                await _fileRepository.InsertAsync(file);
                listFiles.Add(file);
            }


            return new ActionResultReponese<List<FileViewModel>>
            {
                Code = 1,
                Message = "Upload file successful",
                Data = listFiles.Select(x => new FileViewModel
                {
                    IdFile = x.Id,
                    FileName = x.FileName,
                    FolderId = x.Folderld,
                    Size = x.Size,
                    Type = x.Type,
                    Url = x.Url,
                    CreateDate = x.CreateDate,
                    CreatorId = x.CreatorId,
                }).ToList()
            };

            string CreateFolder()
            {
                var mapPath = string.Format(_webHostEnvironment.ContentRootPath + uploadUrl);
                if (!Directory.Exists(mapPath))
                    Directory.CreateDirectory(mapPath);
                return mapPath;
            }

            string CreateFolderBackUp()
            {
                var mapPath = string.Format("D:/BackUp" + uploadUrl);
                if (!Directory.Exists(mapPath))
                    Directory.CreateDirectory(mapPath);

                return mapPath;
            }

            async Task<int> CopyFileToServer(IFormFile file, string uploadPath, bool isImage = false)
            {
                if (System.IO.File.Exists(uploadPath))
                    return -1;

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return 1;
            }
        }
    }
}
