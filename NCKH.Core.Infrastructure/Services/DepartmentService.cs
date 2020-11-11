using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Constans;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFacultyRepository _facultyRepository;

        public DepartmentService(IDepartmentRepository departmentRepository,
                                 IFacultyRepository facultyRepository)
        {
            _departmentRepository = departmentRepository;
            _facultyRepository = facultyRepository;

        }
        public async Task<List<DepartmentViewModel>> SelectAll()
        {
            return await _departmentRepository.SelectAllAsync();
        }
        public async Task<SearchResult<DepartmentViewModel>> SelectByIdFaculty(string idfaculty)
        {
            return await _departmentRepository.SelectByIdFacultyAsync(idfaculty);
        }
        public async Task<ActionResultReponese<DepartmentViewModel>> SelectByIdAsync(string idDepartment, string nameDepartment)
        {
            var result = await _departmentRepository.SelectByIdAsync(idDepartment, nameDepartment);
            if (result == null)
                return new ActionResultReponese<DepartmentViewModel>(-31,"Khong tim thay", "Department");
            return new ActionResultReponese<DepartmentViewModel>
            {
                Code = 1,
                Data = result,
            };
        }
        public async Task<ActionResultReponese<string>> InsertAsync(string NameFaculty, DepartmentMeta department)
        {
            var idfaculty =await _facultyRepository.CheckExitsFacult(NameFaculty);
            if (!idfaculty)
                return new ActionResultReponese<string>(-21, "khoa khong ton tai", "Faculty");
            var namedeartment = await _departmentRepository.CheckExitsDepartment(department.NameDepartment);
            if (namedeartment)
                return new ActionResultReponese<string>(-22, "Bo mon da ton tai", "Department");
            var _department = new Department
            {
                IdDepartment = Guid.NewGuid().ToString(),
                NameDepartment = department.NameDepartment?.Trim(),
                Office = department.Office?.Trim(),
                Addres = department.Addres?.Trim(),
                Email = department.Email?.Trim(),
                PhoneNumber = department.PhoneNumber?.Trim(),
                IdFaculty = department.IdFaculty?.Trim(),
                CreateDate = DateTime.Now,
                LastUpdate = null,
                IsActive = true,
                IsDelete = false
            };
            var Result = await _departmentRepository.InsertAsync(_department);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "them thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "them that bai", "Department", null);

        }
        public async Task<ActionResultReponese<string>> UpdateAsync(string IdDepartment, DepartmentMeta department)
        {
            var idfaculty = await _facultyRepository.CheckExitsFacult(department.IdFaculty);
            if (!idfaculty)
                return new ActionResultReponese<string>(-21, "khoa khong ton tai", "Faculty");
            var _department = new Department
            {
                IdDepartment = IdDepartment?.Trim(),
                NameDepartment = department.NameDepartment?.Trim(),
                Office = department.Office?.Trim(),
                Addres = department.Addres?.Trim(),
                Email = department.Email?.Trim(),
                PhoneNumber = department.PhoneNumber?.Trim(),
                IdFaculty = department.IdFaculty?.Trim(),
                LastUpdate = DateTime.Now,
            };
            var Result = await _departmentRepository.UpdateAsync(_department);
            if (Result > 0)
                return new ActionResultReponese<string>(Result, "Update thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "Update that bai", "Department", null);
        }
    }
}
