using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DtoMapping
{
    public class MapperConfiguration<TSource, TDestination> where TSource : class where TDestination : class, new()
    {
        private Dictionary<PropertyInfo, PropertyInfo> _sourceDestinationPropertiesAccordance;

        public MapperConfiguration()
        {
            _sourceDestinationPropertiesAccordance = new Dictionary<PropertyInfo, PropertyInfo>();
        }

        public void Register(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            if (!(sourceProperty.DeclaringType == typeof (TSource)))
            {
                throw new ArgumentException("Source property must belong to the TSource class.", nameof(sourceProperty));
            }

            if (!(destinationProperty.DeclaringType == typeof (TDestination)))
            {
                throw new ArgumentException("Destination property must belong to the TDestination class.", nameof(destinationProperty));
            }

            if (_sourceDestinationPropertiesAccordance.Keys.Contains(sourceProperty))
            {
                throw new ArgumentException("Accordance for this source property already exists.");
            }

            if (_sourceDestinationPropertiesAccordance.Values.Contains(destinationProperty))
            {
                throw new ArgumentException("Accordance for this destination property already exists.");
            }

            _sourceDestinationPropertiesAccordance.Add(sourceProperty,destinationProperty);
        }
    }
}
