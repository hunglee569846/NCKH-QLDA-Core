using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using Swashbuckle.AspNetCore.Annotations;

namespace QLDA.Core.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Insert, Update, Delete, GetAll")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _iclassService;
        public ClassController(IClassService iclassService)
        {
            _iclassService = iclassService;
        }
        [AcceptVerbs("GET"), Route("GetAll")]
        [SwaggerOperation(Summary = "SelectAll ClasSpecialized User", Description = "Requires login verification!", OperationId = "GetAllClasSpecialized", Tags = new[] { "ClasSpecialized" })]
        public async Task<IActionResult> SelectAllAsync()
        {
            var result = await _iclassService.SelecAllAsync();
            return Ok(result);
        }

        [AcceptVerbs("GET"), Route("GetByidClass/{id}/{idClass}")]
        [SwaggerOperation(Summary = "GetByidClass ClasSpecialized User", Description = "Requires login verification!", OperationId = "GetByidClasSpecialized", Tags = new[] { "ClasSpecialized" })]
        public async Task<IActionResult> SelectById(string id, string idClass)
        {
            var result = await _iclassService.SearchAsync(id,idClass);
            return Ok(result);
        }


        [AcceptVerbs("POST"), Route("post/{ClassName}/{idClass}")]
        [SwaggerOperation(Summary = "post Department User", Description = "Requires login verification!", OperationId = "Post-ClasSpecialized", Tags = new[] { "ClasSpecialized" })]
        public async Task<IActionResult> InsertAsync(string ClassName, string idClass, ClassMeta clas)
        {
            var result = await _iclassService.InsertAsync(ClassName,idClass,clas);
            return Ok(result);
        }


        [AcceptVerbs("PUT"), Route("Update/{id}/{idClass}-{className}")]
        [SwaggerOperation(Summary = "Update Department User", Description = "Requires login verification!", OperationId = "Update-ClasSpecialized", Tags = new[] { "ClasSpecialized" })]
        public async Task<IActionResult> UpdateAsync(string id, string idClass,string className, ClassMeta clas)
        {
            var result = await _iclassService.UpdateAsync(id, idClass, className, clas);
            return Ok(result);
        }

        [AcceptVerbs("DELETE"), Route("Delete/{id}/{idclass}")]
        [SwaggerOperation(Summary = "DELETE Department User", Description = "Requires login verification!", OperationId = "DELETE-ClasSpecialized", Tags = new[] { "ClasSpecialized" })]
        public async Task<IActionResult> DeleteAsync(string id,string idclass)
        {
            var result = await _iclassService.DeleteAsync(id,idclass);
            return Ok(result);
        }

    }
}
