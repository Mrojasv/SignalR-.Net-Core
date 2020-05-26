using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRHubApi.Hubs;
using SignalRHubApi.Manager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRHubApi
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<ChatHub> _clockHub;
        //private readonly IUserConnectionManager _userConnectionManager;

        public Worker(ILogger<Worker> logger, IHubContext<ChatHub> clockHub) //, IUserConnectionManager userConnectionManager)
        {
            _logger = logger;
            _clockHub = clockHub;
            //_userConnectionManager = userConnectionManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                await _clockHub.Clients.All.SendAsync("ReceiveMessage", "Server", "Hola a Todos");
                await _clockHub.Clients.Group("SignalR Users").SendAsync("ReceiveMessage", "Server", "Bienvenido al Grupo");
                //await _clockHub.Clients.Caller.SendAsync("ReceiveMessage", "");

                //var userTemp =  _userConnectionManager.GetUserConnections("Person1");
                //await _clockHub.Clients.User("jPFBqpAZtOO-YKdCAemOcw").SendAsync("ReceiveMessage", "Hola Personalizado");
                await Task.Delay(40000);
            }
        }
    }
}
