using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;

namespace Core.Monitor.Service
{
    [ServiceContract]
    public interface MonitorContract
    {
        [OperationContract(Name = "TraceInformation", IsOneWay = true)]
        void InfoTracer(MonitorData data);

        [OperationContract(Name = "TraceWarning", IsOneWay = true)]
        void WarningTracer(MonitorData data);

        [OperationContract(Name = "TraceError", IsOneWay = true)]
        void ErrorTracer(MonitorData data); 
    }
}