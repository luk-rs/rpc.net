using Microsoft.AspNetCore.Server.Kestrel.Core;
using server.rpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<InterviewsService>();
// app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapGet("/", async context =>
{
  var endpointDataSource = context
      .RequestServices.GetRequiredService<EndpointDataSource>();
  await context.Response.WriteAsJsonAsync(new
  {
    results = endpointDataSource
          .Endpoints
          .OfType<RouteEndpoint>()
          .Where(e => e.DisplayName?.StartsWith("gRPC") == true)
          .Select(e => new
          {
            name = e.DisplayName,
            pattern = e.RoutePattern.RawText,
            order = e.Order
          })
          .ToList()
  });
});

app.Run();
