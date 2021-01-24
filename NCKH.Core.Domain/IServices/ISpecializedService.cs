using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface ISpecializedService
    {
        Task<List<SpecializedsViewModel>> SelectAllAsync();
        Task<ActionResultReponese<string>> InsertAsync(string idSpecialized, string nameSpecialized, SpecializedsMeta specializedMeta);
        Task<ActionResultReponese<string>> UpdateAsync(string id, string nameSpecialized, string idSpecialized, SpecializedsMeta specializedsMeta);
        Task<ActionResultReponese<string>> DeleteAsync(string idSpecialized, string nameSpecialized);
    }
}
