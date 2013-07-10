using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Internal
{
    /// <summary>
    /// Classe interna de gerenciamento de conexões SQLServer
    /// @arquiteto responsavel. Alexandre Alves Danelon -- aadanelon
    /// </summary>
    internal class ConnectionManager
    {
        private static SqlConnection _context;

        /// <summary>
        /// Obtem uma string de conexão SQLServer diante de uma chave de conexão especifica no Config do Serviço
        /// en-US Get a string connection to SQLServer Database from a key stored in Sevices configuration
        /// </summary>
        /// <param name="stringConnection">Key</param>
        /// <returns>string</returns>
        private static string GetDBehConfiguration(string stringConnection)
        {
            return ConfigurationManager.AppSettings[stringConnection].ToString();
        }

        /// <summary>
        /// Cria uma conexão SQLServer com base em uma configuração com a chave "EntityModel" 
        /// </summary>
        /// <returns></returns>
        internal static SqlConnection getContextDB()
        {
            _context = new SqlConnection(GetDBehConfiguration("EntityModel"));
            return _context;
        }

        /// <summary>
        /// Cria uma conexão SQLServer com base em uma configuração com a chave parametrizada
        /// </summary>
        /// <param name="stringConnection"></param>
        /// <returns></returns>
        public static SqlConnection getContextDB(string stringConnection)
        {
            _context = new SqlConnection(GetDBehConfiguration(stringConnection));
            return _context;
        }

    }
}
