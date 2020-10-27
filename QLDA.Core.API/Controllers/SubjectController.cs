using System.ComponentModel.DataAnnotations;
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
    public class SubjectController : ControllerBase
    {

        // GET: BoMonControllers
        private readonly IBoMonService _boMonService;
        public SubjectController(IBoMonService boMonService)
        {
            _boMonService = boMonService;
        }

        [AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Bo Mon User", Description = "Requires login verification!", OperationId = "SelectBoMon", Tags = new[] { "Subject" })]
        public async Task<IActionResult> SelectAll()
        {
            var result = await _boMonService.SelectAll();
            return Ok(result);
        }

        [Route("GetById-MaM-TenBM"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "Get Bo Mon User", Description = "Requires login verification!", OperationId = "SelectBoMon", Tags = new[] { "Subject" })]
        public async Task<IActionResult> SelectById(string MaBM, string TenBM)
        {
            var result = await _boMonService.SelectById(MaBM, TenBM);
            return Ok(result);
        }

        [AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Insert Bo Mon User", Description = "Requires login verification!", OperationId = "InsertBoMon", Tags = new[] { "Subject" })]
        public async Task<IActionResult> InsertAsync([FromBody] BoMonMeta bomonMeta)
        {
            var result = await _boMonService.InsertAsync(bomonMeta);
            return Ok(result);
        }

        [Route("{mabomo}"), AcceptVerbs("PUT")]
        [SwaggerOperation(Summary = "Update Bo Mon User", Description = "Requires login verification!", OperationId = "UpdateBoMon", Tags = new[] { "Subject" })]
        public async Task<IActionResult> UpdateAsync(string mabomo, [FromBody] BoMonMeta bomonMeta)
        {
            var result = await _boMonService.UpdateAsync(mabomo, bomonMeta);
            return Ok(result);
        }

        [Route("delete Subject"), AcceptVerbs("DELETE")]
        [SwaggerOperation(Summary = "Update Bo Mon User", Description = "Requires login verification!", OperationId = "UpdateBoMon", Tags = new[] { "Subject" })]
        public async Task<IActionResult> DeleteAsync(string IdBomon, string TenBM)
        {
            var result = await _boMonService.DeleteAsync(IdBomon, TenBM);
            return Ok(result);
        }
    }
}
