using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormConsumirServicio
{
    public  class Estudiantes
    {
        public int IdEstudiante { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public Estudiantes() { }

        public Estudiantes(int IdEstudiante,
            string Matricula, string Nombre, string Apellido, string Telefono, string Direccion) 
        {
            this.IdEstudiante = IdEstudiante;
            this.Matricula = Matricula;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Telefono = Telefono;
            this.Direccion= Direccion;
        }
    }
}
