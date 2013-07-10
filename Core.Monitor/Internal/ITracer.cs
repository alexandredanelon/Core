using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Monitor.Internal
{
    internal interface ITracer
    {
        bool WriteErrorEvent(string message, string payload);

        bool WriteInfoEvent(string message, string payload);

        bool WriteWarningEvent(string message, string payload);
    }
}
