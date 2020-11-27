using NCKH.Core.Domain.Model;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IStudentRepository
    {
        Task<int> InsertAsync(Students student);
        Task<List<StudentViewModel>> SelectAllAsync(string idClass);
        Task<StudentDetailViewmodel> SelectByIdStudentAsync(string IdStudent);
        Task<int> UpdateAsync(Students studen);
        Task<int> DeleteAsync(string id);
        Task<bool> CheckIstudentAsync(string idstudent);
        Task<Students> GetInfoAsync(string id);
        Task<bool> CheckExistsAsync(string id);

    }
}
