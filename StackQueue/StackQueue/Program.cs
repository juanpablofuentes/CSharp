using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack myStack = new Stack();
            myStack.Push("Saludo");
            myStack.Push(null);
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);
            myStack.Push(4);
            myStack.Push(5);

            //Recorrer la pila
            foreach (var itm in myStack)
                Console.WriteLine(itm);
            Console.WriteLine("---------");

            Console.WriteLine(myStack.Peek()); //Devuelve el último pero no lo quita
            Console.WriteLine(myStack.Peek());
            Console.WriteLine(myStack.Peek());
            Console.WriteLine("---------");

            Console.WriteLine(myStack.Pop()); //Devuelve el último Y LO QUITA
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            Console.WriteLine("---------");

            Console.WriteLine(myStack.Contains("Saludo"));
            Console.WriteLine(myStack.Contains("Pepe"));
            Console.WriteLine("---------");

            Console.WriteLine(myStack.Count);
            Console.WriteLine("---------");

            Queue queue = new Queue();
            queue.Enqueue(3);
            queue.Enqueue(2);
            queue.Enqueue(1);
            queue.Enqueue("Cuatro");
            //Recorrer la pila
            foreach (var itm in queue)
                Console.WriteLine(itm);
            Console.WriteLine("---------");
            Console.WriteLine(queue.Peek());//Muestra sin quitar
            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Peek());
            Console.WriteLine("---------");
            Console.WriteLine(queue.Dequeue());//Muestra QUITANDO
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine("---------");
        }
    }
}
