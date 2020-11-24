using Dapper;
using NCKH.Core.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Repository
{
    public class EducationProgramRepository : IEducationProgramRepository
    {
		private readonly string _connectionString;

		public EducationProgramRepository(string connectionString)
		{
			_connectionString = connectionString;
			
		}

		public async Task<bool> CheckExistsAsync(string idEducationProgram)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				if (con.State == ConnectionState.Closed)
					await con.OpenAsync();

				var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM EducationProgram WHERE IdEducationProgram = @IdEducationProgram AND IsDelete = 0), 1, 0)";

				var result = await con.ExecuteScalarAsync<bool>(sql, new { IdEducationProgram = idEducationProgram });
				return result;
			}

		}
	}
}
