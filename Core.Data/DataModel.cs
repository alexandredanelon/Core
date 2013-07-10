using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public enum ModelType
    {
        Proc = 0x1,
        Sql = 0x0
    }

    public abstract class DataModel
    {
        /// <summary>
        /// Propriedade de mapeamento entre *chaves da tabela* necessarias para materialização e *propriedade da tabela*
        /// </summary>
        protected abstract internal Dictionary<string, string> Keys();

        /// <summary>
        /// Propriedade de mapeamento entre *coluna do tabela* e *propriedade da entidade*
        /// </summary>
        protected abstract internal Dictionary<string, string> ParametersOrColumns();
   
    }
}
