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
    public class TopicsRepository: ITopicsRepository
    {
        private readonly string _ConnectionString;
        public TopicsRepository(string connectionString)
        {
            _ConnectionString = connectionString;
        }

		public async Task<int> InsertAsync(Topics topic)
		{
			int rowAffected = 0;
			using (SqlConnection con = new SqlConnection(_ConnectionString))
			{
				if (con.State == ConnectionState.Closed)
					await con.OpenAsync();

				DynamicParameters param = new DynamicParameters();
				param.Add("@Id", topic.id);
				param.Add("@IdTopics", topic.IdTopics);
				param.Add("@NameTopics", topic.NameTopics);
				param.Add("@IdStudent", topic.IdStudent);
				param.Add("@NameStudent", topic.NameStudent);
				param.Add("@IdTeacherMain", topic.IdTeacherMain);
				param.Add("@NameTeacherMain", topic.NameTeacherMain);
				param.Add("@IdTeacher2", topic.IdTeacher2);
				param.Add("@NameTeacher2", topic.NameTeacher2);
				param.Add("@IsApproval", topic.IsApproval);
				param.Add("@CreateDate", topic.CreateDate);
				param.Add("@LastUpdate", topic.LastUpdate);
				param.Add("@IsDelete", topic.IsDelete);
				param.Add("@IsActive", topic.IsActive);
				rowAffected = await con.ExecuteAsync("[dbo].[spTopic_Insert]", param, commandType: CommandType.StoredProcedure);
			}
			return rowAffected;
		}

		public async Task<int> ConfirmTopics(string idTopics)
        {
			int rowAffected = 0;
			using (SqlConnection con = new SqlConnection(_ConnectionString))
			{
				if (con.State == ConnectionState.Closed)
					await con.OpenAsync();

				DynamicParameters param = new DynamicParameters();
				param.Add("@idTopics", idTopics);
				rowAffected = await con.ExecuteAsync("[dbo].[spTopics_ConfirmTopics]", param, commandType: CommandType.StoredProcedure);
			}
			return rowAffected;
		}
		public async Task<List<TopicsViewModel>> SelectAllAsync()
		{
			using (SqlConnection con = new SqlConnection(_ConnectionString))
			{
				if (con.State == ConnectionState.Closed)
					await con.OpenAsync();

				DynamicParameters param = new DynamicParameters();
				var results = await con.QueryAsync<TopicsViewModel>("[dbo].[spTopic_SelectAll]");
				return results.ToList();
			}

		}
		public async Task<bool> CheckExisIdTopic(string id)
		{
			using (SqlConnection conn = new SqlConnection(_ConnectionString))
			{
				if (conn.State == ConnectionState.Closed)
					await conn.OpenAsync();
				var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM dbo.Topics WHERE IdTopics = @id AND IsDelete = 0 AND IsDelete =0), 1, 0)";

				var result = await conn.ExecuteScalarAsync<bool>(sql, new { Id = id });
				return result;
			}
		}
		public async Task<bool> CheckExisId(string id)
		{
			using (SqlConnection conn = new SqlConnection(_ConnectionString))
			{
				if (conn.State == ConnectionState.Closed)
					await conn.OpenAsync();
				var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM dbo.Topics WHERE Id = @id AND IsDelete = 0 AND IsDelete =0), 1, 0)";

				var result = await conn.ExecuteScalarAsync<bool>(sql, new { Id = id });
				return result;
			}
		}

		public async Task<bool> CheckExisName(string nameTopics)
        {
			using (SqlConnection conn = new SqlConnection(_ConnectionString))
			{
				if (conn.State == ConnectionState.Closed)
					await conn.OpenAsync();
				var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM dbo.Topics WHERE NameTopics = @nameTopics AND IsDelete = 0 AND IsDelete =0), 1, 0)";

				var result = await conn.ExecuteScalarAsync<bool>(sql, new { NameTopics = nameTopics });
				return result;
			}
		}

	}
}
