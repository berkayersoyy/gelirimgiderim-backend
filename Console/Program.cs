

using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DataAccess.FirebaseStorage;
using Core.Entities.Concrete;
using Core.Utilities.RoomInvitation;
using DataAccess.Concrete.Firebase;
using Microsoft.Extensions.Caching.Memory;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            A _a = new A(new RoomManager(new FbRoomDal(new FbUserDal(new FbOperationClaimDal(),new FbUserOperationClaimDal()),new FbUserRoomDal()),new FbInvitationDal(),new InvitationHelper(new CodeGenerator()),new FbUserRoomDal(),new UserManager(new FbUserDal(new FbOperationClaimDal(),new FbUserOperationClaimDal())),new TransactionManager(new FbTransactionDal(),new UserManager(new FbUserDal(new FbOperationClaimDal(),new FbUserOperationClaimDal()))),new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()))));
            System.Console.WriteLine("CHECK "+_a);
            FirebaseStorageRepositoryBase a = new FirebaseStorageRepositoryBase();
            var b =a.Upload(@"C:\Users\BERKAY\Desktop\1.mp4",_a.Get().Id).GetAwaiter().GetResult();
            System.Console.WriteLine(b);
        }



    }
    public interface IA
    {
        void Set();
        Room Get();
    }
    public class A : IA
    {
        private IRoomService _roomService;

        public A(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public void Set()
        {
            var room = _roomService.Get("Aiu7We3mvX1dzFdStbc0");
            _roomService.SetCurrentRoom(room.Data);
        }

        public Room Get()
        {
            return _roomService.GetCurrentRoom().Data;
        }

    }
}
