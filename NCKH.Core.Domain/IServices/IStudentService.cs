using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IStudentService
    {
        Task<StudentDetailViewmodel> SelectById(string IdStudent);
    }
}
