using System.Threading.Tasks;
using Grpc.Core;
using Grpcpoc;

namespace GrpcPocServer
{
    public class GrpcPocServerImpl : GrpcPoc.GrpcPocBase
    {
        public override Task<AdderResponse> Add(AdderRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AdderResponse()
            {
                Sum = request.LeftOperand + request.RightOperand
            });
        }

        public override async Task<ResultResponse> MultiplySeries(IAsyncStreamReader<MultiplicationRequest> requestStream, ServerCallContext context)
        {
            var result = 1;

            while(await requestStream.MoveNext())
            {
                var multiplicand = requestStream.Current;
                result *= multiplicand.Multiplicand;
            }

            return new ResultResponse() { Result = result };
        }
    }
}
