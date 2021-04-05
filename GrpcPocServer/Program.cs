using System;
using Grpc.Core;
using Grpcpoc;

namespace GrpcPocServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 12345;

            Server pocServer = new Server()
            {
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) },
                Services = {GrpcPoc.BindService(new GrpcPocServerImpl())}
            };

            pocServer.Start();

            Console.WriteLine($"Server is listening on port {port}");
            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();

            pocServer.ShutdownAsync().Wait();
        }
    }
}
