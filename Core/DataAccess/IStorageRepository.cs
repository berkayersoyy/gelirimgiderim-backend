using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IStorageRepository
    {
        Task<string> Upload(string path, string roomId,string fileName);
        void Delete(string roomId, string fileName);
        Task<string> Get(string roomId, string fileName);

    }
}