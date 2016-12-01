using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DtoMapping
{    
    internal class MappingExpressionsGenerator 
    {
        private const string SourceParameterName = "source";

        public Expression<Func<TSource, TDestination>> CreateMappingPropertiesExpression<TSource, TDestination>(IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> propertiesToMap) where TSource : class
            where TDestination : class, new()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), SourceParameterName);
            List<MemberBinding> destinationMemberBindings = new List<MemberBinding>();

            foreach (KeyValuePair<PropertyInfo, PropertyInfo> sourceDestinationPair in propertiesToMap)
            {
                PropertyInfo sourceProperty = sourceDestinationPair.Key;
                PropertyInfo destinationProperty = sourceDestinationPair.Value;

                MemberExpression sourcePropertyExpression = Expression.Property(parameter, sourceProperty);
                UnaryExpression convertion = Expression.Convert(sourcePropertyExpression, destinationProperty.PropertyType);
                MemberAssignment destinationPropertyAssignment = Expression.Bind(destinationProperty, convertion);
                destinationMemberBindings.Add(destinationPropertyAssignment);
            }

            NewExpression destinationObjectCreation = Expression.New(typeof(TDestination));
            MemberInitExpression destinationObjectInitialization = Expression.MemberInit(destinationObjectCreation, destinationMemberBindings);
            Expression<Func<TSource, TDestination>> mappingExpression =
                Expression.Lambda<Func<TSource, TDestination>>(destinationObjectInitialization, parameter);
            return mappingExpression;
        }
    }
}
