using System;

namespace Agenda
{
    public class Empresa
    {
        private static int _contadorId = 1; // Contador para ir asignando los IDS a las empresas

        public int IdEmpresa { get; private set; }
        public string NombreComercial { get; set; }
        public string Cif { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; } // Borrado lógico: true (1), false (0)

        public Empresa(string nombreComercial, string cif, string telefono, string correo, string direccion)
        {
            IdEmpresa = _contadorId++; // Se va autoincrementando el ID
            NombreComercial = nombreComercial;
            Cif = cif;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
            Activo = true;
        }

        public override string ToString()
        {
            return $"[ID: {IdEmpresa}] {NombreComercial} | CIF: {Cif} | Tel: {Telefono} | Email: {Correo} | Dir: {Direccion}";
        }
    }
}