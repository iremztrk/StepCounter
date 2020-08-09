using StepCounter.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Xamarin.Forms.Internals;

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
                StepCount = "15793";
            }

           
            //var entries = new[]
            //{
            //    new Microcharts.Entry(11412)
            //    {
            //        Label = "04.08",
            //        ValueLabel = "11412",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(14458)
            //    {
            //        Label = "05.08",
            //        ValueLabel = "14458",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(7428)
            //    {
            //        Label = "06.08",
            //        ValueLabel = "7428",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(12214)
            //    {
            //        Label = "07.08",
            //        ValueLabel = "12214",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(21412)
            //    {
            //        Label = "04.08",
            //        ValueLabel = "21412",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(14458)
            //    {
            //        Label = "05.08",
            //        ValueLabel = "14458",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(7428)
            //    {
            //        Label = "06.08",
            //        ValueLabel = "7428",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    },
            //    new Microcharts.Entry(9214)
            //    {
            //        Label = "07.08",
            //        ValueLabel = "9214",
            //        Color = SKColor.Parse("#800080"),
            //        TextColor = SKColor.Parse("#4B0082")
            //    }
            //};

           
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
                StepCount = (App.todayStep.Sum(p => p.StepData) + App.currentSteps).ToString();
            });
        }
    }
}