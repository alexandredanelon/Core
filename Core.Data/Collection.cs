using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Data
{
    public abstract class Collection<K, T> :  List<T>
        where T : DataModel
        where K : EntityKey
    {


        /// <summary>
        /// Implemnetação do retorno de um comando SQL::Select de 
        /// acordo com as propriedades mapeadas na Entidade de Acordo 
        /// com as Tabelas do DB [ Neccessario para funcionamento do FindQuery ]
        /// </summary>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        protected internal abstract string Query(K objectKey);

        // <summary>
        /// Retorna uma coleção especializada com base em um objeto de buscas
        /// OBS.: Se faz necessario a implementação da interface IQueryable,
        /// na Collection especializada da CollectionDataManager para utilização do metodo
        /// </summary>
        /// <param name="objectKey">Objeto contendo propriedades da busca</param>
        /// <returns>ListOfEntityDataManager</returns>
        public List<T> Find(K objectKey)
        {
            var query = Query(objectKey);

            if (String.IsNullOrEmpty(query) && !query.StartsWith("SELECT")) { throw new Exception("FindQuery with :: " + query); }
            List<T> _lista = null;
            using (var tm = new TransactionManager())
            {
                _lista = MaterializerQuery(query, tm);
            }
            return _lista;
        }

        private static List<T> Reeder(ref Int16 cp, SqlCommand sqlCommand)
        {
            cp = 2;
            SqlDataReader dataReeder = sqlCommand.ExecuteReader();
            cp = 3;
            var typeOfCollection = typeof(List<T>);
            var contextError = string.Empty;
            List<T> listaRetorno = (List<T>)Activator.CreateInstance(typeOfCollection);
            cp = 3;
            try
            {
                while (dataReeder.Read())
                {
                    Type etityType = typeof(T);
                    ConstructorInfo con = etityType.GetConstructor(Type.EmptyTypes);
                    T entityMaterialized = (T)con.Invoke(new DataModel[] { });
                    cp = 4;
                    contextError = "Process begins to extract props";
                    foreach (var oProps in etityType.GetProperties())
                    {
                        //contextError = String.Format("Err in {0} ~> {1}", oProps.Name, entityMaterialized.Columns()[oProps.Name]);

                        var  dataValueReeder = dataReeder[entityMaterialized.ParametersOrColumns()[oProps.Name]];
                        switch (oProps.PropertyType.Name)
                        {
                            case "DateTime":
                                cp = 5;
                                var dateValue = dataValueReeder != DBNull.Value ? Convert.ToDateTime(dataValueReeder) : Convert.ToDateTime("01/01/0001 00:00:00");
                                dataValueReeder = dateValue;
                                break;

                            case "Int32":
                                var numberValue = dataValueReeder != DBNull.Value ? Convert.ToInt32(dataValueReeder) : 0;
                                dataValueReeder = numberValue;
                                break;

                            case "Int16":
                                var numberValue16 = dataValueReeder != DBNull.Value ? Convert.ToInt16(dataValueReeder) : 0;
                                dataValueReeder = numberValue16;
                                break;
                            case "Nullable`1":
                                var vlrNull = dataValueReeder != DBNull.Value ? Convert.ToDateTime(dataValueReeder) : Convert.ToDateTime("01/01/0001 00:00:00");
                                dataValueReeder = vlrNull;
                                break;
                            default:
                                break;
                        }

                        if (string.IsNullOrEmpty(dataValueReeder.ToString()))
                        { dataValueReeder = ""; }
                        contextError = String.Format("Try to set value on {0} with {1}",
                                                      oProps.Name,
                                                      dataValueReeder);
                        oProps.SetValue(entityMaterialized, dataValueReeder, null);
                    }
                    listaRetorno.Add(entityMaterialized);
                }
            }
            catch (Exception e)
            {
                throw new CoreException("Reeder", cp, contextError, "Internal", e);
            }
            return listaRetorno;
        }

        private static List<T> FetchData(TransactionManager tm, ref Int16 cp, ref string errorContext, string _sql)
        {
            errorContext = "Query Command " + _sql + "\n";
            SqlCommand sqlCommand
               = new SqlCommand(_sql, tm.Connection);
            errorContext = "Execute Command ";
            return Reeder(ref cp, sqlCommand);
        }

        private List<T> MaterializerQuery(string sqlQuery, TransactionManager tm)
        {
            var DEFAULT_CORE_CUSTOM_MSG = "NAO FOI POSSIVEL CARREGAR DADOS DO SERVIDOR (1102).";
            Int16 cp = 2;
            var errorContext = "Parse SQL";
            var errorContextSQL = "IN-SQL :: " + sqlQuery;
            try
            {
                return FetchData(tm, ref cp, ref errorContext, sqlQuery);
            }
            catch (Exception e)
            {
                tm.Dispose();
                throw new Core.Common.CoreException("Materializer", cp,
                    String.Concat(errorContext, " >> ", errorContextSQL, " ~ [", e.Message, "]"),
                    DEFAULT_CORE_CUSTOM_MSG, e);
            }

        }
    }
}
