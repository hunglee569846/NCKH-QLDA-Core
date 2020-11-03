using Microsoft.AspNetCore.Mvc;
using NCKH.QLDA.FileManagenment.API.Domain.IServices;
using NCKH.QLDA.FileManagenment.API.Domain.ModelMetas;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [SwaggerTag("Insert, Update, Delete, Get Detail, Search Folders")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderServices _folderService;
        //private readonly IFileService _fileService;
        public FolderController(IFolderServices folderservice)
        {
            _folderService = folderservice;
        }

        [Route("Insert/{IdPath}/{FolderName}/{FolderId}"), AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Insert information folder.", Description = "Requires login verification!", OperationId = "InsertFolder", Tags = new[] { "Folder" })]
        public async Task<IActionResult> InsertAsync(string IdPath, string FolderName,int FolderId, [FromBody] FolderMeta folderMeta)
        {
            var result = await _folderService.InsertAsync(IdPath, FolderName, FolderId, folderMeta);
            if (result.Code <= 0)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
