using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Office.Interop.Excel;

namespace SignalRClientMVC
{
    //public partial class MainWindow : Window
    //{
    //    HubConnection connection;
    //    public MainWindow()
    //    {
    //        //InitializeComponent();

    //        connection = new HubConnectionBuilder()
    //            .WithUrl("http://localhost:60528/ChatHub")
    //            .Build();

    //        connection.Closed += async (error) =>
    //        {
    //            await Task.Delay(new Random().Next(0, 5) * 1000);
    //            await connection.StartAsync();
    //        };
    //    }

    //    private async void connectButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        connection.On<string, string>("ReceiveMessage", (user, message) =>
    //        {
    //            this.Dispatcher.Invoke(() =>
    //            {
    //                var newMessage = $"{user}: {message}";
    //                messagesList.Items.Add(newMessage);
    //            });
    //        });

    //        try
    //        {
    //            await connection.StartAsync();
    //            //messagesList.Items.Add("Connection started");
    //            //connectButton.IsEnabled = false;
    //            //sendButton.IsEnabled = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            //messagesList.Items.Add(ex.Message);
    //        }
    //    }

    //    private async void sendButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {
    //            await connection.InvokeAsync("SendMessage",
    //                "User", "message");
    //        }
    //        catch (Exception ex)
    //        {
    //            //messagesList.Items.Add(ex.Message);
    //        }
    //    }
    //}
}
