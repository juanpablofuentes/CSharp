using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            //Crear un arrayList
            ArrayList arryList1 = new ArrayList();
            //Añadir elementos uno a uno
            arryList1.Add(1);
            arryList1.Add("Eva");
            arryList1.Add(3);
            arryList1.Add(4.5);

            ArrayList arryList2 = new ArrayList() { 100, 200 };
          
          //Añadir un rango
            arryList1.AddRange(arryList2);
            //Lo mostramos con tipo
            Console.WriteLine("Contenido del arrraylist");
            foreach (var item in arryList1)
            {
                Console.WriteLine(item+" - "+item.GetType());
            }
            //Lo mostramos accediendo por índice
            for(int i = 0; i < arryList1.Count; i++)
            {
                Console.WriteLine(arryList1[i]);
            }
            Console.WriteLine("---------------------");

            arryList1.Insert(1, "Elemento añadido");
            arryList1.Insert(2, 100);
            displayArrayList(arryList1);

            arryList1.InsertRange(2, arryList2);
            displayArrayList(arryList1);

            arryList1.Remove(100); //Elimina el elemento 100 (sólo uno)
            Console.WriteLine("Elemento 100 eliminado (el primero)");
            displayArrayList(arryList1);

            while (arryList1.Contains(100)) { arryList1.Remove(100); } //Elimina el elemento 100 (todos)
            displayArrayList(arryList1);

            arryList1.RemoveAt(1); //Elimina el elemento 1 (basado en índice 0)
            displayArrayList(arryList1);
            arryList1.RemoveRange(3, 2);//Elimina dos elementos a partir de la posición 3
            displayArrayList(arryList1);

            arryList1.Sort(new comparador());//Ordena la lista. Utilizamos un comparador porque los tipos son diferentes
            displayArrayList(arryList1);
            Console.WriteLine(arryList1.IndexOf("Eva"));
            Console.WriteLine(arryList1.BinarySearch("Eva",new comparador())); //Podemos usar Binarysearch porque está ordenado

            ArrayList cadenas = new ArrayList() { "pepe", "anacardo", "tomate", "bustrofedonico", "eva" };
            cadenas.Sort();
            displayArrayList(cadenas);
            cadenas.Sort(new otroComparador());
            displayArrayList(cadenas);
        }
        static void displayArrayList(ArrayList lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---------------------");
        }
        public class comparador : IComparer
        {

            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(Object x, Object y)
            {
                return (x.ToString().CompareTo(y.ToString()));
            }

        }
        public class otroComparador : IComparer
        {
            public int Compare(object x, object y)
            {
                //Tengo que comparar los dos objetos
                // Si x es mayor que y devuelvo 1
                //Si y es mayor que x devuelvo -1
                //Si x = y devuelvo 0
                string a = (string)x;
                string b = (string)y;
                if (a.Length > b.Length) { return 1; }
                if (b.Length > a.Length) { return -1; }
                return 0;
            }
        }
    }
}
