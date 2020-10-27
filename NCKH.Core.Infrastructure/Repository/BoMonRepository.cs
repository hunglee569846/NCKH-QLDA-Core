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
    public class BoMonRepository : IBoMonRepository
    {
        private readonly string _ConnectioString;
        private readonly ILogger<BoMonRepository> _logger;
        public BoMonRepository(string ConnectionString,
                                ILogger<BoMonRepository> logger)
        {
            _logger = logger;
            _ConnectioString = ConnectionString;
        }
        public async Task<List<BoMonViewModel>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Result = await conn.QueryAsync<BoMonViewModel>("spSelectAllBoMon");
                return Result.ToList();
            }
        }
        public async Task<BoMonViewModel> SelectByIdAsync(string MaBM, string TenBM)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", MaBM);
                para.Add("@TenBoMon", TenBM);
                var Code = await conn.QuerySingleOrDefaultAsync<BoMonViewModel>("spSelectByIdBomon", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }

        public async Task<int> InsertAsync(BoMon bomon)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", bomon.MaBoMon);
                para.Add("@TenBoMon", bomon.TenBoMon);
                para.Add("@IdFaculty", bomon.IdFaculty);
                para.Add("@CreatDate", bomon.CreateDate);
                para.Add("@IsDelete", bomon.IsDelete);
                para.Add("@IsActive", bomon.IsActive);
                if (bomon.LastUpdate != null && bomon.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", bomon.LastUpdate);
                }
                var Code = await conn.ExecuteAsync("spInsertBoMon", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<int> UpdateAsync(string MaBoMon, BoMon bomon)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@MaBoMon", MaBoMon);
                para.Add("@TenBoMon", bomon.TenBoMon);
                para.Add("@IdFaculty", bomon.IdFaculty);
                var Code = await conn.ExecuteAsync("spUpdateBomon", para, commandType: CommandType.StoredProcedure);
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
                var Code = await conn.ExecuteAsync("spDeleteBomon", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<bool> CheckExitsFacult(string idFacult)
        {

            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM Albums WHERE IdFacult = @idFacult AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { IdFacult = idFacult });
                return result;
            }
        }
    }
}
