using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DtoMapping
{
    public class Mapper : IMapper
    {       
        public TDestination Map<TSource, TDestination>(TSource source) 
            where TSource : class where TDestination : class, new()
        {
            if (source == null)
            {
                return null;
            }

            Func<TSource, TDestination> mappingFunction = MappingFunctionsCache.Instance.TryGetMappingFunction<TSource,TDestination>();

            if (mappingFunction == null)
            {
                mappingFunction = GenerateMappingFunction<TSource, TDestination>(source);
                MappingFunctionsCache.Instance.TryAddMappingFunction<TSource, TDestination>(mappingFunction);
            }

            TDestination destinationObject = mappingFunction(source);
            return destinationObject;
        }

        private bool PropertyCanBeMapped(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            if ((sourceProperty.Name == destinationProperty.Name) && (destinationProperty.SetMethod != null))
            {
                if ((sourceProperty.PropertyType.IsValueType) && (ImplicitNumericConversionsChecker.Instance.ImplicitNumericConversionExists(sourceProperty.PropertyType, destinationProperty.PropertyType)))
                {
                    return true;
                }
                else
                {
                    if (sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        return true;
                    }
                    if ((sourceProperty.PropertyType).IsSubclassOf(destinationProperty.PropertyType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Func<TSource, TDestination> GenerateMappingFunction<TSource, TDestination>(TSource source) where TSource : class where TDestination : class, new()
        {
            PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
            PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertiesToMap = new List<KeyValuePair<PropertyInfo, PropertyInfo>>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                foreach (PropertyInfo destinationProperty in destinationProperties)
                {
                    if (PropertyCanBeMapped(sourceProperty, destinationProperty))
                    {
                        propertiesToMap.Add(new KeyValuePair<PropertyInfo, PropertyInfo>(sourceProperty, destinationProperty));
                    }
                }
            }

            MappingExpressionsGenerator expressionTreeGenerator = new MappingExpressionsGenerator();
            Expression<Func<TSource, TDestination>> mappingExpression = expressionTreeGenerator.CreateMappingPropertiesExpression<TSource, TDestination>(propertiesToMap);
            return mappingExpression.Compile();
        }
    }
}
