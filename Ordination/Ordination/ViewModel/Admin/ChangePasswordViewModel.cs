using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ordination.ViewModel.Admin
{
    class ChangePasswordViewModel : TabViewModel
    {
        AdminDAO adminDao = new AdminDAO();

        RelayCommand _changePasswordUC;

        #region Constructor
        public ChangePasswordViewModel()
        {
        }
        #endregion
       
        #region ChangePassword
        public ICommand ChangePasswordUC
        {
            get
            {
                _changePasswordUC = new RelayCommand(param => this.PasswordChange());
                return _changePasswordUC;
            }
        }

        void PasswordChange()
        {
            if (adminDao.AdminExistsDAO() != "")
            {
                adminDao.UpdatePassworDAO();
            }
            else
            {
                MessageBox.Show("nema");
            }
        }
        #endregion
    }

}
