using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IStorageRepository<T>
    {
        Task<string> Upload(string path, string collection);
        void Delete(string collection, string fileName);
        Task<string> Get(string collection, string fileName);

    }
}