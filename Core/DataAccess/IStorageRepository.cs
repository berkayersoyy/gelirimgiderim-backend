using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IStorageRepository
    {
        Task<List<string>> Upload(string path, string roomId);
        void Delete(string roomId, string fileName);
        Task<string> Get(string roomId, string fileName);

    }
}