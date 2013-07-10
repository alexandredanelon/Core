using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Teste
{
    public class CoreDataLOG : DataModel
    {

        #region Data
        /// <summary>
        /// Nome da Operacao / Metodo do Trace
        /// </summary>
        public String NomeOperacao { get; set; }

        /// <summary>
        /// Texto do trace
        /// </summary>
        public String TextMessageTrace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 TracePoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TraceType { get; set; }

        #endregion

        protected override Dictionary<string, string> Keys()
        {
            return new Dictionary<string, string>();
        }

        protected override Dictionary<string, string> ParametersOrColumns()
        {
            return new Dictionary<string, string>() 
            { 
                { "NomeOperacao", "oper_name" },
                { "TextMessageTrace", "message" },
                { "TracePoint", "trace_id" },
                { "TraceType", "trace_type" }
            };
        }
    }
}
