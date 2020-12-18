using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IRegistTeacherRepository
    {
        Task<List<ViewRegistTeacher>> SelectAllAsync();
        Task<int> InsertAsync(RegistTeacher registtecher);
    }
}
