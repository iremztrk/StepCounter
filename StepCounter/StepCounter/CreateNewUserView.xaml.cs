using StepCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StepCounter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewUserView : ContentPage
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        const string passwordRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-.,;:]).{4,}$";

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (string.IsNullOrEmpty(value))
                    labelNameValidation.IsVisible = true;
                else
                    labelNameValidation.IsVisible = false;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string surname;
        public string Surname
        { 
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                if (string.IsNullOrEmpty(value))
                    labelSurnameValidation.IsVisible = true;
                else
                    labelSurnameValidation.IsVisible = false;
                OnPropertyChanged(nameof(Surname));
            }
        }
        private string email;
        public string Email
        {
            get
            {
                return email; 

            }
            set
            {
                email = value;
                if (string.IsNullOrEmpty(value) || CheckMailRegex() == false)
                    labelEmailValidation.IsVisible = true;
                else
                    labelEmailValidation.IsVisible = false;
                OnPropertyChanged(nameof(Email));
            }
        }
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
                if (string.IsNullOrEmpty(value))
                    labelUsernameValidation.IsVisible = true;
                else
                    labelUsernameValidation.IsVisible = false;
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
                if (string.IsNullOrEmpty(value) || IsPasswordConformingToStandarts() == false)
                    labelPasswordValidation.IsVisible = true;
                else
                    labelPasswordValidation.IsVisible = false;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string confirmpassword;
        public string ConfirmPassword
        {
            get
            {
                return confirmpassword;
            }
            set
            {
                confirmpassword = value;
                if (string.IsNullOrEmpty(value) || value.Equals(Password) == false)
                    labelConfirmPasswordValidation.IsVisible = true;
                else
                    labelConfirmPasswordValidation.IsVisible = false;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        
        public CreateNewUserView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (labelNameValidation.IsVisible == false &&
                labelSurnameValidation.IsVisible == false &&
                labelEmailValidation.IsVisible == false &&
                labelUsernameValidation.IsVisible == false &&
                labelPasswordValidation.IsVisible == false &&
                labelConfirmPasswordValidation.IsVisible == false)
            {

                User newUser = new User();
                newUser.Username = Username;
                newUser.Name = Name;
                newUser.Surname = Surname;
                newUser.Password = Password;
                newUser.Email = Email;

                await App.UserDB.SaveUserAsync(newUser);

                await Navigation.PopAsync();

                await DisplayAlert("", "Registration successful", "Ok");
            }
            else
               await DisplayAlert("Warning", "You must fill in all of the fields", "Ok");
        }

        private bool CheckMailRegex()
        {
            return Regex.IsMatch(Email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        private bool IsPasswordConformingToStandarts()
        {
            return (Regex.IsMatch(Password, passwordRegex, RegexOptions.None, TimeSpan.FromMilliseconds(250)));
        }

        private void NameEntry_Completed(object sender, EventArgs e)
        {
            entrySurname.Focus();
        }

        private void SurnameEntry_Completed(object sender, EventArgs e)
        {
            entryEmail.Focus();
        }

        private void entryEmail_Completed(object sender, EventArgs e)
        {
            entryUsername.Focus();
        }

        private void entryUsername_Completed(object sender, EventArgs e)
        {
            entryPassword.Focus();
        }

        private void entryPassword_Completed(object sender, EventArgs e)
        {
            entryConfirmPassword.Focus();
        }

    }
}