using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _department;
        public DepartmentController(IDepartmentService department)
        {
            _department = department;
        }
        [AcceptVerbs("GET"), Route("GetAll")]
        [SwaggerOperation(Summary = "SelectAll Department User", Description = "Requires login verification!", OperationId = "GetAllDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> SelectAllAsync()
        {
            var result = await _department.SelectAll();
            return Ok(result);
        }
        [AcceptVerbs("GET"), Route("GetByIdFaculty/{idfaculty}")]
        [SwaggerOperation(Summary = "SelectByID Department User", Description = "Requires login verification!", OperationId = "GetAllDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> SelectByIdFacultyAsync(string idfaculty)
        {
            var result = await _department.SelectByIdFaculty(idfaculty);
            return Ok(result);
        }
        [AcceptVerbs("GET"), Route("GetById")]
        [SwaggerOperation(Summary = "SelectByID Department User", Description = "Requires login verification!", OperationId = "GetAllDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> SelectByIdFacultyAsync(string idDepartment, string namedepartment)
        {
            var result = await _department.SelectByIdAsync(idDepartment, namedepartment);
            return Ok(result);
        }

        /// <summary>
        /// //them bang fileExcel conf chinh sua duong dan lay file
        /// </summary>
        /// <param name="NameFaculty"></param>
        /// <returns></returns>
        [AcceptVerbs("POST"), Route("Department/{NameFaculty}")]
        [SwaggerOperation(Summary = "Insert Department User", Description = "Requires login verification!", OperationId = "Insert Department", Tags = new[] { "Department" })]
        public async Task<IActionResult> InsertListExcel(string NameFaculty)
        {
            var result = await _department.InsertListExcelAsync(NameFaculty);
            return Ok(result);
        }


        [AcceptVerbs("POST"),Route("{NameFaculty}")]
        [SwaggerOperation(Summary = "Insert Department User", Description = "Requires login verification!", OperationId = "InsertFaculty", Tags = new[] { "Department" })]
        public async Task<IActionResult> InsertAsync(string NameFaculty,[FromBody] DepartmentMeta departmentMeta)
        {
            var result = await _department.InsertAsync(NameFaculty, departmentMeta);
            return Ok(result);
        }
        [Route("Update/{idDepartment}"), AcceptVerbs("PUT")]
        [SwaggerOperation(Summary = "Update Faculty User", Description = "Requires login verification!", OperationId = "UpdateDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> UpdateAsync(string idDepartment, DepartmentMeta department)
        {
            var result = await _department.UpdateAsync(idDepartment, department);
            return Ok(result);
        }
        [Route("DeleteDepartmernt/{idDepartment}/{nameDepartment}"), AcceptVerbs("DELETE")]
        [SwaggerOperation(Summary ="Delete Department User", Description= "Requires login verification!", OperationId = "DeleteDepartment", Tags = new[] { "Department" })]
        public async Task<IActionResult> DeleteAsync(string idDepartment,string nameDepartment)
        {
            var result = await _department.DeleteAsync(idDepartment, nameDepartment);
            return Ok(result);
        }
    }
}
