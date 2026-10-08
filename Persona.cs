using System;

namespace Agenda
{
    public class Persona
    {
        private static int _contadorId = 1;

        public int IdPersona { get; private set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        
        // Relación 1:N con Empresa
        public int? IdEmpresa { get; set; } // Puede ser null si no tiene empresa asignada
        public Empresa Empresa { get; set; } // Referencia al objeto Empresa

        public Persona(string nombre, string apellidos, string telefono, string correo, Empresa empresa = null)
        {
            IdPersona = _contadorId++;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Correo = correo;
            
            if (empresa != null)
            {
                IdEmpresa = empresa.IdEmpresa;
                Empresa = empresa;
            }
            else
            {
                IdEmpresa = null;
                Empresa = null;
            }
        }

        public override string ToString()
        {
            string nombreEmpresa = Empresa != null ? Empresa.NombreComercial : "Sin empresa asignada";
            return $"[ID: {IdPersona}] {Nombre} {Apellidos} | Tel: {Telefono} | Email: {Correo} | Empresa: {nombreEmpresa}";
        }
    }
}