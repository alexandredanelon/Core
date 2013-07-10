using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using core = Core.Monitor;

namespace Core.Monitor.Service
{
    public class Monitor : MonitorContract
    {
        public void InfoTracer(MonitorData data)
        {
            var fullOperation = "." + data.OperationName;
            core.Monitor.InfoTracer(fullOperation, data.TraceMessage, data.TracePoint);
        }

        public void WarningTracer(MonitorData data)
        {
            var fullOperation = "." + data.OperationName;
            core.Monitor.InfoWaring(fullOperation, data.TraceMessage, data.TracePoint);
        }

        public void ErrorTracer(MonitorData data)
        {
            var fullOperation = "." + data.OperationName;
            core.Monitor.ErrorTracer(fullOperation, data.TraceMessage, data.TracePoint);
        }
    }
}