using System;
using System.Collections.Generic;
using System.Linq;

namespace Agenda
{
    public static class GestionPersonas
    {
        public static List<Persona> listaPersonas = new List<Persona>();

        public static void MostrarMenu()
        {
            string rosaPastel = "\x1b[38;2;255;182;193m";
            string reset = "\x1b[0m";

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine($"{rosaPastel}==================================");
                Console.WriteLine("   AGENDA 2º DAM (Gestión Personas) ");
                Console.WriteLine($"=================================={reset}");
                Console.WriteLine("1. Dar de alta una persona");
                Console.WriteLine("2. Listar todas las personas");
                Console.WriteLine("3. Buscar persona");
                Console.WriteLine("4. Modificar persona");
                Console.WriteLine("5. Dar de baja una persona");
                Console.WriteLine("6. Volver al menú principal");
                Console.WriteLine($"{rosaPastel}=================================={reset}");

                int opcion = Validador.LeerEntero("Seleccione una opción (1-6): ", 1, 6);

                Console.WriteLine();
                switch (opcion)
                {
                    case 1: AltaPersona(); break;
                    case 2: ListarPersonas(); break;
                    case 3: BuscarPersona(); break;
                    case 4: ModificarPersona(); break;
                    case 5: BajaPersona(); break;
                    case 6: salir = true; break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // 1. ALTA
        private static void AltaPersona()
        {
            Console.WriteLine("--- ALTA DE NUEVA PERSONA ---");
            string nombre = Validador.LeerTextoObligatorio("Nombre: ");
            string apellidos = Validador.LeerTextoObligatorio("Apellidos: ");
            string telefono = Validador.LeerTelefonoValido("Teléfono: ");
            string correo = Validador.LeerCorreoValido("Correo electrónico: ");
            string empresa = Validador.LeerTextoObligatorio("Empresa asignada: ");

            Persona nuevaPersona = new Persona(nombre, apellidos, telefono, correo, empresa);
            listaPersonas.Add(nuevaPersona);

            Console.WriteLine($"\nPersona dada de alta correctamente con ID: {nuevaPersona.IdPersona}");
        }

        // 2. LISTADO
        private static void ListarPersonas()
        {
            Console.WriteLine("--- LISTADO DE PERSONAS ---");
            if (listaPersonas.Count == 0)
            {
                Console.WriteLine("No hay personas registradas en el sistema.");
                return;
            }

            foreach (var persona in listaPersonas.OrderBy(p => p.IdPersona))
            {
                Console.WriteLine(persona);
            }
        }

        // 3. BÚSQUEDA
        private static void BuscarPersona()
        {
            Console.WriteLine("--- BÚSQUEDA DE PERSONAS ---");
            Console.WriteLine("1. Buscar por ID");
            Console.WriteLine("2. Buscar por Texto (Nombre / Apellidos)");
            
            int criterio = Validador.LeerEntero("Seleccione criterio (1-2): ", 1, 2);

            if (criterio == 1)
            {
                int id = Validador.LeerEntero("Ingrese ID a buscar: ", 1, int.MaxValue);
                var persona = listaPersonas.FirstOrDefault(p => p.IdPersona == id);

                if (persona != null)
                {
                    Console.WriteLine("\nRegistro encontrado:");
                    Console.WriteLine(persona);
                }
                else
                {
                    Console.WriteLine("\nNo se encontró ninguna persona con ese ID.");
                }
            }
            else
            {
                string texto = Validador.LeerTextoObligatorio("Ingrese texto a buscar: ").ToLower();
                var resultados = listaPersonas.Where(p => 
                    p.Nombre.ToLower().Contains(texto) || 
                    p.Apellidos.ToLower().Contains(texto)
                ).ToList();

                if (resultados.Any())
                {
                    Console.WriteLine($"\nResultados encontrados ({resultados.Count}):");
                    foreach (var p in resultados)
                    {
                        Console.WriteLine(p);
                    }
                }
                else
                {
                    Console.WriteLine("\nNo se encontraron coincidencias en nombre o apellidos.");
                }
            }
        }

        // 4. MODIFICACIÓN
        private static void ModificarPersona()
        {
            Console.WriteLine("--- MODIFICAR PERSONA ---");
            int id = Validador.LeerEntero("Ingrese ID de la persona a modificar: ", 1, int.MaxValue);
            var persona = listaPersonas.FirstOrDefault(p => p.IdPersona == id);

            if (persona == null)
            {
                Console.WriteLine("\nNo existe ningún registro con ese ID.");
                return;
            }

            Console.WriteLine("\nRegistro actual:");
            Console.WriteLine(persona);

            if (!Validador.ConfirmarAccion("\n¿Desea modificar los datos de esta persona?"))
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            Console.WriteLine("\nIntroduce los nuevos datos (o repite los actuales si no deseas cambiarlos):");
            persona.Nombre = Validador.LeerTextoObligatorio($"Nuevo Nombre [{persona.Nombre}]: ");
            persona.Apellidos = Validador.LeerTextoObligatorio($"Nuevos Apellidos [{persona.Apellidos}]: ");
            persona.Telefono = Validador.LeerTelefonoValido($"Nuevo Teléfono [{persona.Telefono}]: ");
            persona.Correo = Validador.LeerCorreoValido($"Nuevo Correo [{persona.Correo}]: ");
            persona.EmpresaAsignada = Validador.LeerTextoObligatorio($"Nueva Empresa [{persona.EmpresaAsignada}]: ");

            Console.WriteLine("\nDatos actualizados correctamente.");
        }

        // 5. BAJA
        private static void BajaPersona()
        {
            Console.WriteLine("--- BAJA DE PERSONA ---");
            int id = Validador.LeerEntero("Ingrese ID de la persona a eliminar: ", 1, int.MaxValue);
            var persona = listaPersonas.FirstOrDefault(p => p.IdPersona == id);

            if (persona == null)
            {
                Console.WriteLine("\nNo existe ningún registro con ese ID.");
                return;
            }

            Console.WriteLine("\nRegistro afectado:");
            Console.WriteLine(persona);

            if (Validador.ConfirmarAccion("¿Está seguro de que desea eliminar este registro?"))
            {
                listaPersonas.Remove(persona);
                Console.WriteLine("Registro eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }
    }
}