using Dapper;
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
    public class ClassRepository : IClassRepository
    {
        private readonly string _connectionString;
        public ClassRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<ClassViewModel>> SelectAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Result = await conn.QueryAsync<ClassViewModel>("spClass_SelectAll");
                return Result.ToList();
            }

        }
        public async Task<int> InsertAsync(ClassSpecialized clas)
        {
            int rowAffected = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", clas.id);
                    param.Add("@IdClass", clas.IdClass);
                    param.Add("@ClassName", clas.ClassName);
                    param.Add("@IdSpecialized", clas.IdSpecialized);
                    param.Add("@IdEducationProgram", clas.IdEducationProgram);
                    param.Add("@Course", clas.Course);
                    if (clas.Createdate != null && clas.Createdate != DateTime.MinValue)
                    {
                        param.Add("@Createdate", clas.Createdate);
                    }
                    if (clas.LastUpdate != null && clas.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", clas.LastUpdate);
                    }
                    param.Add("@IsDelete", clas.IsDelete);
                    param.Add("@IsActive", clas.IsActive);
                    rowAffected = await con.ExecuteAsync("[dbo].[spClas_Insert]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;

            }
           
        }

        public async Task<int> UpdateAsync(ClassSpecialized clas)
        {
            int rowAffected = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", clas.id);
                param.Add("@IdClass", clas.IdClass);
                param.Add("@ClassName", clas.ClassName);
                param.Add("@IdSpecialized", clas.IdSpecialized);
                param.Add("@IdEducationProgram", clas.IdEducationProgram);
                param.Add("@Course", clas.Course);
                if (clas.Createdate != null && clas.Createdate != DateTime.MinValue)
                {
                    param.Add("@Createdate", clas.Createdate);
                }
                if (clas.LastUpdate != null && clas.LastUpdate != DateTime.MinValue)
                {
                    param.Add("@LastUpdate", clas.LastUpdate);
                }
                rowAffected = await con.ExecuteAsync("[dbo].[spClas_Update]", param, commandType: CommandType.StoredProcedure);
            }
            return rowAffected;
        }


        public async Task<ClassViewModel> SelectByIdClass(string id, string idclas)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@IdClass", idclas);
                return await con.QuerySingleOrDefaultAsync<ClassViewModel>("[dbo].[spClass_SelectByID]", param, commandType: CommandType.StoredProcedure);
            }
        }

        //chua xong
        public async Task<ClassSpecialized> GetInfoAsync(string id, string idClas)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@idClass", idClas);
                return await con.QuerySingleOrDefaultAsync<ClassSpecialized>("[dbo].[spClas_GetInfor]", param, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<bool> CheckIdAsync(string id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM Class WHERE Id = @id AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<bool> CheckExistsAsync(string IdClass)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM Class WHERE IdClass = @IdClass AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { IdClass = IdClass });
                return result;
            }
        }
        public async Task<bool> CheckNameExistsAsync(string className)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM Class WHERE ClassName =@className  AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { ClassName = className });
                return result;
            }
        }

        public async Task<int> DeleteAsync(string id, string idclass)
        {
            int rowAffected = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id",id);
                param.Add("@IdClass",idclass);
               
                rowAffected = await con.ExecuteAsync("[dbo].[spClas_DeleteByID]", param, commandType: CommandType.StoredProcedure);
            }
            return rowAffected;
        }

    }
}
