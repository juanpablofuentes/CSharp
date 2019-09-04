using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class NifValidationTesting
    {
        [TestMethod]
        public void NifCorrect()
        {
            var email = "40349280N";
            var result = ValidationsHelper.ValidateNIF(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NIfIncorrect()
        {
            var email = "7291118D";
            var result = ValidationsHelper.ValidateNIF(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NIFIncorrect2()
        {
            var email = "51064247A";
            var result = ValidationsHelper.ValidateNIF(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NifCorrect2()
        {
            var email = "98277525C";
            var result = ValidationsHelper.ValidateNIF(email);
            Assert.IsTrue(result);
        }
    }
}
