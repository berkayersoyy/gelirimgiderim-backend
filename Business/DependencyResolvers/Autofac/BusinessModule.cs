using Business.Abstract;
using Business.Concrete;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Core.Utilities.RoomInvitation;
using DataAccess.Abstract;
using DataAccess.Concrete.Firebase;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers.Autofac
{
    public class BusinessModule:ICoreModule
    {
        public void Load(IServiceCollection collection)
        {
            collection.AddSingleton<IRoomService, RoomManager>();
            collection.AddSingleton<IClaimService, ClaimManager>();
            collection.AddSingleton<IClaimDal, FbClaimDal>();
            collection.AddSingleton<IUserClaimDal, FbUserClaimDal>();
            collection.AddSingleton<IUserService, UserManager>();
            collection.AddSingleton<IUserOperationClaimDal, FbUserOperationClaimDal>();
            collection.AddSingleton<IOperationClaimDal, FbOperationClaimDal>();
            collection.AddSingleton<ISharedClaimDal, FbSharedClaimDal>();
            collection.AddSingleton<IRoomDal, FbRoomDal>();
            collection.AddSingleton<IUserDal, FbUserDal>();
            collection.AddSingleton<IUserRoomDal, FbUserRoomDal>();
            collection.AddSingleton<IInvitationDal, FbInvitationDal>();
            collection.AddSingleton<IInvitationHelper, InvitationHelper>();
            collection.AddMemoryCache();
            collection.AddSingleton<ICacheManager, MemoryCacheManager>();
            collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            collection.AddSingleton<ICodeGenerator, CodeGenerator>();

        }
    }
}