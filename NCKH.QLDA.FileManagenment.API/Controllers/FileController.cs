using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCKH.QLDA.FileManagenment.API.Domain.IServices;
using NCKH.QLDA.FileManagenment.API.Domain.ModelMetas;
using NCKH.QLDA.FileManagenment.API.Infrastructure.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace NCKH.QLDA.FileManagenment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [SwaggerTag("Insert, Update, Delete, Get Detail, Search Folders")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        //private readonly IFileService _fileService;
        public FileController(IFileService fileservice)
        {
            _fileService = fileservice;
        }
        [Route("SearchID/IdPath/FolderName/FolderId"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Insert information File.", Description = "Requires login verification!", OperationId = "InsertFile", Tags = new[] { "File" })]
        public async Task<IActionResult> SearchAsync(string IdFile, string FileName, int FolderId)
        {
            var result = await _fileService.SearchAsync(IdFile, FileName, FolderId);
            return Ok(result);
        }
        [Route("SearchAll/FolderName/FolderId"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Insert information File.", Description = "Requires login verification!", OperationId = "InsertFile", Tags = new[] { "File" })]
        public async Task<IActionResult> GetsAllAsync(string FileName, int FolderId)
        {
            var result = await _fileService.GetsAll(FileName, FolderId);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Upload file.", Description = "Requires login verification!", OperationId = "UploadFile", Tags = new[] { "File" })]
        [HttpPost, DisableRequestSizeLimit]
        [Route("uploads"), AcceptVerbs("POST")]
        public async Task<IActionResult> UploadFileAsync(string IdFile,string FileName, string creatorId, string FolderName, int folderId, IFormFileCollection formFileCollection)
        {
            var result = await _fileService.UploadFiles(IdFile, FileName,  creatorId,  FolderName,folderId,  formFileCollection);
            return Ok(result);
        }

    }
}
