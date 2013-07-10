using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;

namespace Core.Data.Teste
{
    public class VeiculoKey : EntityKey {
    
    }

    public class VeiculoCollection : Collection<Veiculo, VeiculoKey>
    {
        protected override string Query(Veiculo objectKey)
        {
            return "PROC_SELECT";
        }
    }
}
