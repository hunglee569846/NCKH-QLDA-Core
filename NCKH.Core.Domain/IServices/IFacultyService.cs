using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IFacultyService
    {
        Task<List<FacultyViewModel>> SelectAll();
        Task<FacultyViewModel> SelectById(string IdFaculty);
        Task<ActionResultReponese<string>> InsertAsync(string idFaculty, FacultyMeta facultyMata);
        Task<ActionResultReponese<string>> UpdateAsync(string idFaculty, string NameFaculty);
        Task<ActionResultReponese<string>> DeleteAsync(string IdFaculty);
    }
}
