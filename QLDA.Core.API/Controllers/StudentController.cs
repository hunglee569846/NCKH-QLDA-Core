using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IServices;
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

        [Route("GetById/{IdStudent}"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Student User", Description = "Requires login verification!", OperationId = "Select Student ", Tags = new[] { "Student" })]
        public async Task<IActionResult> SelectById(string IdStudent)
        {
            var result = await _studentService.SelectById(IdStudent);
            return Ok(result);
        }
    }
}
