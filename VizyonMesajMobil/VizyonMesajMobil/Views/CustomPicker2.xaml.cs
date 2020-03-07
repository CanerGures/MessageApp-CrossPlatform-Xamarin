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
    public partial class CustomPicker2 : ContentView
    {
        UserDetailViewModel viewModel;
        public Grup SelectGroup;
        public CustomPicker2()
        {
            InitializeComponent();
        }
        public CustomPicker2(UserDetailViewModel viewModel,Grup SelectGroup)
        {
            InitializeComponent();
            this.SelectGroup = SelectGroup;
            picker.ItemsSource = viewModel.GroupList;
            picker.ItemDisplayBinding = new Binding("GrupAdi");
            picker.SelectedItem = viewModel.GroupList.First(p => p.ID == SelectGroup.ID);
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var stack = this.Parent as StackLayout;

            stack.Children.Remove(this);
        }
        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            SelectGroup = (Grup)picker.SelectedItem;
        }
    }
}