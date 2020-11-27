using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class StudentController : ControllerBase
    {
        // GET: BoMonControllers
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Route("GetByIdNameStudent/IdStudent"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Student User", Description = "Requires login verification!", OperationId = "Select Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> SelectByIdStudent(string IdStudent)
        {
            var result = await _studentService.SelectByIdStudent(IdStudent);
            return Ok(result);
        }

        [Route("SelectAllByidclass/{idclass}"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Student by idclass User", Description = "Requires login verification!", OperationId = "SelectAll Byiclass Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> SelectAllAsync(string idclass)
        {
            var result = await _studentService.SelectAllAsync(idclass);
            return Ok(result);
        }

        [Route("Insert/{idStudent}"), AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Get Student User", Description = "Requires login verification!", OperationId = "Insert Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> InsertAsync(string idStudent, StudentMeta nameStudent)
        {
            var result = await _studentService.InsertAsync(idStudent, nameStudent);
            return Ok(result);
        }

        [Route("Update/{id}/{idStudent}"), AcceptVerbs("PUT")]
        [SwaggerOperation(Summary = "Update Student User", Description = "Requires login verification!", OperationId = "Update Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> UpdateAsync(string id,string idStudent, StudentMeta nameStudent)
        {
            var result = await _studentService.UpdateAsync(id,idStudent, nameStudent);
            return Ok(result);
        }

        [Route("Delete/{id}/{idStudent}"), AcceptVerbs("DELETE")]
        [SwaggerOperation(Summary = "Delete Student User", Description = "Requires login verification!", OperationId = "Delete Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> DeleteAsync(string id, string idStudent)
        {
            var result = await _studentService.DeleteAsync(id, idStudent);
            return Ok(result);
        }
    }
}
