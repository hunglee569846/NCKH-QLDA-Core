using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ViewModel;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDA.Core.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Insert, Update, Delete, GetAll")]
    public class RegistTeacherController : ControllerBase
    {
        private readonly IRegistTeacherService _registTeacherService;
        public RegistTeacherController(IRegistTeacherService registTeacherService)
        {
            _registTeacherService = registTeacherService;
        }

        [AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "GETALL RegistTeacher", Description = "Requires login verification!", OperationId = "GetAllRegistTeacher", Tags = new[] { "RegistTeacher" })]
        public async Task<IActionResult> SelectAllAsync()
        {
            var code = await _registTeacherService.SelectAll();
            return Ok(code);
        }

        [AcceptVerbs("POST"),Route("{IdStudent}/{IdTeacherMain}/{IdTopic}")]
        [SwaggerOperation(Summary = "Insert RegistTeacher", Description = "Requires login verification!", OperationId = "InsertRegistTeacher", Tags = new[] { "RegistTeacher" })]
        public async Task<IActionResult> InsertAsync(string IdStudent, string IdTeacherMain, string IdTeacher2, string IdTopic)
        {
            var code = await _registTeacherService.InsertAsync(IdStudent, IdTeacherMain, IdTeacher2, IdTopic);
            return Ok(code);
        }
    }
}
