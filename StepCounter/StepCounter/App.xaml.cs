﻿using StepCounter.Views;
using Xamarin.Forms;
using StepCounter.Data;
using System.IO;
using System;
using StepCounter.Models;

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
        public static DailyStep todayStep;
        public static int currentSteps;

        public static string[] operations = new string[10];
        public static string selectedOperation;


        public App()
        {
            SetOperationValues();

            InitializeComponent();
            MainPage = new NavigationPage(new LoginView());
        }

        private void SetOperationValues()
        {
            operations.SetValue("Toplama", 0);
            operations.SetValue("Ürün Alım", 1);
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
    }
}
