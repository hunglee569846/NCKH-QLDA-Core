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
    public class TopicsService : ITopicsService
	{
		private readonly ITopicsRepository _topicsRepository;
		private readonly ITeacherRepository _teacherRepository;
		private readonly IStudentRepository _studentRepository;
		public TopicsService(ITopicsRepository itopicsRepository,
							  ITeacherRepository iteacherRepository,
							  IStudentRepository studentRepository)
		{
			_topicsRepository = itopicsRepository;
			_teacherRepository = iteacherRepository;
			_studentRepository = studentRepository;
		}
		public async Task<ActionResultReponese<string>> InsertAsync(string idStudent, string idTeacherMain, TopicsMeta topicsMeta)
        {
			var topicId = Guid.NewGuid().ToString();

            var isTeacher = await _teacherRepository.CheckExistsAsync(idTeacherMain);
            if (!isTeacher)
                return new ActionResultReponese<string>(-2, "TeacherMain khong ton tai", "Teacher");

			// check giang vien full de tai chua
			var isTopicsFullExit = await _teacherRepository.CheckCountTopicsAsync(idTeacherMain);
			if (!isTopicsFullExit)
				return new ActionResultReponese<string>(-2, "IdTeacher full de tai", "Teacher");

			var isStudent = await _studentRepository.CheckExistsAsync(idStudent);
			if (!isStudent)
				return new ActionResultReponese<string>(-2, "Student khong ton tai", "Student");

			var topic = new Topics
			{
				id = topicId,
				IdTopics = topicsMeta.IdTopics?.Trim(),
				NameTopics = topicsMeta.NameTopics?.Trim(),
				IdStudent = idStudent?.Trim(),
				IdTeacherMain = idTeacherMain?.Trim(),
				IdTeacher2 = topicsMeta.IdTeacher2?.Trim(),
				IsApproval = false,
				IsActive = true,
				IsDelete = false,
				CreateDate = DateTime.Now,
				LastUpdate = null
			};

			var result = await _topicsRepository.InsertAsync(topic);

			if (result <= 0)
				return new ActionResultReponese<string>(result,"Insert that bai","Topics");

			return new ActionResultReponese<string>(result,"Insert thanh cong","Topics") ;
		}
		public async Task<ActionResultReponese<string>> Confirm(string id)
        {
			var result= await _topicsRepository.ConfirmTopics(id);
			if (result >= 0)
				return new ActionResultReponese<string>(result, "duyet de tai thanh cong", "Comfirm Topics");
			return new ActionResultReponese<string>(result, "duyet de tai that bai", "Comfirm Topics");
        }
		public async Task<List<TopicsViewModel>> SelectAllAsync()
        {
			return await _topicsRepository.SelectAllAsync();
        }
	}
}
