using StepCounter.DependencyServices;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StepCounter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private string stepCount;
        public string StepCount
        {
            get
            {
                return stepCount;
            }
            set
            {
                stepCount = value;
                OnPropertyChanged(nameof(StepCount));
            }
        }


        IStepCounter stepCounterService;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = this;
            stepCounterService = DependencyService.Get<IStepCounter>();
            if(stepCounterService.IsAvailable())
            {
                stepCounterService.StepCountChanged += StepCounterService_StepCountChanged;
                stepCounterService.Start();
            }
            else
            {
                StepCount = "Adım sayma fonksiyonu aktif değil";
            }
        }

        private void StepCounterService_StepCountChanged(object sender, StepCountChangedEventArgs e)
        {
            StepCount = e.Value.ToString();
        }
    }
}