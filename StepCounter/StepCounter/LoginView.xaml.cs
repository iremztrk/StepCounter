using StepCounter.Models;
using StepCounter.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StepCounter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            Login();
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

        private void entryUsername_Completed(object sender, EventArgs e)
        {
            entryPassword.Focus();
        }

        private void entryPassword_Completed(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(Username))
            {
                DisplayAlert("Warning", "Please enter a valid username", "Ok");
                entryUsername.Focus();
            }
            else if (string.IsNullOrEmpty(Password))
            {
                DisplayAlert("Warning", "Please enter a valid password", "Ok");
                entryPassword.Focus();
            }
            else
            {
                Response loginResponse = new Response();
                loginResponse = App.UserDB.Login(Username, Password);

                if(loginResponse.Success )
                    Navigation.PushAsync(new MainPage(Username));
                else
                {
                    DisplayAlert("Warning", loginResponse.ErrorMessage, "Ok");
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Password = "";
        }
    }
}