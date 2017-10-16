using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SimpleTCP;

namespace AppSocketClient
{
    class MainClass
    {
        static HttpClient client = new HttpClient();
        static TcpClient clientSocket = new TcpClient();
        static SimpleTcpClient cliente;


        public static void Main(string[] args)
        {
            //RunAsync().Wait();
            Console.WriteLine("OK");
           // clientSocket.Connect();
            //_clientSocket();
            _simpleTcp();
        }

        public static void _simpleTcp(){
            String message = "{ \"type\": \"video\", \"value\": \"1.mp4\"}";
            cliente = new SimpleTcpClient();
            cliente.Connect("192.168.1.42", 8080);
            cliente.StringEncoder = Encoding.UTF8;
            cliente.DataReceived += client_DataReceived;
            cliente.WriteLineAndGetReply(message, TimeSpan.FromSeconds(3));
            cliente.Disconnect();

        }

        private static void client_DataReceived(object sender, SimpleTCP.Message e){

            Console.WriteLine("Respuesta: " + e.MessageString);
        }


        /**
         * Socket client
         */
        public static void _clientSocket(){

            try
            {

                String message = "Holi";//"{ \"type\": \"video\", \"value\": \"1.mp4\"}";
                                        // Create a TcpClient.
                                        // Note, for this client to work you need to have a TcpServer 
                                        // connected to the same address as specified by the server, port
                                        // combination.
                Int32 port = 8080;
                TcpClient client = new TcpClient("192.168.1.42", port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch(Exception ex){
                Console.WriteLine("PROBLEMAS!" + ex.ToString() );
            }

           

        }


        /**
         * Hilo de conexión
         */
        static async Task<GettingStarted> GetObjectAsync(string path)
        {
            GettingStarted obj = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                obj = await response.Content.ReadAsAsync<GettingStarted>();
            }
            return obj;
        }


        /**
         * Método que accede a la API
         */
        static async Task RunAsync()
        {
            String url_api = "http://manager.rinnolab.cl/ra/api/project/";
           
            try
            {
                // Get the object
                GettingStarted obj = await GetObjectAsync(url_api);
                ShowProject(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }


        /*
         * ListProject
         */

        static void ShowProject(GettingStarted obj)
        {
            Console.WriteLine("# LISTADO DE PROYECTOS #");
            Console.WriteLine("--------------------------------------");
            foreach(Result item in obj.Results){
                Console.WriteLine(" ");
                Console.WriteLine($"Created: {item.Created}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Organization: {item.Organization}");
                Console.WriteLine($"Status: {item.Status}");
                ShowAttributes(item);
                Console.WriteLine("--------------------------------------");
            }
        }


        /*
         * ListAttributes
         */

        static void ShowAttributes(Result objResult)
        {
            Console.WriteLine("# LISTADO DE ATRIBUTOS #");
            Console.WriteLine("***");
            foreach (Attribute item in objResult.Attributes)
            {
                Console.WriteLine(" ");
                Console.WriteLine($" -Id: {item.Id}");
                Console.WriteLine($" -Name: {item.Name}");
                Console.WriteLine($" -Status: {item.Status}");
                Console.WriteLine("***");
            }
        }


        /**
         * Método de impresión
         */
        static void ShowObject(GettingStarted obj)
        {
            Console.WriteLine($"Count: {obj.Count}");
        }

        /**
         * Método que muestra el Json obtenido
         */
        static void ShowJson(GettingStarted obj)
        {
            Console.WriteLine($"Json: {obj.ToJson()}");
        }

    }
}
