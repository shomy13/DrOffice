using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordination.Model
{
    public class Appointment : IDataErrorInfo
    {
        private int _id_appointment;
        private string _date;
        private string _patient_last_name;
        private string _patient_first_name;
        private int _patient_id_patient;
        private string _symptoms;
        private string _diagnosis;
        private string _treatment;

        #region getset
        public int Id_appointment
        {
            get { return _id_appointment; }
            set { _id_appointment = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Patient_last_name
        {
            get { return _patient_last_name; }
            set { _patient_last_name = value; }
        }

        public string Patient_first_name
        {
            get { return _patient_first_name; }
            set { _patient_first_name = value; }
        }

        public int Patient_id_patient
        {
            get { return _patient_id_patient; }
            set { _patient_id_patient = value; }
        }

        public string Symptoms
        {
            get { return _symptoms; }
            set { _symptoms = value; }
        }

        public string Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = value; }
        }

        public string Treatment
        {
            get { return _treatment; }
            set { _treatment = value; }
        }
        #endregion

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        string IDataErrorInfo.Error { get { return null; } }


        static readonly string[] ValidatedProperties =
      {
            "Symptoms",
            "Diagnosis",
            "Treatment"
            
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
                case "Symptoms":
                    error = this.ValidateSymptoms();
                    break;
                case "Diagnosis":
                    error = this.ValidateDiagnosis();
                    break;
                case "Treatment":
                    error = this.ValidateTreatment();
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

        string ValidateSymptoms()
        {
            if (IsStringMissing(this.Symptoms))
            {
                return "Symptoms is missing";
            }
            return null;
        }

        string ValidateDiagnosis()
        {
            if (IsStringMissing(this.Diagnosis))
            {
                return "Diagnosis is missing";
            }
            return null;
        }

        string ValidateTreatment()
        {
            if (IsStringMissing(this.Treatment))
            {
                return "Treatment is missing";
            }
            return null;
        }
    }
}
