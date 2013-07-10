using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Monitor.Proxy;

namespace Core.Monitor.Proxy.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            //var col = new Core.Data.Teste.CoreDataLOGCollection().Find(new Data.Teste.CoreDataLOGKey()); 
            //Console.WriteLine("Operacao | Message  | Type");
            //col.ForEach(item =>
            //{
            //        Console.WriteLine(item.NomeOperacao + " | " + item.TextMessageTrace + " | " + item.TraceType);
            //});
            //Console.Read();

            var proxy = Monitor.Service.MonitorClientFactory.Create();
            for (int i = 0; i <= 3000; i++)
            {
                Console.WriteLine(i);
                proxy.TraceError(new Service.MonitorData
                                            {
                                                OperationName = "OPER->" + i,
                                                TraceMessage = "MSG->" + i,
                                                TracePoint = (Int16)i
                                            });
            }
            proxy.Close();
            Console.Read();
        }
    }
}
