using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DtoMapping;

namespace DtoMappingTests
{
    [TestClass]
    public class MapperTests
    {
        private static readonly Mapper Mapper = new Mapper();

        [TestMethod]
        public void Map_Null_ReturnNull()
        {
            LandAnimal landAnimal = Mapper.Map<Animal, LandAnimal>(null);

            LandAnimal expected = null;
            var actual = landAnimal;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Map_ValueTypesAreEqual_SameValue()
        {
            int age = 20;
            Animal animal = new Animal() {Age = age};

            LandAnimal landAnimal = Mapper.Map<Animal, LandAnimal>(animal);

            var expected = age;
            var actual = landAnimal.Age;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Map_ValueTypeCanBeCast_SameValue()
        {
            float numberOfLegs = 20F;
            Animal animal = new Animal() { NumberOfLegs = numberOfLegs };

            LandAnimal landAnimal = Mapper.Map<Animal, LandAnimal>(animal);

            var expected = numberOfLegs;
            var actual = landAnimal.NumberOfLegs;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Map_ValueTypeCanNotBeCast_Return0()
        {
            byte numerOfEyes = 2;
            Animal animal = new Animal() { NumberOfEyes = numerOfEyes };

            LandAnimal landAnimal = Mapper.Map<Animal, LandAnimal>(animal);

            var expected = 0;
            var actual = landAnimal.NumberOfEyes;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Map_RefTypesAreEqual_SameValue()
        {
            List<string> names = new List<string>();
            Animal animal = new Animal() { Names = names };

            LandAnimal landAnimal = Mapper.Map<Animal, LandAnimal>(animal);

            var expected = names;
            var actual = landAnimal.Names;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Map_DifferentRefTypes_ReturnNull()
        {
            List<int> dimensions = new List<int>();
            Animal animal = new Animal() { Dimensions = dimensions };

            LandAnimal landAnimal = Mapper.Map<Animal, LandAnimal>(animal);

            IEnumerable<int> expected = null;
            var actual = landAnimal.Dimensions;

            Assert.AreEqual(expected, actual);
        }
    }
}
