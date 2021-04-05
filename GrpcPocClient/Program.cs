using System;
using Grpc.Core;
using Grpcpoc;

namespace GrpcPocClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Channel channel = new Channel("localhost:12345", ChannelCredentials.Insecure);

            var pocClient = new GrpcPoc.GrpcPocClient(channel);
            var addRequest = new AdderRequest() { LeftOperand = 1, RightOperand = 2 };

            Console.WriteLine("Invoking the adder server");
            var response = pocClient.Add(addRequest);

            Console.WriteLine($"The sum is: {response.Sum}");

            Console.WriteLine("Shutting client down");
            channel.ShutdownAsync().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
