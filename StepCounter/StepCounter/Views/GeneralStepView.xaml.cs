using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StepCounter.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StepCounter.Views
{
    public partial class GeneralStepView : ContentPage
    {
        List<Month> AllMonths;
        ObservableCollection<Month> months;
        public ObservableCollection<Month> Months
        {
            get => months;
            set
            {
                months = value;
                OnPropertyChanged(nameof(Months));
            }
        }

        private ObservableCollection<DailyStep> stepCountLogList;
        public ObservableCollection<DailyStep> StepCountLogList
        {
            get => stepCountLogList;
            set
            {
                stepCountLogList = value;
                OnPropertyChanged(nameof(StepCountLogList));
            }
        }
        private ObservableCollection<DailyStep> AllStepCountLogList;

        public GeneralStepView()
        {
            AllStepCountLogList = new ObservableCollection<DailyStep>();
            StepCountLogList = new ObservableCollection<DailyStep>();
            Months = new ObservableCollection<Month>();
            AllMonths = new List<Month>();
            InitializeComponent();
            BindingContext = this;

            Init();
        }

        private void Init()
        {
            AllStepCountLogList = App.StepDB.GetCurrentUserDailyStepAsync(App.currentUser.UserId).Result.ToObservableCollection();

            foreach (var item in AllStepCountLogList.Select(p => p.Date))
            {
                if (!Months.Any(p => p.MonthOrder == item.Month))
                {
                    Months.Add(new Month()
                    {
                        MonthOrder = item.Month,
                        MonthName = item.ToString("MMMM"),
                        Year = item.Year
                    });
                }
            }

            Months = Months.OrderByDescending(p => p.Year).ThenByDescending(p => p.MonthOrder).ToObservableCollection();
            Months.First().IsSelected = true;
            StepCountLogList = AllStepCountLogList.Where(p => p.Date.Year == Months.First(x => x.IsSelected).Year && p.Date.Month == Months.First(x => x.IsSelected).MonthOrder).OrderByDescending(p => p.Date).ToObservableCollection();
        }

        private void Month_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                carouselViewMonths.ItemsSource = null;
                var selected = ((e as TappedEventArgs).Parameter as Month);
                if (selected != null)
                {
                    Months.ForEach(p => p.IsSelected = false);
                    Months.First(p => p.MonthOrder == selected.MonthOrder).IsSelected = true;
                    StepCountLogList = AllStepCountLogList.Where(p => p.Date.Year == selected.Year && p.Date.Month == selected.MonthOrder).OrderByDescending(p => p.Date).ToObservableCollection();
                }
                carouselViewMonths.ItemsSource = Months;
            });

        }

        private void StepCountLog_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var selected = ((e as TappedEventArgs).Parameter as DailyStep);
                if (selected != null)
                {
                    //await MainMenuView.Instance.Detail.Navigation.PushAsync(new StepCounterDetailView(selected));
                }
            });
        }

    }

    public class Month
    {
        public int MonthOrder { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public bool IsSelected { get; set; } = false;
        public string MonthYear
        {
            get => MonthName + ", " + Year;
        }

    }

}
