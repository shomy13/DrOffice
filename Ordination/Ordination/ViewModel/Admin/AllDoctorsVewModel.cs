using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.Admin
{
    class AllDoctorsVewModel : TabViewModel
    {
        AdminDAO adminDao = new AdminDAO();

        RelayCommand _deleteDoctorUC;

        #region Constructor
        public AllDoctorsVewModel()
        {
        }
        #endregion

        #region DeleteDoctor
        public ICommand DeleteDoctorUC
        {
            get
            {
                _deleteDoctorUC = new RelayCommand(param => this.DoctorDelete());
                return _deleteDoctorUC;
            }  
            }

        void DoctorDelete()
        {
            adminDao.DeleteDoctorDAO();
        }
        #endregion
    }
}
