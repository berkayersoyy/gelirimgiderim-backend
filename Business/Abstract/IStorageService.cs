using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IStorageService
    {
        IDataResult<List<string>> Upload(string path);
        IResult Delete(string fileName);
        IDataResult<string> Get(string fileName);

    }
}