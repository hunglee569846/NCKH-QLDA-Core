using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyService(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;

        }
        public async Task<ActionResultReponese<string>> InsertAsync(string idFaculty, FacultyMeta facultyMeTa)
        {
            //var isFaculty = await _facultyRepository.CheckExitsFacult(IdFaculty);
            //if (!isFaculty)
            //    return new ActionResultReponese<string>(-21, "IdFaculty not found", "Faculty", null);
            var _faculty = new Faculty
            {
                IdFaculty = idFaculty?.Trim(),
                NameFaculty = facultyMeTa?.NameFaculty.Trim(),
                IsActive = true,
                IsDelete = false
            };
            var Result = await _facultyRepository.InsertAsync(_faculty);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "thanh cong", "Faculty", null);
            return new ActionResultReponese<string>(Result, "that bai", "Faculty", null);
        }
        public async Task<List<FacultyViewModel>> SelectAll()
        {
            return await _facultyRepository.SelectAllAsync();
        }
        public async Task<FacultyViewModel> SelectById(string IdFaculty)
        {
            return await _facultyRepository.SelectByIdAsync(IdFaculty);

        }
        public async Task<ActionResultReponese<string>> UpdateAsync(string idFaculty, string NameFaculty)
        {
            var code = await _facultyRepository.UpdateAsync(idFaculty, NameFaculty);
            if (code >= 0)
                return new ActionResultReponese<string>(code, "Update thanh cong", "Faculty", null);
            return new ActionResultReponese<string>(code, "Update that bai", "Faculty", null);
        }
        public async Task<ActionResultReponese<string>> DeleteAsync(string IdFaculty)
        {
            var code = await _facultyRepository.DeleteAsync(IdFaculty);
            if (code >= 0)
                return new ActionResultReponese<string>(code, "Delete thanh cong", "Faculty", null);
            return new ActionResultReponese<string>(code, "Delete that bai", "Faculty", null);
        }
    }
}
