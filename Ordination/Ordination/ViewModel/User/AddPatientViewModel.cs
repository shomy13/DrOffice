using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class AddPatientViewModel : TabViewModel
    {
        UserDAO userDao = new UserDAO();

        RelayCommand _addNewPatientUC;

        #region AddNewPatient
        public ICommand AddNewPatientUC
        {
            get
            {
                _addNewPatientUC = new RelayCommand(param => this.NewPatientAdd());
                return _addNewPatientUC;
            }
        }

        void NewPatientAdd()
        {
            userDao.AddPatientDAO();
            userDao.AddChartDAO();
        }
        #endregion
    }
}
