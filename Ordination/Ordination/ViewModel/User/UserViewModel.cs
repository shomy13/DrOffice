using Ordination.Model;
using Ordination.Model.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Ordination.ViewModel.User
{
    class UserViewModel : TabViewModel
    {
        UserDAO userDao = new UserDAO();
        
        ObservableCollection<TabViewModel> _tabTemplate;

        RelayCommand _addNewPatient;
        RelayCommand _returnAllPatients;
        RelayCommand _returnAllAppointments;
        RelayCommand _returnDoctor;
        RelayCommand _changePassword;

        #region Constructor
        public UserViewModel()
        {          
        }
        #endregion

       
        #region TabView    
        public ObservableCollection<TabViewModel> ContentTab
        {
            get
            {
                if (_tabTemplate == null)
                {
                    _tabTemplate = new ObservableCollection<TabViewModel>();
                    _tabTemplate.CollectionChanged += this.OnTabChanged;
                }
                return _tabTemplate;
            }
        }
        
        void OnTabChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (TabViewModel tab in e.NewItems)
                    tab.RequestClose += this.OnTabRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (TabViewModel tab in e.OldItems)
                    tab.RequestClose -= this.OnTabRequestClose;
        }

        void OnTabRequestClose(object sender, EventArgs e)
        {
            TabViewModel tab = sender as TabViewModel;
            tab.Dispose();
            this.ContentTab.Remove(tab);
        }

        void SetActiveTab(TabViewModel tab)
        {
            Debug.Assert(this.ContentTab.Contains(tab));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.ContentTab);
            if (collectionView != null)
                collectionView.MoveCurrentTo(tab);
        }
        #endregion

        #region Command AddNewPatient
        public ICommand AddNewPatient
        {
            get
            {
                _addNewPatient = new RelayCommand(param => this.NewPatientAdd());
                return _addNewPatient;
            }
        }

        void NewPatientAdd()
        {
            AddPatientViewModel tab = new AddPatientViewModel();
            this.ContentTab.Add(tab);
            this.SetActiveTab(tab);
        }
        #endregion

        #region Command ReturnAllPatients
        public ICommand ReturnAllPatients
        {
            get
            {
                _returnAllPatients = new RelayCommand(param => this.AllPatientsReturn());
                return _returnAllPatients;
            }
        }

        void AllPatientsReturn()
        {
            userDao.ReturnAllPatientsDAO();

            AllPatientsViewModel tab = this.ContentTab.FirstOrDefault(vm => vm is AllPatientsViewModel)
                as AllPatientsViewModel;

            if(tab == null)
            {
                tab = new AllPatientsViewModel();
                this.ContentTab.Add(tab);
                this.SetActiveTab(tab);
            }
        }
        #endregion

        #region Command ReturnAllAppointments
        public ICommand ReturnAllAppointments
        {
            get
            {
                _returnAllAppointments = new RelayCommand(param => this.AllAppointmentsReturn());
                return _returnAllAppointments;
            }
        }

        void AllAppointmentsReturn()
        {
            userDao.ReturnAllAppointmentsDAO();

            AllAppointmentsViewModel tab = this.ContentTab.FirstOrDefault(vm => vm is AllAppointmentsViewModel)
                as AllAppointmentsViewModel;

            if(tab == null)
            {
                tab = new AllAppointmentsViewModel();
                this.ContentTab.Add(tab);
                this.SetActiveTab(tab);
            }
        }
        #endregion

        #region Command ReturnDoctor
        public ICommand ReturnDoctor
        {
            get
            {
                _returnDoctor = new RelayCommand(param => this.DoctorReturn());
                return _returnDoctor;
            }
        }

        void DoctorReturn()
        {
            

            DoctorViewModel tab = this.ContentTab.FirstOrDefault(vm => vm is DoctorViewModel)
                as DoctorViewModel;

            if (tab == null)
            {
                tab = new DoctorViewModel();
                this.ContentTab.Add(tab);
                this.SetActiveTab(tab);
            }
        }
        #endregion

        #region Command ChangePassword
        public ICommand ChangePassword
        {
            get
            {
                _changePassword = new RelayCommand(param => this.PasswordChange());
                return _changePassword;
            }
        }

        void PasswordChange()
        {
         UserChangePasswordViewModel tab = this.ContentTab.FirstOrDefault(vm => vm is UserChangePasswordViewModel)
                as UserChangePasswordViewModel;

            if (tab == null)
            {
                tab = new UserChangePasswordViewModel();
                this.ContentTab.Add(tab);
                this.SetActiveTab(tab);
            }
        }
        #endregion
    }
}
