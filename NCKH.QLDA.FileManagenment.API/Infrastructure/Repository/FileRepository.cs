using Dapper;
using NCKH.Infrastruture.Binding.Constans;
using NCKH.Infrastruture.Binding.ViewModel;
using NCKH.QLDA.FileManagenment.API.Domain.IRepository;
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
        public async Task<List<FileViewModel>> SelectAllAsync( string FileName, int FolderId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();
                DynamicParameters para = new DynamicParameters();
                para.Add("@FileName", FileName);
                para.Add("@FolderId", FolderId);
                var code = await con.QueryAsync<FileViewModel>("spSearchFile", para, commandType: CommandType.StoredProcedure);
                return code.ToList();
            }
            
        }
    }
}
