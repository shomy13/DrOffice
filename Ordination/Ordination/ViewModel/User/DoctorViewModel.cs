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
    class DoctorViewModel : TabViewModel, IDataErrorInfo
    {
        
        static UserDAO userDao = new UserDAO();

        Doctor _doctor = userDao.ReturnDoctorDAO();

        

        #region getset
        public string First_name
        {
            get { return _doctor.First_name; }
            set
            {
                if (value == _doctor.First_name)
                    return;

                _doctor.First_name = value;
            }
        }

        public string Last_name
        {
            get { return _doctor.Last_name; }
            set
            {
                if (value == _doctor.Last_name)
                    return;

                _doctor.Last_name = value;
            }
        }

        public string Address
        {
            get { return _doctor.Address; }
            set
            {
                if (value == _doctor.Address)
                    return;

                _doctor.Address = value;
            }
        }
        public string Email
        {
            get { return _doctor.Email; }
            set
            {
                if (value == _doctor.Email)
                    return;

                _doctor.Email = value;
            }
        }

        public string Phone_number
        {
            get { return _doctor.Phone_number; }
            set
            {
                if (value == _doctor.Phone_number)
                    return;

                _doctor.Phone_number = value;
            }
        }

        public string Birth_date
        {
            get { return _doctor.Birth_date; }
            set
            {
                if (value == _doctor.Birth_date)
                    return;

                _doctor.Birth_date = value;
            }
        }

        public string User_name
        {
            get { return _doctor.User_name; }
            set
            {
                if (value == _doctor.User_name)
                    return;

                _doctor.User_name = value;
            }
        
        }
        #endregion
        RelayCommand _updateDoctorUC;

        #region Constructor
        public DoctorViewModel()
        {
        }
        #endregion

        #region UpdateDoctorUC
        public ICommand UpdateDoctorUC
        {
            get
            {
                _updateDoctorUC = new RelayCommand(param => this.DoctorUpdate());
                return _updateDoctorUC;
            }
        }

        
        void DoctorUpdate()
        {
            userDao.UpdateDoctorDAO(_doctor);
        }
        #endregion

        #region IDataErrorInfo
        string IDataErrorInfo.Error
        {
            get { return (_doctor as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (_doctor as IDataErrorInfo)[propertyName];

                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }
        #endregion
    }
}
