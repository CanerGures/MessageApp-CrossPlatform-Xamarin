using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizyonMesajMobil.Models;
using VizyonMesajMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupPage : ContentPage
    {
        public GrupPage()
        {
            InitializeComponent();
            GetGrupList();
            NavigationPage.SetHasBackButton(this, false);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var grup = args.SelectedItem as Grup;
            if (grup == null)
                return;

            await Navigation.PushAsync(new GrupDetailPage(new GrupDetailViewModel(grup)));

            // Manually deselect item.
            userlist.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new GrupNewRegistryPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetGrupList();
        }
        public void GetGrupList()
        {
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/getGrupList");
            client.Timeout = -1;
            var response = new RestRequest(Method.GET);
            response.AddHeader("Content-Type", "application/json");
            response.AddHeader("Authorization", "Bearer " + App.LoginToken);
            IRestResponse result = client.Execute(response);
            var SmsUserList = JsonConvert.DeserializeObject<List<Grup>>(result.Content);
            userlist.ItemsSource = SmsUserList;

        }
    }

}