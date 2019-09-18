using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContarAsincrono
{
    class Program
    {
        private static string[] URL = new string[] { "https://docs.microsoft.com/", "http://trifulcas.com",
                            "https://amazon.es","https://elpais.com"};
        private static int totalChar = 0;

        static void Main(string[] args)
        {
            Task[] tareas=new Task[URL.Length];
            for(int i=0;i<URL.Length;i++)
            {
                tareas[i] = DoSomethingAsync(URL[i]);
            }


            Task.WaitAll(tareas);
            Console.WriteLine("Total de letras: "+totalChar);
        }
        
        static async Task DoSomethingAsync(String url)
        {
            await GetStringAsync(url); // Esperando a que acabe
        }

        static async Task GetStringAsync(String url)
        {
            using (var httpClient = new HttpClient())
            {
                string result = await httpClient.GetStringAsync(url);
                
                Console.Write($"La url {url} tiene ");
                Console.WriteLine($" {result.Length} caracteres");
                totalChar += result.Length;
            }
        }
        
    }
}
