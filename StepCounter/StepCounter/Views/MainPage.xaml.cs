using StepCounter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace StepCounter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
       
        public MainPage(string username)
        {
            App.currentUser = new User();
            App.todayStep = new DailyStep();
            InitializeComponent();

            MasterBehavior = MasterBehavior.Default;

            // kullanıcı bılgılerını al
            GetCurrentUser(username);

            // logout fonksıyonunu ekle ve ıcersıne o gunku adım sayısı bılgılerını kaydet
        }

        private void GetCurrentUser(string username)
        {
            App.currentUser = App.UserDB.ReadUserWithUsernameAsync(username).Result;

            // o gunlu adım sayısı bılgılerını al
            ReadCurrentUsersTodaySteps();
        }

        private void ReadCurrentUsersTodaySteps()
        {
            App.todayStep = App.StepDB.ReadTodayDailyStepAsync(App.currentUser.UserId).Result;
            if (App.todayStep == null)
            {
                App.todayStep = new DailyStep();
                App.todayStep.UserId = App.currentUser.UserId;
                App.todayStep.Date = DateTime.Today;
                App.todayStep.StepData = 0;
            }
        }

    }
}