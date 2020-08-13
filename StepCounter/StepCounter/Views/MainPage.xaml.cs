using StepCounter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            App.todayStep = new List<DailyStep>();
            InitializeComponent();

            // MasterBehavior = MasterBehavior.Default;

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
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            OperationsSelection();
        }

        private async void OperationsSelection()
        {
            App.selectedOperation = await DisplayActionSheet("Operasyon Seçiniz", "cancel", null, App.operations.Values.ToArray());
            if (!string.IsNullOrEmpty(App.selectedOperation) && (App.selectedOperation != "cancel"))
            {
                
            }
        }
    }
}