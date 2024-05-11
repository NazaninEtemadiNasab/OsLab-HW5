using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class Program
{
    private const int BufferSize = 1024;

    public static void Main()
    {
        int port;

        Console.WriteLine("Please provide the port number :");
        port = Int32.Parse(Console.ReadLine());

        try
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener tcpServer = new TcpListener(localAddr, port);
            tcpServer.Start();

            Console.WriteLine("Listening.");

            while (true)
            {
                TcpClient client = tcpServer.AcceptTcpClient();
                Console.WriteLine("Client Added.");

                using (NetworkStream networkStream = client.GetStream())
                using (FileStream fileStream = File.Create("save-server.dat"))
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;

                    while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }

                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }
      
        catch (Exception ex)
        {
            Console.WriteLine("error:  " + ex.Message);
        }

        Console.WriteLine("Please enter to exit.");
        Console.ReadKey();
    }
}/*using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listen = null;
            try
            {
                listen = new TcpListener(IPAddress.Any, 5000);
                listen.Start();
                Console.WriteLine("Listening.");

                while (true)
                {
                    using (TcpClient client = listen.AcceptTcpClient())
                    {
                        Console.WriteLine("Client Added.");

                        using (NetworkStream stream = client.GetStream())
                        using (var fileStream = File.Create("save-server.txt")) 
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fileStream.Write(buffer, 0, bytesRead);
                            }
                        }

                        Console.WriteLine("Successfully.");

                        Console.WriteLine("Enter the path:");
                        string filePath = Console.ReadLine();
                        using (FileStream fileStream = File.OpenRead(filePath))
                        using (NetworkStream stream = client.GetStream())
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream.Write(buffer, 0, bytesRead);
                            }
                        }

                        Console.WriteLine("Successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                listen?.Stop();
            }
            Console.WriteLine("Please enter to exit: ");
            Console.ReadKey();
        }
    }
}

*//*        Dictionary<string, string> type = new Dictionary<string, string>()
        {
            { "FFD8FF", ".jpg" }, // JPEG
            { "89504E47", ".png" }, // PNG
            { "25504446", ".pdf" }, // PDF
            { "494433", ".mp3" }, // MP3
            { "FFFB50", ".mp3" }, // MP3
            { "66747970", ".mp4" }, // MP4
            { "6D6F6F76", ".mov" }, // MOV
            { "EFBBBF", ".txt" }, // TXT
            { "FFFE", ".txt" }, // TXT
            { "FEFF", ".txt" } // TXT

        };*/


