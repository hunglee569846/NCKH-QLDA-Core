using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace QLDA.Core.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Insert, Update, Delete, GetAll")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _faculty;
        public FacultyController(IFacultyService faculty)
        {
            _faculty = faculty;
        }

        [AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Insert Department User", Description = "Requires login verification!", OperationId = "InsertFaculty", Tags = new[] { "Faculty" })]
        public async Task<IActionResult> InsertAsync([FromBody] FacultyMeta facultyMeta)
        {
            var result = await _faculty.InsertAsync(facultyMeta);
            return Ok(result);
        }
        [AcceptVerbs("GET"),Route("GetById/{IdFaculty}")]
        [SwaggerOperation(Summary = "SelectByID Faculty User", Description = "Requires login verification!", OperationId = "GetByIdFaculty", Tags = new[] { "Faculty" })]
        public async Task<IActionResult> SelectById(string IdFaculty)
        {
            var result = await _faculty.SelectById(IdFaculty);
            return Ok(result);
        }
        [AcceptVerbs("GET"), Route("GetAll")]
        [SwaggerOperation(Summary = "SelectByID Faculty User", Description = "Requires login verification!", OperationId = "GetAllFaculty", Tags = new[] { "Faculty" })]
        public async Task<IActionResult> SelectAllAsync()
        {
            var result = await _faculty.SelectAll();
            return Ok(result);
        }
        [Route("Update/{IdFaculty}/{NameFaculty}"), AcceptVerbs("PUT")]
        [SwaggerOperation(Summary = "Update Faculty User", Description = "Requires login verification!", OperationId = "UpdateBoMon", Tags = new[] { "Faculty" })]
        public async Task<IActionResult> UpdateAsync(string IdFaculty,string NameFaculty)
        {
            var result = await _faculty.UpdateAsync(IdFaculty, NameFaculty);
            return Ok(result);
        }
        [Route("Delete Faculty/{IdFaculty}"), AcceptVerbs("DELETE")]
        [SwaggerOperation(Summary = "Update Faculty User", Description = "Requires login verification!", OperationId = "UpdateBoMon", Tags = new[] { "Faculty" })]
        public async Task<IActionResult> DeleteAsync(string IdFaculty)
        {
            var result = await _faculty.DeleteAsync(IdFaculty);
            return Ok(result);
        }
    }
}
