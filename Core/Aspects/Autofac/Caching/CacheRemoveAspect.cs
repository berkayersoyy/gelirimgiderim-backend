using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        /// <summary>
        /// Cache remove aspect for removing caches from memory.
        /// </summary>
        /// <param name="pattern"></param>
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        /// <summary>
        /// Removes the cache from memory by pattern.
        /// </summary>
        /// <param name="invocation"></param>
        public override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}