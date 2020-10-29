using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        
        public DepartmentService(IDepartmentRepository boMonRepository )
        {
            _departmentRepository = boMonRepository;
            
        }
        public async Task<List<DepartmentViewModel>> SelectAll()
        {
            return await _departmentRepository.SelectAllAsync();
        }
        public async Task<DepartmentViewModel> SelectById(string IdDepartment, string NameDepartment)
        {
            return await _departmentRepository.SelectByIdAsync(IdDepartment, NameDepartment);
        }
        public async Task<ActionResultReponese<string>> InsertAsync(DepartmentMeta bomonMeta)
        {
            var _bomon = new Department
            {
                //  id = Guid.NewGuid().ToString(),
                IdDepartment = bomonMeta.IdDepartment?.Trim(),
                NameDepartment = bomonMeta.NameDepartment?.Trim(),
                IdFaculty = bomonMeta.IdFaculty?.Trim(),
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false
            };
            var Result = await _departmentRepository.InsertAsync(_bomon);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "that bai", "Department", null);
        }
        public async Task<ActionResultReponese<string>> UpdateAsync(string IdDepartment, DepartmentMeta department)
        {
            var _bomon = new Department
            {
                //  id = Guid.NewGuid().ToString(),
                NameDepartment = department.NameDepartment?.Trim(),
                IdFaculty = department.IdFaculty?.Trim(),
                IsActive = true,
                IsDelete = false,
                LastUpdate = DateTime.Now,
            };
            var Result = await _departmentRepository.UpdateAsync(IdDepartment, _bomon);
            if (Result > 0)
                return new ActionResultReponese<string>(Result, "Update thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "Update that bai", "Department", null);
        }
        public async Task<ActionResultReponese<string>> DeleteAsync(string IdDepartment, string NameDepartment)
        {
            var Result = await _departmentRepository.DeleteAsync(IdDepartment, NameDepartment);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Delete thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "Delete that bai", "Department", null);
        }
    }
}
