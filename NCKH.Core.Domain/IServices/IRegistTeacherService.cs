using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface IRegistTeacherService
    {
        Task<List<ViewRegistTeacher>> SelectAll();
        Task<ActionResultReponese<string>> InsertAsync(string IdStudent, string IdTeacherMain, string IdTeacher2, string IdTopic);
    }
}
