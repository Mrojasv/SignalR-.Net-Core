using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRHubApi.Manager;
using System;
using System.Threading.Tasks;

namespace SignalRHubApi.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        //private readonly IUserConnectionManager _userConnectionManager;
        //public ChatHub(IUserConnectionManager userConnectionManager)
        //{
        //    _userConnectionManager = userConnectionManager;
        //}

        //private readonly static ConnectionMapping<string> _connections =
        //new ConnectionMapping<string>();

        #region HubMethods
        public Task SendMessage(string user, string message)
        {
            //Clients.Caller.SendAsync("ReceiveMessage", user, "Could not find that user.");
            return Clients.All.SendAsync("ReceiveMessage", user, message);
            
        }
        //public Task SendMessage(string message)
        //{
        //    return Clients.All.SendAsync("ReceiveMessage",message);
        //}

        //public Task SendMessageToCaller(string message)
        //{
        //    return Clients.Caller.SendAsync("ReceiveMessage", message);
        //}

        public Task SendMessageToGroup(string user, string message)
        {
            return Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
        }
        #endregion

        #region HubMethods
        public Task SendMessageToGroupName(string groupName, string user, string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveMessage", user, $"(Group): {message}");
        }
        public async Task AddToGroup(string user, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, $"({Context.ConnectionId}) has joined the group {groupName}.");
        }
        public async Task RemoveFromGroup(string user, string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, $"({Context.ConnectionId}) has left the group {groupName}.");
        }
        public async Task SendPrivateMessage(string user, string forWho, string message)
        {
            await Clients.Client(forWho).SendAsync("ReceiveMessage", user, $"(PrivateMessage) {message}.");
            //return Clients.User(forWho).SendAsync("ReceiveMessage", user, $"(PrivateMessage) {message}." );

        }
        public async Task GetInfo()
        {
            string name = Context.User.Identity.Name ?? Context.UserIdentifier;

            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "mismo","The message");

            //await Clients.User(Context.ConnectionId).SendAsync("ReceiveMessage", Context.ConnectionId.ToString(), Context.ConnectionId.ToString());
        }
        #endregion

        #region HubMethodName
        [HubMethodName("SendMessageToUser")]
        public Task DirectMessage(string user, string fromUser, string message)
        {
            return Clients.User(fromUser).SendAsync("ReceiveMessage", user, message);
        }
        #endregion

        #region ThrowHubException
        public Task ThrowException()
        {
            throw new HubException("This error will be sent to the client!");
        }
        #endregion

        #region OnConnectedAsync
        public override async Task OnConnectedAsync()
        {
            //var httpContext = this.Context.GetHttpContext();
            //var userId = httpContext.Request.Query["userId"];
            //_userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

            string name = Context.User.Identity.Name ?? Context.UserIdentifier;
            //_connections.Add(name, Context.ConnectionId);

            //await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        #endregion

        #region OnDisconnectedAsync
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //var connectionId = Context.ConnectionId;
            //_userConnectionManager.RemoveUserConnection(connectionId);

            string name = Context.User.Identity.Name;
            //_connections.Remove(name, Context.ConnectionId);

            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        #endregion
    }
}
