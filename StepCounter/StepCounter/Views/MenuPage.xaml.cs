using System.ComponentModel;
using Xamarin.Forms;

namespace StepCounter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer99
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void buttonLogout_Clicked(object sender, System.EventArgs e)
        {
            App.todayStep.StepData = App.todayStep.StepData + App.currentSteps;
            App.StepDB.SaveUserDailyStepAsync(App.todayStep);
            Navigation.PopToRootAsync();
        }
    }
}