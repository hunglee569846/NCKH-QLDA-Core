using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IStudentService
    {
        Task<StudentDetailViewmodel> SelectByIdStudent(string IdStudent);
        Task<ActionResultReponese<string>> InsertAsync(string idStudent, StudentMeta studentMeta);
        Task<List<StudentViewModel>> SelectAllAsync(string idclass);
        Task<ActionResultReponese<string>> UpdateAsync(string id, string idstudent, StudentMeta studenMeta);
        Task<ActionResultReponese> DeleteAsync(string id, string idStudent);


    }
}
