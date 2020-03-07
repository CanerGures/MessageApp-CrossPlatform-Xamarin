using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupNewRegistryPage : ContentPage
    {
        public GrupNewRegistryPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            var name = GrupNameEntry.Text;
            var aboneid = "1";

            string JsonRequest = "";
            JsonRequest += "{\"Grupadi\": \"";
            JsonRequest += name;
            JsonRequest += "\",\"AboneID\": \"";
            JsonRequest += aboneid;
            JsonRequest += "\"}";
            
            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/newGrup");
            client.Timeout = -1;
            var response = new RestRequest(Method.POST);
            response.AddHeader("Content-Type", "application/json");
            response.AddHeader("Authorization", "Bearer " + App.LoginToken);
            response.AddParameter("application/json", JsonRequest, ParameterType.RequestBody);
            IRestResponse result = client.Execute(response);
            await Navigation.PopModalAsync();
            if (result.IsSuccessful)
            {
                await DisplayAlert("Uyarı", "Yeni Kayıt Başarıyla Oluşturuldu", "Tamam");
            }
            else
            {
                await DisplayAlert("Uyarı", "Birşeyler Ters Gitti. Kontrol Ederek Tekrar Deneyin.", "Tamam");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}