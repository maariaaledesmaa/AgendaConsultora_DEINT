using System;
using System.Text.RegularExpressions;

namespace Agenda
{
    public static class Validador // Comprobar las entradas del usuario por consola
    {
        public static int LeerEntero(string mensaje, int min, int max) // Número entre MIN y MAX
        {
            int opcion;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim();

                if (int.TryParse(entrada, out opcion) && opcion >= min && opcion <= max)
                {
                    return opcion;
                }
                
                Console.WriteLine($"> ERROR: Introduce un número entero entre {min} y {max}.");
            }
        }

        public static string LeerTextoObligatorio(string mensaje) // No puede estar vacío
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada;
                }

                Console.WriteLine("> ERROR: Este campo no puede estar vacío.");
            }
        }

        public static string LeerTelefonoValido(string mensaje) // Permitir solo los números/símbolos válidos para un teléfono y que no esté vacío
        {
            while (true)
            {
                string entrada = LeerTextoObligatorio(mensaje);
                // Permite números, espacios, prefijos '+' y guiones (mínimo 7 dígitos)
                if (Regex.IsMatch(entrada, @"^\+?[0-9\s\-]{7,15}$"))
                {
                    return entrada;
                }

                Console.WriteLine("> ERROR: Teléfono inválido. (Ejemplos: +34 612345678 o 952123456).");
            }
        }

        public static string LeerCorreoValido(string mensaje) // Comprobando el formato del correo
        {
            while (true)
            {
                string entrada = LeerTextoObligatorio(mensaje);
                // Comprobación de formato estándar usuario@dominio.ext
                if (Regex.IsMatch(entrada, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    return entrada;
                }

                Console.WriteLine("> ERROR: Formato de correo incorrecto. (Ejemplo: nombre@dominio.com)");
            }
        }

        public static bool ConfirmarAccion(string mensaje) // Confirmación por consola
        {
            while (true)
            {
                Console.Write($"{mensaje} (S/N): ");
                string respuesta = Console.ReadLine()?.Trim().ToUpper();

                if (respuesta == "S") return true;
                if (respuesta == "N") return false;

                Console.WriteLine("> ERROR: Responde únicamente con 'S' (Sí) o 'N' (No).");
            }
        }
    }
}