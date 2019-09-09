using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comics
{
    public class AutorCRUD : CRUD
    {
        public AutorCRUD(Contexto contexto) : base(contexto) { }
        public override bool create()
        {
            try
            {
                Console.WriteLine("Añadir autor");
                Console.WriteLine("Introduzca el nombre");
                string nombre = Console.ReadLine();
                if (!checkName(nombre)) { return false; }
                Console.WriteLine("Introduzca la nacionalidad");
                string nacionalidad = Console.ReadLine();
                Console.WriteLine("Introduzca el año de nacimiento");
                string year = Console.ReadLine();
                if (!year.All(char.IsDigit))
                {
                    Console.WriteLine("El año tiene que ser numérico");
                    return false;
                }
                Autor aut = new Autor { Nombre = nombre, Nacionalidad = nacionalidad, AnyNacimiento=int.Parse(year) };
                _contexto.Autor.Add(aut);
                _contexto.SaveChanges();
                Console.WriteLine("Autor creado con id: " + aut.Id);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }


        }

        public override bool delete()
        {
            try
            {
                Autor aut;
                Console.WriteLine("Introduzca el autor");
                string c = Console.ReadLine();
                aut = FindAll(c);
                if (aut == null)
                {
                    Console.WriteLine("el autor no existe");
                }
                else
                {
                    Console.WriteLine("Eliminará el autor " + aut.Nombre);
                    Console.WriteLine("¿Está seguro?");
                    string res = Console.ReadLine();

                    if (res.ToLower() == "y")
                    {
                        _contexto.Autor.Remove(aut);
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
                Console.WriteLine("id\t\tNombre\t\t\tNacionalidad\t\t\tAño Nacimiento");
                foreach (Autor aut in _contexto.Autor)
                {
                    Console.WriteLine($"{aut.Id}\t\t{aut.Nombre}\t\t\t{aut.Nacionalidad}\t\t\t{aut.AnyNacimiento}");
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public override bool update()
        {
            try
            {
                Autor aut;
                Console.WriteLine("Introduzca el autor");
                string c = Console.ReadLine();
                aut = FindAll(c);
                if (aut == null)
                {
                    Console.WriteLine("el autor no existe");
                }
                else
                {
                    Console.WriteLine("Introduzca el nombre");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Introduzca la nacionalidad");
                    string nacionalidad = Console.ReadLine();
                    Console.WriteLine("Introduzca el año de nacimiento");
                    string year = Console.ReadLine();
                   
                    if (nombre != "")
                    {
                        aut.Nombre = nombre;
                    }
                    if (nacionalidad != "")
                    {
                        aut.Nacionalidad = nacionalidad;
                    }
                    if (year != "")
                    {
                        aut.AnyNacimiento = int.Parse(year);
                    }
                    _contexto.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        public Autor FindAll(string aut)
        {
            if (aut.All(char.IsDigit))
            {
                return _contexto.Autor.Find(int.Parse(aut));
            }
            else
            {
                return findByName(aut);
            }
        }
        private Autor findByName(string nombre)
        {
            return _contexto.Autor.Where(c => c.Nombre == nombre).FirstOrDefault();
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
