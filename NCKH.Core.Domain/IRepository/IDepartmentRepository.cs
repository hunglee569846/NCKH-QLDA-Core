using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentViewModel>> SelectAllAsync();
        Task<DepartmentViewModel> SelectByIdAsync(string IdDepartment, string NameDepartment);
        Task<int> InsertAsync(Department department);
        Task<int> UpdateAsync(string IdDepartment, Department department);
        Task<int> DeleteAsync(string IdDepartment, string NameDepartment);

        Task<bool> CheckExitsFacult(string IdFacult);

    }
}
