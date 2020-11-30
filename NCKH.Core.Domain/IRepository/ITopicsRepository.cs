using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface ITopicsRepository
    {
        Task<int> ConfirmTopics(string idTopics);
        Task<int> InsertAsync(Topics topic);
        Task<List<TopicsViewModel>> SelectAllAsync();
        Task<bool> CheckExisId(string id);
        Task<bool> CheckExisName(string nameTopics);
    }
}
