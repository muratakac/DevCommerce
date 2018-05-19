using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace DevCommerce.Core.CrossCuttingConcerns.Cache
{

    [PSerializable]
    public class CacheProvider : OnMethodBoundaryAspect
    {
        private ICacheProvider _cacheProvider;
        public Type ProviderType { get; set; }
        public int Duration { get; set; }

        public override void RuntimeInitialize(MethodBase method)
        {
            _cacheProvider = (ICacheProvider)Activator.CreateInstance(ProviderType, "localhost", 6379);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var key = DeriveCacheKey(args);
            if (!_cacheProvider.IsInCache(key))
                return;

            args.ReturnValue = JsonConvert.DeserializeObject(_cacheProvider.Get(key), ((MethodInfo)(args.Method)).ReturnType);
            args.FlowBehavior = FlowBehavior.Return;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (args.Method.IsConstructor)
                return;

            _cacheProvider.Set(DeriveCacheKey(args), JsonConvert.SerializeObject(args.ReturnValue), TimeSpan.FromMinutes(Duration));
        }

        #region Private Methods
        private static string DeriveCacheKey(MethodExecutionArgs args)
        {
            return $"{args.Method.DeclaringType.Name}-{args.Method.Name}{(args.Arguments.Any() ? "-" + args.Arguments.Aggregate((first, second) => first + "-" + second) : "")}";
        }
        #endregion
    }
}
