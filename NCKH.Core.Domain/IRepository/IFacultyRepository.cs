using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IFacultyRepository
    {
        Task<List<FacultyViewModel>> SelectAllAsync();
        Task<FacultyViewModel> SelectByIdAsync(string IdFaculty);
        Task<int> InsertAsync(Faculty faculty);
        Task<int> UpdateAsync(string IdFaculty,string NameFaculty);
        Task<int> DeleteAsync(string IdFaculty);
        Task<bool> CheckExitsFacult(string IdFacult);
    }
}
