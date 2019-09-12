using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comics
{
    public class CategoriaCRUD : CRUD
    {
        public CategoriaCRUD(Contexto contexto) : base(contexto) { }
        public override bool create()
        {
            try
            {
                Console.WriteLine("Añadir categoría");
                Console.WriteLine("Introduzca el nombre");
                string nombre = Console.ReadLine();
                if (!checkName(nombre)) { return false; }
                Console.WriteLine("Introduzca la descripción");
                string descripcion = Console.ReadLine();
                Categoria cat = new Categoria { Nombre = nombre, Descripcion = descripcion };
                _contexto.Categoria.Add(cat);
                _contexto.SaveChanges();
                Console.WriteLine("Categoría creada con id: " + cat.Id);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }


        }

        public override bool delete()
        {
            try
            {
                Categoria cat;
                Console.WriteLine("Introduzca la categoría");
                string c = Console.ReadLine();
                cat = FindAll(c);
                if (cat == null)
                {
                    Console.WriteLine("La categoría no existe");
                }
                else
                {
                    Console.WriteLine("Eliminará la categoría " + cat.Nombre);
                    Console.WriteLine("¿Está seguro?");
                    string res = Console.ReadLine();

                    if (res.ToLower() == "y")
                    {
                        _contexto.Categoria.Remove(cat);
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
                Console.WriteLine("id\tNombre\t\t\tDescripción");
                foreach (Categoria cat in _contexto.Categoria)
                {
                    Console.WriteLine($"{cat.Id}\t{cat.Nombre,-50}\t{cat.Descripcion}");
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public override bool update()
        {
            try
            {
                Categoria cat;
                Console.WriteLine("Introduzca la categoría");
                string c = Console.ReadLine();
                cat = FindAll(c);
                if (cat == null)
                {
                    Console.WriteLine("La categoría no existe");
                }
                else
                {
                    Console.WriteLine("Introduzca el nombre");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Introduzca la descripción");
                    string descripcion = Console.ReadLine();
                    if (nombre != "")
                    {
                        cat.Nombre = nombre;
                    }
                    if (descripcion != "")
                    {
                        cat.Descripcion = descripcion;
                    }
                    _contexto.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        public Categoria FindAll(string cat)
        {
            if (cat.All(char.IsDigit))
            {
                return _contexto.Categoria.Find(int.Parse(cat));
            }
            else
            {
                return findByName(cat);
            }
        }
        public Categoria findByName(string nombre)
        {
            return _contexto.Categoria.Where(c => c.Nombre == nombre).FirstOrDefault();
        }
        private bool checkName(string nombre)
        {
            if (nombre == "")
            {
                Console.WriteLine("El nombre no puede estar vacío");
                return false;
            }
            if (findByName(nombre) != null)
            {
                Console.WriteLine("Ya existe con ese nombre");
                return false;
            }
            return true;
        }
    }
}
