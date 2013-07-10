using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Core.Monitor.Service
{
    [DataContract]
    public class MonitorData
    {
        [DataMember]
        public string TraceMessage { get; set; }

        [DataMember]
        public short TracePoint { get; set; }

        [DataMember]
        public string OperationName { get; set; }

    }
}