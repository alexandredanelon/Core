using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel;
using Winsoft.Framework.MonitorProxy;
             
///TODO:  Linha de comando para gerar o proxy
///TODO:  1: svcutil /t:code http://<service_url> 
///TODO:  2: /out:<file_name>.cs /config:<file_name>.config

namespace Core.Monitor.Service
{
    public class MonitorClientFactory
    {

        private static MonitorContractClient __factory;

        public static MonitorContractClient Create()
        {

            //var __configFileReader = XMLUtil.LoadXMLFile(ConfigurationManager.AppSettings["ConfigMonitor"].ToString());
            var __hostName = ConfigurationManager.AppSettings["HostName"].ToString(); //XMLUtil.ReadXMLNode(__configFileReader, ConfigurationManager.AppSettings["HostName"].ToString());
            var __trace = ConfigurationManager.AppSettings["Trace"].ToString(); //XMLUtil.ReadXMLNode(__configFileReader, ConfigurationManager.AppSettings["Trace"].ToString()).ToLower().Equals("true");

            if (__factory == null)
            {
                NetTcpBinding __netProxyBind =
                    new NetTcpBinding();

                var __defaultTimer = TimeSpan.FromMinutes(15.0);
                var __sizes = 245536;

                __netProxyBind.MaxBufferSize = __sizes;
                __netProxyBind.MaxReceivedMessageSize = __sizes;
                __netProxyBind.MaxConnections = 100;
                __netProxyBind.MaxBufferPoolSize = 20;                                                                 
                __netProxyBind.Security.Mode = SecurityMode.Transport;
                __netProxyBind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                __netProxyBind.TransferMode = TransferMode.Buffered;
                __netProxyBind.TransactionFlow = false;                                                  
                __netProxyBind.ReaderQuotas.MaxStringContentLength = __sizes;
                __netProxyBind.ReaderQuotas.MaxArrayLength = __sizes;
                __netProxyBind.ReaderQuotas.MaxBytesPerRead = __sizes;
                __netProxyBind.ReaderQuotas.MaxNameTableCharCount = __sizes;                             
                __netProxyBind.CloseTimeout = __defaultTimer;
                __netProxyBind.OpenTimeout = __defaultTimer;
                __netProxyBind.SendTimeout = __defaultTimer;

                __factory = new MonitorContractClient(__netProxyBind, __trace.Equals("true"), new System.ServiceModel.EndpointAddress(String.Format("{0}", __hostName)));
            }

            return __factory;
        }
    }
}
