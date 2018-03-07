
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ContactsXamarin.Models;
using Microsoft.AspNet.SignalR;

namespace ContactsXamarin.Server
{
    public class MyHub : Hub
    {
       
        public MyHub()
        {
            
            var taskTimer = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                   
                    string timeNow = DateTime.Now.ToString();
                    Clients.All.SendServerTime(timeNow);
                    await Task.Delay(3000);
                }
            }, 
            
            TaskCreationOptions.LongRunning
                );
        }

        public void GetContact(string Name, string ImageUrl)
        {
            var contact = new List<Contact>();
            
            Clients.All.ShowContact(Name, ImageUrl);
           
        }
    }
}