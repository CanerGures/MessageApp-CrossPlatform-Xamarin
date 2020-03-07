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
    public partial class GrupUpdatePage : ContentPage
    {
        GrupDetailViewModel viewModel;
        public GrupUpdatePage(GrupDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public GrupUpdatePage()
        {
            InitializeComponent();
        }
        public void Update_Clicked(object sender, EventArgs e)
        {
            Grup grup = new Grup
            {
                ID = Convert.ToInt32(viewModel.Grup.ID),
                GrupAdi = GrupNameEntry.Text

            };
            Update(grup);   
        }
        public async void Update(Grup kullanici)
        {
            bool answer = await DisplayAlert("Uyarı", "Kaydı Güncellemek İster Misiniz ?", "Evet", "Hayır");
            if (answer)
            {
                var jsonrequest = "";
                jsonrequest += "{\"GsmNo1\": \"";
                jsonrequest += kullanici.GrupAdi;
                jsonrequest += "\"}";       
                var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/updateGrup");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + App.LoginToken);
                request.AddParameter("application/json", jsonrequest, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                if (response.IsSuccessful)
                {
                    await DisplayAlert("Uyarı", "Kayıt Başarıyla Güncellendi", "Tamam");
                    await Navigation.PushAsync(new RehberPage());
                }
                else
                {
                    await DisplayAlert("Uyarı", "Birşeyler Ters Gitti, Tekrar Deneyin", "Tamam");
                }
            }
        }
    }
}
    
    
   

    
