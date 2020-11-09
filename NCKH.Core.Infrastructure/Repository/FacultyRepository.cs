using Dapper;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
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
        public async Task<FacultyViewModel> SelectByIdAsync(string IdFaculty)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdFaculty", IdFaculty);
                var Code = await conn.QuerySingleAsync<FacultyViewModel>("[spSelectByIdFaculty]", para, commandType: CommandType.StoredProcedure);
                return Code;
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
        public async Task<bool> CheckExitsFacult(string idFacult)
        {

            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Faculty WHERE Idfaculty =@idFacult  AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { IdFacult = idFacult });
                return result;
            }
        }

    }
}
