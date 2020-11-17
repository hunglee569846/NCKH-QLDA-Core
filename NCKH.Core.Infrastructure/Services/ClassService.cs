using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _class;
        public ClassService(IClassRepository Class_)
        {
            _class = Class_;
         }
        public async Task<List<ClassViewModel>> SelecAllAsync()
        {
            return await _class.SelectAll();
        }
    }
}
