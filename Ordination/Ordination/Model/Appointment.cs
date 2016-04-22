using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordination.Model
{
    public class Appointment
    {
        private int _id_appointment;
        private string _date;
        private string _patient_last_name;
        private string _patient_first_name;
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
    }
}
