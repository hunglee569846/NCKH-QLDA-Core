using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
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
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsService _itopicsService;
        public TopicsController(ITopicsService topicsService)
        {
            _itopicsService = topicsService;
        }

        [Route("Get"), AcceptVerbs("GET")]
        [SwaggerOperation(Summary = "GetAll Topics User", Description = "Requires login verification!", OperationId = "GetAll Topics ", Tags = new[] { "Topics" })]
        public async Task<IActionResult> GetAll()
        {
            var result = await _itopicsService.SelectAllAsync();
            return Ok(result);
        }

        [Route("Insert/{isStudent}/{idTeacherMain}"), AcceptVerbs("POST")]
        [SwaggerOperation(Summary = "Insert Topics User", Description = "Requires login verification!", OperationId = "Insert Topics ", Tags = new[] { "Topics" })]
        public async Task<IActionResult> InsertAsync(string isStudent, string idTeacherMain, TopicsMeta topicsMeta)
        {
            var result = await _itopicsService.InsertAsync(isStudent, idTeacherMain, topicsMeta);
            return Ok(result);
        }

        [Route("Update/{id}"), AcceptVerbs("PUT")]
        [SwaggerOperation(Summary = "Confirm Topics User", Description = "Requires login verification!", OperationId = "Confirm Topics ", Tags = new[] { "Topics" })]
        public async Task<IActionResult> ConFirmTopics(string id)
        {
            var result = await _itopicsService.Confirm(id);
            return Ok(result);
        }
    }
}
