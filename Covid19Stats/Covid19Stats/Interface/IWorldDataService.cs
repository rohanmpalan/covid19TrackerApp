using Covid19Stats.Model;
using System.Threading.Tasks;

namespace Covid19Stats.Interface
{
    public interface IWorldDataService
    {
      Task<CoronaStats> GetTotalStats();
    }
}
