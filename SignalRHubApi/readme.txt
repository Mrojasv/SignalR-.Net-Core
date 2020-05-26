
1.In the integrated terminal, run the following command to install LibMan.
dotnet tool install -g Microsoft.Web.LibraryManager.Cli

2.Run the following command to get the SignalR client library by using LibMan. You might have to wait a few seconds before seeing output.
libman install @microsoft/signalr@latest -p unpkg -d wwwroot/js/signalr --files dist/browser/signalr.js --files dist/browser/signalr.min.js

3.In the Hubs folder, create a ChatHub.cs file with the following code:
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

4.Add the following highlighted code to the Startup.cs file.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                           .WithOrigins("http://localhost:52623")
                           .AllowCredentials();
                }));

        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/ChatHub");
        });
    }

5.Replace the content in Pages\Index.cshtml with the following code:
@page
    <div class="container">
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-2">User</div>
            <div class="col-4"><input type="text" id="userInput" /></div>
        </div>
        <div class="row">
            <div class="col-2">Message</div>
            <div class="col-4"><input type="text" id="messageInput" /></div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-6">
                <input type="button" id="sendButton" value="Send Message" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <hr />
        </div>
    </div>
    <div class="row">
        <div class="col-6">
            <ul id="messagesList"></ul>
        </div>
    </div>
<script src="~/js/signalr/dist/browser/signalr.js"></script>
<script src="~/js/chat.js"></script>

6.In the wwwroot/js folder, create a chat.js file with the following code:
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

7. Errors
dotnet dev-certs https --clean
dotnet dev-certs https --trust
