using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traductor
{
    class Program
    {
        static void Main(string[] args)
        {
            Translate t = new Translate();
            t.addWord("red", "rojo");
            t.addWord("blue", "azul");
            t.addWord("green", "verde");
            t.addWord("yellow", "amarillo");
            t.addWord("white", "blanco");
            Console.WriteLine(t.firstWord());
            Console.WriteLine(t.search("white"));
            Console.WriteLine(t.search("blue"));
            t.removeTranslation("verde");
            Console.WriteLine(t.search("green"));
             Console.WriteLine(t.search("green"));
        }
    }
}
