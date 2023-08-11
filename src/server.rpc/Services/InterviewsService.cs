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
        return base.RegisterAttempt(request, context);
    }
}
