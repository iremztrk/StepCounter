using SQLite;
using StepCounter.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StepCounter.Data
{
    public class DailyStepData
    {
        readonly SQLiteAsyncConnection _database;

        public DailyStepData(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<DailyStep>().Wait();
        }

        public Task<List<DailyStep>> GetCurrentUserDailyStepAsync(int userId)
        {
            return _database.Table<DailyStep>().Where(p => p.UserId == userId).ToListAsync();
        }

        public Task<DailyStep> ReadTodayDailyStepAsync(int userId)
        {
            return _database.Table<DailyStep>().Where(p => p.UserId == userId && p.Date == DateTime.Today).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserDailyStepAsync(DailyStep dailyStep)
        {
            if (dailyStep.DailyStepId != 0)
                return _database.UpdateAsync(dailyStep);
            else
                return _database.InsertAsync(dailyStep);
        }
    }
}