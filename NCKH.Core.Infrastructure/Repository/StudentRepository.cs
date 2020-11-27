using Dapper;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.Model;
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
    public class StudentRepository : IStudentRepository
    {
        private readonly string _ConnectioString;
        public StudentRepository(string Connectionstring)
        {
            _ConnectioString = Connectionstring;
        }
        public async Task<StudentDetailViewmodel> SelectByIdStudentAsync(string IdStudent)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdStudent", IdStudent);
                var Code = await conn.QuerySingleOrDefaultAsync<StudentDetailViewmodel>("[dbo].[spStudent_SearchDetail]", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
        public async Task<int> InsertAsync(Students student)
        {
            int rowAffected = 0;
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", student.id);
                param.Add("@IdStudent", student.idStudent);
                param.Add("@LastName", student.LastName);
                param.Add("@Name", student.Name);
                param.Add("@Email", student.Email);
                param.Add("@IdClass", student.IdClass);
                param.Add("@PhoneNumber", student.PhoneNumber);
                if (student.CreateDate != null && student.CreateDate != DateTime.MinValue)
                {
                    param.Add("@CreateDate", student.CreateDate);
                }
                param.Add("@IsDelete", student.IsDelete);
                param.Add("@IsActive", student.IsActive);
                rowAffected = await conn.ExecuteAsync("[dbo].[spStuden_Insert]", param, commandType: CommandType.StoredProcedure);

            }
            return rowAffected;
        }

        public async Task<int> UpdateAsync(Students studen)
        {
            int rowAffected = 0;
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", studen.id);
                param.Add("@IdStudent", studen.idStudent);
                param.Add("@LastName", studen.LastName);
                param.Add("@Name", studen.Name);
                param.Add("@Email", studen.Email);
                param.Add("@IdClass", studen.IdClass);
                param.Add("@PhoneNumber", studen.PhoneNumber);
               
                rowAffected = await conn.ExecuteAsync("[dbo].[spStudent_Update]", param, commandType: CommandType.StoredProcedure);
            }
            return rowAffected;
        }


        public async Task<List<StudentViewModel>> SelectAllAsync(string idClass)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@idClass", idClass);
                var Result = await conn.QueryAsync<StudentViewModel>("[dbo].[spStuden_SelectAll]", para, commandType: CommandType.StoredProcedure);
                return Result.ToList();
            }

        }
        public async Task<Students> GetInfoAsync(string id)
        {
            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return await con.QuerySingleOrDefaultAsync<Students>("[dbo].[spStuden_SelectByID]", param, commandType: CommandType.StoredProcedure);
            }

        }
        public async Task<bool> CheckExistsAsync(string id)
        {
            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM Student WHERE Id = @Id AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<bool> CheckIstudentAsync(string idstudent)
        {
            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM dbo.Student WHERE IdStudent = @idstudent AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { IdStudent = idstudent });
                return result;
            }
        }
        public async Task<int> DeleteAsync(string id)
        {
            int rowAffected = 0;
            using (SqlConnection con = new SqlConnection(_ConnectioString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                rowAffected = await con.ExecuteAsync("[dbo].[spStuden_DeleteByID]", param, commandType: CommandType.StoredProcedure);
            }
            return rowAffected;

        }
    }
}