using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHash
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable();

            ht.Add(1, "Uno");
            ht.Add(2, "Dos");
            ht.Add(3, "tres");
            ht.Add(4, "Cuatro");
            ht.Add(5, null);
            ht.Add("cc", "Cinco");
            ht.Add(8.5F, 8.5);
            //Recorremos con tipo diccionario
            foreach (DictionaryEntry item in ht)
                Console.WriteLine($"key:{item.Key}, value:{item.Value}");

            Console.WriteLine("------");
            Hashtable ht2 = new Hashtable()
                {
                    { 1, "Uno" },
                    { 2, "Dos" },
                    { 3, "tres" },
                    { 4, "Cuatro" },
                    { 5, null },
                    { "cc", "Cinco" },
                    { 8.5F, 8.5 }
                };
            //Recorremos utlizando las claves
            foreach (var key in ht2.Keys)
                Console.WriteLine("Key:{0}, Value:{1}", key, ht[key]);

            ht.Remove("cc"); //Elimina el elemento de clave cc
            Console.WriteLine("------");
            Console.WriteLine(ht.Contains(2));// Imprime true
            Console.WriteLine(ht.ContainsKey(2));// Imprime true
            Console.WriteLine(ht.Contains("Dos")); //Imprime false
            Console.WriteLine(ht.ContainsValue("Uno")); // Imprime true

            SortedList sortedList1 = new SortedList();
            sortedList1.Add(3, "Three");
            sortedList1.Add(4, "Four");
            sortedList1.Add(1, "One");
            sortedList1.Add(5, "Five");
            sortedList1.Add(2, "Two");

            SortedList sortedList2 = new SortedList();
            sortedList2.Add("one", 1);
            sortedList2.Add("two", 2);
            sortedList2.Add("three", 3);
            sortedList2.Add("four", 4);

            SortedList sortedList3 = new SortedList();
            sortedList3.Add(1.5, 100);
            sortedList3.Add(3.5, 200);
            sortedList3.Add(2.4, 300);
            sortedList3.Add(2.3, null);
            sortedList3.Add(1.1, null);

            SortedList sortedList4 = new SortedList()
{
    {3, "Three"},
    {4, "Four"},
    {1, "One"},
    {5, "Five"},
    {2, "Two"}
};
            foreach (DictionaryEntry kvp in sortedList1)
                Console.WriteLine("key: {0}, value: {1}", kvp.Key, kvp.Value);
            Console.WriteLine("------");
            foreach (DictionaryEntry kvp in sortedList2)
                Console.WriteLine("key: {0}, value: {1}", kvp.Key, kvp.Value);
            Console.WriteLine("------");
            foreach (DictionaryEntry kvp in sortedList3)
                Console.WriteLine("key: {0}, value: {1}", kvp.Key, kvp.Value);
            Console.WriteLine("------");
            foreach (DictionaryEntry kvp in sortedList4)
                Console.WriteLine("key: {0}, value: {1}", kvp.Key, kvp.Value);
            Console.WriteLine("------");
            foreach (var kvp in sortedList4.Keys)
                Console.WriteLine("key: {0}, value: {1}", kvp, sortedList4[kvp]);
            Console.WriteLine("------");

            sortedList2.Remove("one");//Elimina el elemento de clave 'one'
            sortedList2.RemoveAt(0);//Elimina el elemento 0, es decir, el primero
            foreach (DictionaryEntry kvp in sortedList2)
                Console.WriteLine("key: {0}, value: {1}", kvp.Key, kvp.Value);
            Console.WriteLine("------");
            SortedList sortedList = new SortedList()
            {
                { 3, "Three"},
                                { 4, "Four"},
                                { 1, "One"},
                                { 8, "Eight"},
                                { 2, "Two"}
            };
            Console.WriteLine(sortedList.Contains(2)); // Imprime true
            Console.WriteLine(sortedList.Contains(4)); // Imprime true
            Console.WriteLine(sortedList.Contains(6)); // Imprime false

            Console.WriteLine(sortedList.ContainsKey(2)); // Imprime true
            Console.WriteLine(sortedList.ContainsKey(6)); // Imprime false

            Console.WriteLine(sortedList.ContainsValue("One")); // Imprime true
            Console.WriteLine(sortedList.ContainsValue("Ten")); // Imprime false

        }

    }
}
