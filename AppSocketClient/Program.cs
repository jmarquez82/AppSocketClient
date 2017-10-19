using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/**
 * autor: Jm
 * 
 */

namespace AppSocketClient
{
    class MainClass
    {
        static HttpClient client = new HttpClient();

        /*Method main*/
        public static void Main(string[] args)
        {

            //Test TCP Socket
            Console.WriteLine("OK");
            while(true){
                String valor = Console.ReadLine();
                _clientSocket(valor);
            }

            //Test Json to C#
            // Activar para ver Json to Object
            //RunAsync().Wait();
           
        }

        /**
         * Socket client
         */
        public static void _clientSocket(String valor){

            try
            {
                //Json Format
                //Examples:
                //
                //Video
                //String message = "{ \"type\": \"video\", \"value\": \"1.mp4\"}";
                //Imagen
                //String message = "{ \"type\": \"image\", \"value\": \"http://urlimagen.jpg\"}";
                //Map
                //String message = "{ \"type\": \"map\", \"value\": \"-76.3,-31.5\"}";
                //
                //*Los archivos de videos, se encontrarán almacenados de forma local.

                String message = "{ \"type\": \"video\", \"value\": \""+ valor + ".mp4\"}";

                Int32 port = 8080;
                TcpClient client = new TcpClient("192.168.1.77", port);
                Byte[] data = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                //data = new Byte[256];

                // String to store the response ASCII representation.
                //String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();

            }
            catch(Exception ex){
                Console.WriteLine("PROBLEMAS!" + ex.ToString() );
            }
        }



        /**
         * JSON TO OBJECT
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
