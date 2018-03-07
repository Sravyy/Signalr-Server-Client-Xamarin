using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ContactsXamarin
{
    public partial class App : Application
    {
        public SignalRClient SignalRClient = new SignalRClient("http://localhost:49721");

        public App()
        {
            SignalRClient.Start().ContinueWith(task => {
                if (task.IsFaulted)
                    MainPage.DisplayAlert("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message, "OK");
            });

            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                if (!SignalRClient.IsConnectedOrConnecting)
                    SignalRClient.Start();

                return true;
            });

            var mainView = new MainPage();



        }
    }
}
