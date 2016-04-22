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
        UserViewModel uvm = new UserViewModel();
        Patient _selectedItem = new Patient();
        static UserDAO userDao = new UserDAO();

        private List<Patient> _allPatientList = userDao.ReturnAllPatientsDAO();

        public AllPatientsViewModel()
        {
        }

        #region getset
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
            
            /*PatientViewModel tab = uvm.ContentTab.FirstOrDefault(vm => vm is PatientViewModel)
                as PatientViewModel;

            if (tab == null)
            {
                tab = new PatientViewModel();
                uvm.ContentTab.Add(tab);
                uvm.SetActiveTab(tab);
            }*/

            PatientViewModel tab = new PatientViewModel();
             uvm.ContentTab.Add(tab);
           uvm.SetActiveTab(tab);
           
        }
        #endregion

    }
}
