using Dapper;
using NCKH.Infrastruture.Binding.ViewModel;
using NCKH.QLDA.FileManagenment.API.Domain.IRepository;
using NCKH.QLDA.FileManagenment.API.Domain.Models;
using NCKH.QLDA.FileManagenment.API.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly string _connectionString;
        public FileRepository(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        //search theo FileId hoac FolderId ten file gan dung
        public async Task<SearchResult<FileViewModel>> SearchAsync(string IdFile, string FileName, int FolderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@IdFile", IdFile);
                    para.Add("@FileName", FileName);
                    para.Add("@FolderId", FolderId);
                    using (var multi = await con.QueryMultipleAsync("spSearchFile", para, commandType: CommandType.StoredProcedure))
                    {
                        return new SearchResult<FileViewModel>
                        {
                            TotalRows = (await multi.ReadAsync<int>()).SingleOrDefault(),
                            Data = (await multi.ReadAsync<FileViewModel>()).ToList()
                        };
                    }

                }
            }
            catch (Exception)
            {
                return new SearchResult<FileViewModel> { TotalRows = 0, Data = null };
            }
        }
        //Search All 
        public async Task<List<FileViewModel>> SelectAllAsync(string FileName, int FolderId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@FileName", FileName);
                para.Add("@FolderId", FolderId);
                var code = await con.QueryAsync<FileViewModel>("SpSelectFileAll", para, commandType: CommandType.StoredProcedure);
                return code.ToList();
            }

        }
        public async Task<int> InsertAsync(Files file)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@Id", file.Id);
                para.Add("@FileCode", file.FileCode);
                para.Add("@FileName", file.FileName);
                para.Add("@Type", file.Type);
                para.Add("@Size", file.Size);
                para.Add("@Url", file.Url);
                para.Add("@FolderId", file.Folderld);
                para.Add("@CreateorId", file.CreatorId);
                if (file.CreateDate != null && file.CreateDate != DateTime.MinValue)
                {
                    para.Add("@CreateDate", file.CreateDate);
                }
                if (file.LastUpdate != null && file.LastUpdate != DateTime.MinValue)
                {
                    para.Add("@LastUpdate", file.LastUpdate);
                }
                if (file.DeleteTime != null && file.DeleteTime != DateTime.MinValue)
                {
                    para.Add("@DeleteTime", file.DeleteTime);
                }
                para.Add("@IsDelete", file.IsDelete);
                para.Add("@IsActive", file.IsActive);
                var code = await con.ExecuteAsync("[dbo].[SpFiles_Insert]", para, commandType: CommandType.StoredProcedure);
                return code;
            }

        }
        public async Task<bool> CheckExistsByFolderIdName(string id, int? folderId, string fileName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    var sql = @"
					SELECT IIF (EXISTS (SELECT 1 FROM Files WHERE Id != @Id AND ISNULL(FolderId,0) = ISNULL(@folderId,0) AND IsDelete = 0 AND FileName = @FileName), 1, 0)";

                    var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id, FolderId = folderId, FileName = fileName });
                    return result;
                }

            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}
