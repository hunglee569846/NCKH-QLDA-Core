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

        [Route(" GETbyIdNameStudent/IdStudent"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Student User", Description = "Requires login verification!", OperationId = "Select Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> SelectById(string IdStudent,string NameStudent)
        {
            var result = await _studentService.SelectById(IdStudent, NameStudent);
            return Ok(result);
        }

        [Route(" SelectAllByid/{idclass}"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Student by idclass User", Description = "Requires login verification!", OperationId = "SelectAll Byiclass Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> SelectAllAsync(string idclass)
        {
            var result = await _studentService.SelectAllAsync(idclass);
            return Ok(result);
        }

        [Route(" Insert/{idStudent}"), AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Get Student User", Description = "Requires login verification!", OperationId = "Select Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> InsertAsync(string idStudent, StudentMeta nameStudent)
        {
            var result = await _studentService.InsertAsync(idStudent, nameStudent);
            return Ok(result);
        }
    }
}
