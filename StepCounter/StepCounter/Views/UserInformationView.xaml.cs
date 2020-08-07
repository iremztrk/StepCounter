using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StepCounter.Views
{
    public partial class UserInformationView : ContentPage
    {
        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private int weight;
        public int Weight
        {
            get => weight;
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private int userHeight;
        public int UserHeight
        {
            get => userHeight;
            set
            {
                userHeight = value;
                OnPropertyChanged(nameof(UserHeight));
            }
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string mail;
        public string Mail
        {
            get => mail;
            set
            {
                mail = value;
                OnPropertyChanged(nameof(Mail));
            }
        }

        public UserInformationView()
        {
            InitializeComponent();
            BindingContext = this;


            Age = 29;
            Weight = 95;
            UserHeight = 187;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Username = App.currentUser.Username;
            FullName = App.currentUser.FullName;
            Mail = App.currentUser.Email;
        }

        private async void buttonSubmit_Clicked(System.Object sender, System.EventArgs e)
        {
            await DisplayAlert("", "Successfully submitted", "OK");
            await Navigation.PopAsync();
        }
    }
}
 