using System;
using System.Collections.Generic;
using memoryList.ViewModels;
using Xamarin.Forms;

namespace memoryList.Views
{
    public partial class OthersPage : ContentPage
    {
        OthersViewModel viewModel;

        public OthersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OthersViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewOtherPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Others.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}
