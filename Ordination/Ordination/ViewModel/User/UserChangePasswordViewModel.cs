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
    class UserChangePasswordViewModel : TabViewModel
    {
        UserDAO userDao = new UserDAO();

        private string _user_name;
        private string _password;
        private string _passwordNew;
        private string _passwordNewConfirm;
        RelayCommand _userChangePasswordUC;

        #region Constructor
        public UserChangePasswordViewModel()
        {
            base.DisplayText = "Change password";
        }
        #endregion

        #region getset
        public string User_name
        {
            get { return _user_name; }
            set
            {
                if (value == _user_name)
                    return;
                _user_name = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password)
                    return;
                _password = value;
            }
        }

        public string PasswordNew
        {
            get { return _passwordNew; }
            set
            {
                if (value == _passwordNew)
                    return;
                _passwordNew = value;
            }
        }

        public string PasswordNewConfirm
        {
            get { return _passwordNewConfirm; }
            set
            {
                if (value == _passwordNewConfirm)
                    return;
                _passwordNewConfirm = value;
            }
        }
        #endregion

        #region UserChangePassword
        public ICommand UserChangePasswordUC
        {
            get
            {
                _userChangePasswordUC = new RelayCommand(param =>this.PasswordChange());
                return _userChangePasswordUC;
            }
        }

        void PasswordChange()
        {
            int id = userDao.DoctorExistsDAO(User_name, Password);


            if ( id == idLogedIn)
            {
                if (PasswordNew != null && PasswordNewConfirm != null)
                {
                    if (PasswordNew.Equals(PasswordNewConfirm))
                    {
                        userDao.UserChangePasswordDAO(PasswordNew, id);
                        OnRequestClose();
                    }
                    else
                    {
                        MessageBox.Show("Confirm password failed", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Enter and confirm new password", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            else
            {
                MessageBox.Show("Username or password is incorrect!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        #endregion

    }
}
