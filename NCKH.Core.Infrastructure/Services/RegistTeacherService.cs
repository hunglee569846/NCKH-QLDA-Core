using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class RegistTeacherService : IRegistTeacherService
    {
        private readonly IRegistTeacherRepository _iregistTeacher;
        private readonly IStudentRepository _iStudemtRepository;
        private readonly ITeacherRepository _iTeacherRepository;
        private readonly ITopicsRepository _iToppicRepository;
        public RegistTeacherService(IRegistTeacherRepository registTeacher,
                                    IStudentRepository studentRepository,
                                    ITeacherRepository teacherRepository,
                                    ITopicsRepository topicsRepository)
        {
            _iregistTeacher = registTeacher;
            _iStudemtRepository = studentRepository;
            _iTeacherRepository = teacherRepository;
            _iToppicRepository = topicsRepository;
        }

       public async Task<List<ViewRegistTeacher>> SelectAll()
        {
            return await _iregistTeacher.SelectAllAsync();
        }
        

        public async Task<ActionResultReponese<string>> InsertAsync(string IdStudent, string IdTeacherMain, string IdTeacher2, string IdTopic)
        {
            
            var isIdStudent = await _iStudemtRepository.CheckExistsAsync(IdStudent);
            if (!isIdStudent)
                return new ActionResultReponese<string>(-21, "IdStudent Khong ton tai", "Student");
           
            var isIteacherMain = await _iTeacherRepository.CheckExistsAsync(IdTeacherMain);
            if(!isIteacherMain)
                return new ActionResultReponese<string>(-21, "IdTeacherMain Khong ton tai", "Teacher");

            if (IdTeacher2 != null)
            {
                var isIteacher2 = await _iTeacherRepository.CheckExistsAsync(IdTeacher2);
                if (!isIteacher2)
                    return new ActionResultReponese<string>(-21, "IdTeacher2 Khong ton tai", "Teacher");
            }

            var istopic = await _iToppicRepository.CheckExisId(IdTopic);
            if (!istopic)
                return new ActionResultReponese<string>(-21, "IdTopic Khong ton tai", "Topics");
            var _ReistTeacher = new RegistTeacher
            {
                id = Guid.NewGuid().ToString(),
                IdStudent = IdStudent?.Trim(),
                IdTeacherMain = IdTeacherMain?.Trim(),
                IdTeacher2 = IdTeacher2?.Trim(),
                IdTopic = IdTopic?.Trim(),
                CreateDate = DateTime.Now,
                LastUpdate = null,
                IsActive = true,
                IsDone = false,
            };
            var code= await _iregistTeacher.InsertAsync(_ReistTeacher);
            if (code > 0)
                return new ActionResultReponese<string>(code, "Insert thanh cong", "RegistTeacher");
            return new ActionResultReponese<string>(code, "Insert that bai", "RegistTeacher");
        }
    }
}
