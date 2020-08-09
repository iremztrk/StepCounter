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

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            (this.Parent as MasterDetailPage).IsPresented = false;
            UserInformationView userInformation = new UserInformationView();
            GeneralStepView generalStepView = new GeneralStepView();
            (this.Parent as MasterDetailPage).Detail.Navigation.PushAsync(generalStepView); //(userInformation);
        }
    }
}