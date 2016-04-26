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
        //static DateTime dt = _selectedDate;
        RelayCommand _viewList;
        private ObservableCollection<Appointment> _allAppointmentsList = new ObservableCollection<Appointment>();
            //userDao.ReturnAllAppointmentsDAO(dt.ToShortDateString());
        
        static private DateTime _selectedDate;
        #region Connstructor
        public AllAppointmentsViewModel()
        {
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
            _allAppointmentsList = userDao.ReturnAllAppointmentsDAO(SelectedDate);
            OnPropertyChanged("_allAppointmentsList");
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

                Console.WriteLine("aa" + SelectedDate.ToShortDateString());
            }
        }
    }
}
