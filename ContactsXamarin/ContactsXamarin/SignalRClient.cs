using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsXamarin
{
    public class SignalRClient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private HubConnection Connection;
        private IHubProxy ContactHubProxy;

        public delegate void ShowContact(string Name, string ImageUrl);
        public event ShowContact OnContactShown;

        public SignalRClient(string url)
        {
            Connection = new HubConnection(url);

            Connection.StateChanged += (StateChange obj) =>
            {
                OnPropertyChanged("ConnectionState");
            };

            ContactHubProxy = Connection.CreateHubProxy("Contact");
            ContactHubProxy.On<string, string>("ShowContact", (Name, ImageUrl) =>
            {
                OnContactShown?.Invoke(Name, ImageUrl);
            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetContact(string Name, string ImageUrl)
        {
            ContactHubProxy.Invoke(Name, ImageUrl);
        }

        public Task Start()
        {
            return Connection.Start();
        }

        public bool IsConnectedOrConnecting
        {
            get
            {
                return Connection.State != ConnectionState.Disconnected;
            }
        }

        public ConnectionState ConnectionState { get { return Connection.State; } }

        public static async Task<SignalRClient> CreateAndStart(string url)
        {
            var client = new SignalRClient(url);
            await client.Start();
            return client;
        }
    }
}
