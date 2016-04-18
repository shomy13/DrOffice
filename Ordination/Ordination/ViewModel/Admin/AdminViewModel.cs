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

namespace Ordination.ViewModel.Admin
{
    class AdminViewModel : TabViewModel
    {
        ObservableCollection<TabViewModel> _tabTemplate;

        AdminDAO adminDao = new AdminDAO();

        RelayCommand _addNewDoctor;
        RelayCommand _returnAllDoctors;
        RelayCommand _changePassword;
        

        #region Constructor
        public AdminViewModel()
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

        #region Command AddNewDoctor
        public ICommand AddNewDoctor
        {
            get
            {
                _addNewDoctor = new RelayCommand(param => this.NewDoctorAdd());
                return _addNewDoctor;
            }
        }

        void NewDoctorAdd()
        {
            AddDoctorViewModel tab = new AddDoctorViewModel();
            this.ContentTab.Add(tab);
            this.SetActiveTab(tab);
        }
        #endregion

        #region Command ReturnAllDoctors
        public ICommand ReturnAllDoctors
        {
            get
            {
                _returnAllDoctors = new RelayCommand(param => this.AllPatientsReturn());
                return _returnAllDoctors;
            }
        }

        void AllPatientsReturn()
        {
            adminDao.ReturnAllDoctorsDAO();

            AllDoctorsVewModel tab = this.ContentTab.FirstOrDefault(vm => vm is AllDoctorsVewModel)
                as AllDoctorsVewModel;

            if (tab == null)
            {
                tab = new AllDoctorsVewModel();
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
            ChangePasswordViewModel tab = this.ContentTab.FirstOrDefault(vm => vm is ChangePasswordViewModel)
                   as ChangePasswordViewModel;

            if (tab == null)
            {
                tab = new ChangePasswordViewModel();
                this.ContentTab.Add(tab);
                this.SetActiveTab(tab);
            }
        }
        #endregion
    }
}
