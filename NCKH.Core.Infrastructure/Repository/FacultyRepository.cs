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
    public class FacultyRepository : IFacultyRepository
    {
        private readonly string _ConnectioString;
        public FacultyRepository(string ConnectionString)
        {
          
            _ConnectioString = ConnectionString;
        }
        public async Task<int> InsertAsync(Faculty faculty)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdFaculty", faculty.IdFaculty);
                para.Add("@NameFaculty", faculty.NameFaculty);
                para.Add("@IsDelete", faculty.IsDelete);
                para.Add("@IsActive", faculty.IsActive);
            
                var Code = await conn.ExecuteAsync("[spInsertFaculty]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        //chi tra ra 1 ban ghi voi dieu kien dung
        //check TotalRows theo thu tu trong StoredProcedure
        public async Task<SearchResult<FacultyViewModel>> SelectByIdAsync(string IdFaculty)
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
                        var faculty = await multi.ReadAsync<FacultyViewModel>();
                        var totalrow = (await multi.ReadAsync<int>()).Single();
                        return new SearchResult<FacultyViewModel>
                        {
                            TotalRows = totalrow,
                            Data = faculty,
                        };
                    }
                }
            }
            catch (Exception)
            {
                return new SearchResult<FacultyViewModel> { TotalRows = 0, Data = null };
            }
           
        }
        public async Task<List<FacultyViewModel>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Code = await conn.QueryAsync<FacultyViewModel>("[spSelectAllFaculty]");
                return Code.ToList();
            }
        }
        public async Task<int> DeleteAsync (string IdFaculty)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdFaculty", IdFaculty);
                var Code = await conn.ExecuteAsync("[spDeleteFaculty]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<int> UpdateAsync(string IdFaculty, string NameFaculty)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdFaculty", IdFaculty);
                para.Add("@NameFaculty", NameFaculty);
                var Code = await conn.ExecuteAsync("[spUpdateFaculty]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<bool> CheckExitsFacult(string namefaculty)
        {

            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Faculty WHERE NameFaculty = @namefaculty  AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { NameFaculty = namefaculty });
                return result;
            }
        }

    }
}
