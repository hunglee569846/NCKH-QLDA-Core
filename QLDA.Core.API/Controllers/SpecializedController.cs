using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using Swashbuckle.AspNetCore.Annotations;

namespace QLDA.Core.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Insert, Update, Delete, GetAll")]
    public class SpecializedController : ControllerBase
    {
        private readonly ISpecializedService _specializedService;
        public SpecializedController(ISpecializedService specializedService)
        {
            _specializedService = specializedService;
        }
        [AcceptVerbs("GET"), Route("GetAllAsync")]
        [SwaggerOperation(Summary = "SelectAll Specialized User", Description = "Requires login verification!", OperationId = "GetAllAsyncSpecialized", Tags = new[] { "Specialized" })]
        public async Task<IActionResult> SelectAllAsync()
        {
            var result = await _specializedService.SelectAllAsync();
            return Ok(result);
        }

        [AcceptVerbs("POST"), Route("{idSpecialized}/{nameSpecialized}")]
        [SwaggerOperation(Summary = "Insert Specialized User", Description = "Requires login verification!", OperationId = "InsertAsyncSpecialized", Tags = new[] { "Specialized" })]
        public async Task<IActionResult> InsertAsync(string idSpecialized, string nameSpecialized, SpecializedsMeta specializedsMeta)
        {
            var result = await _specializedService.InsertAsync(idSpecialized, nameSpecialized, specializedsMeta);
            return Ok(result);
        }
        [AcceptVerbs("PUT"), Route("{id}/{nameSpecialized}/{idSpeacialized}")]
        [SwaggerOperation(Summary = "Update Specialized User", Description = "Requires login verification!", OperationId = "UpdateAsyncSpecialized", Tags = new[] { "Specialized" })]
        public async Task<IActionResult> UpdateAsync(string id, string nameSpecialized, string idSpeacialized, SpecializedsMeta specializedsMeta)
        {
            var result = await _specializedService.UpdateAsync(id, nameSpecialized, idSpeacialized, specializedsMeta);
            return Ok(result);
        }
        [AcceptVerbs("DELETE"), Route("Delete/{idSpecialized}/{nameSpecialized}")]
        [SwaggerOperation(Summary = "Delete Specialized User", Description = "Requires login verification!", OperationId = "DeleteAsyncSpecialized", Tags = new[] { "Specialized" })]
        public async Task<IActionResult> DeleteAsync(string idSpecialized, string nameSpecialized)
        {
            var result = await _specializedService.DeleteAsync(idSpecialized, nameSpecialized);
            return Ok(result);
        }
    }
}
