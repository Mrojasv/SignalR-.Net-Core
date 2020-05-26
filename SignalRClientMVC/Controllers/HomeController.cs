using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;
using SignalRClientMVC.Models;



namespace SignalRClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private List<string> _messages = new List<string>();
        //HubConnection connection;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //HubConnection connection = new HubConnectionBuilder().WithUrl("http://localhost:60528/ChatHub", option =>
            //{
            //    option.AccessTokenProvider = () =>
            //    {
            //        return Task.FromResult(SignalRHelper.GenerateAccessToken("http://localhost:60528/ChatHub", "APIClient", Config));
            //    };
            //}).Build();

            //HubConnection connection = new HubConnectionBuilder().WithUrl("http://localhost:60528/ChatHub").Build();
            //connection.Closed += async (error) =>
            //{
            //    await Task.Delay(new Random().Next(0, 5) * 1000);
            //    await connection.StartAsync();
            //};
            //connection.On<string, string>("ReceiveMessage", (user, message) =>
            //{
            //    var encodedMsg = $"{user}: {message}";
            //    _messages.Add(encodedMsg);
            //    //StateHasChanged();
            //});
            //connection.StartAsync(); //.Wait();
                                     //connection.InvokeAsync("Subscribe", "TESTGROUP").Wait();



            try
            {
                //connection = new HubConnectionBuilder()
                //    .WithUrl("http://localhost:60528/ChatHub")
                //    .WithAutomaticReconnect()
                //    .Build();

                //connection.Closed += async (error) =>
                //{
                //    await Task.Delay(new Random().Next(0, 5) * 1000);
                //    await connection.StartAsync();
                //};

                //connection.StartAsync();
                //messagesList.Items.Add("Connection started");
                //connectButton.IsEnabled = false;
                //sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            ChatModel model = new ChatModel();
            model.User = "Person 2";
            model.Message = "Hi";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatModel model)
        {
            try
            {
                //if (connection == null)
                //{
                //    connection = new HubConnectionBuilder()
                //    .WithUrl("http://localhost:60528/ChatHub")
                //    .WithAutomaticReconnect()
                //    .Build();

                //    connection.Closed += async (error) =>
                //    {
                //        await Task.Delay(new Random().Next(0, 5) * 1000);
                //        await connection.StartAsync();
                //    };
                //}
                ////CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ////CancellationToken cancellationToken = cancellationTokenSource.Token;

                //await connection.StartAsync();

                //#region snippet_InvokeAsync
                //await connection.InvokeAsync("SendMessage", model.User, model.Message);
                //#endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> GetMessage()
        {
            try
            {
                //connection = new HubConnectionBuilder()
                //.WithUrl("http://localhost:60528/ChatHub")
                //.Build();

                //connection.Closed += async (error) =>
                //{
                //    await Task.Delay(new Random().Next(0, 5) * 1000);
                //    await connection.StartAsync();
                //};

                //connection.On<string, string>("ReceiveMessage", (user, message) =>
                //{
                //    this.Dispatcher.Invoke(() =>
                //    {
                //        var newMessage = $"{user}: {message}";
                //        model.messagesList += newMessage;
                //    });
                //});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
