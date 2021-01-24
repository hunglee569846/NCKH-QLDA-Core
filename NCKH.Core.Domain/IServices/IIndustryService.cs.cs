using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IIndustryService
    {
        Task<List<IndustryViewModel>> SelectAll();
        Task<ActionResultReponese<string>> InsertAsync(string idDepartment, IndustryMeta industryMeta);
        Task<IndustryViewModel> SelectById(string idIndustry);
        Task<ActionResultReponese<string>> DeleteAsync(string nameIndustry);
        Task<ActionResultReponese<string>> UpdateAsync(string nameIndustry, IndustryMeta industryMeta);
    }
}
