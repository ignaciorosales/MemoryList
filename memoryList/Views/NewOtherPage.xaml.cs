using System;
using System.Collections.Generic;
using memoryList.Models;
using Xamarin.Forms;

namespace memoryList.Views
{
    public partial class NewOtherPage : ContentPage
    {
        public Other Other { get; set; }

        public NewOtherPage()
        {
            InitializeComponent();

            Other = new Other
            {
                Text = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddOther", Other);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
