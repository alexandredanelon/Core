using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Monitor.Internal;

namespace Core.Monitor
{
    public class Monitor
    {
        private static ITracer eventTracer;
        /// <summary>
        /// Nivél de trace de acordo com appFabric
        /// </summary>
        private enum TraceType
        {
            ERROR = 0x2,
            INFO = 0x4,
            WARNING = 0x6
        }

        private static void Tracer(TraceType type, string operationName, string traceMessage, short tracePoint)
        {
            var customMSG = String.Format("0. {0} at point {1}.{2}",
                                      traceMessage, tracePoint,operationName);
       
            eventTracer = TracerFactory.Create();

            try
            {
                switch (type)
                {
                    case TraceType.ERROR:
                        eventTracer.WriteErrorEvent(customMSG, operationName);
                        break;
                    case TraceType.INFO:
                        eventTracer.WriteInfoEvent(customMSG, operationName);
                        break;
                    case TraceType.WARNING:
                        eventTracer.WriteWarningEvent(customMSG, operationName);
                        break;
                }
            }
            catch (Exception e)
            {
                WindowsEventTrace eERR = new WindowsEventTrace();
                var __internalErrorMessage = String.Format("Internal Error {0} \n in source {1} ", customMSG, e.StackTrace);
                eERR.WriteErrorEvent(__internalErrorMessage, string.Empty);
                throw new Exception(__internalErrorMessage);
            }
        }

        /// <summary>
        /// Utilizado apenas conjunto com a classe Winsoft.Framework.Exception.
        /// </summary>
        /// <param name="operationName">Nome da classe / metodo aonde sera disparada a Exception</param>
        /// <param name="traceMessage"></param>
        /// <param name="tracePoint"></param>
        public static void ErrorTracer(string operationName, string traceMessage, short tracePoint)
        {
            Tracer(TraceType.ERROR, operationName, traceMessage, tracePoint);
        }

        /// <summary>
        /// Utilizado para auxiliar na identificação de diagnosticos e performance 
        /// do sistema podendo ser desligado com TraceOn =  false no AppConfig 
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="traceMessage"></param>
        /// <param name="tracePoint"></param>
        public static void InfoTracer(string operationName, string traceMessage, short tracePoint)
        {
            Tracer(TraceType.INFO, operationName, traceMessage, tracePoint);
        }

        public static void InfoWaring(string operationName, string traceMessage, short tracePoint)
        {
            Tracer(TraceType.WARNING, operationName, traceMessage, tracePoint);
        }
    }
}
