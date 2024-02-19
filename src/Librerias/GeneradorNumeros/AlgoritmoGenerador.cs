using System;

namespace GeneradorNumeros
{
    public class AlgoritmoGenerador
    {
        public static int[] GenerarNumerosAleatorios()
        {
            // Creamos una instancia de la clase Random
            Random rnd = new Random();

            // Creamos un array para almacenar los números aleatorios
            int[] numerosAleatorios = new int[9];

            // Generamos 9 números aleatorios y los almacenamos en el array
            for (int i = 0; i < 9; i++)
            {
                numerosAleatorios[i] = rnd.Next();
            }

            Console.WriteLine(numerosAleatorios);
            // Retornamos el array de números aleatorios como enteros
            return numerosAleatorios;
        }
    }
   }

