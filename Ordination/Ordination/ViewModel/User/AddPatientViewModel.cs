using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class AddPatientViewModel : TabViewModel, IDataErrorInfo
    {
        UserDAO userDao = new UserDAO();

        Patient _patient = new Patient();
        UserViewModel uvm = new UserViewModel();
        RelayCommand _addNewPatientUC;
        public AddPatientViewModel()
        {
            base.DisplayText = "Add patient";
        }

        #region getset
        public string First_name
        {
            get { return _patient.First_name; }
            set
            {
                if (value == _patient.First_name)
                    return;

                _patient.First_name = value;
            }
        }

        public string Last_name
        {
            get { return _patient.Last_name; }
            set
            {
                if (value == _patient.Last_name)
                    return;

                _patient.Last_name = value;
            }
        }

        public string Address
        {
            get { return _patient.Address; }
            set
            {
                if (value == _patient.Address)
                    return;

                _patient.Address = value;
            }
        }
        public string Email
        {
            get { return _patient.Email; }
            set
            {
                if (value == _patient.Email)
                    return;

                _patient.Email = value;
            }
        }

        public string Phone_number
        {
            get { return _patient.Phone_number; }
            set
            {
                if (value == _patient.Phone_number)
                    return;

                _patient.Phone_number = value;
            }
        }

        public string Birth_date
        {
            get { return _patient.Birth_date; }
            set
            {
                if (value == _patient.Birth_date)
                    return;

                _patient.Birth_date = value;
            }
        }

        
        #endregion

        #region AddNewPatient
        public ICommand AddNewPatientUC
        {
            get
            {
                _addNewPatientUC = new RelayCommand(
                    param => this.NewPatientAdd(),
                    param => this.CanSave);
                return _addNewPatientUC;
            }
        }

        void NewPatientAdd()
        {
            userDao.AddPatientDAO(_patient);
           
            userDao.AddChartDAO(idLogedIn, userDao.ReturnLastPatientDAO());
            
            base.DisplayText = String.Format("{0} {1}", _patient.First_name, _patient.Last_name);
            OnPropertyChanged("DisplayText");
            OnPropertyChanged("AllPatientList");

            AllPatientsViewModel tab = uvm.ContentTab.FirstOrDefault(vm => vm is AllPatientsViewModel)
                   as AllPatientsViewModel;

            if (tab != null)
            {
                uvm.ContentTab.Remove(tab);
                tab = new AllPatientsViewModel();
                uvm.ContentTab.Add(tab);
                uvm.SetActiveTab(tab);
            }
            
            
        }

        bool CanSave
        {
            get {return _patient.IsValid; }
        }
        #endregion

        #region IDataErrorInfo
        string IDataErrorInfo.Error
        {
            get { return (_patient as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (_patient as IDataErrorInfo)[propertyName];

                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }
        #endregion
    }
}
