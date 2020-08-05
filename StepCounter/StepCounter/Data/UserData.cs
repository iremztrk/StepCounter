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

        public Task<User> ReadUserWithUsernameAsync(string username)
        {
            return _database.Table<User>().Where(p => p.Username == username).FirstOrDefaultAsync();
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

        public Response CheckUserBeforeInsert(User user)
        {
            Response response = new Response();
            User existUser = new User();

            existUser = _database.Table<User>().Where(p => p.Username == user.Username).FirstOrDefaultAsync().Result;
            if(existUser != null)
            {
                response.Success = false;
                response.ErrorMessage = "Bu kullanıcı adı daha once alınmıstır";

                return response;
            }

            existUser = _database.Table<User>().Where(p => p.Email == user.Email).FirstOrDefaultAsync().Result;
            if (existUser != null)
            {
                response.Success = false;
                response.ErrorMessage = "Bu email ile daha onceden bır kayıt olusturulmustur";

                return response;
            }

            return response;
        }

        public Response Login(string username, string password)
        {
            Response response = new Response();

            var user = _database.Table<User>().Where(p => p.Username == username).FirstOrDefaultAsync().Result;
            if(user == null)
            {
                response.ErrorMessage = "User not found";
                response.Success = false;
                return response;
            }
            else if(user.Password != password)
            {
                response.ErrorMessage = "The password you entered is incorrect";
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
