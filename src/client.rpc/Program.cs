using Grpc.Net.Client;
using server.rpc;

var channel = GrpcChannel.ForAddress("https://localhost:7166");
var client = new Interviews.InterviewsClient(channel);

var request = (int idx) => new NewAttemptRequest()
{
    Company = $"Company {idx}",
    Position = position.Backend,
    Seniority = seniority.Senior,
    Tech = tech.Csharp

};


Parallel.For(0, 1001, async idx =>
{
    var registrationTask = client.RegisterAttemptAsync(request(idx));


    var response = await registrationTask;

    var formatted = $"{idx:D4}";

    if (idx == 1000)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{formatted} - {response.Id}");
        Console.ResetColor();
    }
    else
        Console.WriteLine($"{formatted} - {response.Id}");
});

_ = Console.ReadKey();
