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
    public class RegistTeacherRepository: IRegistTeacherRepository
    {
        private readonly string _ConnectionString;
        public RegistTeacherRepository(string connectionstring)
        {
            _ConnectionString = connectionstring;
        }
        public async Task<List<ViewRegistTeacher>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var code = await conn.QueryAsync<ViewRegistTeacher>("spRegistTeacher_SelectAll");
                return code.ToList();
            }
        }
        public async Task<int> InsertAsync(RegistTeacher registTeacher)
        {
            int rowinface = 0;
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@id", registTeacher.id);
                para.Add("@IdStudent", registTeacher.IdStudent);
                para.Add("@IdTeacherMain", registTeacher.IdTeacherMain);
                if (registTeacher.IdTeacher2 != null)
                {
                    para.Add("@IdTeacher2", registTeacher.IdTeacher2);
                }
               
                para.Add("@IdTopic", registTeacher.IdTopic);
                if(registTeacher.CreateDate !=null && registTeacher.CreateDate != DateTime.MinValue)
                {
                    para.Add("@CreateDate", registTeacher.CreateDate);
                }
                if(registTeacher.LastUpdate != null && registTeacher.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpDate", registTeacher.LastUpdate);
                }
                para.Add("@IsActive", registTeacher.IsActive);
                para.Add("@IsDone", registTeacher.IsDone);
                rowinface = await conn.ExecuteAsync("spRegistTeacher_Insert", para, commandType: CommandType.StoredProcedure);
                return rowinface;
            }
        }
    }
}
