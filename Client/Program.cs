using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class Program
{
    private const int CustomBufferSize = 1024;

    public static void Main()
    {
        int customPort;
        string customFilePath;

        Console.WriteLine("Please provide the port number for the server:");
        customPort = Int32.Parse(Console.ReadLine());

        Console.WriteLine("Enter the file path:");
        customFilePath = Console.ReadLine();

        try
        {
            using (TcpClient customClient = new TcpClient("127.0.0.1", customPort))
            {
                Console.WriteLine("Established connection with the server...");

                using (NetworkStream customNetworkStream = customClient.GetStream())
                using (FileStream customFileStream = File.OpenRead(customFilePath))
                {
                    byte[] customBuffer = new byte[CustomBufferSize];
                    int customBytesRead;

                    while ((customBytesRead = customFileStream.Read(customBuffer, 0, customBuffer.Length)) > 0)
                    {
                        customNetworkStream.Write(customBuffer, 0, customBytesRead);
                    }

                    Console.WriteLine("successfully.");
                }
            }
        }
        catch (SocketException)
        {
            Console.WriteLine("Failed to connect.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("error: " + ex.Message);
        }

        Console.WriteLine("Please enter to exit.");
        Console.ReadKey();
    }
}





/*
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace FileTransferClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                Console.WriteLine("Connected to server.");

                while (client.Connected)
                {
                    //var path=@"C:\Users\beta laptop\source\repos\Server-Oslab-HW5\Client-Oslab-HW5\bin\Debug";
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
                    TcpClient client1 = new TcpClient("127.0.0.1", 5000);

                    using (NetworkStream stream = client1.GetStream())
                    using (var fileStream = File.Create("save-client.txt"))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    Console.WriteLine("Successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("Please enter to exit: ");
            Console.ReadKey();
        }
    }
}
*/