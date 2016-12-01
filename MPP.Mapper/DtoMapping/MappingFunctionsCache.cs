using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DtoMapping
{
    public sealed class MappingFunctionsCache
    {
        private static readonly Lazy<MappingFunctionsCache> Lazy = new Lazy<MappingFunctionsCache>(() => new MappingFunctionsCache());

        public static MappingFunctionsCache Instance { get { return Lazy.Value; } }
        private readonly Dictionary<KeyValuePair<Type, Type>, Delegate> _cachedMappingFunctions;

        private MappingFunctionsCache()
        {
            _cachedMappingFunctions = new Dictionary<KeyValuePair<Type, Type>, Delegate>();
        }

        public bool ContainsMappingFunction(Type tSource, Type tDestination)
        {
            foreach (KeyValuePair<Type, Type> sourceDestinationPair in _cachedMappingFunctions.Keys)
            {
                if ((sourceDestinationPair.Key == tSource) && (sourceDestinationPair.Value) == tDestination)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool TryAddMappingFunction<TSource, TDestination>(Func<TSource, TDestination> mappingExpression)
        {
            if (ContainsMappingFunction(typeof(TSource), typeof(TDestination)))
            {
                return false;
            }

            _cachedMappingFunctions.Add(new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination)), 
                mappingExpression);
            return true;
        }

        public Func<TSource, TDestination> TryGetMappingFunction<TSource, TDestination>()
        {
            if (!ContainsMappingFunction(typeof(TSource), typeof(TDestination)))
            {
                return null;
            }

            return (Func<TSource, TDestination>)_cachedMappingFunctions[new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination))];
        }
    }
}
