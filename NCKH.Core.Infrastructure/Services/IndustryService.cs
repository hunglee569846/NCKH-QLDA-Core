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

    }
}
