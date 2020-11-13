using Dapper;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Repository
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly string _ConnectionString;
        public IndustryRepository(string connectionstring)
        {
            _ConnectionString = connectionstring;
        }
        public async Task<List<IndustryViewModel>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var code = await conn.QueryAsync<IndustryViewModel>("spIndustry_SelectAll");
                return code.ToList();
            }

        }
        public async Task<IndustryViewModel> SelectByIdAsync(string idIndustry)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdIndustry", idIndustry);
                var code = await conn.QuerySingleOrDefaultAsync<IndustryViewModel>("spIndustry_SelectById", para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<Industry> GetInfoAsync(string NameIndustry)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@NameIndustry", NameIndustry);
                var code = await conn.QuerySingleOrDefaultAsync<Industry>("spIndustry_GetInfo", para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<int> InsertAsync(Industry industry)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdIndustry", industry.IdIndustry);
                para.Add("@NameIndustry", industry.NameIndustry);
                para.Add("@IdDepartment", industry.IdDepartment);
                para.Add("@Email", industry.Email);
                para.Add("@PhoneNumber", industry.PhoneNumber);
                para.Add("@Adress", industry.Address);
                para.Add("@Details", industry.Details);
                para.Add("@CreateDate", industry.CreateDate);
                para.Add("@IsDelete", industry.IsDelete);
                para.Add("@IsActive", industry.IsActive);
                if (industry.LastUpdate != null && industry.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", industry.LastUpdate);
                }
                if (industry.Deletetime != null && industry.Deletetime != DateTime.MinValue)
                {
                    para.Add("@Deletetime", industry.Deletetime);
                }  
                var code = await conn.ExecuteAsync("spIndustry_Insert",para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<int> UpdateAsync(Industry industry)
        {
             using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdIndustry", industry.IdIndustry);
                para.Add("@NameIndustry", industry.NameIndustry);
                para.Add("@IdDepartment", industry.IdDepartment);
                para.Add("@Email", industry.Email);
                para.Add("@PhoneNumber", industry.PhoneNumber);
                para.Add("@Adress", industry.Address);
                para.Add("@Details", industry.Details);
                if (industry.CreateDate != null && industry.CreateDate != DateTime.MinValue)
                {
                    para.Add("@CreateDate", industry.CreateDate);
                }
                para.Add("@IsDelete", industry.IsDelete);
                para.Add("@IsActive", industry.IsActive);
                if (industry.LastUpdate != null && industry.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", industry.LastUpdate);
                }
                if (industry.Deletetime != null && industry.Deletetime != DateTime.MinValue)
                {
                    para.Add("@Deletetime", industry.Deletetime);
                }
                var code = await conn.ExecuteAsync("spIndustry_Update", para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<int> DeleteAsync(string idIndustry)
        {
            using(SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@idIndustry", idIndustry);
                var code = await conn.ExecuteAsync("spIndustry_Delete", para, commandType: CommandType.StoredProcedure);
                return code;
            }
        }
        public async Task<bool> checkexitNameIndustry(string nameIndustry)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Industry WHERE NameIndustry = @nameIndustry AND IsDelete = 0), 1, 0)";

                var result = await conn.ExecuteScalarAsync<bool>(sql, new { NameIndustry = nameIndustry });
                return result;
            }
        }
        public async Task<bool> checkexitIdIndustry(string idIndustry)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Industry WHERE IdIndustry = @idIndustry AND IsDelete = 0), 1, 0)";

                var result = await conn.ExecuteScalarAsync<bool>(sql, new { IdIndustry = idIndustry });
                return result;
            }
        }

    }
}
