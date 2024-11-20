// HTTPServer.cs - A simple HTTP server using HttpListener (HTTP only)
using System;
using System.Net;
using System.Text;
using System.Threading;

class HTTPServer
{
    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // Listening on port 8080

        listener.Start();
        Console.WriteLine("Listening for HTTP requests on http://localhost:8080/");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string responseString = "<html><body><h1>Insecure HTTP Response</h1><p>This is an unencrypted HTTP response.</p></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }
}
