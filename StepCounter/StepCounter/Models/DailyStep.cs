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
    }
}