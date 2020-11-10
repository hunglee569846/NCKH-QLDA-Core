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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _department;
        public DepartmentController(IDepartmentService department)
        {
            _department = department;
        }

        [AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Department User", Description = "Requires login verification!", OperationId = "SelectDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> SelectAll()
        {
            var result = await _department.SelectAll();
            return Ok(result);
        }

        [Route("GetById-MaM-TenBM"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Department User", Description = "Requires login verification!", OperationId = "SelectBoMon", Tags = new[] { "Department" })]
        public async Task<IActionResult> SelectById(string MaBM, string TenBM)
        {
            var result = await _department.SelectById(MaBM, TenBM);
            return Ok(result);
        }

        [AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Insert Department User", Description = "Requires login verification!", OperationId = "InsertBoMon", Tags = new[] { "Department" })]
        public async Task<IActionResult> InsertAsync(string IdFaculty,[FromBody] DepartmentMeta bomonMeta)
        {
            var result = await _department.InsertAsync(IdFaculty, bomonMeta);
            return Ok(result);
        }

        [Route("{IdDepartment}"), AcceptVerbs("PUT")]
        [SwaggerOperation(Summary = "Update Department User", Description = "Requires login verification!", OperationId = "UpdateBoMon", Tags = new[] { "Department" })]
        public async Task<IActionResult> UpdateAsync(string IdDepartment, [FromBody] DepartmentMeta bomonMeta)
        {
            var result = await _department.UpdateAsync(IdDepartment, bomonMeta);
            return Ok(result);
        }

        [Route("delete Department"), AcceptVerbs("DELETE")]
        [SwaggerOperation(Summary = "Update Department User", Description = "Requires login verification!", OperationId = "UpdateBoMon", Tags = new[] { "Department" })]
        public async Task<IActionResult> DeleteAsync(string IdDepartment, string NameDepartment)
        {
            var result = await _department.DeleteAsync(IdDepartment, NameDepartment);
            return Ok(result);
        }
    }
}
