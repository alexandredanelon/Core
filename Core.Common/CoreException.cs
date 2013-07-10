using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class CoreException :  System.Exception
    {

        private int     _CheckPoint { get; set; }
        private string  _Operation { get; set; }
        private string _CustomMessage { get; set; }
        private string _stackTrace { get; set; }

        public CoreException(string operation, Int16 checkPoint, string msg, string custom, Exception e)
            : base(msg, e)
        {
            _CheckPoint = checkPoint;
            _Operation = operation;
            _CustomMessage = custom;
            _stackTrace  = e.StackTrace;
            //var monitor = MonitorClientFactory.Create();
            //monitor.TraceError(new monitor.service.MonitorData { OperationName = _Operation, TraceMessage = String.Format("Error in operation {0} \n on point {1} \n with message {2} \n and default {3}", _Operation, _CheckPoint, _CustomMessage, base.Message) + " >> " + _stackTrace });
        }

        public override string Message
        {
            get
            {  
                return _CustomMessage;
            }
        }
    

    }
}
