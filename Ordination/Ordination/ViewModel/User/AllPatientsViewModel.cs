using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class AllPatientsViewModel : TabViewModel
    {
        RelayCommand _selectedCommand;
        RelayCommand _deletePatient;
        RelayCommand _searchCommand;
        UserViewModel uvm = new UserViewModel();
        Patient _selectedItem = new Patient();
        static UserDAO userDao = new UserDAO();
        AllAppointmentsViewModel avm = new AllAppointmentsViewModel();
        private static int _id_patient;
        private string _textSearch;
       private ObservableCollection<Patient> _allPatientList =
           // new ObservableCollection<Patient>();
           userDao.ReturnAllPatientsDAO(idLogedIn);


        #region Constructor
        public AllPatientsViewModel()
        {
                

            base.DisplayText = "All patients";
        }
        #endregion

        #region getset

        #region ICollectionView
        private ICollectionView _allPatientsView { get; set; }
        #endregion

        #region IdPatient
        public int Id_patient
        {
            get { return _id_patient; }
            set { _id_patient = value; }
        }
        #endregion

        #region ObservableCollection
        public ObservableCollection<Patient> AllPatientList
        {
            get {return _allPatientList;}
        }
        #endregion

        #region Patient
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

        #region TextSearch
        public string TextSearch
        {
            get { return _textSearch; }
            set { _textSearch = value;
                
            }
        }
        #endregion

        #endregion

        #region Filter
        private bool FilterPatient(object item)
        {

            Patient p = item as Patient;

            //if (p.Last_name.Contains(TextSearch))
            if(p.Last_name.ToLower().Contains(TextSearch.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
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
            _allPatientList = userDao.ReturnAllPatientsDAO(idLogedIn);
            OnPropertyChanged("AllPatientList");
          
        }
        #endregion

        #region SearchCommand
        public ICommand SearchCommand
        {
            get
            {
                _searchCommand = new RelayCommand(param => Search());
                return _searchCommand;
            }
        }

        public void Search()
        {
            
                this._allPatientsView = CollectionViewSource.GetDefaultView(_allPatientList);
            this._allPatientsView.Filter = FilterPatient;
            OnPropertyChanged("AllPatientList");
           

            
        }
        #endregion

    }
}