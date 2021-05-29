using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using Core.Utilities.RoomInvitation;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class StorageManager:IStorageService
    {
        private IStorageDal _storageDal;
        private IRoomService _roomService;
        private ICodeGenerator _codeGenerator;

        public StorageManager(IStorageDal storageDal, IRoomService roomService, ICodeGenerator codeGenerator)
        {
            _storageDal = storageDal;
            _roomService = roomService;
            _codeGenerator = codeGenerator;
        }

        public IDataResult<List<string>> Upload(string path)
        {
            List<string> list = new List<string>();
            var currentRoom = _roomService.GetCurrentRoom();
            var fileName = _codeGenerator.Generate()+".jpg";
            var result = _storageDal.Upload(path, currentRoom.Data.Id,fileName).GetAwaiter().GetResult();
            list.Add(result);
            list.Add(fileName);
            return new SuccessDataResult<List<string>>(list);
        }

        public IResult Delete(string fileName)
        {
            var currentRoom = _roomService.GetCurrentRoom();
            _storageDal.Delete(currentRoom.Data.Id, fileName);
            return new SuccessResult();
        }

        public IDataResult<string> Get(string fileName)
        {
            var currentRoom = _roomService.GetCurrentRoom();
            var result = _storageDal.Get(currentRoom.Data.Id,fileName).GetAwaiter().GetResult();
            return new SuccessDataResult<string>(result);
        }
    }
}