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
    public partial class GrupDetailPage : ContentPage
    {
        GrupDetailViewModel viewModel;
        public GrupDetailPage()
        {
            InitializeComponent();
            GetRehberNamesinGrup();
        }
        public GrupDetailPage(GrupDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetRehberNamesinGrup();


        }
        private void SMS_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GrupMessageRegistry(viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Uyarı", "Kaydı Silmek İstediğinize Emin Misiniz?", "Evet", "Hayır");
            if (answer)
            {
                Delete();
                await DisplayAlert("Uyarı", "Kayıt Başarıyla Silindi", "Tamam");
                await Navigation.PushAsync(new RehberPage());
            }

        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GrupUpdatePage(viewModel));
        }
        public void Delete()
        {
            var ID = viewModel.Grup.ID;
            var deleterequest = "";
            deleterequest += "{\n\tID:";
            deleterequest += ID;
            deleterequest += "\n}";
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/deleteGrup");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + App.LoginToken);
            request.AddParameter("application/json", deleterequest, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
        public void GetRehberNamesinGrup()
        {
            var grupid = viewModel.Grup.ID;
            var getrehbernames = "";
            getrehbernames += "{\"ID\":\"";
            getrehbernames += grupid;
            getrehbernames += "\"}";
            

            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/getRehberinGroups");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer "+ App.LoginToken);
            request.AddParameter("application/json", getrehbernames, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var SmsUserList = JsonConvert.DeserializeObject<List<Rehber>>(response.Content);

            userlist.ItemsSource = SmsUserList;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var rehber = args.SelectedItem as Rehber;
            if (rehber == null)
                return;

            await Navigation.PushAsync(new RehberDetailPage(new UserDetailViewModel(rehber)));

            // Manually deselect item.
            userlist.SelectedItem = null;
        }
        
        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new GrupAddPerson(viewModel)));
        }
    }
}