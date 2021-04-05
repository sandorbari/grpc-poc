using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpcpoc;

namespace GrpcPocClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Channel channel = new Channel("localhost:12345", ChannelCredentials.Insecure);

            var pocClient = new GrpcPoc.GrpcPocClient(channel);
            var addRequest = new AdderRequest() { LeftOperand = 1, RightOperand = 2 };
            
            Console.WriteLine("Invoking the adder server");
            var response = pocClient.Add(addRequest);

            Console.WriteLine($"The sum is: {response.Sum}");

            //Client side streaming test
            using var streamingCall = pocClient.MultiplySeries();
            for(int i = 1; i < 10; ++i)
            {
                await streamingCall.RequestStream.WriteAsync(new MultiplicationRequest() { Multiplicand = i });
            }
            await streamingCall.RequestStream.CompleteAsync();

            var result = await streamingCall.ResponseAsync;

            Console.WriteLine($"The result of the multiplication is: {result.Result}");

            Console.WriteLine("Shutting client down");
            channel.ShutdownAsync().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
