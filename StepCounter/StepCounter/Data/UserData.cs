using SQLite;
using StepCounter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StepCounter.Data
{
    public class UserData
    {
        readonly SQLiteAsyncConnection _database;

        public UserData(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> ReadUserAsync(int Id)
        {
            return _database.Table<User>().Where(p => p.UserId == Id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.UserId != 0)
                return _database.UpdateAsync(user);
            else
                return _database.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public Response Login(string username, string password)
        {
            Response response = new Response();

            var user = _database.Table<User>().Where(p => p.Username == username).FirstOrDefaultAsync().Result;
            if(user == null)
            {
                response.ErrorMessage = "yok boyle bır kullanıcı";
                response.Success = false;
                return response;
            }
            else if(user.Password != password)
            {
                response.ErrorMessage = "hatalı bır parola gırdınız";
                response.Success = false;
                return response;
            }
            else
            {
                response.Success = true;
                return response;
            }
        }
    }
}
