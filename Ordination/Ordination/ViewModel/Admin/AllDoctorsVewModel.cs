using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.Admin
{
    class AllDoctorsVewModel : TabViewModel
    {
        

       static AdminDAO adminDao = new AdminDAO();

        private ObservableCollection<Doctor> _allDoctorsList =
            //new ObservableCollection<Doctor>();
         adminDao.ReturnAllDoctorsDAO();
        private Doctor _selectedDoctor = new Doctor();

        

        public ObservableCollection<Doctor> AllDoctorsList
        {
            

            get { return _allDoctorsList; }
            set { _allDoctorsList = value;
                OnPropertyChanged("_allDoctorList");
            }
        }

        public Doctor DoctorSelected
        {
            get { return _selectedDoctor; }
            set { _selectedDoctor = value;
            }
        }

      

        RelayCommand _deleteDoctorUC;
        

        #region Constructor
        public AllDoctorsVewModel()
        {
            base.DisplayText = "All Doctors";
            // _allDoctorsList = adminDao.ReturnAllDoctorsDAO();
            // OnPropertyChanged("AllDoctorsList");
            
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
           Doctor d = new Doctor();
           
           adminDao.DeleteDoctorDAO(DoctorSelected.Id_doctor);
           _allDoctorsList = adminDao.ReturnAllDoctorsDAO();
            OnPropertyChanged("AllDoctorsList");
            
        }
        #endregion
        

        public void NewDoctorFunction(Doctor doc)
        {
            adminDao.AddDoctorDAO(doc);
            _allDoctorsList.Add(doc);
            OnPropertyChanged("AllDoctorsList");
            
           
        }
    }
}
