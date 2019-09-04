using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Group.Salto.Testing.HelpersTesting
{
    [TestClass]

    public class PasswordGeneratorTest
    {
        [TestMethod]
        public void GeneratePasswords()
        {
            var passwords = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                passwords.Add(PasswordGenerator.GeneratePassword(8, 20));
            }
            Assert.IsTrue(passwords.All(x=>x.Length >= 8 && x.Length < 20));
        }
    }
}
