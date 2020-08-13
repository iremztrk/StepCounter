using SQLite;

namespace StepCounter.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public int Weight { get; set; }

        public string FullName
        {
            get => this.Name + " " + this.Surname;
        }
    }
}
