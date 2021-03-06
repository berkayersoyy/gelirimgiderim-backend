using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Core.DependencyResolvers
{
    public class CoreModule:ICoreModule
    {
        /// <summary>
        /// Core IOC Container with AutoFac.
        /// </summary>
        /// <param name="services"></param>
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        }
    }
}
