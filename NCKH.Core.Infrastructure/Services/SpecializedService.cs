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
    public class SpecializedService : ISpecializedService
    {
        private readonly ISpecializedRepository _ispecializedRepository;
        private readonly IIndustryRepository _iIndustryRepository;
        public SpecializedService(ISpecializedRepository ispecializedRepository,
                                  IIndustryRepository iIndustryRepository)
        {
            _ispecializedRepository = ispecializedRepository;
            _iIndustryRepository = iIndustryRepository;
        }
        public async Task<List<SpecializedsViewModel>> SelectAllAsync()
        {
            return await _ispecializedRepository.SelectAll();
        }
        public async Task<ActionResultReponese<string>> InsertAsync(string idSpecialized, string nameSpecialized, SpecializedsMeta specializedMeta)
        {
            var isSpecialized = await _ispecializedRepository.CheckExistByIdSpecialized(idSpecialized);
            if (isSpecialized)
                return new ActionResultReponese<string>(-3, "IdSpecialized da ton tai", "Specialized");
            var isNameSpecialized = await _ispecializedRepository.CheckExistByNameSpecialized(nameSpecialized);
            if (isNameSpecialized)
                return new ActionResultReponese<string>(-3, "NameSpecialized da ton tai", "Specialized");
            var isIdIndustry = await _iIndustryRepository.checkexitIdIndustry(specializedMeta.IdIndustry);
            if (!isIdIndustry)
                return new ActionResultReponese<string>(-21, "IdIndustry khong tai", "Industry");

            var _specialized = new Specialized
            {
                Id = Guid.NewGuid().ToString(),
                IdSpecialized = idSpecialized?.Trim(),
                NameSpecialized = nameSpecialized?.Trim(),
                IdIndustry = specializedMeta.IdIndustry?.Trim(),
                Address = specializedMeta.Address?.Trim(),
                Email = specializedMeta.Email?.Trim(),
                PhoneNumber = specializedMeta.PhoneNumber?.Trim(),
                Office = specializedMeta.Office?.Trim(),
                Note = specializedMeta.Note?.Trim(),
                CreateDate = DateTime.Now,
                LastUpdate = null,
                IsActive = true,
                IsDelete = false
            };
            var Result =await _ispecializedRepository.InsertAsync(_specialized);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Insert thanh cong ", "Specialized");
            return new ActionResultReponese<string>(Result, "Insert that bai", "Specialized");
        }
        public async Task<ActionResultReponese<string>> UpdateAsync(string id, string nameSpecialized,string idSpeacialized, SpecializedsMeta specializedsMeta)
        {
            var isExist = await _ispecializedRepository.CheckExist(id);
            if (!isExist)
                return new ActionResultReponese<string>(-3, "Specialized khong ton tai", "Specialized");
            var isNameSpecialized = await _ispecializedRepository.CheckExistByNameSpecialized(nameSpecialized);
            if (!isNameSpecialized)
                return new ActionResultReponese<string>(-4, "NameSpecialized khong ton tai", "Specialized");
            var isSpecialized = await _ispecializedRepository.CheckExistByIdSpecialized(idSpeacialized);
            if (isSpecialized)
                return new ActionResultReponese<string>(-5, "IdSpecialized da ton tai", "Specialized");
            var isIdIndustry = await _iIndustryRepository.checkexitIdIndustry(specializedsMeta.IdIndustry);
            if (!isIdIndustry)
                return new ActionResultReponese<string>(-21, "IdIndustry khong tai", "Industry");
            var _specializedupdate = new Specialized
            {
                Id = id,
                IdSpecialized = idSpeacialized?.Trim(),
                NameSpecialized = nameSpecialized?.Trim(),
                IdIndustry = specializedsMeta.IdIndustry?.Trim(),
                Address = specializedsMeta.Address?.Trim(),
                Email = specializedsMeta.Email?.Trim(),
                PhoneNumber = specializedsMeta.PhoneNumber?.Trim(),
                Office = specializedsMeta.Office?.Trim(),
                Note = specializedsMeta.Note?.Trim(),
                LastUpdate = DateTime.Now,
                IsActive = true,
                IsDelete = false
            };
            var Result = await _ispecializedRepository.UpdateAsync(_specializedupdate);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Update thanh cong ", "Specialized");
            return new ActionResultReponese<string>(Result, "Update that bai", "Specialized");
        }
        public async Task<ActionResultReponese<string>> DeleteAsync(string id,string nameSpeacialized)
        {
            var isCheckNameSpeacialized = await _ispecializedRepository.CheckExistByNameSpecialized(nameSpeacialized);
            if (!isCheckNameSpeacialized)
                return new ActionResultReponese<string>(-4, "NameSpecialized khong ton tai", "Specialized");

            var isCheckSpeacialized = await _ispecializedRepository.CheckExistByIdSpecialized(id);
            if (!isCheckSpeacialized)
                return new ActionResultReponese<string>(-5, "Specialized khong ton tai", "Specialized");

            var getInfo = await _ispecializedRepository.GetInfoAsync(nameSpeacialized);

            var Result = await _ispecializedRepository.DeleteAsync(getInfo.Id);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Delete thanh cong ", "Specialized");
            return new ActionResultReponese<string>(Result, "Delete that bai", "Specialized");
        }
       
    }
}
