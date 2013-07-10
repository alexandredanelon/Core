using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using System.Security.Permissions;

namespace Core.Monitor.Internal
{
    internal class WindowsEventTrace : ITracer
    {
        private static string __sourceName = "Agil.Monitor.Tracer";
        private static string __logName = __sourceName + "." + AppName;

        private static string AppName
        {
            get
            {
                var __appName = string.Empty;
                try
                {
                    __appName = ConfigurationManager.AppSettings["AppNameMonitor"].ToString();
                }
                catch (Exception)
                {
                    throw new Exception("Chave de configuração { TraceOn } não localizada na Web.config");
                }
                return __appName;

            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public WindowsEventTrace()
        {
            if (!EventLog.SourceExists(__sourceName))
            {
                EventLog.CreateEventSource(__sourceName, __logName);
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Exiting, execute the application a second time to use the source.");

                return;
            }
        }

        public bool WriteErrorEvent(string message, string payload)
        {
            var tracer = new EventLog();
            tracer.Source = __sourceName;
            tracer.WriteEntry(message, EventLogEntryType.Error);
            return true;
        }

        public bool WriteInfoEvent(string message, string payload)
        {
            var tracer = new EventLog();
            tracer.Source = __sourceName;
            tracer.WriteEntry(message, EventLogEntryType.Information);
            return true;
        }

        public bool WriteWarningEvent(string message, string payload)
        {
            var tracer = new EventLog();
            tracer.Source = __sourceName;
            tracer.WriteEntry(message, EventLogEntryType.Warning);
            return true;
        }

    }
}
