using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IBoMonService
    {
        Task<List<BoMonViewModel>> SelectAll();
        Task<BoMonViewModel> SelectById(string MaBM, string TenBM);
        Task<ActionResultReponese<string>> InsertAsync(BoMonMeta bomonMeta);
        Task<ActionResultReponese<string>> UpdateAsync(string MaBoMon, BoMonMeta bomonMeta);
        Task<ActionResultReponese<string>> DeleteAsync(string IdBomon, string TenBM);
    }
}
