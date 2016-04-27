using Ordination.Model;
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
    class AllPatientsViewModel : TabViewModel
    {
        RelayCommand _selectedCommand;
        RelayCommand _deletePatient;
        UserViewModel uvm = new UserViewModel();
        Patient _selectedItem = new Patient();
        static UserDAO userDao = new UserDAO();
        AllAppointmentsViewModel avm = new AllAppointmentsViewModel();
        private static int _id_patient;

        private List<Patient> _allPatientList = userDao.ReturnAllPatientsDAO();

        public AllPatientsViewModel()
        {
            base.DisplayText = "All patients";
        }

        #region getset

        public int Id_patient
        {
            get { return _id_patient; }
            set { _id_patient = value; }
        }

        public List<Patient> AllPatientList
        {
            get { return _allPatientList;}
           
            
        }

        public Patient SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem= value;
               // OnPropertyChanged("SelectedItem");
            }
        }
        #endregion


        #region SelectedCommand
        public ICommand SelectedCommand
        {
            get
            {
                _selectedCommand = new RelayCommand(param => CommandSelected(param));
                return _selectedCommand;

            }
        }

        public void CommandSelected(object s)
        {

            _id_patient = Int32.Parse(s.ToString());
            PatientViewModel tab = new PatientViewModel();
             uvm.ContentTab.Add(tab);
           uvm.SetActiveTab(tab);

            
           
           
        }

        public int returnId()
        {   if (avm.returnInt() != 0)
                return avm.returnInt();
            return _id_patient;
        }
        #endregion

        #region DeletePatientCommand
        public ICommand DeletePatient
        {
            get
            {
                _deletePatient = new RelayCommand(param => PatientDelete(param));
                return _deletePatient;

            }
        }

        public void PatientDelete(object s)
        {
            int id = Int32.Parse(s.ToString());
            userDao.DeletePatientDAO(id);
            _allPatientList = userDao.ReturnAllPatientsDAO();
            OnPropertyChanged("AllPatientList");
        }
        #endregion

    }
}
