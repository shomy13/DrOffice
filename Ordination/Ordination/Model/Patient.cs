using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Ordination.Model
{
    public class Patient
    {
        
        private int id_patient;
        private string first_name;
        private string last_name;
        private string address;
        private string phone_number;
        private string birth_date;

        #region public getset
        public int Id_Patient
        {
            get
            {
                return id_patient;
            }
            set
            {

                id_patient = value;
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
        #endregion
    }
}
