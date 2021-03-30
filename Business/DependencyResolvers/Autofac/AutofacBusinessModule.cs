using Autofac;
using Business.Abstract;
using Business.Concrete;
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

            builder.RegisterType<TransactionManager>().As<ITransactionService>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        }
    }
}