using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinPrism7.Interfaces
{
    public interface IDataStore
    {
        Task<bool> AddItemAsync<T>(T item);
        //Task<bool> UpdateItemAsync<T>(string id, T item);
        //Task<bool> DeleteItemAsync<T>(string id);
        //Task<T> GetItemAsync<T>(string id);
       Task<IEnumerable<T>> GetItemsAsync<T>();
    }
}
