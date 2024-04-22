using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.models;

namespace OnlineStore
{
    internal class LoginViewModel : LoginModel
    {
        LoginModel loginModel = new LoginModel();

        public string Username
        {
            get
            {
                return loginModel.Username;
            }
            set
            {
                loginModel.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return loginModel.Password;
            }
            set
            {
                loginModel.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string RoleUser
        {
            get
            {
                return loginModel.RoleUser;
            }
            set
            {
                loginModel.RoleUser = value;
                OnPropertyChanged(nameof(RoleUser));
            }
        }

        public bool AttemptLogin()
        {

            if (Username == "admin" && Password == "admin")
            {
                return true;

            }
            else if (Username == "user" && Password == "user")
            {
                return true;
            }
            else if (Username == "Emp" && Password == "Emp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
