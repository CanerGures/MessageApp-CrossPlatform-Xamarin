using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VizyonMesajMobil.Models;
using VizyonMesajMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupMessageRegistry : ContentPage
    {
        GrupDetailViewModel viewModel;
        public GrupMessageRegistry(GrupDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        private async void SMSendd_Clicked(object sender, EventArgs e)
        {
            string message = myEditor.Text;
            await SendSMSAsync(message);
            await Navigation.PushAsync(new GrupPage());
        }

        private async Task SendSMSAsync(string message)
        {
            var grupid = viewModel.Grup.ID;
            var getrehbernames = "";
            getrehbernames += "{\"ID\":\"";
            getrehbernames += grupid;
            getrehbernames += "\"}";


            var client = new RestClient("http://***********");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + App.LoginToken);
            request.AddParameter("application/json", getrehbernames, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var GrupKullanicilarList = JsonConvert.DeserializeObject<List<Rehber>>(response.Content);
            string TelefonNoList = GetTelno(GrupKullanicilarList);
            string XmlRequest = "";
            XmlRequest += "<soap:Envelope xmlns:xsi=\"*******\" xmlns:xsd=\"http:***********" xmlns:soap=\"http:********"> ";
            XmlRequest += "  <soap:Header>";
            XmlRequest += "    <securty xmlns=\"http://tempuri.org/\">";
            XmlRequest += "      <KullaniciAdi>aykatech</KullaniciAdi>";
            XmlRequest += "      <Parola>****</Parola>";
            XmlRequest += "      <Orijin>AYKATECH</Orijin>";
            XmlRequest += "    </securty>";
            XmlRequest += "  </soap:Header>";
            XmlRequest += "    <soap:Body>";
            XmlRequest += "      <TekMesajCokNumara xmlns=\"*******">";
            XmlRequest += "        <message>" + message + "</message>";
            XmlRequest += "        <numbers>";
            XmlRequest += TelefonNoList;
            XmlRequest += "        </numbers>";
            XmlRequest += "      </TekMesajCokNumara>";
            XmlRequest += "    </soap:Body>";
            XmlRequest += "  </soap:Envelope>";
            var clientMesaj = new RestClient("*********");
            client.Timeout = -1;
            var requestMesaj = new RestRequest(Method.POST);
            requestMesaj.AddHeader("Content-Type", "text/xml");
            requestMesaj.AddHeader("Authorization", "Bearer " + App.LoginToken);
            requestMesaj.AddParameter("application/json", XmlRequest, ParameterType.RequestBody);
            IRestResponse responseMesaj = clientMesaj.Execute(requestMesaj);           
            if (responseMesaj.IsSuccessful)
            {
                await DisplayAlert("Uyarı", "Mesaj Başarıyla Gönderildi", "Tamam");

            }
            else
            {
                await DisplayAlert("Uyarı", "Birşeyler Ters Gitti, Tekrar Deneyin", "Tamam");
            }
        }

        public string GetTelno(List<Rehber> GrupKullanicilarList)
        {
            List<string> TelefonNoList = new List<string>();
            foreach (Rehber Kullanici in GrupKullanicilarList)
            {
                TelefonNoList.Add("<TelefonNo><TelNo>" + Kullanici.GsmNo1.ToString() + "</TelNo></TelefonNo>");
                TelefonNoList.Add("<TelefonNo><TelNo>" + Kullanici.GsmNo2.ToString() + "</TelNo></TelefonNo>");
            }

            return string.Join("\n", TelefonNoList);
        }
    }
    
}