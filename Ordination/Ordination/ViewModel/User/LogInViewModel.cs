using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class LogInViewModel : TabViewModel
    {
        
        RelayCommand _logInCommand;
        UserDAO userDao = new UserDAO();
        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #region Constructor
        public LogInViewModel()
        {
            base.DisplayText = "LogIn";
        }
        #endregion

        #region LogInCommand
        public ICommand LogInCommand
        {
            get { _logInCommand = new RelayCommand(param => LogIn());
                return _logInCommand;
            }
        }

        void LogIn()
        {
            int i = userDao.DoctorExistsDAO(UserName, Password);
            if (i == 0)
            {
                MessageBox.Show("Wrong username or password!","ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                idLogedIn = i;
                
                OnRequestClose();
                MessageBox.Show("Welcome "+userDao.ReturnDoctorDAO(i).First_name.ToString(), "Hi!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion
    }
}
