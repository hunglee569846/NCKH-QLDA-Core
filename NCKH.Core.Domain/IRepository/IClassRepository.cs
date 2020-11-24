using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IClassRepository
    {
        Task<List<ClassViewModel>> SelectAll();
        Task<int> InsertAsync(ClassSpecialized clas);
        Task<int> UpdateAsync(ClassSpecialized clas);
        Task<int> DeleteAsync(string id, string Iidclas);
        Task<bool> CheckExistsAsync(string idClass);
        Task<bool> CheckIdAsync(string id);
        Task<bool> CheckNameExistsAsync(string className);
        Task<ClassSpecialized> GetInfoAsync(string idClas, string id);
        Task<ClassViewModel> SelectByIdClass(string id, string idclas);
    }
}
