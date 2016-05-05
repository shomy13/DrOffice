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
    class AllAppointmentsViewModel : TabViewModel
    {
        UserDAO userDao = new UserDAO();
        UserViewModel uvm = new UserViewModel();
        RelayCommand _viewList;
        RelayCommand _selectedAppointmentCommand;
        private static int _id_patient =0;
        private ObservableCollection<Appointment> _allAppointmentsList = new ObservableCollection<Appointment>();
        
        
        static private DateTime _selectedDate;
        #region Connstructor
        public AllAppointmentsViewModel()
        {base.DisplayText = "All apointments";
        }
        #endregion

        public ICommand ViewList
        {
            get
            {
                _viewList = new RelayCommand(pram => ListView());
                return _viewList;
            }
        }

        void ListView()
        {
            _allAppointmentsList = userDao.ReturnAllAppointmentsDAO(SelectedDate, idLogedIn);
            OnPropertyChanged("AllAppointmentsList");
        }

        public ICommand SelectedAppointmentCommand
        {
            get { _selectedAppointmentCommand = new RelayCommand(param => AppointmentSelected(param));
                return _selectedAppointmentCommand;
            }
        }

        void AppointmentSelected(object s)
        {
            _id_patient = Int32.Parse(s.ToString());
            PatientViewModel tab = new PatientViewModel();
            uvm.ContentTab.Add(tab);
            uvm.SetActiveTab(tab);
            _id_patient = 0;
           
        }

        public ObservableCollection<Appointment> AllAppointmentsList
        {
            get { return _allAppointmentsList; }
            set { _allAppointmentsList = value; }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public int Id_patient
        {
            get { return _id_patient; }
            set { _id_patient = value; }
        }

        public int returnInt()
        {
            
            return Id_patient;
        }
    }
}
