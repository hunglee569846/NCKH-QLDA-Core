using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.Model;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _iclassRepository;

        public StudentService(IStudentRepository studentRepository,
                              IClassRepository iclassRepository)
        {
            _studentRepository = studentRepository;
            _iclassRepository = iclassRepository;

        }
        public async Task<StudentDetailViewmodel> SelectById(string IdStudent, string NameStudent)
        {
            return await _studentRepository.SelectByIdAsync(IdStudent, NameStudent);

        }
        public async Task<ActionResultReponese<string>> InsertAsync(string idStudent, StudentMeta studentMeta)
        {
            var _idstudent = Guid.NewGuid().ToString();
            var isIdClass = await _iclassRepository.CheckIdAsync(studentMeta.IdClass);
            if (!isIdClass)
                return new ActionResultReponese<string>(-5, "IdClass khong ton tai", "ClassSpecializd");
            var _student = new Students
            {
                id = _idstudent,
                idStudent = idStudent?.Trim(),
                LastName = studentMeta.LastName?.Trim(),
                Name = studentMeta.Name?.Trim(),
                Email = studentMeta.Email?.Trim(),
                IdClass = studentMeta.IdClass?.Trim(),
                PhoneNumber = studentMeta.PhoneNumber?.Trim(),
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,
            };
            var result = await _studentRepository.InsertAsync(_student);
            if (result >= 0)
                return new ActionResultReponese<string>(result, "Insert thanh cong", "Students");
            return new ActionResultReponese<string>(result, "Insert that bai", "Students");

        }
        public async Task<List<StudentViewModel>> SelectAllAsync(string idclass)
        {
            return await _studentRepository.SelectAllAsync(idclass);
        }
    }
}
