using NCKH.Infrastruture.Binding.ViewModel;
using NCKH.QLDA.FileManagenment.API.Domain.IRepository;
using NCKH.QLDA.FileManagenment.API.Domain.IServices;
using NCKH.QLDA.FileManagenment.API.Domain.ViewModels;
using NCKH.QLDA.FileManagenment.API.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<SearchResult<FileViewModel>> SearchAsync(string IdFile, string FileName, int FolderId)
        {
            return await _fileRepository.SearchAsync(IdFile, FileName, FolderId);
        }
        public async Task<List<FileViewModel>> GetsAll(string FileName, int FolderId)
        {
           return await _fileRepository.SelectAllAsync(FileName, FolderId);
        }
    }
}
