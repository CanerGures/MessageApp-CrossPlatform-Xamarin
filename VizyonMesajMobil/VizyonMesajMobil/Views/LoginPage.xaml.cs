using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizyonMesajMobil.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = LoginUserName.Text;
            var password = LoginPassword.Text;
            String BasicEncode = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic " + BasicEncode);
            IRestResponse response = client.Execute(request);
            if(response.IsSuccessful)
            {
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                App.LoginToken = loginResponse.token;
                await Navigation.PushAsync(new RehberPage());
            }
            else
            {
                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                await DisplayAlert("alert", errorResponse.Message, "OK");
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}