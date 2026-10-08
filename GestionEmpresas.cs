using System;
using System.Collections.Generic;
using System.Linq;

namespace Agenda
{
    public static class GestionEmpresas
    {
        public static List<Empresa> listaEmpresas = new List<Empresa>();

        public static void MostrarMenu()
        {
            string rosaPastel = "\x1b[38;2;255;182;193m";
            string reset = "\x1b[0m";

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine($"{rosaPastel}==================================");
                Console.WriteLine("   AGENDA 2º DAM (Gestión Empresas) ");
                Console.WriteLine($"=================================={reset}");
                Console.WriteLine("1. Dar de alta una empresa");
                Console.WriteLine("2. Listar todas las empresas activas");
                Console.WriteLine("3. Buscar empresa");
                Console.WriteLine("4. Ver plantilla de personas de una empresa");
                Console.WriteLine("5. Modificar empresa");
                Console.WriteLine("6. Dar de baja una empresa");
                Console.WriteLine("7. Volver al menú principal");
                Console.WriteLine($"{rosaPastel}=================================={reset}");

                int opcion = Validador.LeerEntero("Seleccione una opción (1-7): ", 1, 7);

                Console.WriteLine();
                switch (opcion)
                {
                    case 1: AltaEmpresa(); break;
                    case 2: ListarEmpresas(); break;
                    case 3: BuscarEmpresa(); break;
                    case 4: VerPersonasDeEmpresa(); break;
                    case 5: ModificarEmpresa(); break;
                    case 6: BajaEmpresa(); break;
                    case 7: salir = true; break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // 1. ALTA
        private static void AltaEmpresa()
        {
            Console.WriteLine("--- ALTA DE NUEVA EMPRESA ---");
            string nombre = Validador.LeerTextoObligatorio("Nombre Comercial: ");
            string cif = Validador.LeerTextoObligatorio("CIF: ");

            if (listaEmpresas.Any(e => e.Activo && e.Cif.Equals(cif, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"\nYa existe una empresa activa registrada con el CIF: {cif}");
                return;
            }

            string telefono = Validador.LeerTelefonoValido("Teléfono: ");
            string correo = Validador.LeerCorreoValido("Correo electrónico: ");
            string direccion = Validador.LeerTextoObligatorio("Dirección: ");

            Empresa nuevaEmpresa = new Empresa(nombre, cif, telefono, correo, direccion);
            listaEmpresas.Add(nuevaEmpresa);

            Console.WriteLine($"\nEmpresa dada de alta correctamente con ID: {nuevaEmpresa.IdEmpresa}");
        }

        // 2. LISTADO (Solo activas)
        private static void ListarEmpresas()
        {
            Console.WriteLine("--- LISTADO DE EMPRESAS ACTIVAS ---");
            var activas = listaEmpresas.Where(e => e.Activo).OrderBy(e => e.IdEmpresa).ToList();

            if (activas.Count == 0)
            {
                Console.WriteLine("No hay empresas activas registradas en el sistema.");
                return;
            }

            foreach (var empresa in activas)
            {
                int cantidadPersonas = GestionPersonas.listaPersonas.Count(p => p.IdEmpresa == empresa.IdEmpresa);
                Console.WriteLine($"{empresa} | Plantilla: {cantidadPersonas} persona(s)");
            }
        }

        // 3. BÚSQUEDA
        private static void BuscarEmpresa()
        {
            Console.WriteLine("--- BÚSQUEDA DE EMPRESAS ---");
            Console.WriteLine("1. Buscar por ID");
            Console.WriteLine("2. Buscar por Nombre Comercial o CIF");

            int criterio = Validador.LeerEntero("Seleccione criterio (1-2): ", 1, 2);

            if (criterio == 1)
            {
                int id = Validador.LeerEntero("Ingrese ID a buscar: ", 1, int.MaxValue);
                var empresa = listaEmpresas.FirstOrDefault(e => e.Activo && e.IdEmpresa == id);

                if (empresa != null)
                {
                    Console.WriteLine("\nRegistro encontrado:");
                    Console.WriteLine(empresa);
                }
                else
                {
                    Console.WriteLine("\nNo se encontró ninguna empresa activa con ese ID.");
                }
            }
            else
            {
                string texto = Validador.LeerTextoObligatorio("Ingrese texto a buscar: ").ToLower();
                var resultados = listaEmpresas.Where(e => e.Activo && (
                    e.NombreComercial.ToLower().Contains(texto) ||
                    e.Cif.ToLower().Contains(texto)
                )).ToList();

                if (resultados.Any())
                {
                    Console.WriteLine($"\nResultados encontrados ({resultados.Count}):");
                    foreach (var e in resultados)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    Console.WriteLine("\nNo se encontraron coincidencias en empresas activas.");
                }
            }
        }

        // 4. VER PERSONAS DE UNA EMPRESA
        private static void VerPersonasDeEmpresa()
        {
            Console.WriteLine("--- CONSULTAR PLANTILLA DE PERSONAS ---");
            int id = Validador.LeerEntero("Ingrese ID de la empresa: ", 1, int.MaxValue);
            var empresa = listaEmpresas.FirstOrDefault(e => e.Activo && e.IdEmpresa == id);

            if (empresa == null)
            {
                Console.WriteLine("\nNo existe ninguna empresa activa con ese ID.");
                return;
            }

            var personasAsociadas = GestionPersonas.listaPersonas.Where(p => p.IdEmpresa == empresa.IdEmpresa).ToList();

            Console.WriteLine($"\nEmpresa: {empresa.NombreComercial} (CIF: {empresa.Cif})");
            Console.WriteLine($"Total personas vinculadas: {personasAsociadas.Count}");
            Console.WriteLine("--------------------------------------------------");

            if (!personasAsociadas.Any())
            {
                Console.WriteLine("Esta empresa no tiene personas vinculadas actualmente.");
                return;
            }

            foreach (var p in personasAsociadas)
            {
                Console.WriteLine($"[ID Persona: {p.IdPersona}] {p.Nombre} {p.Apellidos} | Tel: {p.Telefono} | Email: {p.Correo}");
            }
        }

        // 5. MODIFICACIÓN
        private static void ModificarEmpresa()
        {
            Console.WriteLine("--- MODIFICAR EMPRESA ---");
            int id = Validador.LeerEntero("Ingrese ID de la empresa a modificar: ", 1, int.MaxValue);
            var empresa = listaEmpresas.FirstOrDefault(e => e.Activo && e.IdEmpresa == id);

            if (empresa == null)
            {
                Console.WriteLine("\nNo existe ninguna empresa activa con ese ID.");
                return;
            }

            Console.WriteLine("\nRegistro actual:");
            Console.WriteLine(empresa);

            if (!Validador.ConfirmarAccion("\n¿Desea modificar los datos de esta empresa?"))
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            Console.WriteLine("\nIntroduce los nuevos datos (o repite los actuales si no deseas cambiarlos):");
            empresa.NombreComercial = Validador.LeerTextoObligatorio($"Nuevo Nombre Comercial [{empresa.NombreComercial}]: ");
            empresa.Cif = Validador.LeerTextoObligatorio($"Nuevo CIF [{empresa.Cif}]: ");
            empresa.Telefono = Validador.LeerTelefonoValido($"Nuevo Teléfono [{empresa.Telefono}]: ");
            empresa.Correo = Validador.LeerCorreoValido($"Nuevo Correo [{empresa.Correo}]: ");
            empresa.Direccion = Validador.LeerTextoObligatorio($"Nueva Dirección [{empresa.Direccion}]: ");

            Console.WriteLine("\nDatos actualizados correctamente.");
        }

        // 6. BAJA (Con control de integridad referencial)
        private static void BajaEmpresa()
        {
            Console.WriteLine("--- BAJA DE EMPRESA ---");
            int id = Validador.LeerEntero("Ingrese ID de la empresa a eliminar: ", 1, int.MaxValue);
            var empresa = listaEmpresas.FirstOrDefault(e => e.Activo && e.IdEmpresa == id);

            if (empresa == null)
            {
                Console.WriteLine("\nNo existe ninguna empresa activa con ese ID.");
                return;
            }

            // Validación de Integridad Referencial
            var personasVinculadas = GestionPersonas.listaPersonas.Where(p => p.IdEmpresa == empresa.IdEmpresa).ToList();
            if (personasVinculadas.Any())
            {
                Console.WriteLine($"\nNO SE PUEDE DAR DE BAJA ESTA EMPRESA.");
                Console.WriteLine($"Tiene {personasVinculadas.Count} persona(s) asociada(s) actualmente.");
                Console.WriteLine("Política de seguridad: Debe desvincular o reasignar a las personas antes de dar de baja la empresa.");
                return;
            }

            Console.WriteLine("\nRegistro afectado:");
            Console.WriteLine(empresa);

            if (Validador.ConfirmarAccion("¿Está seguro de que desea eliminar este registro?"))
            {
                empresa.Activo = false;
                Console.WriteLine("Registro eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }
    }
}