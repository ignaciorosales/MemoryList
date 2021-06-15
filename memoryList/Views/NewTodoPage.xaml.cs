using System;
using System.Collections.Generic;
using memoryList.Models;
using Xamarin.Forms;

namespace memoryList.Views
{
    public partial class NewTodoPage : ContentPage
    {
        public Todo Todo { get; set; }

        public NewTodoPage()
        {
            InitializeComponent();

            Todo = new Todo
            {
                Text = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddTodo", Todo);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
