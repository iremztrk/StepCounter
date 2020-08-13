using SQLite;
using System;

namespace StepCounter.Models
{
    public class DailyStep
    {
        [PrimaryKey, AutoIncrement]
        public long DailyStepId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public int StepData { get; set; }
        public int OperationType { get; set; }

        public string StepCountStr
        {
            get => StepData.ToString() + " steps";
        }

        public string LoggedDate
        {
            get => Date.ToString("dd MMMM");
        }

        public string LoggedDay
        {
            get => Date.ToString("dd");
        }

        public string LoggedDay2
        {
            get => Date.ToString("ddd");
        }

        public string LoggedMonth
        {
            get => Date.ToString("MMMM");
        }

        public string Distance
        {
            get => Helpers.ConversionHelper.StepsToKilometers(StepData).ToString("0.00 km");
        }

        public string Distance2
        {
            get => Helpers.ConversionHelper.StepsToKilometers(StepData).ToString("0.00");
        }

        public string Calories
        {
            get => Helpers.ConversionHelper.CaloriesBurnt(Helpers.ConversionHelper.StepsToMiles(StepData)) + " calories";
        }

        public string Calories2
        {
            get => Helpers.ConversionHelper.CaloriesBurnt(Helpers.ConversionHelper.StepsToMiles(StepData));
        }
    }
}