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
    public class SpecializedRepository : ISpecializedRepository
    {
        private readonly string _connectionstring;
        public SpecializedRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public async Task<List<SpecializedsViewModel>> SelectAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Code = await conn.QueryAsync< SpecializedsViewModel>("spSpecialized_SelectAll");
                return Code.ToList();
            }
        }
        public async Task<int> InsertAsync(Specialized specialzed)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@id", specialzed.Id);
                para.Add("@NameSpecialized", specialzed.NameSpecialized);
                para.Add("@Office", specialzed.Office);
                para.Add("@Email", specialzed.Email);
                para.Add("@PhoneNumber", specialzed.PhoneNumber);
                para.Add("@Address", specialzed.Address);
                para.Add("@Note", specialzed.Note);
                para.Add("@IdIndustry", specialzed.IdIndustry);
                para.Add("@LastUpdate", specialzed.LastUpdate);
                para.Add("@CreateDate", specialzed.CreateDate);
                para.Add("@IsDelete", specialzed.IsDelete);
                para.Add("@IsActive", specialzed.IsActive);
                var Code = await conn.ExecuteAsync("spSpecialized_SelectAll");
                return Code;
            }
        }
        public async Task<bool> CheckExistByIdSpecialized(string idSpecialized)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Specializeds WHERE IdSpecialized = @idSpecialized AND IsDelete = 0), 1, 0)";
                var result = await conn.ExecuteScalarAsync<bool>(sql, new { IdSpecialized = idSpecialized });
                return result;
            }
        }
        public async Task<Industry> GetInfoAsync(string nameSpecialized)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@nameSpeacialized", nameSpecialized);
                var code = await conn.QuerySingleOrDefaultAsync<Industry>("spSpecialized_GetInfo", para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<bool> CheckExistByNameSpecialized(string nameSpecailized)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Specializeds WHERE NameSpecialized = @nameSpecailized AND IsDelete = 0), 1, 0)";
                var result = await conn.ExecuteScalarAsync<bool>(sql, new { NameSpecialized = nameSpecailized });
                return result;
            }
        }
    }
}
