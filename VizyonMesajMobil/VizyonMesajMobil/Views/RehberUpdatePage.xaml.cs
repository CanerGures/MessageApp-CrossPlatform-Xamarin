using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using VizyonMesajMobil.Models;
using VizyonMesajMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RehberUpdatePage : ContentPage
    {
        private UserDetailViewModel viewModel;
        
        public RehberUpdatePage(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            
            foreach (Grup rehbergrup in viewModel.RehberGrupList)
            {
                CustomPicker2 newPicker = new CustomPicker2(viewModel, rehbergrup);
                pickerStack.Children.Add(newPicker);
            }            
        }
        public RehberUpdatePage()
        {
            InitializeComponent();
        }

        public void Update_Clicked(object sender, EventArgs e)
        {
            Rehber kullanici = new Rehber
            {
                ID = Convert.ToInt32(viewModel.Item.ID),
                GsmNo1 = Phone1Entry.Text,
                GsmNo2 = Phone2Entry.Text,
                Adi = NameEntry.Text,
                Soyadi = SurnameEntry.Text
            };
            Update(kullanici);            
            AddtoGroup();
        }
        public async void Update(Rehber kullanici)
        {
            bool answer = await DisplayAlert("Question?", "Would you like to Update This User ?", "Yes", "No");
            if (answer)
            {
                var jsonrequest = "";
                jsonrequest += "{\"GsmNo1\": \"";
                jsonrequest += kullanici.GsmNo1;
                jsonrequest += "\",\"GsmNo2\": \"";
                jsonrequest += kullanici.GsmNo2;
                jsonrequest += "\",\"Adi\": \"";
                jsonrequest += kullanici.Adi;
                jsonrequest += "\",\"Soyadi\": \"";
                jsonrequest += kullanici.Soyadi;
                jsonrequest += "\",\"AktifPasif\":\"1\" ,\"ID\":\"";
                jsonrequest += kullanici.ID;
                jsonrequest += "\"}";

                var client = new RestClient("http://192.168.1.57/VizyonMesajMobilAPI/api/updateRehber");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + App.LoginToken);
                request.AddParameter("application/json", jsonrequest, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);                
                if (response.IsSuccessful)
                {
                    await DisplayAlert("Alert", "User Successfully Updated", "OK");
                    await Navigation.PushAsync(new RehberPage());
                }
                else
                {
                    await DisplayAlert("Alert", "Something Went Wrong! Try Again.", "OK");
                }

            }
        }
        public void AddtoGroup()
        {

            List<int> GrupIDList = new List<int>();
            //Adds groupID's to a variable 
            foreach (var pickerview in pickerStack.Children)
            {
                if (pickerview is CustomPicker )
                {
                    Grup selectgroup = ((CustomPicker)pickerview).SelectGroup;//here you will get your select group,then you could get its ID ,GroupName or SubsID 
                    GrupIDList.Add(selectgroup.ID);
                }
                else if (pickerview is CustomPicker2)
                {
                    Grup selectgroup = ((CustomPicker2)pickerview).SelectGroup;//here you will get your select group,then you could get its ID ,GroupName or SubsID 
                    GrupIDList.Add(selectgroup.ID);
                }
            }
            GrupRehber newGrupRehber = new GrupRehber();
            newGrupRehber.RehberID = viewModel.Item.ID;
            newGrupRehber.GrupIDList = GrupIDList;
            //This request deletes all previous groups of the registry and adds current ones.
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