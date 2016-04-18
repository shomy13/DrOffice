using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.Admin
{
    class AddDoctorViewModel : TabViewModel
    {
        AdminDAO adminDao = new AdminDAO();

        RelayCommand _addNewDoctorUC;

        #region Constructor
        public AddDoctorViewModel()
        {
        }
        #endregion

        #region AddNewDoctor
        public ICommand AddNewDoctorUC
        {
            get
            {
                _addNewDoctorUC = new RelayCommand(param => this.NewDoctorAdd());
                return _addNewDoctorUC;
            }
        }

        void NewDoctorAdd()
        {
            adminDao.AddDoctorDAO();
        }
        #endregion
    }
}
