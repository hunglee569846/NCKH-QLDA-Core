using System.Threading.Tasks;

namespace NCKH.Core.Domain.IRepository
{
    public interface IEducationProgramRepository
    {
        Task<bool> CheckExistsAsync(string idEducationProgram);

    }
}
