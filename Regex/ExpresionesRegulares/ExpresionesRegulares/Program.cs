using System;
using System.Text.RegularExpressions;

namespace ExpresionesRegulares
{
    class Program
    {
        static void Main(string[] args)
        {
            string texto = "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero, adarga antigua, rocín flaco y galgo corredor. Una olla de algo más vaca que carnero, salpicón las más noches, duelos y quebrantos los sábados, lantejas los viernes, algún palomino de añadidura los domingos, consumían las tres partes de su hacienda. El resto della concluían sayo de velarte, calzas de velludo para las fiestas, con sus pantuflos de lo mesmo, y los días de entresemana se honraba con su vellorí de lo más fino. Tenía en su casa una ama que pasaba de los cuarenta y una sobrina que no llegaba a los veinte, y un mozo de campo y plaza que así ensillaba el rocín como tomaba la podadera. Frisaba la edad de nuestro hidalgo con los cincuenta años. Era de complexión recia, seco de carnes, enjuto de rostro, gran madrugador y amigo de la caza. Quieren decir que tenía el sobrenombre de «Quijada», o «Quesada», que en esto hay alguna diferencia en los autores que deste caso escriben, aunque por conjeturas verisímilesII se deja entender que se llamaba «Quijana». Pero esto importa poco a nuestro cuento: basta que en la narración dél no se salga un punto de la verdad.";

             string patron = "no";
            MatchCollection matches = Regex.Matches(texto, patron,
              RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            WriteMatches(texto, matches);

             patron = @"no\b";
             matches = Regex.Matches(texto, patron,
              RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            WriteMatches(texto, matches);

            patron = @"\bno\b";
            matches = Regex.Matches(texto, patron,
             RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            WriteMatches(texto, matches);

            patron = @"\w+";
            matches = Regex.Matches(texto, patron,
             RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            WriteMatches(texto, matches);

            patron = @"\bm\w+";
            matches = Regex.Matches(texto, patron,
             RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            WriteMatches(texto, matches);
        }
        static void WriteMatches(string texto, MatchCollection matches)
        {
            Console.WriteLine("Apariciones encontradas: " + matches.Count);
            foreach (Match m in matches)
            {
                int index = m.Index;
                string result = m.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = texto.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;
                Console.WriteLine($"Posición: {index} | \tValor: {m.Value}\t | {texto.Substring(index - charsBefore, charsToDisplay)}");
            }
        }
    }
}
