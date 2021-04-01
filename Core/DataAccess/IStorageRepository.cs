using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IStorageRepository<T>
    {
        void Upload(string path, string collection,string email, string password);
        void Delete(string path, string collection, string email, string password);
        Task<string> Get(string path, string collection,string email, string password);
        Task<List<string>> GetAll(string collection, string email, string password);

    }
}