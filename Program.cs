using System;

namespace Agenda
{
    class Program
    {
        static void Main(string[] args)
        {
            CargarDatosPrueba();

            string rosaPastel = "\x1b[38;2;255;182;193m";
            string reset = "\x1b[0m";

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine($"{rosaPastel}==================================");
                Console.WriteLine("   AGENDA 2º DAM (Menú Principal)   ");
                Console.WriteLine($"=================================={reset}");
                Console.WriteLine("1. Gestión de Personas");
                Console.WriteLine("2. Gestión de Empresas");
                Console.WriteLine("3. Salir");
                Console.WriteLine($"{rosaPastel}=================================={reset}");

                int opcion = Validador.LeerEntero("Seleccione una opción (1-3): ", 1, 3);

                switch (opcion)
                {
                    case 1:
                        GestionPersonas.MostrarMenu();
                        break;
                    case 2:
                        GestionEmpresas.MostrarMenu();
                        break;
                    case 3:
                        salir = true;
                        Console.WriteLine("\n¡Hasta pronto!");
                        break;
                }
            }
        }

        private static void CargarDatosPrueba() // Datos de prueba
        {
            // Alta de Empresas
            Empresa emp1 = new Empresa("Tech Solutions S.L.", "B12345678", "+34 911223344", "contacto@techsolutions.es", "Calle Mayor 12, Madrid");
            Empresa emp2 = new Empresa("Innovatech Digital", "A87654321", "+34 952001122", "info@innovatech.com", "Av. Andalucía 45, Málaga");
            GestionEmpresas.listaEmpresas.Add(emp1);
            GestionEmpresas.listaEmpresas.Add(emp2);

            // Alta de Personas vinculadas y no vinculadas
            GestionPersonas.listaPersonas.Add(new Persona("Carlos", "Gómez Pérez", "+34 612345678", "carlos@gmail.com", emp1));
            GestionPersonas.listaPersonas.Add(new Persona("Ana", "Martínez López", "+34 699887766", "ana.martinez@empresa.com", emp1));
            GestionPersonas.listaPersonas.Add(new Persona("Luis", "Silva Fernández", "+34 952123456", "luis.silva@consultoria.es", emp2));
            GestionPersonas.listaPersonas.Add(new Persona("Marta", "Ríos Castro", "+34 600112233", "marta.rios@email.com", null)); // Sin empresa
        }
    }
}