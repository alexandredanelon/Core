using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;

namespace Core.Data.Teste
{
    public class Veiculo : EntityManager
    {

        protected override string SQLInsert()
        {
            //base.BuildInsertHelper("TB1");
            return "PROC_INSERT";
        }

        protected override string SQLUpdate()
        {
            throw new NotImplementedException();
        }

        protected override string SQLDelete()
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, string> Keys()
        {
          return  new Dictionary<string, string>() { {"", ""} };
        }

        protected override Dictionary<string, string> ParametersOrColumns()
        {
            return new Dictionary<string, string>() { { "", "" } };
        }

        protected override ModelType ManagerType()
        {
            return ModelType.Proc;
        }
    }
}
