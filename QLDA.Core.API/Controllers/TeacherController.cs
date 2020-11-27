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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _ITeacherService;
        public TeacherController(ITeacherService iTeacherService)
        {
            _ITeacherService = iTeacherService;
        }
        [Route("GetAll"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "GETAll Teachers User", Description = "Requires login verification!", OperationId = "GetAll Teachers ", Tags = new[] { "Teachers" })]
        public async Task<IActionResult> SelectAll()
        {
            var result = await _ITeacherService.SelectAllAsync();
            return Ok(result);
        }

        [Route("Get/{idDepartment}"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "GET Teachers User", Description = "Requires login verification!", OperationId = "Get Teachers ", Tags = new[] { "Teachers" })]
        public async Task<IActionResult> SearchByIdDepartment(string idDepartment)
        {
            var result = await _ITeacherService.SelectbyIdDepartmentAsync(idDepartment);
            return Ok(result);
        }

        [Route("Insert/{idTeacher}/{idDepartment}"), AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Insert Teachers User", Description = "Requires login verification!", OperationId = "Insert Teachers ", Tags = new[] { "Teachers" })]
        public async Task<IActionResult> InsertAsync(string idTeacher, string idDepartment, TeacherMeta teacherMeta)
        {
            var result = await _ITeacherService.InsertAsync(idTeacher, idDepartment, teacherMeta);
            return Ok(result);
        }
    }
}
