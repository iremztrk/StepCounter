using StepCounter.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StepCounter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyStepView : ContentPage
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

        public DailyStepView()
        {
            InitializeComponent();
            BindingContext = this;

            stepCounterService = DependencyService.Get<IStepCounter>();

            if (stepCounterService.IsAvailable())
            {
                stepCounterService.StepCountChanged += StepCounterService_StepCountChanged;
                stepCounterService.Start();
            }
            else
            {
                StepCount = "Adım sayma fonksiyonu aktif değil";
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //stepCounterService.Stop();
        }

        private void StepCounterService_StepCountChanged(object sender, StepCountChangedEventArgs e)
        {
            App.currentSteps = (int)e.Value;
            StepCount = (App.todayStep.StepData + App.currentSteps).ToString(); //e.Value.ToString();
            //App.todayStep.StepData = (int)e.Value;
        }
    }
}