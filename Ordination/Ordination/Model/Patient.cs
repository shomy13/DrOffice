using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Ordination.Model
{
    public class Patient : IDataErrorInfo
    {
        
        private int id_patient;
        private string first_name;
        private string last_name;
        private string address;
        private string email;
        private string phone_number;
        private string birth_date;

        #region public getset
        public int Id_patient
        {
            get { return id_patient; }
            set { id_patient = value; }
        }

        public string First_name
        {
            get { return first_name; }
            set { first_name = value; }
        }

        public string Last_name
        {
            get { return last_name; }
            set { last_name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone_number
        {
            get { return phone_number; }
            set { phone_number = value; }
        }

        public string Birth_date
        {
            get { return birth_date; }
            set { birth_date = value; }
        }

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }
        #endregion

        #region Validation
        static readonly string[] ValidatedProperties =
       {
            "First_name",
            "Last_name",
            "Email",
            "Address",
            "Phone_number",
            "Birth_date"
            
        };

        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "First_name":
                    error = this.ValidateFirst_name();
                    break;
                case "Last_name":
                    error = this.ValidateLast_name();
                    break;
                case "Email":
                    error = this.ValidateEmail();
                    break;
                case "Address":
                    error = this.ValidateAddress();
                    break;
                case "Phone_number":
                    error = this.ValidatePhone_number();
                    break;
                case "Birth_date":
                    error = this.ValidateBirth_date();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Customer: " + propertyName);
                    break;
            }
            return error; ;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        string ValidateFirst_name()
        {
            if (IsStringMissing(this.First_name))
            {
                return "First name is missing";
            }
            return null;
        }

        string ValidateLast_name()
        {
            if (IsStringMissing(this.Last_name))
            {
                return "Last name is missing";
            }
            return null;
        }

        string ValidateAddress()
        {
            if (IsStringMissing(this.Address))
            {
                return "Address is missing";
            }
            return null;
        }

        string ValidatePhone_number()
        {
            if (IsStringMissing(this.Phone_number))
            {
                return "Phone number is missing";
            }
            return null;
        }

        string ValidateEmail()
        {
            if (IsStringMissing(this.Email))
            {
                return "Email is missing";
            }
            else if (!IsValidEmailAddress(this.Email))
            {
                return "Email address is invalid";
            }
            return null;
        }

        string ValidateBirth_date()
        {
            if (IsStringMissing(this.Birth_date))
            {
                return "Birth date is missing";
            }
            else if (!IsValidDate(this.Birth_date))
            {
                return "Birth date is invalid";
            }
            return null;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        static bool IsValidDate(string date)
        {
            if (IsStringMissing(date))
                return false;

            string pattern = @"((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))\-((0[13578])|(1[02]))\-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))\-((0[469])|11)\-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9][0-9](([13579][26])|([2468][048])))|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))\-02\-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))\-02\-((0[1-9])|([1][0-9])|([2][0-8])))";

            return Regex.IsMatch(date, pattern, RegexOptions.IgnoreCase);
        }

        #endregion
    }
}
