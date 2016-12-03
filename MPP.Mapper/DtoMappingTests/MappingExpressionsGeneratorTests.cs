using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DtoMapping;

namespace DtoMappingTests
{
    [TestClass]
    public class MappingExpressionsGeneratorTests
    {
        private static readonly MappingExpressionsGenerator Generator = new MappingExpressionsGenerator();

        [TestMethod]
        public void CreateMappingPropertiesExpression_ReturnCorrect()
        {
            var testData = CreateTestPropertiesToMap();

            Animal testAnimal = new Animal()
            {
                Age = 10,
                NumberOfEyes = 2,
                NumberOfLegs = 4,
                Dimensions = new List<int>(),
                Names = new List<string>()
            };

            Expression<Func<Animal, LandAnimal>> expectedExpression = animal => new LandAnimal()
            {
                Age = animal.Age,
                NumberOfLegs = animal.NumberOfLegs,
                Names = animal.Names
            };

            var actualExpression = Generator.CreateMappingPropertiesExpression<Animal, LandAnimal>(testData);

            var expected = expectedExpression.Compile().Invoke(testAnimal);
            var actual = actualExpression.Compile().Invoke(testAnimal);

            Assert.IsTrue(LandAnimal.LandAnimalComparer.Equals(expected, actual));
        }

        private List<KeyValuePair<PropertyInfo, PropertyInfo>> CreateTestPropertiesToMap()
        {
            PropertyInfo[] sourceProperties = typeof(Animal).GetProperties();
            PropertyInfo[] destinationProperties = typeof(LandAnimal).GetProperties();
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

            return propertiesToMap;
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
    }
}
