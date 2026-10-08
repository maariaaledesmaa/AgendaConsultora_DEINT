using System;

namespace Agenda
{
    public class Persona
    {
        private static int _contadorId = 1; // Contador para ir asignando los IDS a los usuarios

        public int IdPersona { get; private set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string EmpresaAsignada { get; set; }

        public Persona(string nombre, string apellidos, string telefono, string correo, string empresaAsignada)
        {
            IdPersona = _contadorId++; // Se va autoincrementando el ID
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Correo = correo;
            EmpresaAsignada = empresaAsignada;
        }

        public override string ToString()
        {
            return $"[ID: {IdPersona}] {Nombre} {Apellidos} | Tel: {Telefono} | Email: {Correo} | Empresa: {EmpresaAsignada}";
        }
    }
}