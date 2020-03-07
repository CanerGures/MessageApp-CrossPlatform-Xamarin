using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VizyonMesajMobil.Models;
using RestSharp;
using VizyonMesajMobil.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace VizyonMesajMobil.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RehberRehberNewRegistryPage : ContentPage
    {
        
        private UserDetailViewModel viewModel;
        public RehberRehberNewRegistryPage(ViewModels.UserDetailViewModel pviewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = pviewModel;
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
                await DisplayAlert("Alert", "New User Successfully Created", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Something Went Wrong! Group cannot be found. Try Again.", "OK");
            }       
                           
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public void AddtoGroup(int newRehberID)
        {
            List<int> GrupIDList = new List<int>();

            foreach (var pickerview in pickerStack.Children)
            {
                if (pickerview is CustomPicker && ((CustomPicker)pickerview).SelectGroup != null)
                {
                    Grup selectgroup = ((CustomPicker)pickerview).SelectGroup;//here you will get your select group,then you could get its ID ,GroupName or SubsID 
                    GrupIDList.Add(selectgroup.ID);
                }
            }

            GrupRehber newGrupRehber = new GrupRehber();
            newGrupRehber.RehberID = newRehberID;
            newGrupRehber.GrupIDList = GrupIDList;

            var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/addtogrup");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + App.LoginToken);
            request.AddParameter("application/json", JsonConvert.SerializeObject(newGrupRehber), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
        //Add new picker to select new group
        private void Button_Clicked(object sender, EventArgs e)
        {
            CustomPicker newPicker = new CustomPicker(viewModel);
            pickerStack.Children.Add(newPicker);
        }
    }
}