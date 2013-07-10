using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Data
{
    /// <summary>
    /// Utilizada para manipulação de dados
    /// </summary>
    public abstract class EntityManager : DataModel
    {
        #region Managers

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            using (var tm = new TransactionManager())
            {
                SaveEntity(this, tm);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public void Save(TransactionManager transaction)
        {
            SaveEntity(this, transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Remove()
        {
            using (var tm = new TransactionManager())
            {
                SaveEntity(this, tm);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public void Remove(TransactionManager transaction)
        {
            DeleteEntity(this, transaction);
        }

        #endregion

        #region Templates

        /// <summary>
        /// Implemnetação do retorno de um comando SQL::Insert /  Proc  de 
        /// acordo com as propriedades mapeadas na Entidade de Acordo 
        /// com as Tabelas do DB [ Neccessario para funcionamento do FindQuery ]
        /// Opcional :: Retornar o string BuildInsertHelper para montar o insert
        /// </summary>
        /// <returns></returns>
        protected internal abstract string SQLInsert();

        /// <summary>
        /// Implemnetação do retorno de um comando SQL:: / Update ou Proc  de 
        /// acordo com as propriedades mapeadas na Entidade de Acordo 
        /// com as Tabelas do DB [ Neccessario para funcionamento do FindQuery ]
        ///  Opcional :: Retornar o string BuildInsertHelper para montar o update
        /// </summary>
        /// <returns></returns>
        protected internal abstract string SQLUpdate();

        /// <summary>
        /// Implemnetação do retorno de um comando SQL::Delete  de 
        /// acordo com as propriedades mapeadas na Entidade de Acordo 
        /// com as Tabelas do DB [ Neccessario para funcionamento do FindQuery ]
        ///  Opcional :: Retornar o string BuildInsertHelper para montar o delete
        /// </summary>
        /// <returns></returns>
        protected internal abstract string SQLDelete();

        /// <summary>
        /// Escolha tipo de percistencia [Proc ou SQLAnscII
        /// </summary>
        /// <returns></returns>
        protected abstract internal ModelType ManagerType();

        #endregion

        #region Sql Helpers 

        protected internal string BuildInsertHelper(string targetTable)
        {
            if (this.ManagerType()==ModelType.Sql)
            {
                return Internal.SqlLanguageHelper.Insert(this, targetTable);
            }
            else
            {
                throw new Exception("tipo de Entidade não permitida");
            }
        }

        protected internal string BuildUpdateHelper(string targetTable)
        {
            if (this.ManagerType() == ModelType.Sql)
            {
                return Internal.SqlLanguageHelper.Update(this, targetTable);
            }
            else
            {
                throw new Exception("tipo de Entidade não permitida");
            }
        }

        protected internal string BuildDeleteHelper(string targetTable)
        {
            if (this.ManagerType() == ModelType.Sql)
            {
                return Internal.SqlLanguageHelper.Delete(this, targetTable);
            }
            else
            {
                throw new Exception("tipo de Entidade não permitida");
            }
        }

        #endregion

        #region Managers

        /// <summary>
        /// Responsavel por realizar a percistencia do objeto na entidade
        /// </summary>
        /// <param name="entityDataObject">Entidade Percistente</param>
        private void SaveEntity(DataModel entityDataObject, TransactionManager tm)
        {
           
            Int16 cp = 1;
            var sqlManupalation = SQLInsert();
            var propertyType = entityDataObject.GetType();
            try
            {
                foreach (var  key in entityDataObject.Keys())
                {
                  var value =   propertyType.GetProperty(key.Key).GetValue(entityDataObject, null);
                  if (value != null)
                  {
                      sqlManupalation = SQLUpdate();
                      return;
                  }
                }
                
                var sqlCommand
                    = new SqlCommand(sqlManupalation, tm.Connection);
                if (this.ManagerType() == ModelType.Proc)
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    var procData =  entityDataObject;  

                    foreach (var param in procData.ParametersOrColumns())
                    {
                       var valueData 
                           = propertyType.GetProperty(param.Key).GetValue(entityDataObject, null);

                       sqlCommand.Parameters.Add(new SqlParameter
                                                   {
                                                       ParameterName = String.Format("@{0}", param.Value), 
                                                       Value = valueData
                                                   });
                    }
 
                }
                else
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                }
                cp = 2;
                if (tm.HasTransactionScope)
                {
                    sqlCommand.Transaction = tm.Transaction;
                }
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                tm.Transaction.Rollback();
                tm.Dispose();
                throw new CoreException("Entity.Save", cp, e.Message, sqlManupalation, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityDataObject"></param>
        /// <param name="tm"></param>
        private void DeleteEntity(DataModel entityDataObject, TransactionManager tm)
        {
            var cmmSql = string.Empty;
            Int16 cp = 1;
            try
            {
                cmmSql = SQLDelete();
                var sqlCommand
                    = new SqlCommand(cmmSql, tm.Connection);
                cp = 2;
                if (tm.HasTransactionScope)
                {
                    sqlCommand.Transaction = tm.Transaction;
                }
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                tm.Transaction.Rollback();
                tm.Dispose();
                throw new CoreException("Entity.Remove", cp, ex.Message, cmmSql, ex);
            }
        }

        #endregion

    }
}
