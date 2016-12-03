using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DtoMapping;

namespace DtoMappingTests
{
    [TestClass]
    public class ImplicitNumericConversionsCheckerTests
    {
        private static readonly ImplicitNumericConversionsChecker Checker = ImplicitNumericConversionsChecker.Instance;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ImplicitNumericConversionExists_TypeOfSourseIsNull_ThrowException()
        {
            Checker.ImplicitNumericConversionExists(null, typeof (int));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ImplicitNumericConversionExists_TypeOfDestinationIsNull_ThrowException()
        {
            Checker.ImplicitNumericConversionExists(typeof(int), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ImplicitNumericConversionExists_TypeOfSourseIsNotPrimitive_ThrowException()
        {
            Checker.ImplicitNumericConversionExists(typeof(DateTime), typeof(int));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ImplicitNumericConversionExists_TypeOfDestinationIsNotPrimitive_ThrowException()
        {
            Checker.ImplicitNumericConversionExists(typeof(int), typeof(DateTime));
        }

        [TestMethod]
        public void ImplicitNumericConversionExists_SourseIsIntAndDestinationIsByte_ReturnFalse()
        {
            Assert.IsFalse(Checker.ImplicitNumericConversionExists(typeof(int), typeof(byte)));
        }

        [TestMethod]
        public void ImplicitNumericConversionExists_SourseIsIntAndDestinationIsLong_ReturnTrue()
        {
            Assert.IsTrue(Checker.ImplicitNumericConversionExists(typeof(int), typeof(long)));
        }
    }
}
