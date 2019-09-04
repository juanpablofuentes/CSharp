using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class DecimalConversionTesting
    {
        [TestMethod]
        public void TestSpSuccess()
        {
            decimal value = (decimal)12.25;
            var valueConverted = value.DecimalToString("ES");
            var valueConvertBack = valueConverted.StringToDecimal("ES");
            Assert.IsTrue(valueConvertBack == value);
        }

        [TestMethod]
        public void TestSpSuccess2()
        {
            decimal value = (decimal)12000.25;
            var valueConverted = value.DecimalToString("ES");
            var valueConvertBack = valueConverted.StringToDecimal("ES");
            Assert.IsTrue(valueConvertBack == value);
        }

        [TestMethod]
        public void TestSpSuccess3()
        {
            var valueConverted = "12.2333,25";
            var valueConvertBack = valueConverted.StringToDecimal("ES");
            Assert.IsTrue(valueConvertBack == (decimal)122333.25);
        }

        [TestMethod]
        public void TestSpError()
        {
            var valueConverted = "12,2333.25";
            var valueConvertBack = valueConverted.StringToDecimal("ES");
            Assert.IsTrue(valueConvertBack != (decimal)122333.25);
        }

        [TestMethod]
        public void TestENSuccess()
        {
            decimal value = (decimal)12000.25;
            var valueConverted = value.DecimalToString("EN");
            var valueConvertBack = valueConverted.StringToDecimal("EN");
            Assert.IsTrue(valueConvertBack == value);
        }

        [TestMethod]
        public void TestENSuccess2()
        {
            var valueConverted = "12,2333.25";
            var valueConvertBack = valueConverted.StringToDecimal("EN");
            Assert.IsTrue(valueConvertBack == (decimal)122333.25);
        }

        [TestMethod]
        public void TestENError()
        {
            var valueConverted = "12.2333,25";
            var valueConvertBack = valueConverted.StringToDecimal("EN");
            Assert.IsTrue(valueConvertBack != (decimal)122333.25);
        }
    }
}
