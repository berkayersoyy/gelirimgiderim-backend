using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        /// <summary>
        /// Cache Aspect for caching.
        /// </summary>
        /// <param name="duration">Cache duration, default is 60 minute.</param>
        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        /// <summary>
        /// Gets the method and check if value exists in memory and returns.
        /// </summary>
        /// <param name="invocation"></param>
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);
        }
    }
}