using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Backend.Communication;

namespace ServerCommunication
{
    class ServerRunner
    {

        private static void ProcessClientRequests(Object obj)
        {

            TcpClient client = (TcpClient)obj;
            var bytesRead = 0;
            var message = new byte[4096];
            try
            {
                bytesRead = client.GetStream().Read(message, 0, 4096);
                XmlSerializer ser = new XmlSerializer(typeof(Object));
                ASCIIEncoding encoder = new ASCIIEncoding();
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
                
                TextReader tr = new StringReader(encoder.GetString(message, 0, bytesRead));
                Object p = ser.Deserialize(tr);
                Console.WriteLine(p.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }

        static void Main()
        {
            TcpListener listener = null;
            try
            {
                var address = IPAddress.Parse("127.0.0.1");
                var port = 2345;
                listener = new TcpListener(address, port);
                listener.Start();
                Console.WriteLine(
                    String.Format("Server has been initialized at IP: {0} PORT: {1}", 
                    address.ToString(), 
                    port));

                while (true)
                {
                    Console.WriteLine("Waiting for new connection.");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Accepted new client");
                    Thread t = new Thread(ProcessClientRequests);
                    t.Start(client);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
    }
}
