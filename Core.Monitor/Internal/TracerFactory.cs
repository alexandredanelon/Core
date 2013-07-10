using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Monitor.Internal
{
    internal class TracerFactory
    {
        public static ITracer Create()
        {
            var __logType = ConfigurationManager.AppSettings["LogType"].ToString();
            ITracer o=null;
            switch (__logType)
            {
                case "APP_FABRIC" :
                    o = new WCFDiagnosticsExt();
                    break;
                case "CORE_DB" :
                    o = new CoreDataLOG();
                    break;
                default:
                    o = new WindowsEventTrace();
                    break;
            }
            return o;
        }
    }
}
