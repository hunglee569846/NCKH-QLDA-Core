using NCKH.Core.Domain.Model;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface ITeacherRepository
    {
        Task<bool> CheckExistsAsync(string id);
        //Task<bool> CheckExistsByTenantIdAsync(string tenantId, string id);
        Task<bool> CheckIdTeacherAsync(string idteacher);
        Task<int> InsertAsync(Teachers teacher);
        Task<SearchResult<TeacherViewModel>> SelectByIdDepartmentAsync(string idDepartment);
        Task<List<TeacherViewModel>> SelectAllAsync();
        Task<bool> CheckCountTopicsAsync(string id);

    }
}
