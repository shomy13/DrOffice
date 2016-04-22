using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class PatientViewModel : TabViewModel
    {
        static UserDAO userDao = new UserDAO();
        UserViewModel uvm = new UserViewModel();

        private Patient _patient = userDao.ReturnOnePatientDAO();
        private ObservableCollection<Appointment> _allAppointments = userDao.ReturnAllAppointmentsByUserDAO();
        private Appointment _appointmentById = userDao.ReturnAppointmentByIdDAO();
        RelayCommand _saveCommand;
        RelayCommand _addAppointment;

        #region setget
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
            get { return _allAppointments;  }
        }

        #endregion

        #region SaveCommand
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = new RelayCommand(param => CommandSave());
                return _saveCommand;
            }
        }
        void CommandSave()
        {
            userDao.ReturnAppointmentByIdDAO();
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

        #region Constructor
        public PatientViewModel()
        {
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
        #endregion
    }
}
