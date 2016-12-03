using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DtoMapping;

namespace DtoMappingTests
{
    [TestClass]
    public class MappingFunctionsCaсheTests
    {
        [TestMethod]
        public void TryGetMappingFunction_TwoTimesInvoke_ReturnSameReferences()
        {
            MappingFunctionsCache mappingFunctionsCache = MappingFunctionsCache.Instance;

            var firstInvoke = mappingFunctionsCache.TryGetMappingFunction<Animal, LandAnimal>();
            var secondInvoke = mappingFunctionsCache.TryGetMappingFunction<Animal, LandAnimal>();

            Assert.IsTrue(firstInvoke == secondInvoke);
        }
        
    }
}
