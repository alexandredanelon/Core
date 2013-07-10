using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    /// <summary>
    /// @Doc Interface comum de mapeamento entre tabelas
    /// do DB e Entidades da Aplicação
    /// 
    /// @arquiteto responsavel. Alexandre Alves Danelon -- aadanelon
    /// </summary>
    internal abstract class EntityDataModel : DataModel
    {
    
        #region Utilitario de Mapeament

        /// <summary>
        /// propriedade de mapeamento entre tabela do *DataBase* e *Entitade*
        /// </summary>
        protected abstract internal string Table();

        /// <summary>
        /// Propriedade de mapeamento entre *coluna do tabela* e *propriedade da entidade*
        /// </summary>
        protected abstract internal Dictionary<string, string> Columns();

      
        #endregion
    }
}
