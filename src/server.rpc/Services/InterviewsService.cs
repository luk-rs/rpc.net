using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace server.rpc.Services;

public class InterviewsService : Interviews.InterviewsBase
{
    private readonly ILogger<InterviewsService> _logger;

    public InterviewsService(ILogger<InterviewsService> logger)
    {
        _logger = logger;
    }

    public override Task<AttemptRegistration> RegisterAttempt(NewAttemptRequest request, ServerCallContext context)
    {
        var registration = new AttemptRegistration()
        {
            Id = $"{Guid.NewGuid()}",
            Date = Timestamp.FromDateTime(DateTime.UtcNow)

        };

        return Task.FromResult(registration);
    }
}
