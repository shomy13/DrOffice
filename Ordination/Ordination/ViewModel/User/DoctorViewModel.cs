using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class DoctorViewModel : TabViewModel
    {
        UserDAO userDao = new UserDAO();

        RelayCommand _updateDoctorUC;

        #region Constructor
        public DoctorViewModel()
        {
        }
        #endregion

        #region UpdateDoctorUC
        public ICommand UpdateDoctorUC
        {
            get
            {
                _updateDoctorUC = new RelayCommand(param => this.DoctorUpdate());
                return _updateDoctorUC;
            }
        }
        void DoctorUpdate()
        {
            userDao.UpdateDoctorDAO();
        }
        #endregion
    }
}
