using StepCounter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StepCounter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewUserView());
        }

        private void switchPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value) // (sender as Switch).IsToggled
                entryPassword.IsPassword = false;
            else
                entryPassword.IsPassword = true;
        }
    }
}