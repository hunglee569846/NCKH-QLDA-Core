using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IDepartmentService
    {
        Task<List<DepartmentViewModel>> SelectAll();
        Task<DepartmentViewModel> SelectById(string IdDepartment, string NameDepartment);
        Task<ActionResultReponese<string>> InsertAsync(string idFaculty,DepartmentMeta department);
        Task<ActionResultReponese<string>> UpdateAsync(string IdDepartment, DepartmentMeta department);
        Task<ActionResultReponese<string>> DeleteAsync(string IdDepartment, string NameDepartment);
    }
}
