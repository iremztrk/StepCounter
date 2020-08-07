using System.ComponentModel;
using Xamarin.Forms;

namespace StepCounter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer99
    // by visiting https://aka.ms/xamarinforms-previewer
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

            OperationsSelection();
        }

        private async void OperationsSelection()
        {
            App.selectedOperation = await DisplayActionSheet("Operasyon Seçiniz", "cancel", null, App.operations);
            if (!string.IsNullOrEmpty(App.selectedOperation) && (App.selectedOperation != "cancel"))
            {

            }
        }

        private void buttonLogout_Clicked(object sender, System.EventArgs e)
        {
            App.todayStep.StepData = App.todayStep.StepData + App.currentSteps;
            App.StepDB.SaveUserDailyStepAsync(App.todayStep);
            Navigation.PopToRootAsync();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            (this.Parent as MasterDetailPage).IsPresented = false;
            UserInformationView userInformation = new UserInformationView();
            (this.Parent as MasterDetailPage).Detail.Navigation.PushAsync(userInformation);
            //(this.Parent as MasterDetailPage).Navigation.PushAsync(userInformation);
            //Navigation.PushAsync(userInformation);
        }
    }
}