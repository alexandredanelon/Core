using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    /// <summary>
    /// Coordenador de Transações de entidades para o SQLServer
    ///  @arquiteto responsavel. Alexandre Alves Danelon -- aadanelon
    /// </summary>
    public class TransactionManager : IDisposable
    {
       internal SqlConnection Connection { get; set; }
       internal SqlTransaction Transaction { get; set; }

       internal bool HasTransactionScope { get; set; } 
             
       internal TransactionManager() {
           Connection = Internal.ConnectionManager.getContextDB();
           Connection.Open();
           HasTransactionScope = false;
       }

       /// <summary>
       /// inicia uma conexão especifica sem transaction
       /// </summary>
       /// <param name="connectionName"></param>
       public TransactionManager(string connectionName)
       {
           Connection = Internal.ConnectionManager.getContextDB(connectionName);
           Connection.Open();
           HasTransactionScope = false;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="name">Nome da Transação</param>
       /// <param name="connectionName">Configuração com Banco de dados correspondente</param>
       public TransactionManager(string name, string connectionName)
       {
           Connection = Internal.ConnectionManager.getContextDB(connectionName);
           Connection.Open();
           HasTransactionScope = true;
           Transaction = Connection.BeginTransaction(name);
       }


       public void Commit()
       {
           Transaction.Commit();
       }

       public void Rollback()
       {
           Transaction.Rollback();
       }

       public void Dispose()
       {
           Connection.Close();
       }
    }
}
