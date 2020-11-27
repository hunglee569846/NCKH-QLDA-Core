using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.Model;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Core.Infrastructure.Repository;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class TeacherService : ITeacherService
    {
		private readonly ITeacherRepository _teacheRepository;
		private readonly IDepartmentRepository _departmentRepository;
		public TeacherService(ITeacherRepository teacheRepository,
						      IDepartmentRepository departmentRepository)
		{
			_departmentRepository = departmentRepository;
			_teacheRepository = teacheRepository;
		}

		public async Task<ActionResultReponese<string>> InsertAsync(string idTeacher, string idDepartment, TeacherMeta teacherMeta)
        {
			var teacheId = Guid.NewGuid().ToString();

			var isNameExit = await _teacheRepository.CheckIdTeacherAsync(idTeacher);
			if (isNameExit)
				return new ActionResultReponese<string>(-2,"IdTeacher da ton tai","Teacher");


			var isCheckDepartment = await _departmentRepository.CheckExitsByIdDepartment(idDepartment);
			if (!isCheckDepartment)
				return new ActionResultReponese<string>(-3, "IdDepartment khong ton tai", "Department");

			var teache = new Teachers
			{
				Id = teacheId,
				IdTeacher = idTeacher?.Trim(),
				NameTeacher = teacherMeta.NameTeacher?.Trim(),
				IdDepartment = idDepartment?.Trim(),
				Note = teacherMeta.Note?.Trim(),
				WorkingCompany = teacherMeta.WorkingCompany?.Trim(),
				PhoneNumber = teacherMeta.PhoneNumber?.Trim(),
				Email = teacherMeta.Email?.Trim(),
				CountTopics = teacherMeta.CountTopics,
				CreateDate = DateTime.Now,
				IsActive = true,
				LastUpdate =null,
				IsDelete = false,
				IsTopicsFull = false,
			};

			var result = await _teacheRepository.InsertAsync(teache);

			if (result >= 0)
				return new ActionResultReponese<string>(result,"Insert Thanh cong","Teacher");
			return new ActionResultReponese<string>(result, "Insert that bai", "Teacher");

		}
		public async Task<List<TeacherViewModel>> SelectAllAsync()
        {
			return await _teacheRepository.SelectAllAsync();
        }
		public async Task<SearchResult<TeacherViewModel>> SelectbyIdDepartmentAsync(string idDepartment)
        {
			 return await _teacheRepository.SelectByIdDepartmentAsync(idDepartment);
			
        }
	}
}
