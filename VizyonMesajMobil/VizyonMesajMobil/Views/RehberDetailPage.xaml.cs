using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VizyonMesajMobil.Models;
using VizyonMesajMobil.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using RestSharp;
using System.Collections.Generic;

namespace VizyonMesajMobil.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RehberDetailPage : ContentPage
    {
        UserDetailViewModel viewModel;
        NewUserViewModel kviewmodel;
        public RehberDetailPage()
        {            
            InitializeComponent();
            
        }
        public RehberDetailPage(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetGrupname(); 
        }
        private void SMS_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessageRegistry(viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Uyarı", "Kaydı silmek istediğinize emin misiniz =", "Evet", "Hayır");
            if (answer)
            {
                Delete();
                await DisplayAlert("Uyarı", "Kayıt başarıyla silindi.", "OK");                
            }
        }
        private async void Update_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new RehberUpdatePage(viewModel));
        }
        public void Delete()
        {
            //deletes selected registry
            var ID = viewModel.Item.ID;
            var deleterequest = "";
            deleterequest += "{\n\tID:";
            deleterequest += ID;
            deleterequest += "\n}";
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/deleteRehber");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + App.LoginToken);
            request.AddParameter("application/json", deleterequest, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Navigation.PushAsync(new GrupPage());
        }
        public void GetGrupname()
        {
            var Id = viewModel.Item.ID;
            var getrequest = "";
            getrequest += "{\"ID\":\"";
            getrequest += Id;
            getrequest += "\"}";

            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/getGrupName");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer "+ App.LoginToken);
            request.AddParameter("application/json", getrequest, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var SmsUserList = JsonConvert.DeserializeObject<List<Grup>>(response.Content);
            viewModel.RehberGrupList = SmsUserList;            
            gruplist.ItemsSource = SmsUserList;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var grup = args.SelectedItem as Grup;
            if (grup == null)
                return;

            await Navigation.PushAsync(new GrupDetailPage(new GrupDetailViewModel(grup)));

            // Manually deselect item.
            gruplist.SelectedItem = null;
        }
        
    }
}