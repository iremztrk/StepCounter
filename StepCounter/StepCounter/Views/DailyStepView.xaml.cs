using System;
using System.Linq;
using Microcharts;
using SkiaSharp;
using StepCounter.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StepCounter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyStepView : ContentPage
    {
        private int stepCount;
        public int StepCount
        {
            get
            {
                return stepCount;
            }
            set
            {
                stepCount = value;
                OnPropertyChanged(nameof(StepCount));
                OnPropertyChanged(nameof(Distance));
                OnPropertyChanged(nameof(Calories));
            }
        }

        public string Distance
        {
            get
            {
                if (StepCount > 0)
                    return Helpers.ConversionHelper.StepsToKilometers(StepCount).ToString("0.00");
                else
                    return "0";
            }
        }

        public string Calories
        {
            get
            {
                if (StepCount > 0)
                    return Helpers.ConversionHelper.CaloriesBurnt(Helpers.ConversionHelper.StepsToMiles(StepCount));
                else
                    return "0";
            }
        }

        IStepCounter stepCounterService;
        Microcharts.Entry[] entries;

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
                if(App.currentSteps != 0 )
                    StepCount = App.currentSteps;
                else
                    StepCount = 15793;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //stepCounterService.Stop();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var allStepData = App.StepDB.GetCurrentUserDailyStepAsync(App.currentUser.UserId).Result;

            allStepData = allStepData.Where(p => p.Date > DateTime.Now.Date.AddDays(-7)).OrderBy(p => p.Date).ToList();

            if (allStepData.Any())
            {
                entries = new Microcharts.Entry[allStepData.Count];

                foreach (var item in allStepData)
                {
                    entries.SetValue(new Microcharts.Entry(item.StepData)
                    {
                        Label = item.LoggedDate,
                        ValueLabel = item.StepData.ToString(),
                        Color = SKColor.Parse("#800080"),
                        TextColor = SKColor.Parse("#4B0082")
                    }, allStepData.IndexOf(item));
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    chart1.Chart = new LineChart
                    {
                        Entries = entries,
                        LineMode = LineMode.Straight,
                        LineSize = 8,
                        LabelTextSize = 42,
                        PointMode = PointMode.Circle,
                        PointSize = 18,
                        BackgroundColor = SKColor.Parse("#ADD8E6")
                    };
                });
            }
        }

        private void StepCounterService_StepCountChanged(object sender, StepCountChangedEventArgs e)
        {
            App.currentSteps = (int)e.Value;
            Device.BeginInvokeOnMainThread(() =>
            {
                StepCount = (App.todayStep.Sum(p => p.StepData) + App.currentSteps);
            });
        }
    }
}