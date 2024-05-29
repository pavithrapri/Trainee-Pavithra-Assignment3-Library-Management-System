using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public interface ICosmosDbService<T>
    {
        Task AddItemAsync(T item, string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(string queryString);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
    }
}
