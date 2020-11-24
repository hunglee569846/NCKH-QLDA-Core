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
                para.Add("@IdSpecialized", specialzed.IdSpecialized);
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
                var Code = await conn.ExecuteAsync("[spSpecialized_Insert]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<int> UpdateAsync(Specialized specialized)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@id", specialized.Id);
                para.Add("@IdSpecialized", specialized.IdSpecialized);
                para.Add("@NameSpecialized", specialized.NameSpecialized);
                para.Add("@Office", specialized.Office);
                para.Add("@Email", specialized.Email);
                para.Add("@PhoneNumber", specialized.PhoneNumber);
                para.Add("@Address", specialized.Address);
                para.Add("@Note", specialized.Note);
                para.Add("@IdIndustry", specialized.IdIndustry);
                para.Add("@LastUpdate", specialized.LastUpdate);
                if (specialized.CreateDate !=null && specialized.CreateDate != DateTime.MinValue)
                {
                    para.Add("@CreateDate", specialized.CreateDate);
                }
                para.Add("@IsDelete", specialized.IsDelete);
                para.Add("@IsActive", specialized.IsActive);
                var Code = await conn.ExecuteAsync("[spSpecialized_Update]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }

        }
        public async Task<int> DeleteAsync(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@id", id);
                var Code = await conn.ExecuteAsync("[spSpeacialized_Delete]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }

        }
        public async Task<bool> CheckExistByIdSpecialized(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Specializeds WHERE Id = @id AND IsDelete = 0), 1, 0)";
                var result = await conn.ExecuteScalarAsync<bool>(sql, new { Id= id });
                return result;
            }
        }
        public async Task<Specialized> GetInfoAsync(string nameSpecialized)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@nameSpeacialized", nameSpecialized);
                var code = await conn.QuerySingleOrDefaultAsync<Specialized>("spSpecialized_GetInfo", para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<bool> CheckExistByNameSpecialized(string nameSpecialized)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Specializeds WHERE NameSpecialized = @nameSpecialized AND IsDelete = 0), 1, 0)";
                var result = await conn.ExecuteScalarAsync<bool>(sql, new { NameSpecialized = nameSpecialized });
                return result;
            }
        }

        public async Task<bool> CheckExist(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Specializeds WHERE Id = @id AND IsDelete = 0), 1, 0)";
                var result = await conn.ExecuteScalarAsync<bool>(sql, new { Id = id });
                return result;
            }
        }
    }
}
