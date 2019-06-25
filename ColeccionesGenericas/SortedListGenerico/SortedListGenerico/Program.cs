using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedListGenerico
{
    class Program
    {
        private class InvertedComparer : IComparer<String>
        {
            public int Compare(string x, string y)
            {
                return y.CompareTo(x);
            }
        }
        static void Main(string[] args)
        {

            SortedList<string, string> sList = new SortedList<string, string>(new InvertedComparer());
            sList.Add("ana", "Ana Pérez");
            sList.Add("eva", "Eva Pi");
            sList.Add("juan", "Juan Buj");
            sList.Add("rosa", "Rosa Sanz");
            sList.Add("pedro", "Pedro Sánchez");
            foreach(string valor in sList.Values)
            {
                Console.WriteLine(valor);
            }
            Console.WriteLine(sList.ContainsKey("ana"));
            Console.WriteLine(sList.ContainsValue("Eva Pi"));
            Console.WriteLine(sList.IndexOfKey("ana"));
            Console.WriteLine(sList.IndexOfValue("Eva Pi"));
            Console.WriteLine(sList.Keys[0]);
            Console.WriteLine(sList.Values[0]);
        }
    }
}
