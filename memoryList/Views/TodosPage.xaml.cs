using System;
using System.Collections.Generic;
using memoryList.ViewModels;
using Xamarin.Forms;

namespace memoryList.Views
{
    public partial class TodosPage : ContentPage
    {
        TodosViewModel viewModel;

        public TodosPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TodosViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewTodoPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Todos.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}
