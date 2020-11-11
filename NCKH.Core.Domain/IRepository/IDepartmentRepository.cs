using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentViewModel>> SelectAllAsync();
        Task<DepartmentViewModel> SelectByIdAsync(string idDepartment, string NameDepartment);
        Task<SearchResult<DepartmentViewModel>> SelectByIdFacultyAsync(string IdFaculty);
        Task<int> InsertAsync(Department department);
        Task<int> UpdateAsync(Department department);
        //Task<int> DeleteAsync(string IdDepartment, string NameDepartment);
        Task<bool> CheckExitsDepartment(string NameDepartment);
    }
}
