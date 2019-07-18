using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionMetodo
{
    public static class Extension
    {
        public static bool mayorQue(this int i, int value)
        {
            return i > value;
        }
        public static bool par(int i)
        {
            return i % 2 == 0;
        }
        public static bool esPar(this int i)
        {
            return i % 2 == 0;
        }
        public static bool esPrimo(this int i)
        {
            if (i <= 1) { return false; }
            if (i == 2) { return true; }
            for (int j = 2; j <= Math.Ceiling(Math.Sqrt(i)); j++)
            {
                if (i % j == 0) { return false; }
            }
            return true;
        }
        public static List<int> factores(this int i)
        {
            List<int> f = new List<int>();
            if (i < 1) { return null; }

            for (int j = 1; j <= i; j++)
            {
                if (i % j == 0) { f.Add(j); }
            }
            return f;
        }
        public static int siguientePrimo(this int i)
        {
            do { i++; }
            while (!i.esPrimo());
            return i;
        }
        public static string trimAll(this string c)
        {
            return c.Replace(" ", "");
        }
        public static string toTitle(this string c)
        {
            string[] cads = c.Split(" ");
            for (int i = 0; i < cads.Length; i++)
            {
                cads[i] = cads[i].ToUpper()[0] + cads[i].Substring(1).ToLower();
            }
            return String.Join(" ", cads);
        }
        public static string reverse(this string c)
        {
            string res = "";
            for (int i = c.Length - 1; i >= 0; i--)
            {
                res += c[i];
            }
            return res;
        }
        public static bool palindromo(this string c)
        {
            c = c.trimAll().ToLower();
            return c == c.reverse();
        }
        public static List<string> anagrama(this string cad)
        {
            List<string> l = new List<string>();
            string resto;
            if (cad.Length == 1) { l.Add(cad); return l; }
            for (int i = 0; i < cad.Length; i++)
            {
                resto = cad.Remove(i, 1);
                List<string> ana = resto.anagrama();
                foreach (string a in ana)
                {
                    if (!l.Contains(cad[i] + a))
                    {
                        l.Add(cad[i] + a);
                    }

                }
            }
            return l;
        }
    }
}
