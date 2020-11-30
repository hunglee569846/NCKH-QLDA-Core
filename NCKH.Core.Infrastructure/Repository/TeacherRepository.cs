using Dapper;
using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.Model;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly string _ConnectionString;
        public TeacherRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        public async Task<bool> CheckExistsAsync(string id)
        {
            using(SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM Teacher WHERE Id = @Id AND IsDelete = 0), 1, 0)";

                var result = await conn.ExecuteScalarAsync<bool>(sql, new { Id = id });
                return result;
            }

        }

        public async Task<bool> CheckCountTopicsAsync(string id)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM Teacher WHERE Id = @Id AND IsDelete = 0 AND IsTopicsFull =0), 1, 0)";

                var result = await conn.ExecuteScalarAsync<bool>(sql, new { Id = id });
                return result;
            }
        }

       public async Task<bool> CheckIdTeacherAsync(string idTeacher)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM Teacher WHERE IdTeacher = @idTeacher AND IsDelete = 0), 1, 0)";

                var result = await conn.ExecuteScalarAsync<bool>(sql, new { IdTeacher = idTeacher });
                return result;
            }
        }
        public async Task<int> InsertAsync(Teachers teacher)
        {
            int rowAffected = 0;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", teacher.Id);
                param.Add("@IdTeacher", teacher.IdTeacher);
                param.Add("@NameTeacher", teacher.NameTeacher);
                param.Add("@IdDepartment", teacher.IdDepartment);
                param.Add("@Note", teacher.Note);
                param.Add("@WorkingCompany", teacher.WorkingCompany);
                param.Add("@PhoneNumber", teacher.PhoneNumber);
                param.Add("@Email", teacher.Email);
                param.Add("@CountTopics", teacher.CountTopics);
                if (teacher.CreateDate != null && teacher.CreateDate != DateTime.MinValue)
                {
                    param.Add("@CreateDate", teacher.CreateDate);
                }
                if (teacher.LastUpdate != null && teacher.LastUpdate != DateTime.MinValue)
                {
                    param.Add("@LastUpdate", teacher.LastUpdate);
                }
                param.Add("@IsTopicsFull", teacher.IsTopicsFull);
                param.Add("@IsDelete", teacher.IsDelete);
                param.Add("@IsActive", teacher.IsActive);
                rowAffected = await con.ExecuteAsync("[dbo].[spTeache_Insert]", param, commandType: CommandType.StoredProcedure);
            }
            return rowAffected;
        }

        public async Task<SearchResult<TeacherViewModel>> SelectByIdDepartmentAsync(string idDepartment)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@IdDepartment", idDepartment);
                    using (var multi = await con.QueryMultipleAsync("[dbo].[spTeache_Search]", param, commandType: CommandType.StoredProcedure))
                    {
                        var totalrow = (await multi.ReadAsync<int>()).SingleOrDefault();
                        var Teacher = (await multi.ReadAsync<TeacherViewModel>()).ToList();

                        return new SearchResult<TeacherViewModel>
                        {
                            TotalRows = totalrow,
                            Data = Teacher
                        };
                    }
                }
            }
            catch (Exception)
            {

                return new SearchResult<TeacherViewModel> { TotalRows = 0, Data = null };
            }
        }

        public async Task<List<TeacherViewModel>> SelectAllAsync()
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                if(conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Code = await conn.QueryAsync<TeacherViewModel>("[dbo].[spTeacher_SelectAll]");
                return Code.ToList() ;

            }
        }
        public async Task<GetInforTeacherViewMode> GetInfoAsync(string id)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return await con.QuerySingleOrDefaultAsync<GetInforTeacherViewMode>("[dbo].[spTeacher_SelectByID]", param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
