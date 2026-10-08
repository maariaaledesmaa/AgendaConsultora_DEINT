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
                Console.WriteLine("5. Asignar o cambiar empresa");
                Console.WriteLine("6. Desvincular de empresa");
                Console.WriteLine("7. Dar de baja una persona");
                Console.WriteLine("8. Volver al menú principal");
                Console.WriteLine($"{rosaPastel}=================================={reset}");

                int opcion = Validador.LeerEntero("Seleccione una opción (1-8): ", 1, 8);

                Console.WriteLine();
                switch (opcion)
                {
                    case 1: AltaPersona(); break;
                    case 2: ListarPersonas(); break;
                    case 3: BuscarPersona(); break;
                    case 4: ModificarPersona(); break;
                    case 5: AsignarOCambiarEmpresa(); break;
                    case 6: DesvincularEmpresa(); break;
                    case 7: BajaPersona(); break;
                    case 8: salir = true; break;
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

            Empresa empresaSeleccionada = SeleccionarEmpresaOpcional();

            Persona nuevaPersona = new Persona(nombre, apellidos, telefono, correo, empresaSeleccionada);
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

            if (!Validador.ConfirmarAccion("\n¿Desea modificar los datos personales de este registro?"))
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            Console.WriteLine("\nIntroduce los nuevos datos (o repite los actuales si no deseas cambiarlos):");
            persona.Nombre = Validador.LeerTextoObligatorio($"Nuevo Nombre [{persona.Nombre}]: ");
            persona.Apellidos = Validador.LeerTextoObligatorio($"Nuevos Apellidos [{persona.Apellidos}]: ");
            persona.Telefono = Validador.LeerTelefonoValido($"Nuevo Teléfono [{persona.Telefono}]: ");
            persona.Correo = Validador.LeerCorreoValido($"Nuevo Correo [{persona.Correo}]: ");

            Console.WriteLine("\nDatos actualizados correctamente.");
        }

        // 5. ASIGNAR O CAMBIAR EMPRESA
        private static void AsignarOCambiarEmpresa()
        {
            Console.WriteLine("--- ASIGNAR O CAMBIAR EMPRESA ---");
            int id = Validador.LeerEntero("Ingrese ID de la persona: ", 1, int.MaxValue);
            var persona = listaPersonas.FirstOrDefault(p => p.IdPersona == id);

            if (persona == null)
            {
                Console.WriteLine("\nNo existe ninguna persona con ese ID.");
                return;
            }

            Console.WriteLine($"\nPersona seleccionada: {persona.Nombre} {persona.Apellidos}");
            Console.WriteLine($"Empresa actual: {(persona.Empresa != null ? persona.Empresa.NombreComercial : "Sin empresa")}");

            Empresa empresa = SeleccionarEmpresaObligatoria();
            if (empresa != null)
            {
                persona.IdEmpresa = empresa.IdEmpresa;
                persona.Empresa = empresa;
                Console.WriteLine($"\nEmpresa '{empresa.NombreComercial}' asignada correctamente a {persona.Nombre} {persona.Apellidos}.");
            }
        }

        // 6. DESVINCULAR EMPRESA
        private static void DesvincularEmpresa()
        {
            Console.WriteLine("--- DESVINCULAR PERSONA DE EMPRESA ---");
            int id = Validador.LeerEntero("Ingrese ID de la persona a desvincular: ", 1, int.MaxValue);
            var persona = listaPersonas.FirstOrDefault(p => p.IdPersona == id);

            if (persona == null)
            {
                Console.WriteLine("\nNo existe ninguna persona con ese ID.");
                return;
            }

            if (persona.Empresa == null)
            {
                Console.WriteLine($"\nLa persona {persona.Nombre} {persona.Apellidos} no tiene ninguna empresa asignada actualmente.");
                return;
            }

            Console.WriteLine($"\nRegistro afectado: {persona}");

            if (Validador.ConfirmarAccion($"¿Desea desvincular a {persona.Nombre} {persona.Apellidos} de {persona.Empresa.NombreComercial}?"))
            {
                persona.IdEmpresa = null;
                persona.Empresa = null;
                Console.WriteLine("Persona desvinculada correctamente. Sus datos personales se han conservado.");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }

        // 7. BAJA
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

        // MÉTODOS AUXILIARES PARA SELECCIÓN DE EMPRESAS
        private static Empresa SeleccionarEmpresaOpcional()
        {
            var empresasActivas = GestionEmpresas.listaEmpresas.Where(e => e.Activo).ToList();

            if (!empresasActivas.Any())
            {
                Console.WriteLine("\n(Nota: No hay empresas activas registradas. La persona se creará sin empresa asignada).");
                return null;
            }

            if (!Validador.ConfirmarAccion("¿Desea asignar esta persona a una empresa existente?"))
            {
                return null;
            }

            return SeleccionarEmpresaObligatoria();
        }

        private static Empresa SeleccionarEmpresaObligatoria()
        {
            var empresasActivas = GestionEmpresas.listaEmpresas.Where(e => e.Activo).ToList();

            if (!empresasActivas.Any())
            {
                Console.WriteLine("\nNo hay empresas activas registradas en el sistema.");
                return null;
            }

            Console.WriteLine("\n--- EMPRESAS ACTIVAS DISPONIBLES ---");
            foreach (var e in empresasActivas)
            {
                Console.WriteLine($"[ID: {e.IdEmpresa}] {e.NombreComercial} (CIF: {e.Cif})");
            }

            while (true)
            {
                int idEmpresa = Validador.LeerEntero("Ingrese el ID de la empresa a seleccionar: ", 1, int.MaxValue);
                var empresa = empresasActivas.FirstOrDefault(e => e.IdEmpresa == idEmpresa);

                if (empresa != null)
                {
                    return empresa;
                }

                Console.WriteLine("ID no válido o perteneciente a una empresa inactiva. Intente de nuevo.");
            }
        }
    }
}