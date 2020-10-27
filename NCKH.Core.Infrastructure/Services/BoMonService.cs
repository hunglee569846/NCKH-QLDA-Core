using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.Resources;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Constans;
using NCKH.Infrastruture.Binding.IServices;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class BoMonService : IBoMonService
    {
        private readonly IBoMonRepository _boMonRepository;
        
        public BoMonService(IBoMonRepository boMonRepository )
        {
            _boMonRepository = boMonRepository;
            
        }
        public async Task<List<BoMonViewModel>> SelectAll()
        {
            return await _boMonRepository.SelectAllAsync();
        }
        public async Task<BoMonViewModel> SelectById(string MaBM, string TenBM)
        {
            return await _boMonRepository.SelectByIdAsync(MaBM, TenBM);
        }
        public async Task<ActionResultReponese<string>> InsertAsync(BoMonMeta bomonMeta)
        {
            var _bomon = new BoMon
            {
                //  id = Guid.NewGuid().ToString(),
                MaBoMon = bomonMeta.MaBoMon?.Trim(),
                TenBoMon = bomonMeta.TenBoMon?.Trim(),
                IdFaculty = bomonMeta.IdFaculty?.Trim(),
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false
            };
            var Result = await _boMonRepository.InsertAsync(_bomon);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "thanh cong", "BoMon", null);
            return new ActionResultReponese<string>(Result, "that bai", "BoMon", null);
        }
        public async Task<ActionResultReponese<string>> UpdateAsync(string MaBoMon, BoMonMeta bomonMeta)
        {
            var _bomon = new BoMon
            {
                //  id = Guid.NewGuid().ToString(),
                TenBoMon = bomonMeta.TenBoMon?.Trim(),
                IdFaculty = bomonMeta.IdFaculty?.Trim(),
                IsActive = true,
                IsDelete = false,
                LastUpdate = DateTime.Now,
            };
            var Result = await _boMonRepository.UpdateAsync(MaBoMon, _bomon);
            if (Result > 0)
                return new ActionResultReponese<string>(Result, "Update thanh cong", "BoMon", null);
            return new ActionResultReponese<string>(Result, "Update that bai", "BoMon", null);
        }
        public async Task<ActionResultReponese<string>> DeleteAsync(string IdBomon, string TenBM)
        {
            var Result = await _boMonRepository.DeleteAsync(IdBomon, TenBM);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "Delete thanh cong", "BoMon", null);
            return new ActionResultReponese<string>(Result, "Delete that bai", "BoMon", null);
        }
    }
}
