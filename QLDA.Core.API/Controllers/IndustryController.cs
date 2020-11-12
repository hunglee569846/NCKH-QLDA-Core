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
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;
        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }
        [AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "GETALL Industry", Description = "Requires login verification!", OperationId = "GetAllIndustry", Tags = new[] {"Industry"})]
        public async Task<IActionResult> SelectAll()
        {
            var code= await _industryService.SelectAll();
            return Ok(code);

        }
        [AcceptVerbs("POST"),Route("InsertDepartment/{idDepartment}")]
        [SwaggerOperation(Summary = "GETALL Industry", Description = "Requires login verification!", OperationId = "GetAllIndustry", Tags = new[] {"Industry"})]
        public async Task<IActionResult> InsertAsync(string idDepartment,IndustryMeta industrymeta)
        {
            var code= await _industryService.InsertAsync(idDepartment, industrymeta);
            return Ok(code);
        }
    } 
}
