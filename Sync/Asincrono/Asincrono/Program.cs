using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Asincrono
{
    class Program
    {
        private const string URL = "https://docs.microsoft.com/en-us/dotnet/csharp/csharp";

        static void Main(string[] args)
        {
            DoSynchronousWork();
            var someTask = DoSomethingAsync();
            var foo=callArbol();
            Console.WriteLine("Fin árbol");
            someTask.Wait();
            Console.WriteLine("Se acabó");
        }
        public static void DoSynchronousWork()
        {
            
            Console.WriteLine("1. Esto se ejecuta de manera síncrona");
        }

        static async Task DoSomethingAsync() 
        {
            Console.WriteLine("2. Tarea asíncrona iniciada...");
            await GetStringAsync(); // Esperando a que acabe
        }

        static async Task GetStringAsync()
        {
            using (var httpClient = new HttpClient())
            {
                Console.WriteLine("3. Esperando el resultado...");
                string result = await httpClient.GetStringAsync(URL); 
                Console.WriteLine("4. Tarea ha finalizado ...");
                Console.WriteLine($"5. La longitud del texto es {URL}");
                Console.WriteLine($"6. {result.Length} caracteres");
            }
        }
        static async Task callArbol()
        {
            await DoSynchronousWorkAfterAwait();
        }
        static async Task DoSynchronousWorkAfterAwait()
        {
             Console.WriteLine("7. Mientras esperamos se pueden hacer otras cosas...");
            for (var i = 0; i <= 5; i++)
            {
                for (var j = i; j <= 5; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}
