using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19Stats.Interface
{
    public interface IRepository<T> where T : class, new()
    {
      Task<T> GetAsync(string url);
      Task<List<T>> GetAllAsync(string url);
    }
}
