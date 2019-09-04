using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]
    public class InvalidCharactersTest
    {
        [TestMethod]
        public void TextCorrect()
        {
            var text = "manolo";
            var result = ValidationsHelper.IsTextNameValid(text);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TextCorrect2()
        {
            var text = "juan garcia";
            var result = ValidationsHelper.IsTextNameValid(text);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TextCorrectApostrof()
        {
            var text = "M'aria Lopez cuartero";
            var result = ValidationsHelper.IsTextNameValid(text);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestInCorrect()
        {
            var text = "m<anolo>";
            var result = ValidationsHelper.IsTextNameValid(text);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HTMLTestInCorrect()
        {
            var text = "<a href=";
            var result = ValidationsHelper.IsTextNameValid(text);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AmpersantTestInCorrect()
        {
            var text = "ABC&DEF";
            var result = ValidationsHelper.IsTextNameValid(text);
            Assert.IsFalse(result);
        }
    }
}