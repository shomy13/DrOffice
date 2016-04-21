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
        UserViewModel uv = new UserViewModel();

        Patient _selectedItem = new Patient();
        static UserDAO userDao = new UserDAO();

        private List<Patient> _allPatientList = userDao.ReturnAllPatientsDAO();

        public AllPatientsViewModel()
        {
        }
        public List<Patient> AllPatientList
        {
            get { return _allPatientList; }
            
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

        public ICommand SelectedCommand
        {
            get
            {
                _selectedCommand = new RelayCommand(param => CommandSelected());
                return _selectedCommand;

            }
        }

        public void CommandSelected()
        {
            
           PatientViewModel tab = new PatientViewModel();
             uv.ContentTab.Add(tab);
            uv.SetActiveTab(tab);
            Console.WriteLine(SelectedItem.Id_patient);
        }

       
    }
}
