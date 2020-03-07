using System;
using VizyonMesajMobil.Models;
using VizyonMesajMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VizyonMesajMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPicker : ContentView
    {
        UserDetailViewModel viewModel;
        public Grup SelectGroup;
        
        public CustomPicker()
        {
            InitializeComponent();
        }
        public CustomPicker(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            picker.ItemsSource = viewModel.GroupList;
            picker.ItemDisplayBinding = new Binding("GrupAdi");          
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