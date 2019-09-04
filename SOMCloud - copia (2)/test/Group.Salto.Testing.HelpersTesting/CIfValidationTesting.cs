using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class CIfValidationTesting
    {
        [TestMethod]
        public void CIFCorrect()
        {
            var email = "B23470248";
            var result = ValidationsHelper.ValidateCif(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CIFIncorrect()
        {
            var email = "7291118D";
            var result = ValidationsHelper.ValidateCif(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CIFIncorrect2()
        {
            var email = "51064247A";
            var result = ValidationsHelper.ValidateCif(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CIFCorrect2()
        {
            var email = "S3678144A";
            var result = ValidationsHelper.ValidateCif(email);
            Assert.IsTrue(result);
        }
    }
}
