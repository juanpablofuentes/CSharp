using System;
using System.Linq;
using System.Text;

namespace Group.Salto.Common.Helpers
{
    public static class PasswordGenerator
    {
        public static string GeneratePassword(int minLenght, int maxLenght)
        {
            Random rand = new Random();
            var lenght = rand.Next(minLenght, maxLenght-2);
            string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            res.Append(valid[rnd.Next(valid.Length)]);
            valid = "0123456789abcdefghijklmnopqrstuvwxyz";
            while (2 < lenght--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            var specialCharacters = "?¿|&()#%";
            res.Append(specialCharacters[rnd.Next(specialCharacters.Length)]);
            return res.ToString();
        }
    }
}
