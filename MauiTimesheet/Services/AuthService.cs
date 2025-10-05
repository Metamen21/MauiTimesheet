using MauiTimesheet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTimesheet.Services
{
    internal class AuthService
    {
        private List<RegisterModel> users = [];
        public string Name { get; private set; }
        //public int Id { get; private set; }

        public bool IsLoggedIn => Name != null;
        public async Task<bool> LoginAsync(LoginModel model)
        {
            var user = users.FirstOrDefault(u => u.UserName.Equals(model.UserName,StringComparison.OrdinalIgnoreCase) && u.Password==model.Password);
            if(user == null)
            {
                MauiInterop.AlertAsync("Invalid credentials","Error");
                return false;
            }
            //Id = user.Id;
            Name = user.FullName;
            return true;

        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var existing=users.FirstOrDefault(u => u.UserName == model.UserName);
            if(existing != null)
            {
             existing.FullName = model.FullName;
                existing.Password = model.Password;
            }
            else
            {
                users.Add(model);
            }
        }

        public void Logout()
        {
            Name = string.Empty;
            //Id = 0;
        }
    }
}
