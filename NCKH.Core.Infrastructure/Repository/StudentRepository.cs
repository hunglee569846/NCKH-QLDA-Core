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
        public async Task<StudentDetailViewmodel> SelectByIdAsync(string IdStudent, string NameStudent)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdStudent", IdStudent);
                para.Add("@NameStudent", NameStudent);
                var Code = await conn.QuerySingleOrDefaultAsync<StudentDetailViewmodel>("[spSearchStudentDetail]", para, commandType: CommandType.StoredProcedure);
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
    }
}