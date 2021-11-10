using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadAlu.Services
{
    public interface IEducandoDataStore<T>
    {
        Task<T> GetEducandoAsync(string id);
        Task<IEnumerable<T>> GetEducandosAsync(bool forceRefresh = false);
    }
}