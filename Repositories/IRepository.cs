using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetByName(string name);
        Task<IEnumerable<T>> GetByPartialMatch(string partialName);
        Task<T> GetByIdAsync(long id);
    }
}