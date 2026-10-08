using System;
using Agenda;

namespace Agenda
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}