using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class PatientViewModel : TabViewModel, IDataErrorInfo
    {
        static UserDAO userDao = new UserDAO();
        UserViewModel uvm = new UserViewModel();
        static AllPatientsViewModel pvm = new AllPatientsViewModel();
        private Patient _patient = userDao.ReturnOnePatientDAO(pvm.returnId());
        private ObservableCollection<Appointment> _allAppointments = userDao.ReturnAllAppointmentsByUserDAO(pvm.returnId());
        private Appointment _appointmentById = new Appointment();
        private int _id_patient = pvm.returnId();
        RelayCommand _saveCommand;
        RelayCommand _addAppointment;
        RelayCommand _viewAppointment;

        #region Constructor
        public PatientViewModel()
        {
            base.DisplayText = String.Format("{0} {1}", _patient.Last_name, _patient.First_name);
        }
        #endregion


        #region SaveCommand
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = new RelayCommand(
                    param => CommandSave(),
                    param => canSave
                    );
                return _saveCommand;
            }
        }
        void CommandSave()
        {
            userDao.UpdatePatientDAO(_patient, pvm.returnId());
            base.DisplayText = string.Format("{0} {1}", _patient.Last_name, _patient.First_name);
            OnPropertyChanged("DisplayText");
        }
        bool canSave
        {
            get { return _patient.IsValid; }
        }

        #endregion

        #region AddAppointmentCommand
        public ICommand AddAppointment
        {
            get {
                _addAppointment = new RelayCommand(param => AppointmentAdd());
                return _addAppointment; }
        }
        void AppointmentAdd()
        {
            AddAppointmentViewModel tab = new AddAppointmentViewModel();
            uvm.ContentTab.Add(tab);
            uvm.SetActiveTab(tab);
        }
        #endregion

        #region ViewAppointmen
        public ICommand ViewAppointment
        {
            get { _viewAppointment = new RelayCommand(param => AppointmentView(param));
                return _viewAppointment;
            }
        }

        void AppointmentView(object s)
        {
            int id = Int32.Parse(s.ToString());
            _appointmentById = userDao.ReturnAppointmentByIdDAO(id);
            OnPropertyChanged("Symptoms");
            OnPropertyChanged("Diagnosis");
            OnPropertyChanged("Treatment");
            
        }
        #endregion

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

        public int Id_patient
        {
            get { return _id_patient; }
            set { _id_patient = value; }
        }
        public string Symptoms
        {
            get { return _appointmentById.Symptoms; }
        }

        public string Diagnosis
        {
            get { return _appointmentById.Diagnosis; }
        }

        public string Treatment
        {
            get { return _appointmentById.Treatment; }
        }
        public ObservableCollection<Appointment> AllAppointments
        {
            get { return _allAppointments; }
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

        public int returnID()
        {
            return _id_patient;
        }
    }
}
