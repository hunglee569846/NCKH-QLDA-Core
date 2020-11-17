using Dapper;
using NCKH.Core.Domain.IRepository;
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
    public class ClassRepository : IClassRepository
    {
        private readonly string _connectionString;
        public ClassRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<ClassViewModel>> SelectAll()
        {
            using (SqlConnection conn= new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();
                var Result = await conn.QueryAsync<ClassViewModel>("spClass_SelectAll");
                return Result.ToList();
            }

        }
    }
}
