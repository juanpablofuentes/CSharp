using first.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace first
{
    class Program
    {
        static void Main(string[] args)
        {
            escuelaContext context = new escuelaContext();
            var lista = from al in context.Alumno where true;
        }
        static void cursoAlumnos(string nombre, List<int> alumnos)
        {
            using (var context = new escuelaContext())
            {
                Curso c = new Curso() { Nombre = nombre };
                Alumno al;

                context.Curso.Add(c);
                foreach (int i in alumnos)
                {
                    al = context.Alumno.Find(i);
                    al.Curso = c;
                }
                //Opción alternativa
                //foreach (int i in alumnos)
                //{
                //    al = context.Alumno.Find(i);
                //    c.Alumno.Add(al);
                //}
                context.SaveChanges();
            }
        }
        static void buscarAlumnosSQL(string nombre)
        {
            using (var context = new escuelaContext())
            {
                var res = context.Alumno.FromSql("select * from alumno where nombre like '%" + nombre + "%'");

                foreach (Alumno al in res)
                {
                    Console.WriteLine(al.Nombre);
                }
            }
        }
        static void buscarAlumnos(string nombre)
        {
            using (var context = new escuelaContext())
            {
                var res = context.Alumno.Where(el => el.Nombre.Contains(nombre))
                     .Include(el => el.Curso);
                foreach (Alumno al in res)
                {
                    Console.WriteLine(al.Nombre);
                }
            }
        }
        static void buscarAlumnosCurso(string nombre)
        {
            using (var context = new escuelaContext())
            {
                var res = context.Alumno.Where(el => el.Nombre.Contains(nombre));
                foreach (Alumno al in res)
                {
                    Console.WriteLine($"{al.Nombre} - {al.Curso?.Nombre}");
                }
            }
        }
        static void buscarAlumnosCursoProfesor(string nombre)
        {
            using (var context = new escuelaContext())
            {
                var res = context.Alumno.Where(el => el.Nombre.Contains(nombre))
                   .Include(el => el.Curso).ThenInclude(c => c.Profesor);
                foreach (Alumno al in res)
                {
                    string p = al.Curso?.Profesor.Count > 0 ? String.Join(",", al.Curso?.Profesor) : "";
                    Console.WriteLine($"{al.Nombre} - {al.Curso?.Nombre} - {p}");
                }
            }
        }
        static void insertarProfesor(string nombre)
        {
            using (var context = new escuelaContext())
            {
                Profesor p = new Profesor() { Nombre = nombre };
                context.Profesor.Add(p);
                context.SaveChanges();
            }
        }
        static void cambiaNombre(string viejo, string nuevo)
        {
            using (var context = new escuelaContext())
            {
                Alumno alum = context.Alumno.Where(al => al.Nombre == viejo).FirstOrDefault();
                if (alum != null)
                {
                    alum.Nombre = nuevo;
                    context.SaveChanges();
                }
            }
        }
        static void borraAlumno(int idalumno)
        {
            using (var context = new escuelaContext())
            {
                Alumno al = context.Alumno.Find(idalumno);
                if (al != null)
                {
                    context.Alumno.Remove(al);
                    context.SaveChanges();
                }
            }
        }
        static void agregarAlumnos(string nombre, int cantidad)
        {
            using (var context = new escuelaContext())
            {

                for (int i = 0; i < cantidad; i++)
                {
                    context.Alumno.Add(new Alumno(nombre + i));
                }


                context.SaveChanges();
            }
        }
    }
}
