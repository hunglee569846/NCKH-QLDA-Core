using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface ITeacherService
    {
        Task<ActionResultReponese<string>> InsertAsync(string idTeacher, string idDepartment,TeacherMeta teacherMeta);
        Task<SearchResult<TeacherViewModel>> SelectbyIdDepartmentAsync(string idDepartment);
        Task<List<TeacherViewModel>> SelectAllAsync();
    }
}
