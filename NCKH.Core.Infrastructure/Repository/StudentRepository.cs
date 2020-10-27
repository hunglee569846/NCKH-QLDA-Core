using Dapper;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public async Task<StudentDetailViewmodel> SelectByIdAsync(string IdStudent)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectioString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@IdStudent", IdStudent);
                var Code = await conn.QuerySingleOrDefaultAsync<StudentDetailViewmodel>("spStudentDetailViewModel", para, commandType: CommandType.StoredProcedure);
                return Code;
            }
        }
    }
}
