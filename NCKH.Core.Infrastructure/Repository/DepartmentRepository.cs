using Dapper;
using Microsoft.Extensions.Logging;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _ConnectioString;
        private readonly ILogger<DepartmentRepository> _logger;
        public DepartmentRepository(string ConnectionString,
                                ILogger<DepartmentRepository> logger)
        {
            _logger = logger;
            _ConnectioString = ConnectionString;
        }
        public async Task<List<DepartmentViewModel>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Result = await conn.QueryAsync<DepartmentViewModel>("[spSelectAllDepartment]");
                return Result.ToList();
            }
        }
        public async Task<DepartmentViewModel> SelectByIdAsync(string IdDepartment, string NameDepartment)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", IdDepartment);
                para.Add("@TenBoMon", NameDepartment);
                var Code = await conn.QuerySingleOrDefaultAsync<DepartmentViewModel>("spSelectByIdDepartment", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }

        public async Task<int> InsertAsync(Department department)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", department.IdDepartment);
                para.Add("@TenBoMon", department.NameDepartment);
                para.Add("@IdFaculty", department.IdFaculty);
                para.Add("@CreatDate", department.CreateDate);
                para.Add("@IsDelete", department.IsDelete);
                para.Add("@IsActive", department.IsActive);
                if (department.LastUpdate != null && department.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", department.LastUpdate);
                }
                var Code = await conn.ExecuteAsync("[spInsertDepartment]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<int> UpdateAsync(string IdDepartment, Department department)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", IdDepartment);
                para.Add("@TenBoMon", department.NameDepartment);
                para.Add("@IdFaculty", department.IdFaculty);
                var Code = await conn.ExecuteAsync("[spUpdateDepartment]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<int> DeleteAsync(string IdBomon, string TenBM)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", IdBomon);
                para.Add("@TenBoMon", TenBM);
                var Code = await conn.ExecuteAsync("spDeleteDepartment", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
       
    }
}
