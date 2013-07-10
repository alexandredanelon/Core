using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Teste
{
    public class CoreDataLOGKey : Data.EntityKey {
    }

   public class CoreDataLOGCollection : Data.Collection<CoreDataLOGKey, CoreDataLOG>
    {
       protected override string Query(CoreDataLOGKey objectKey)
       {
           var doQuery = new StringBuilder();

           doQuery.Append("SELECT oper_name ");
           doQuery.Append(",message ");
           doQuery.Append(",trace_id ");
           doQuery.Append(",trace_type ");
           doQuery.Append("FROM dbo.trace_monitor ");

           return doQuery.ToString();
       }
    }
}
