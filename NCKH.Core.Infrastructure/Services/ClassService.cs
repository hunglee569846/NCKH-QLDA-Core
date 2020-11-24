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
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ISpecializedRepository _ispecializedRepository;
        private readonly IEducationProgramRepository _iEducationProgramRepository;
        public ClassService(IClassRepository classRepository,
							ISpecializedRepository ispecializedRepository,
							IEducationProgramRepository iEducationProgramRepository)
        {
			_classRepository = classRepository;
			_ispecializedRepository = ispecializedRepository;
			_iEducationProgramRepository = iEducationProgramRepository;

		 }
        public async Task<List<ClassViewModel>> SelecAllAsync()
        {
            return await _classRepository.SelectAll();
        }
        public async Task<ActionResultReponese<string>> InsertAsync(string className, string idClass, ClassMeta clasMeta)
        {
			var idGui = Guid.NewGuid().ToString();
            var isNameExit = await _classRepository.CheckExistsAsync(idClass);
            if (isNameExit)
                return new ActionResultReponese<string>(-2,"da ton tai", "ClassSpecialized");
			var isSpecialized = await _ispecializedRepository.CheckExistByIdSpecialized(clasMeta.IdSpecialized);
			if (!isSpecialized)
				return new ActionResultReponese<string>(-3, "IdSpecialized khong ton tai", "Specialized");
            var isEducation = await _iEducationProgramRepository.CheckExistsAsync(clasMeta.IdEducationProgram);
            if (!isEducation)
                return new ActionResultReponese<string>(-5, "EducationProgram khong ton tai", "EducationProgram");
            var clas = new ClassSpecialized
			{
				id = idGui,
				IdClass = idClass.Trim(),
				ClassName = className.Trim(),
				IdSpecialized = clasMeta.IdSpecialized?.Trim(),
				IdEducationProgram = clasMeta.IdEducationProgram?.Trim(),
				Course = clasMeta.Course?.Trim(),
				Createdate = DateTime.Now,
				LastUpdate = null,
				IsActive = true,
				IsDelete = false
			};
			var result = await _classRepository.InsertAsync(clas);
			if (result >= 0)
				return new ActionResultReponese<string>(result, "thanh cong", "ClassSpecializd", null);
			return new ActionResultReponese<string>(result, "that bai", "ClassSpecializd", null);

		}
		public async Task<ActionResultReponese<string>> UpdateAsync(string id, string idClass,string className, ClassMeta clasMeta)
		{
			var info = await _classRepository.GetInfoAsync(id,idClass);
			if (info == null)
				return new ActionResultReponese<string>(-5,"IdClass khong ton tai","ClassSpecializd");
			var isNameExit = await _classRepository.CheckNameExistsAsync(className);
			if (isNameExit)
				return new ActionResultReponese<string>(-2, "ClasName da ton tai", "ClassSpecialized");
			var isSpecialized = await _ispecializedRepository.CheckExistByIdSpecialized(clasMeta.IdSpecialized);
			if (!isSpecialized)
				return new ActionResultReponese<string>(-3, "IdSpecialized khong ton tai", "Specialized");
			var isEducation = await _iEducationProgramRepository.CheckExistsAsync(clasMeta.IdEducationProgram);
			if (!isEducation)
				return new ActionResultReponese<string>(-5, "EducationProgram khong ton tai", "EducationProgram");
			info.id = id;
			info.IdClass = idClass?.Trim();
			info.ClassName = className?.Trim();
			info.IdSpecialized = clasMeta.IdSpecialized?.Trim();
			info.IdEducationProgram = clasMeta.IdEducationProgram?.Trim();
			info.Course = clasMeta.Course?.Trim();
			info.LastUpdate = DateTime.Now;
			info.LastUpdate = null;
			var result = await _classRepository.UpdateAsync(info);

			if (result <= 0)
				return new ActionResultReponese<string>(result,"UpDateClass false","ClassSpecialized");
			return new ActionResultReponese<string>(result, "UpDateClass thanh cong", "ClassSpecialized");
		}


        public async Task<ActionResultReponese> DeleteAsync(string id,string idclass)
        {
            var checkExist = await _classRepository.CheckExistsAsync(idclass);
            if (!checkExist)
                return new ActionResultReponese(-2,"Class khong ton tai","ClassSpecialized");
			var result = await _classRepository.DeleteAsync(id,idclass);
			if (result <= 0)
				return new ActionResultReponese<string>(result, "DeleteClass that bai", "ClassSpecialized");
			return new ActionResultReponese<string>(result, "DeleteClass thanh cong", "ClassSpecialized");
		}

        public async Task<ClassViewModel> SearchAsync(string id, string idclass)
		{
			return await _classRepository.SelectByIdClass(id, idclass);
		}
	}
}
	

