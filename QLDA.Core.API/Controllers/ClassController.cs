using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IServices;
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
        [SwaggerOperation(Summary = "SelectAll Department User", Description = "Requires login verification!", OperationId = "GetAllDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> SelectAllAsync()
        {
            var result = await _iclassService.SelecAllAsync();
            return Ok(result);
        }
    }
}
