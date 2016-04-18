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

        RelayCommand _userChangePasswordUC;

        #region Constructor
        public UserChangePasswordViewModel()
        {
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
            
            if (userDao.UserExistsDAO() != "")
            {
                userDao.UserChangePasswordDAO();
            }
            else
            {
                MessageBox.Show("ne postoji");
            }
        }
        #endregion

    }
}
