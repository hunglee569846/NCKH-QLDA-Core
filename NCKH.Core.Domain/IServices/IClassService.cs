using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IClassService
    {
        Task<List<ClassViewModel>> SelecAllAsync();
        Task<ActionResultReponese<string>> InsertAsync(string className, string idClass,ClassMeta _clasMeta);
        Task<ClassViewModel> SearchAsync(string id, string idclass);
        Task<ActionResultReponese<string>> UpdateAsync(string id, string idClass,string clasName, ClassMeta clasMeta);
        Task<ActionResultReponese> DeleteAsync(string id, string idclass);

    }
}
