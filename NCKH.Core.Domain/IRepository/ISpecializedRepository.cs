using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface ISpecializedRepository
    {
        Task<List<SpecializedsViewModel>> SelectAll();
        Task<int> InsertAsync(Specialized specialized);
        Task<bool> CheckExistByIdSpecialized(string idSpecailized);
        Task<bool> CheckExistByNameSpecialized(string nameSpecailized);
        Task<bool> CheckExist(string id);
        Task<Specialized> GetInfoAsync(string nameSpecialized);
        Task<int> UpdateAsync(Specialized specialized);
        Task<int> DeleteAsync(string id);

    }
}
