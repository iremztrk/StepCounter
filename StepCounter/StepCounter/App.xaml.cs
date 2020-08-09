using StepCounter.Views;
using Xamarin.Forms;
using StepCounter.Data;
using System.IO;
using System;
using StepCounter.Models;
using System.Collections.Generic;
using System.Linq;

namespace StepCounter
{
    public partial class App : Application
    {

        private static UserData userDB;
        public static UserData UserDB
        {
            get
            {
                if(userDB == null)
                {
                    userDB = new UserData(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return userDB;
            }
        }

        private static DailyStepData stepDB;
        public static DailyStepData StepDB
        {
            get
            {
                if(stepDB == null)
                {
                    stepDB = new DailyStepData(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Step.db3"));
                }
                return stepDB;
            }
        }

        public static User currentUser;
        public static List<DailyStep> todayStep;
        public static int currentSteps;

        public static Dictionary<int, string> operations = new Dictionary<int, string>();
        public static string selectedOperation;


        public App()
        {
            SetOperationValues();

            InitializeComponent();

           // ArrangeDummyData(1);

            MainPage = new NavigationPage(new LoginView());
        }

        private void SetOperationValues()
        {
            operations.Add(0, "Genel Operasyon");
            operations.Add(1, "Ürün Alım");
            operations.Add(2, "Toplama");
        }

        public static int GetOperationValue()
        {
            return operations.Where(p => p.Value == selectedOperation).Select(p => p.Key).FirstOrDefault();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        public void ArrangeDummyData(int id)
        {
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-1), StepData = 4253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-2), StepData = 15253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-3), StepData = 6422 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-4), StepData = 331 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-5), StepData = 6312 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-6), StepData = 11973 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-7), StepData = 2517 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-8), StepData = 6754 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-9), StepData = 6348 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-10), StepData = 14412 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-11), StepData = 4253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-12), StepData = 15253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-13), StepData = 6422 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-14), StepData = 331 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-15), StepData = 6312 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-16), StepData = 11973 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-17), StepData = 2517 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-18), StepData = 6754 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-19), StepData = 6348 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-21), StepData = 4253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-22), StepData = 15253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-23), StepData = 6422 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-24), StepData = 331 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-25), StepData = 6312 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-26), StepData = 11973 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-27), StepData = 2517 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-28), StepData = 6754 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-29), StepData = 6348 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-31), StepData = 4253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-32), StepData = 15253 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-33), StepData = 6422 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-34), StepData = 331 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-35), StepData = 6312 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-36), StepData = 11973 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-37), StepData = 2517 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-38), StepData = 6754 });
            StepDB.SaveUserDailyStepAsync(new DailyStep() { UserId = id, OperationType = 0, Date = DateTime.Now.AddDays(-39), StepData = 6348 });
        }
    }
}
