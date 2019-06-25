using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionarioGenérico
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "Uno");
            dict.Add(2, "Dos");
            dict.Add(3, "Tres");

            dict.Add(new KeyValuePair<int, string>(4, "Cuatro"));
            dict.Add(new KeyValuePair<int, string>(5, "Cinco"));

            IDictionary<int, string> dict2 = new Dictionary<int, string>()
                                            {
                                                {1,"Uno"},
                                                {2, "Dos"},
                                                {3,"Tres"}
                                            };

            //Recorrer un diccionario
            foreach (KeyValuePair<int, string> item in dict)
            {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }
            Console.WriteLine("-----");

            for (int i = 0; i < dict.Count; i++)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                                                        dict.Keys.ElementAt(i),
                                                        dict[dict.Keys.ElementAt(i)]);
            }
            Console.WriteLine("-----");
            foreach (int key in dict.Keys)
            {
                Console.WriteLine("Key: {0}, Value: {1}", key, dict[key]);
            }
            Console.WriteLine("-----");

            foreach (string valor in dict.Values)
            {
                Console.WriteLine("Value: {0}", valor);
            }
            Console.WriteLine("-----");
            Console.WriteLine(dict[1]); //returns Uno
            Console.WriteLine(dict[2]); // returns Dos
            Console.WriteLine("-----");
            string result;
            for (int i = 0; i < 10; i++)
            {
                if (dict.TryGetValue(i, out result))
                {
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("No hay valor para esa posición");
                }
            }

            Console.WriteLine(dict.ContainsKey(1)); // returns true
            Console.WriteLine(dict.ContainsKey(4)); // returns false
         

            Console.WriteLine(dict.Contains(new KeyValuePair<int, string>(1, "Uno"))); // returns true


            //Eliminar
            dict.Remove(1);

        }
    }
}
