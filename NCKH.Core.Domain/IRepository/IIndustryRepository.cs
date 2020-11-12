using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IIndustryRepository
    {
        Task<List<IndustryViewModel>> SelectAllAsync();
        Task<int> InsertAsync(Industry industry);
        Task<bool> checkexitNameIndustry(string nameIndustry);
    }
}
