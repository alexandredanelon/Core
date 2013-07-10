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
    public abstract class EntityProcModel : DataModel
    {
    
        #region Utilitario de Mapeamento


        /// <summary>
        /// Propriedade de mapeamento entre *coluna do tabela* e *propriedade da entidade*
        /// </summary>
        protected abstract internal Dictionary<string, string> Parameters();

  
        #endregion
    }
}
