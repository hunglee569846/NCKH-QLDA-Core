using Dapper;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _ConnectioString;
        public DepartmentRepository(string ConnectionString)
        {
            _ConnectioString = ConnectionString;
        }
        public async Task<List<DepartmentViewModel>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Code = await conn.QueryAsync<DepartmentViewModel>("[spDepartment_SelectAll]");
                return Code.ToList();
            }
        }

        public async Task<DepartmentViewModel> SelectByIdAsync(string idDepartment, string namepartment)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdDepartment", idDepartment);
                para.Add("@NameDepartment", namepartment);
                var result = await conn.QuerySingleOrDefaultAsync<DepartmentViewModel>("spDepartment_SelectById", para, commandType: CommandType.StoredProcedure);
                return result;
            }

        }

        public async Task<SearchResult<DepartmentViewModel>> SelectByIdFacultyAsync(string IdFaculty)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectioString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@IdFaculty", IdFaculty);
                    using (var multi = await conn.QueryMultipleAsync("[spDepartment_SearchByIdFaculty]", para, commandType: CommandType.StoredProcedure))
                    {
                        var department = await multi.ReadAsync<DepartmentViewModel>();
                        var totalrow = (await multi.ReadAsync<int>()).Single();
                        return new SearchResult<DepartmentViewModel>
                        {
                            TotalRows = totalrow,
                            Data = department,
                        };
                    }
                }
            }
            catch (Exception)
            {

                return new SearchResult<DepartmentViewModel> { TotalRows = 0, Data = null };
            }
            
        }
        public async Task<int> InsertAsync(Department department)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdDepartment", department.IdDepartment);
                para.Add("@NameDepartment", department.NameDepartment);
                para.Add("@Office", department.Office);
                para.Add("@Addres", department.Addres);
                para.Add("@Email", department.Email);
                para.Add("@PhoneNumber", department.PhoneNumber);
                para.Add("@IdFaculty", department.IdFaculty);
                if (department.CreateDate != null && department.CreateDate != DateTime.MinValue)
                {
                    para.Add("@CreatDate", department.CreateDate);
                }
                para.Add("@IsDelete", department.IsDelete);
                para.Add("@IsActive", department.IsActive);
                if (department.LastUpdate != null && department.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", department.LastUpdate);
                }
                var Code = await conn.ExecuteAsync("[spDepartment_Insert]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<bool> CheckExitsDepartment(string namedepartment)
        {
            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Department WHERE NameDepartment = @namedepartment AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { NameDepartment = namedepartment });
                return result;
            }
        }
        public async Task<int> UpdateAsync(Department department)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdDepartment", department.IdDepartment);
                para.Add("@NameDepartment", department.NameDepartment);
                para.Add("@Office", department.Office);
                para.Add("@Addres", department.Addres);
                para.Add("@Email", department.Email);
                para.Add("@PhoneNumber", department.PhoneNumber);
                para.Add("@IdFaculty", department.IdFaculty);
                if (department.LastUpdate != null && department.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", department.LastUpdate);
                }
                var Code = await conn.ExecuteAsync("[spDepartment_Update]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
    }
}
