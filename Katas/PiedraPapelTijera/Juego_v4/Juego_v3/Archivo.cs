using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_v3
{
    class Archivo : IMostrar
    {
        private string _filename;
        private bool _append;
        public Archivo(string filename = "resultado.txt", bool append=true)
        {
            _filename = filename;
            _append = append;
        }
        public void mostrar(string res)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, _filename),_append);
            outputFile.WriteLine(res);
            outputFile.Close();
        }
    }
}
