using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class IndustryService : IIndustryService
    {
        private readonly IIndustryRepository _iindustryRepository;
        private readonly IDepartmentRepository _iDepartmentRepository;
        public IndustryService(IIndustryRepository iindustryRepository,
                               IDepartmentRepository iDepartmentRepository)
        {
            _iindustryRepository = iindustryRepository;
            _iDepartmentRepository = iDepartmentRepository;
        }
        public async Task<List<IndustryViewModel>> SelectAll()
        {
            return await _iindustryRepository.SelectAllAsync();

        }
        public async Task<ActionResultReponese<string>> InsertAsync(string idDepartment,IndustryMeta industryMeta)
        {
            var isdepartment = await _iDepartmentRepository.CheckExitsByIdDepartment(idDepartment);
            if (!isdepartment)
                return new ActionResultReponese<string>(-21, "IdDepartment khong ton tai", "Department");

            var isnameIndustry = await _iindustryRepository.checkexitNameIndustry(industryMeta.NameIndustry);
            if (isnameIndustry)
                return new ActionResultReponese<string>(-31, "NameIndustry da ton tai", "Industry");

            var _industry = new Industry
            {
                IdIndustry = Guid.NewGuid().ToString(),
                NameIndustry = industryMeta.NameIndustry?.Trim(),
                IdDepartment = idDepartment?.Trim(),
                Address = industryMeta.Address?.Trim(),
                Email = industryMeta.Email?.Trim(),
                PhoneNumber = industryMeta.PhoneNumber?.Trim(),
                Details = industryMeta.Details?.Trim(),
                CreateDate= DateTime.Now,
                Deletetime= null,
                LastUpdate= null,
                IsActive = true,
                IsDelete = false,
            };
            var code = await _iindustryRepository.InsertAsync(_industry);
            if (code >= 0)
                return new ActionResultReponese<string>(code, "Insert thanh cong", "Industry");
            return new ActionResultReponese<string>(code, "Insert that bai", "Industry");
        }

        public async Task<IndustryViewModel> SelectById(string nameIndustry)
        {
            var industryInfo = await _iindustryRepository.GetInfoAsync(nameIndustry);
            return await _iindustryRepository.SelectByIdAsync(industryInfo.IdIndustry);
        }

        public async Task<ActionResultReponese<string>> UpdateAsync(string nameIndustry, IndustryMeta industryMeta)
        {
            var isnameIndustry = await _iindustryRepository.checkexitNameIndustry(nameIndustry);
            if (!isnameIndustry)
                return new ActionResultReponese<string>(-31, "NameIndustry khong ton tai", "Industry");
           
            var getInfoIndustry = await _iindustryRepository.GetInfoAsync(nameIndustry);
            var isDepartmenrt = await _iDepartmentRepository.CheckExitsByIdDepartment(industryMeta.IdDepartment);
            if (!isDepartmenrt)
                return new ActionResultReponese<string>(-31, "idDepartment khong ton tai", "Department");

            var _industryUpdate = new Industry
            {
                IdIndustry = getInfoIndustry.IdIndustry?.Trim(),
                NameIndustry = industryMeta.NameIndustry?.Trim(),
                Address = industryMeta.Address?.Trim(),
                Details = industryMeta.Details?.Trim(),
                Email = industryMeta.Email?.Trim(),
                IdDepartment = industryMeta.IdDepartment?.Trim(),
                PhoneNumber = industryMeta.PhoneNumber?.Trim(),
                LastUpdate = DateTime.Now,
                CreateDate = getInfoIndustry.CreateDate,
                Deletetime = null,
                IsActive = true,
                IsDelete = false,
            };
            var Result = await _iindustryRepository.UpdateAsync(_industryUpdate);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Update Nganh thanh cong", "Industry");
            return new ActionResultReponese<string>(Result, "Update Nganh that bai", "Industry");
        }
        public async Task<ActionResultReponese<string>> DeleteAsync(string nameIndustry)
        {
            var isnameIndustry = await _iindustryRepository.checkexitNameIndustry(nameIndustry);
            if (!isnameIndustry)
                return new ActionResultReponese<string>(-31, "NameIndustry khong ton tai", "Industry");
            var getInfoIndustry = await _iindustryRepository.GetInfoAsync(nameIndustry);
            var Result = await _iindustryRepository.DeleteAsync(getInfoIndustry.IdIndustry);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Delete Nganh thanh cong", "Industry");
            return new ActionResultReponese<string>(Result, "Delete Nganh that bai", "Industry");
        }
    }
}
