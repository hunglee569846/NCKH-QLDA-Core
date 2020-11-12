using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IDepartmentService
    {
        Task<List<DepartmentViewModel>> SelectAll();
        Task<ActionResultReponese<DepartmentViewModel>> SelectByIdAsync(string idDepartment, string nameDepartment);
        Task<SearchResult<DepartmentViewModel>> SelectByIdFaculty(string idfaculty);
        Task<ActionResultReponese<string>> InsertAsync(string nameDepartment,DepartmentMeta department);
        Task<ActionResultReponese<string>> UpdateAsync(string IdDepartment, DepartmentMeta department);
        Task<ActionResultReponese<string>> DeleteAsync(string IdDepartment, string NameDepartment);
    }
}
