using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comics
{
    public class ComicCRUD : CRUD
    {
        public ComicCRUD(Contexto contexto) : base(contexto) { }
        public override bool create()
        {
            try
            {
                CategoriaCRUD catcrud = new CategoriaCRUD(_contexto);
                AutorCRUD autcrud = new AutorCRUD(_contexto);
                Console.WriteLine("Añadir Comic");
                Console.WriteLine("Introduzca el titulo");
                string titulo = Console.ReadLine();
                if (!checkName(titulo)) { return false; }
                Console.WriteLine("Introduzca la descripción");
                string descripcion = Console.ReadLine();
                Console.WriteLine("Introduzca la fecha");
                string year = Console.ReadLine();
                DateTime fecha;
                if (!DateTime.TryParse(year,out fecha))
                {
                    Console.WriteLine("Fecha incorrecta");
                    return false;
                }
                Console.WriteLine("Introduzca la categoría");
                string cat = Console.ReadLine();
                Categoria categoria = catcrud.FindAll(cat);
                if (categoria == null)
                {
                    Console.WriteLine("La categoría no existe");
                    return false;
                }
                Comic com = new Comic { Titulo = titulo, Descripcion = descripcion, Fecha=fecha, Categoria=categoria };
                _contexto.Comic.Add(com);
                _contexto.SaveChanges();
                Console.WriteLine("Comic creado con id: " + com.Id);
                do
                {
                    Console.WriteLine("Introduzca el autor (0) para salir");
                    string aut = Console.ReadLine();
                    if (aut=="0") { break; }
                    Autor autor = autcrud.FindAll(aut);
                    if (autor == null)
                    {
                        Console.WriteLine("El autor no existe");
                        return false;
                    }
                    Console.WriteLine("Introduzca el rol");
                    string rol=Console.ReadLine();
                    ComicAutor comaut = new ComicAutor { Autor = autor, Comic = com, Rol=rol };
                    _contexto.ComicAutor.Add(comaut);
                    _contexto.SaveChanges();
                } while (true);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }


        }

        public override bool delete()
        {
            try
            {
                Comic com;
                Console.WriteLine("Introduzca el Comic");
                string c = Console.ReadLine();
                com = FindAll(c);
                if (com == null)
                {
                    Console.WriteLine("el Comic no existe");
                }
                else
                {
                    Console.WriteLine("Eliminará el Comic " + com.Titulo);
                    Console.WriteLine("¿Está seguro?");
                    string res = Console.ReadLine();

                    if (res.ToLower() == "y")
                    {
                        _contexto.Comic.Remove(com);
                        _contexto.SaveChanges();
                    }


                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public override bool read()
        {
            try
            {
                Console.WriteLine("id\ttitulo\tdescripcion\tFecha\tCategoria");
                foreach (Comic com in _contexto.Comic.Include("Categoria"))
                {
                    Console.WriteLine($"{com.Id}\t{com.Titulo}\t{com.Descripcion}\t{com.Fecha}\t{com.Categoria.Nombre}");
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public override bool update()
        {
            try
            {
                CategoriaCRUD catcrud = new CategoriaCRUD(_contexto);
                Comic com;
                Console.WriteLine("Introduzca el Comic");
                string c = Console.ReadLine();
                com = FindAll(c);
                if (com == null)
                {
                    Console.WriteLine("el Comic no existe");
                }
                else
                {
                    Console.WriteLine("Introduzca el titulo");
                    string titulo = Console.ReadLine();
                   
                    Console.WriteLine("Introduzca la descripción");
                    string descripcion = Console.ReadLine();
                    Console.WriteLine("Introduzca la fecha");
                    string year = Console.ReadLine();
                    DateTime fecha=DateTime.Now;
                    if (year!="" && !DateTime.TryParse(year, out fecha))
                    {
                        Console.WriteLine("Fecha incorrecta");
                        return false;
                    }
                    Console.WriteLine("Introduzca la categoría");
                    string cat = Console.ReadLine();

                    Categoria categoria = catcrud.FindAll(cat);
                    if (cat != "" && categoria == null)
                    {
                        Console.WriteLine("La categoría no existe");
                        return false;
                    }

                    if (titulo != "")
                    {
                        com.Titulo = titulo;
                    }
                    if (descripcion != "")
                    {
                        com.Descripcion = descripcion;
                    }
                    if (year != "")
                    {
                        com.Fecha = fecha;
                    }
                    if (cat != "")
                    {
                        com.Categoria = categoria;
                    }
                    _contexto.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        private Comic FindAll(string com)
        {
            if (com.All(char.IsDigit))
            {
                return _contexto.Comic.Find(int.Parse(com));
            }
            else
            {
                return findByName(com);
            }
        }
        private Comic findByName(string titulo)
        {
            return _contexto.Comic.Where(c => c.Titulo == titulo).FirstOrDefault();
        }
        private bool checkName(string titulo)
        {
            if (titulo == "")
            {
                Console.WriteLine("El titulo no puede estar vacío");
                return false;
            }
            if (findByName(titulo) != null)
            {
                Console.WriteLine("Ya existe con ese titulo");
                return false;
            }
            return true;
        }
    }
}
