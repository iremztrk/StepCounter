using StepCounter.Views;
using Xamarin.Forms;
using StepCounter.Data;
using System.IO;
using System;

namespace StepCounter
{
    public partial class App : Application
    {

        private static UserData userDB;
        public static UserData UserDB
        {
            get
            {
                if(userDB == null)
                {
                    userDB = new UserData(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return userDB;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
