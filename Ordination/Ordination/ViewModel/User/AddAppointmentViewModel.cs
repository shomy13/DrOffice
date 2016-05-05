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
    class AddAppointmentViewModel : TabViewModel
    {
        UserDAO userDao = new UserDAO();
        RelayCommand _addAppointment;
        Appointment _appointmant = new Appointment();
        PatientViewModel pvm = new PatientViewModel();
        public AddAppointmentViewModel()
        {
            base.DisplayText = "Add appointment";
        }

        #region getset
        public string Symptoms
        {
            get { return _appointmant.Symptoms; }
            set { _appointmant.Symptoms = value; }
        }

        public string Diagnosis
        {
            get { return _appointmant.Diagnosis; }
            set { _appointmant.Diagnosis = value; }
        }

        public string Treatment
        {
            get { return _appointmant.Treatment; }
            set { _appointmant.Treatment = value; }
        }

        #endregion

        #region AddAppointmentCommand
        public ICommand AddAppointment
        {
            get
            {
                _addAppointment = new RelayCommand(
                    param => AppointmentAdd(),
                    param=>CanSave);
                return _addAppointment;
            }
        }
        void AppointmentAdd()
        {
            userDao.AddAppointmentDAO(_appointmant, pvm.returnID());
           
            OnRequestClose();
        }

        bool CanSave
        {
            get { return _appointmant.IsValid; }
        }
        #endregion
    }
}
