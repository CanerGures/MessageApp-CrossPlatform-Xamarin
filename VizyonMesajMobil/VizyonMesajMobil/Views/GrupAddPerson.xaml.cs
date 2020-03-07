using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VizyonMesajMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupAddPerson : ContentPage
    {
        GrupDetailViewModel viewModel;
        public GrupAddPerson()
        {
            InitializeComponent();
        }
        public GrupAddPerson(GrupDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public async void Save_Clicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            var surname = SurnameEntry.Text;
            var phone1 = Phone1Entry.Text;
            var phone2 = Phone2Entry.Text;

            string JsonRequest = "";
            JsonRequest += "{\"GsmNo1\": \"";
            JsonRequest += phone1;
            JsonRequest += "\",\"GsmNo2\": \"";
            JsonRequest += phone2;
            JsonRequest += "\",\"Adi\": \"";
            JsonRequest += name;
            JsonRequest += "\",\"Soyadi\": \"";
            JsonRequest += surname;
            JsonRequest += "\",\"AktifPasif\":\"1\"}";


            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/newRehber");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + App.LoginToken);
            request.AddParameter("application/json", JsonRequest, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);            
            await Navigation.PopModalAsync();
            if (response.IsSuccessful)
            {
                dynamic newRehberResponse = JObject.Parse(response.Content);
                int newRehberID = newRehberResponse.RehberID;
                AddtoGroup(newRehberID);
                await DisplayAlert("Uyarı", "Yeni Kayıt Başarıyla Oluşturuldu", "Tamam");
                await Navigation.PushAsync(new GrupPage());
            }
            else
            {
                await DisplayAlert("Uyarı", "Birşeyler Ters Gitti, Tekrar Deneyin", "Tamam");
            }
            
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public void AddtoGroup(int newRehberID)
        {

            var ID = viewModel.Grup.ID;
            var rehberID = newRehberID;

            string jsonrequest = "";
            jsonrequest += "{\n\t\"RehberID\":\"";
            jsonrequest += rehberID;
            /* ID is GroupID below*/
            jsonrequest += "\",\"GrupID\":\"";
            jsonrequest += ID;
            jsonrequest += "\"\n}\n";


            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/AddRehberToGroups");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + App.LoginToken);
            request.AddParameter("application/json", jsonrequest, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}