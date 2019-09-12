using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Enlaces
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString("https://amazon.es");
                Regex r = new Regex(@"<a.*?href=(""|')(?<href>.*?)(""|').*?>(?<value>.*?)</a>");

                foreach (Match match in r.Matches(s))
                {
                    Console.WriteLine(match.Groups["href"].Value+" | "+match.Groups["value"].Value);
                }
                    
            }
        }
    }
}
