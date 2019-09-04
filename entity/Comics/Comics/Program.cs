using System;

namespace Comics
{
    class Program
    {
        class foo
        {
            public int test { get; set; }
        }
        static void Main(string[] args)
        {
            foo a = new foo();
            foo b = new foo();

            a.test = 5;
            b.test = 6;
            Console.WriteLine(a.test);
            Console.WriteLine(b.test);
            a = b;
            b.test = 8;
            pepe(b);
            Console.WriteLine(a.test);
            Console.WriteLine(b.test);
        }
        static void pepe(foo t)
        {
            t.test = 16;
        }
    }
}
