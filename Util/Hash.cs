using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Util
{
    public static class Hash
    {
        private const int _tamSal = 16;

        /// <summary>
        /// Aplica la funcion hash a una cadena utilizando el algoritmo SHA256
        /// </summary>
        /// <param name="cadena">cadena a la que se le aplica el hash</param>
        /// <param name="sal">cadena de bytes que constituyen la sal, puede ser null</param>
        /// <returns>una cadena de bytes que consta del hash calculado con sha256 + sal</returns>
        public static byte[] SHA256ConSal(string cadena, byte[] sal)
        {
            if (cadena == null)
            {
                throw new ArgumentNullException();
            }

            // Entrada es una cadena de bytes correspondiente con la cadena ingresada
            // la sal es una cadena generada de manera aleatoria que se concatena con la entrada
            // para evitar los ataques por diccionario
            // entradaConSal es la concatenacion de la entrada y la sal
            // hash es un objeto que contiene la funcionalidad necesaria para aplicarle la funcion hash
            // a la entradaConSal
            if (sal == null)
            {
                sal = new byte[_tamSal];
                // generamos la sal
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {

                    rng.GetNonZeroBytes(sal);
                }
            }

            byte[] entrada = Encoding.UTF8.GetBytes(cadena);
            byte[] entradaConSal = new byte[entrada.Length + sal.Length];
            byte[] resultado;
            byte[] resultadoConSal;
            SHA256Managed hash = new SHA256Managed();

            // copiamos la entrada al inicio de entradaConSal
            for (int i = 0; i < entrada.Length; ++i)
            {
                entradaConSal[i] = entrada[i];
            }

            // copiamos la sal al final de entradaConSal
            for (int i = 0; i < sal.Length; ++i)
            {
                entradaConSal[i + entrada.Length] = sal[i];
            }

            resultado = hash.ComputeHash(entradaConSal);

            resultadoConSal = new byte[resultado.Length + sal.Length];

            // copiamos el resultado a resultadoConSal
            for (int i = 0; i < resultado.Length; ++i)
            {
                resultadoConSal[i] = resultado[i];
            }

            // copiamos la sal al resultado
            for (int i = 0; i < sal.Length; ++i)
            {
                resultadoConSal[i + resultado.Length] = sal[i];
            }

            return resultadoConSal;
        }

        /// <summary>
        /// Dado un hash y una cadena, determina si el hash puede ser obtenido utilizando la cadena dada
        /// </summary>
        /// <param name="hash">cadena de bytes a comparar</param>
        /// <param name="cadena">cadena a la cual se le aplicara el hash para comparar</param>
        /// <returns>true si la se puede obtener el hash con la cadena ingresada</returns>
        public static bool VerificarHash(byte[] hash, string cadena)
        {
            byte[] sal = new byte[_tamSal];
            byte[] resultado;

            // obtenemos la sal del hash (el hash consta del hash calculado con sha256 + sal)
            for (int i = 0; i < sal.Length; ++i)
            {
                sal[i] = hash[SHA256.Create().HashSize / 8 + i];
            }

            // calculamos el hash con la cadena y la sal obtenida
            resultado = SHA256ConSal(cadena, sal);

            return (resultado.SequenceEqual(hash));
        }
    }
}
