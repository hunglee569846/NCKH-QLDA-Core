using Dapper;
using NCKH.QLDA.FileManagenment.API.Domain.IRepository;
using NCKH.QLDA.FileManagenment.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.Repository
{
    public class FoderRepository : IFolderRepository
    {
        private readonly string _ConnectionString;
        public FoderRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        public async Task<int> InsertAsync(string IdPath,string FolderName, string FolderId, Folder folder)
        {
            int rowaffaceted = 0;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@FolderName", FolderName);
                para.Add("@IdPath", IdPath);
                para.Add("@NamePath", folder.NamePath);
                para.Add("@FolderId", FolderId);
                para.Add("@Level", folder.Level);
                para.Add("@ChildCount", folder.ChildCount);
                para.Add("@Description", folder.Description);
                if (folder.CreateTime != null && folder.CreateTime != DateTime.MinValue)
                {

                    para.Add("@createTime", folder.CreateTime);
                }
                para.Add("@lastUpdate", folder.LastUpdate);
                if (folder.DeleteTime != null && folder.DeleteTime != DateTime.MinValue)
                {

                    para.Add("@DeleteTime", folder.DeleteTime);
                }
                para.Add("IsDelete", folder.IsDelete);
                para.Add("@IsActive", folder.IsActive);
                rowaffaceted = await con.ExecuteAsync("[spInsertFolder]", para, commandType: CommandType.StoredProcedure);
            }
            return rowaffaceted;
        }
        public async Task<bool> CheckExitsFolder(string folderId)
        {

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"SELECT IIF (EXISTS (SELECT 1 FROM dbo.Folders WHERE FolderId =@folderId  AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { FolderId = folderId });
                return result;
            }
        }
    }
}
