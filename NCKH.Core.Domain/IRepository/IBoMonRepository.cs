using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IBoMonRepository
    {
        Task<List<BoMonViewModel>> SelectAllAsync();
        Task<BoMonViewModel> SelectByIdAsync(string MaBM, string TenBM);
        Task<int> InsertAsync(BoMon bomon);
        Task<int> UpdateAsync(string MaBoMon, BoMon bomon);
        Task<int> DeleteAsync(string IdBomon, string TenBM);
        Task<bool> CheckExitsFacult(string IdFacult);

    }
}
