using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class EmailValidationsTesting
    {
        [TestMethod]
        public void EmailTestingCorrect()
        {
            var email = "jcollado@pasiona.com";
            var result = ValidationsHelper.IsEmailValid(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EmailValidationsIncorrect()
        {
            var email = "jcollado";
            var result = ValidationsHelper.IsEmailValid(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EmailValidationsIncorrect2()
        {
            var email = "@hotmail.com";
            var result = ValidationsHelper.IsEmailValid(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EmailValidationCorrect2()
        {
            var email = "joan.Isaac.Collado.65@pasiona.com";
            var result = ValidationsHelper.IsEmailValid(email);
            Assert.IsTrue(result);
        }
    }
}
