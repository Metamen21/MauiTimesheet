using MauiTimesheet.Data.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTimesheet.Data
{
    public class DatabaseService
    {
        public readonly SQLiteAsyncConnection _connection;
        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "maui-timesheet.db3");
            _connection = new SQLiteAsyncConnection(dbPath,
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);


            InitializeDatabase();
        }

        public void InitializeDatabase()
        {
            var connection = _connection.GetConnection();
            using(connection.Lock())
            {
                connection.CreateTable<User>();
            }
        }
         
        public async Task EnsureCreatedAsync()
        {
            await _connection.CreateTableAsync<User>();
        }



        public Task InsertAsyncUser(User user) => _connection.InsertAsync(user);

        public Task<User> GetUserByUsername(string username) =>
            _connection.Table<User>()
            .Where(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefaultAsync();

    }
}
