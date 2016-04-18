using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordination.Model
{
    public class Doctor
    {
        private int id_doctor;
        private string first_name;
        private string last_name;
        private string address;
        private string phone_number;
        private string birth_date;
        private string user_name;
        private string password;

        #region public getset
        public int Id_doctor {
            get
            {
                return id_doctor;
            } set {
               
                id_doctor = value;
            }
        }

        public string First_name
        {
            get
            {
                return first_name;
            }
            set
            {
                
                first_name = value;
            }
        }

        public string Last_name
        {
            get
            {
                return last_name;
            }
            set
            {
                
                last_name = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
            
                address = value;
            }
        }

        public string Phone_number
        {
            get
            {
                return phone_number;
            }
            set
            {
                phone_number = value;
            }
        }

        public string Birth_date
        {
            get
            {
                return birth_date;
            }
            set
            {
                birth_date = value;
            }
        }

        public string User_name
        {
            get
            {
                return user_name;
            }
            set
            {
                user_name = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        #endregion
    }
}
