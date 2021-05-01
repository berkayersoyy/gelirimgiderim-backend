using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Entities.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.RoomInvitation;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Firebase;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FbTransactionDal>().As<ITransactionDal>().SingleInstance();
            builder.RegisterType<FbCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<FbUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<FbOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();
            builder.RegisterType<FbUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
            builder.RegisterType<FbRoomDal>().As<IRoomDal>().SingleInstance();
            builder.RegisterType<FbUserRoomDal>().As<IUserRoomDal>().SingleInstance();
            builder.RegisterType<FbInvitationDal>().As<IInvitationDal>().SingleInstance();
            builder.RegisterType<FbClaimDal>().As<IClaimDal>().SingleInstance();
            builder.RegisterType<FbUserClaimDal>().As<IUserClaimDal>().SingleInstance();

            builder.RegisterType<TransactionManager>().As<ITransactionService>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<RoomManager>().As<IRoomService>().SingleInstance();
            builder.RegisterType<ClaimManager>().As<IClaimService>().SingleInstance();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
            builder.RegisterType<CodeGenerator>().As<ICodeGenerator>().SingleInstance();
            builder.RegisterType<InvitationHelper>().As<IInvitationHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                });
        }
    }
}
