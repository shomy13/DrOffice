using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.Admin
{
    class AllDoctorsVewModel : TabViewModel
    {
        static AdminDAO adminDao = new AdminDAO();

        private ObservableCollection<Doctor> _allDoctorsList = adminDao.ReturnAllDoctorsDAO();
        private Doctor _selectedDoctor = new Doctor();

        

        public ObservableCollection<Doctor> AllDoctorsList
        {
            get { return _allDoctorsList; }
        }

        public Doctor DoctorSelected
        {
            get { return _selectedDoctor; }
            set { _selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        /*public List<Doctor> AllDoctorsList
        {
            get { return _allDoctorsList; }
        }*/

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
            adminDao.DeleteDoctorDAO(DoctorSelected.Id_doctor);
        }
        #endregion
    }
}
