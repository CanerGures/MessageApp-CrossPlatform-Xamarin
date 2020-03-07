using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VizyonMesajMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageRegistry : ContentPage
    {
        UserDetailViewModel viewModel;
        public MessageRegistry(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        private async void SMSendd_Clicked(object sender, EventArgs e)
        {
            string message = myEditor.Text;
            SendSMS(message);
            await Navigation.PushAsync(new RehberPage());
        }
        public async void SendSMS(string message)
        {
            string TelefonNoList = "";
            TelefonNoList += "<TelefonNo><TelNo>";
            TelefonNoList += viewModel.Item.GsmNo1;
            TelefonNoList += " </TelNo></TelefonNo >";
            TelefonNoList += "<TelefonNo><TelNo>";
            TelefonNoList += viewModel.Item.GsmNo2;
            TelefonNoList += " </TelNo></TelefonNo >";
            string XmlRequest = "";
            XmlRequest += "<soap:Envelope xmlns:xsi=\"http:************" xmlns:xsd=\"http:**********" xmlns:soap=\"http:**********"> ";
            XmlRequest += "  <soap:Header>";
            XmlRequest += "    <securty xmlns=\"http:*********">";
            XmlRequest += "      <KullaniciAdi>aykatech</KullaniciAdi>";
            XmlRequest += "      <Parola>*****</Parola>";
            XmlRequest += "      <Orijin>AYKATECH</Orijin>";
            XmlRequest += "    </securty>";
            XmlRequest += "  </soap:Header>";
            XmlRequest += "    <soap:Body>";
            XmlRequest += "      <TekMesajCokNumara xmlns=\"*********">";
            XmlRequest += "        <message>" + message + "</message>";
            XmlRequest += "        <numbers>";
            XmlRequest += TelefonNoList;
            XmlRequest += "        </numbers>";
            XmlRequest += "      </TekMesajCokNumara>";
            XmlRequest += "    </soap:Body>";
            XmlRequest += "  </soap:Envelope>";
            var client = new RestClient("http:/***********");
            client.Timeout = -1;
            var response = new RestRequest(Method.POST);
            response.AddHeader("Content-Type", "text/xml");
            response.AddHeader("Authorization", "Bearer " + App.LoginToken);
            response.AddParameter("text/xml", XmlRequest, ParameterType.RequestBody);
            IRestResponse result = client.Execute(response);
            if (result.IsSuccessful)
            {
                await DisplayAlert("Uyarı", "Mesaj Başarıyla Gönderildi", "Tamam");
            }
            else
            {
                await DisplayAlert("Uyarı", "Birşeyler Ters Gitti, Tekrar Deneyin", "Tamam");
                
            }
        }
    }
}