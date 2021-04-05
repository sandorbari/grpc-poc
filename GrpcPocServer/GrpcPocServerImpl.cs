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
    }
}
