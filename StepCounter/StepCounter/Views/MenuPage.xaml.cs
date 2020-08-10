using System;
using System.ComponentModel;
using System.Linq;
using StepCounter.Models;
using Xamarin.Forms;

namespace StepCounter.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
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

        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = this;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Username = App.currentUser.Name + " " + App.currentUser.Surname;

           // OperationsSelection();
        }

        private async void OperationsSelection()
        {
            App.selectedOperation = await DisplayActionSheet("Operasyon Seçiniz", "cancel", null, App.operations.Values.ToArray());
            if (!string.IsNullOrEmpty(App.selectedOperation) && (App.selectedOperation != "cancel"))
            {

            }
        }

        private void buttonLogout_Clicked(object sender, System.EventArgs e)
        {
            DailyStep dailyStep = new DailyStep()
            {
                Date = DateTime.Now.Date,
                UserId = App.currentUser.UserId,
                OperationType = App.GetOperationValue(),
                StepData = App.currentSteps
            };

            App.StepDB.SaveUserDailyStepAsync(dailyStep);
            Navigation.PopToRootAsync();
        }

        private void buttonUserInfo_Clicked(object sender, EventArgs e)
        {
            (this.Parent as MasterDetailPage).IsPresented = false;
            UserInformationView userInformation = new UserInformationView();
            (this.Parent as MasterDetailPage).Detail.Navigation.PushAsync(userInformation); 
        }

        private void buttonMonthlySummary_Clicked(object sender, EventArgs e)
        {
            (this.Parent as MasterDetailPage).IsPresented = false;
            GeneralStepView generalStepView = new GeneralStepView();
            (this.Parent as MasterDetailPage).Detail.Navigation.PushAsync(generalStepView); 
        }
    }
}