using ContactsXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();


            listView.ItemsSource = new List<Contact>
            {
                new Contact { Name = "Mosh", ImageUrl ="http://lorempixel.com/100/100/people/1"},
                new Contact { Name = "Mob", ImageUrl ="http://lorempixel.com/100/100/people/3"},
                new Contact { Name = "Dan", ImageUrl ="http://lorempixel.com/100/100/people/2"}
            };

        }
	}
}
