using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class NIEValidationTest
    {
        [TestMethod]
        public void NIEXTestingCorrect()
        {
            var NIE = "X1234567L";
            var result = ValidationsHelper.ValidateNIE(NIE);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NIEYTestingCorrect()
        {
            var NIE = "Y1234567X";
            var result = ValidationsHelper.ValidateNIE(NIE);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NIEXTestingInCorrect()
        {
            var NIE = "X1234567T";
            var result = ValidationsHelper.ValidateNIE(NIE);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ListNIEsTestingCorrect()
        {
            List<string> NIEs = new List<string>();
            NIEs.Add("X7654321J");
            NIEs.Add("Y7654321G");
            NIEs.Add("X5135467G");

            bool result = false;

            foreach(string nie in NIEs)
            {
                result = ValidationsHelper.ValidateNIE(nie);
                if (!result) break;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ListNIEsTestingInCorrect()
        {
            List<string> NIEs = new List<string>();
            NIEs.Add("X7654321J");
            NIEs.Add("X7654321E");
            NIEs.Add("Y7654321G");

            bool result = false;

            foreach (string nie in NIEs)
            {
                result = ValidationsHelper.ValidateNIE(nie);
                if (!result) break;
            }

            Assert.IsFalse(result);
        }
    }
}