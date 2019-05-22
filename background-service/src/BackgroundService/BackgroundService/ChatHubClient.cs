using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundService
{
    public class ChatHubClient : IHostedService
    {
        private readonly ILogger<ChatHubClient> _logger;
        private HubConnection _connection;

        public ChatHubClient(ILogger<ChatHubClient> logger)
        {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chat")
                .Build();

            _connection.On<string, string>("Send", (nick, message) => Console.WriteLine(message));
        }

#region StartAsync
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Loop is here to wait until the server is running
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);
                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
        }
#endregion
#region StopAsync
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _connection.DisposeAsync();
        }
    }
#endregion
}