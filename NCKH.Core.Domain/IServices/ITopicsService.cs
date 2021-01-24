using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IServices
{
    public interface ITopicsService
    {
        Task<ActionResultReponese<string>> Confirm(string id);
        Task<List<TopicsViewModel>> SelectAllAsync();
        Task<ActionResultReponese<string>> InsertAsync(string isStudent, string idTeacherMain, TopicsMeta topicsMeta);
    }
}
