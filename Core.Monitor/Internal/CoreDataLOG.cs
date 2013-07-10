using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Monitor.Internal
{
    public class CoreDataLOG : Data.EntityManager,  ITracer
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
        public short TracePoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TraceType { get; set; }

        #endregion

        #region Manager

        protected override string SQLInsert()
        {
            return "dbo.proc_ins_monitor";
        }

        protected override string SQLUpdate()
        {
            throw new NotImplementedException();
        }

        protected override string SQLDelete()
        {
            throw new NotImplementedException();
        }

        protected override Data.ModelType ManagerType()
        {
            return Data.ModelType.Proc;
        }

        protected override Dictionary<string, string> Keys()
        {
            return new Dictionary<string, string>();
        }

        protected override Dictionary<string, string> ParametersOrColumns()
        {
            return new Dictionary<string, string>() 
            { 
                { "NomeOperacao", "pname_oper" },
                { "TextMessageTrace", "pmessage" },
                { "TracePoint", "ptrace_id" },
                { "TraceType", "ptrace_type" }

            };
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="payload"></param>
        /// <returns>[TRUE]</returns>
        public bool WriteErrorEvent(string message, string payload)
        {
            var data = new CoreDataLOG() 
            { 
                NomeOperacao = payload, 
                TextMessageTrace = message, 
                TracePoint = 0, 
                TraceType = "ERROR" 
            };
            data.Save();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="payload"></param>
        /// <returns>[TRUE]</returns>
        public bool WriteInfoEvent(string message, string payload)
        {
            var data = new CoreDataLOG()
            {
                NomeOperacao = payload,
                TextMessageTrace = message,
                TracePoint = 0,
                TraceType = "INFO"
            };
            data.Save();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="payload"></param>
        /// <returns>[TRUE]</returns>
        public bool WriteWarningEvent(string message, string payload)
        {
            var data = new CoreDataLOG()
            {
                NomeOperacao = payload,
                TextMessageTrace = message,
                TracePoint = 0,
                TraceType = "WARN"
            };
            data.Save();
            return true;
        }
    }
}
