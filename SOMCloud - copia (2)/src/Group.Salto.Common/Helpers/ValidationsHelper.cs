using Group.Salto.Common.Constants;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Group.Salto.Common.Helpers
{
    public static class ValidationsHelper
    {
        public static bool IsEmailValid(string value)
        {
            var result = false;
            if (!string.IsNullOrEmpty(value))
            {
                result = Regex.IsMatch(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            }

            return result;
        }

        public static bool ValidateNIF(string data)
        {
            try
            {
                //Si el largo del NIF es diferente a 9, acaba el método.
                if (data.Length != 9)
                {
                    return false;
                }

                string nifLettersSequence = "TRWAGMYFPDXBNJZSQVHLCKE";
                data = data.ToUpper();
                string nifNumber = data.Substring(0, data.Length - 1);
                nifNumber = nifNumber.Replace("X", "0").Replace("Y", "1").Replace("Z", "2");
                char letraNIF = data[8];
                int i = int.Parse(nifNumber) % 23;
                return letraNIF == nifLettersSequence[i];
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateCif(string cif)
        {
            try
            {
                if (string.IsNullOrEmpty(cif)) return false;
                cif = cif.Trim().ToUpper();
                if (cif.Length != 9) return false;

                string firstChar = cif.Substring(0, 1);
                string firstValidLetters = "ABCDEFGHJNPQRSUVW";
                if (!firstValidLetters.Any(x => x.ToString() == firstChar)) return false;

                string digits = cif.Substring(1, 7);
                if (!int.TryParse(digits, out int value))
                {
                    return false;
                }

                var sumaPar = digits.ToList().Where((c, i) => i % 2 != 0).Sum(x => int.Parse(x.ToString()));
                var sumaImpar = digits.Where((c, i) => i % 2 == 0)
                    .Sum(x => int.Parse(x.ToString()) * 2 % 10 + int.Parse(x.ToString()) * 2 / 10);
                int sumaTotal = sumaPar + sumaImpar;
                sumaTotal = (10 - (sumaTotal % 10)) % 10;
                string digitoControl = "";
                var controlCharacters = "NPQRSW";
                string characters = "JABCDEFGHI";
                if (controlCharacters.Contains(firstChar))
                {
                    digitoControl = characters[sumaTotal].ToString();
                }
                else
                {
                    digitoControl = sumaTotal.ToString();
                }

                return digitoControl.Equals(cif.Substring(8, 1));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateNIE(string value)
        {
            string allowedChars = "TRWAGMYFPDXBNJZSQVHLCKET";

            string initialLetter = string.Empty;
            string controlDigit = string.Empty;
            int dniNumber;
            string valueToValid = (value ?? string.Empty).ToString();

            if (!string.IsNullOrEmpty(valueToValid))
            {
                string nie = DeleteInvalidChars(valueToValid);

                if (nie.Length != 9 && nie.Length != 11)
                    return false;

                if (!Regex.IsMatch(nie, @"[K-MX-Z]\d{7}[" + allowedChars + "]$"))
                    return false;

                initialLetter = nie.FirstOrDefault().ToString();
                Int32.TryParse(nie.Substring(1, 7), out dniNumber);
                controlDigit = nie.LastOrDefault().ToString();

                switch (initialLetter)
                {
                    case "X":
                        break;
                    case "Y":
                        dniNumber += 10000000;
                        break;
                    case "Z":
                        dniNumber += 20000000;
                        break;
                }

                if (controlDigit != GetNIELetter(dniNumber, allowedChars))
                    return false;
            }

            return true;
        }

        private static string DeleteInvalidChars(string number)
        {
            string chars = @"[^\w]";
            Regex regex = new Regex(chars);
            return regex.Replace(number, string.Empty).ToUpper();
        }

        private static string GetNIELetter(int DNINumber, string allowedChars)
        {
            int index = DNINumber % 23;
            return allowedChars[index].ToString();
        }

        public static bool MinLength(string value, short minvalue)
        {
            if (!string.IsNullOrEmpty(value))
                return (value.Length > minvalue);
            else
                return true;
        }

        public static bool MaxLength(string value, short maxvalue)
        {
            if (!string.IsNullOrEmpty(value))
                return (value.Length < maxvalue);
            else
                return true;
        }

        public static bool IsTextNameValid(string value)
        {
            bool result = false;
            string invalidCharacters = AppConstants.InvalidNamesCharacters;

            if (!string.IsNullOrEmpty(value))
            {
                int length = value.Length;
                int index = 0;
                do
                {
                    result = invalidCharacters.Contains(value[index]);
                    index++;
                }
                while (index < length && !result);
            }

            return !result;
        }

        public static bool ValidateUrl(string url)
        {
            if (url == null || url == "") return false;

            Regex oRegExp = new Regex(@"(http|ftp|https)://([\w-]+\.)+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            var validUrl = oRegExp.Match(url).Success;
            return validUrl;
        }

        public static bool IsMinDateValue(DateTime value)
        {
            return value == DateTime.MinValue;
        }
    }
}