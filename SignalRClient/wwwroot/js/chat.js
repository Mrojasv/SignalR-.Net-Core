"use strict";

this.loginToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

    var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:60528/ChatHub").withAutomaticReconnect().build();
//var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44391/ChatHub", { accessTokenFactory: () => this.loginToken }
//    ).withAutomaticReconnect().build();


//var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:60528/ChatHub", options => {
//        //options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
//        //options.UseDefaultCredentials = true;
//    }).withAutomaticReconnect().build();
    //.withUrl("http://localhost:60528/ChatHub")
    //.WithUrl("http://localhost:60528/ChatHub", options => {
    //    options.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
    //})
    

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
    //connection.invoke('getinfo');
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("ConnectButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    debugger;
//    if (user != null) {
//        debugger;
//        startConnection(user);
//        connection.onclose(reconnect);
//    }
    
//    event.preventDefault();
//});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var forUser = document.getElementById("forUserInput").value;
    debugger;
    if (forUser == "") {
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        connection.invoke("SendPrivateMessage", user, forUser, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});
document.getElementById("sendGroupButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var group = document.getElementById("groupInput").value;
    if (group != null) {
        connection.invoke("SendMessageToGroupName", group, user, message).catch(function (err) {
            return console.error(err.toString());
        });
    } 
    event.preventDefault();
});
document.getElementById("JoinButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var group = document.getElementById("groupInput").value;
    if (group != null) {
        connection.invoke("AddToGroup", user,group).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});
document.getElementById("LeaveButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var group = document.getElementById("groupInput").value;
    if (group != null) {
        connection.invoke("RemoveFromGroup", user,group).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});

//function startConnection(userId) {
//    console.log('connecting...');
//    debugger;
//    connection.user.value = userId;
//    connection.start()
//        .then(function () { document.getElementById("sendButton").disabled = false; })
//        .catch(reconnect);
//}
//function reconnect() {
//    console.log('reconnecting...');
//    setTimeout(startConnection, 2000);
//}