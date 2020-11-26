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
        public async Task<StudentDetailViewmodel> SelectByIdStudent(string IdStudent, string NameStudent)
        {
            return await _studentRepository.SelectByIdStudentAsync(IdStudent, NameStudent);

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

        public async Task<ActionResultReponese<string>> UpdateAsync(string id,string idstudent, StudentMeta studenMeta)
        {
            var isNameExit = await _studentRepository.CheckExistsAsync(id);
            if (!isNameExit)
                return new ActionResultReponese<string>(-4, "Student khong ton tai", "Student");
            var isIdclass = await _iclassRepository.CheckIdAsync(studenMeta.IdClass);
            if(!isIdclass)
                return new ActionResultReponese<string>(-3, "Class khong ton tai", "ClassPecialized");

            var info = await _studentRepository.GetInfoAsync(id);
            if (info == null)
                return new ActionResultReponese<string>(-1, "Student khong tim thay", "Student");

            info.id = id.ToString();
            info.idStudent = idstudent?.Trim();
            info.LastName = studenMeta.LastName?.Trim();
            info.Name = studenMeta.Name?.Trim();
            info.Email = studenMeta.Email?.Trim();
            info.IdClass = studenMeta.IdClass?.Trim();
            info.PhoneNumber = studenMeta.PhoneNumber?.Trim();
       
            var result = await _studentRepository.UpdateAsync(info);
            if (result >= 0)
                return new ActionResultReponese<string>(result, "Update Student thanh cong", "Student");
            return new ActionResultReponese<string>(result, "Update Student khong thanh cong", "strudent");
        }

        public async Task<List<StudentViewModel>> SelectAllAsync(string idclass)
        {
            return await _studentRepository.SelectAllAsync(idclass);
        }
    }
}
