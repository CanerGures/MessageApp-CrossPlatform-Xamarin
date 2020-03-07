using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VizyonMesajMobil.Models;
using VizyonMesajMobil.Views;
using VizyonMesajMobil.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;

namespace VizyonMesajMobil.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RehberPage : ContentPage
    {

        
        public RehberPage()
        {
            InitializeComponent();
            GetKullanicilar();
            NavigationPage.SetHasBackButton(this, false);

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var rehber = args.SelectedItem as Rehber;
            if (rehber == null)
                return;

            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/getGrupList");
            client.Timeout = -1;
            var response = new RestRequest(Method.GET);
            response.AddHeader("Content-Type", "application/json");
            response.AddHeader("Authorization", "Bearer " + App.LoginToken);
            IRestResponse result = client.Execute(response);
            var SmsGrupList = JsonConvert.DeserializeObject<List<Grup>>(result.Content);

            UserDetailViewModel viewModel = new UserDetailViewModel(rehber);
            viewModel.GroupList = SmsGrupList;
            await Navigation.PushAsync(new RehberDetailPage(viewModel));

            // Manually deselect item.
            userlist.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/getGrupList");
            client.Timeout = -1;
            var response = new RestRequest(Method.GET);
            response.AddHeader("Content-Type", "application/json");
            response.AddHeader("Authorization", "Bearer " + App.LoginToken);
            IRestResponse result = client.Execute(response);
            var SmsGrupList = JsonConvert.DeserializeObject<List<Grup>>(result.Content);
            UserDetailViewModel newuserVM = new UserDetailViewModel();
            newuserVM.GroupList = SmsGrupList;
            await Navigation.PushModalAsync(new NavigationPage(new RehberRehberNewRegistryPage(newuserVM)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetKullanicilar();

          
        }
        public void GetKullanicilar()
        {
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/getRehberList");
            client.Timeout = -1;
            var response = new RestRequest(Method.GET);
            response.AddHeader("Content-Type", "application/json");
            response.AddHeader("Authorization", "Bearer " + App.LoginToken);
            IRestResponse result = client.Execute(response);
            var SmsUserList = JsonConvert.DeserializeObject<List<Rehber>>(result.Content);
            userlist.ItemsSource = SmsUserList;
          
        }
    }
}