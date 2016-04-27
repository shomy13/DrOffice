using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ordination.Model.DAO
{
    public class UserDAO
    {

        #region constructor
        public UserDAO()
        {
        }
        #endregion

        string ConnectionString = @" Data source = C:\Users\dev2\Documents\Shomy\DrOffice\DrOffice.db";

        #region ReturnOnePatient

        public Patient ReturnOnePatientDAO(int id)
        {
            Patient p = new Patient();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();
                        Console.WriteLine("dsaad"+id);
                        cmd.CommandText = @"SELECT * FROM patient WHERE id_patient = "+id+"";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                

                                p.First_name = reader["first_name"].ToString();
                                p.Last_name = reader["last_name"].ToString();
                                p.Address = reader["address"].ToString();
                                p.Email = reader["email"].ToString();
                                p.Phone_number = reader["phone_number"].ToString();
                                p.Birth_date = Convert.ToDateTime(reader["Birth_date"]).ToString("yyyy-MM-dd");

                            }
                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return p;
        }
        #endregion

        #region ReturnOneDoctor
        public Doctor ReturnDoctorDAO()
        {
            Doctor d = new Doctor();

            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM doctor WHERE id_doctor=1";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                d.First_name = reader["first_name"].ToString();
                                d.Last_name = reader["last_name"].ToString();
                                d.Address = reader["address"].ToString();
                                d.Email = reader["email"].ToString();
                                d.Phone_number = reader["phone_number"].ToString();
                                d.Birth_date = Convert.ToDateTime(reader["Birth_date"]).ToString("yyyy-MM-dd");

                                /* Console.WriteLine(reader["id_doctor"] + " " + reader["first_name"] + " " + reader["last_name"]
                                     + " " + reader["address"] + " " + reader["email"] + " " + reader["phone_number"] + " " + Convert.ToDateTime(reader["Birth_date"]).ToString("yyyy-MM-dd")
                                     + " " + reader["user_name"] + " " + reader["password"]);*/
                            }
                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return d;
        }
        #endregion

        #region ReturnLastPatientID

        public int ReturnLastPatientDAO()
        {
            int id = 0;
            
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM patient ORDER BY rowid DESC LIMIT 1";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            
                            if (reader.Read())
                            {
                                id = reader.GetInt32(0);
                            }
                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return id;
        }
        #endregion

        #region ReturnAllPatients

        public List<Patient> ReturnAllPatientsDAO()
        {
            List<Patient> list = new List<Patient>();
            

            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM patient
                        ORDER BY first_name, last_name, birth_date";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient p = new Patient();
                                p.Id_patient = reader.GetInt32(0);
                                p.First_name = reader["first_name"].ToString();
                                p.Last_name = reader["last_name"].ToString();
                                p.Address = reader["address"].ToString();
                                p.Email = reader["email"].ToString();
                                p.Phone_number = reader["phone_number"].ToString();
                                p.Birth_date = Convert.ToDateTime(reader["birth_date"]).ToString("dd/MM/yyyy");

                                list.Add(p);
                            
                                 
                            }
                        }
                       
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return list;
        }
        #endregion

        #region ReturnAllAppointments

        public ObservableCollection<Appointment> ReturnAllAppointmentsDAO( DateTime date)
        {
            ObservableCollection<Appointment> _list = new ObservableCollection<Appointment>();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT appointment.id_appointment, appointment.date, patient.last_name, patient.first_name, patient.id_patient
                                            FROM appointment
                                            INNER JOIN chart
                                            ON appointment.fk_id_chart = chart.id_chart
                                            INNER JOIN patient
                                            ON chart.fk_id_patient = patient.id_patient
                                            WHERE date = '"+date.ToString("yyyy-MM-dd")+"'ORDER BY date, last_name";
                      

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Appointment _appointment = new Appointment();

                                _appointment.Id_appointment = reader.GetInt32(0);
                                _appointment.Date = Convert.ToDateTime(reader["date"]).ToString("dd/MM/yyyy");
                                _appointment.Patient_first_name = reader["first_name"].ToString();
                                _appointment.Patient_last_name = reader["last_name"].ToString();
                                _appointment.Patient_id_patient = reader.GetInt32(4);
                                _list.Add(_appointment);

                                Console.WriteLine(reader["date"]+""+reader["first_name"]);
                              
                            }
                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return _list;
        }
        #endregion

        #region ReturnAllAppointmentsByIdUser

        public ObservableCollection<Appointment> ReturnAllAppointmentsByUserDAO(int id)
        {
            ObservableCollection<Appointment> _list = new ObservableCollection<Appointment>();

            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT appointment.id_appointment, appointment.date
                                            FROM appointment
                                            INNER JOIN chart
                                            ON appointment.fk_id_chart = chart.id_chart
                                            INNER JOIN patient
                                            ON chart.fk_id_patient = patient.id_patient
                                            WHERE id_patient ="+id+" ORDER BY date, last_name";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Appointment _appointment = new Appointment();
                                
                                _appointment.Id_appointment = reader.GetInt32(0);
                                _appointment.Date = Convert.ToDateTime(reader["date"]).ToString("dd/MM/yyyy");

                                _list.Add(_appointment);
                              
                            }
                        }

                       
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return _list;
        }
        #endregion

        #region ReturnAppointmentById

        public Appointment ReturnAppointmentByIdDAO(int id)
        {
            Appointment _appointment = new Appointment();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT *
                                            FROM appointment
                                            WHERE id_appointment = "+id+"";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _appointment.Symptoms = reader["symptoms"].ToString();
                                _appointment.Diagnosis = reader["diagnosis"].ToString();
                                _appointment.Treatment = reader["treatment"].ToString();
                                
                            }
                        }
                       // Console.WriteLine(_appointment.Symptoms+_appointment.Diagnosis+_appointment.Treatment);
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return _appointment;
        }
        #endregion

        #region AddPatient
        public void AddPatientDAO( Patient p)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = String.Format(" INSERT INTO patient (first_name, last_name, address, email, phone_number, birth_date) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                            p.First_name, p.Last_name, p.Address, p.Email, p.Phone_number, p.Birth_date);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient successfully added!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
        }
        #endregion

        #region UpdateDoctor
        public void UpdateDoctorDAO( Doctor d)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = String.Format(
                            "UPDATE doctor SET first_name = '{0}', last_name = '{1}', address = '{2}', email = '{3}', phone_number = '{4}',birth_date = '{5}' WHERE id_doctor = 1 ",
                            d.First_name, d.Last_name, d.Address, d.Email, d.Phone_number, d.Birth_date);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Doctor successfully updated!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
        }
        #endregion

        #region UpdatePatient
        public void UpdatePatientDAO(Patient p, int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = String.Format(
                            "UPDATE patient SET first_name = '{0}', last_name = '{1}', address = '{2}', email = '{3}', phone_number = '{4}',birth_date = '{5}' WHERE id_patient = {6} ",
                            p.First_name, p.Last_name, p.Address, p.Email, p.Phone_number, p.Birth_date, id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient successfully updated!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
        }
        #endregion

        #region UpdatePassword
        public void UserChangePasswordDAO(string newPassword, int id)
        {

            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"UPDATE doctor
                                            SET password = '"+newPassword+"'WHERE id_doctor = "+id+"";
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Password updated!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }

        }
        #endregion

        #region AddChart
        public void AddChartDAO( int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"INSERT INTO chart
                                          (fk_id_doctor, fk_id_patient)
                                          VALUES
                                          (1, "+id+")";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Chart successfully added!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
        }
        #endregion

        #region AddAppointment
        public void AddAppointmentDAO( Appointment a, int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = String.Format("INSERT INTO appointment (fk_id_chart, symptoms, diagnosis, treatment)VALUES((SELECT chart.id_chart FROM chart INNER JOIN patient ON chart.fk_id_patient = patient.id_patient WHERE id_patient = {3}), '{0}', '{1}', '{2}')"
                            , a.Symptoms, a.Diagnosis, a.Treatment, id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Appointment successfully added!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
        }
        #endregion

        #region UserExists
        public int UserExistsDAO(string user_name, string password)
        {
            int id = -1;
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM doctor WHERE user_name='"+user_name+"' and password='"+password+"'";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader == null)
                                    id = -1;
                                else
                                    id = reader.GetInt32(0);

                                Console.WriteLine(id);
                            }

                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
            return id;
        }
        #endregion

        #region DeletePatient
        public void DeletePatientDAO(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"DELETE FROM appointment
                                            WHERE fk_id_chart=(SELECT chart.id_chart
                                            FROM chart
                                            INNER JOIN patient
                                            ON chart.fk_id_patient = patient.id_patient
                                            WHERE
                                            patient.id_patient = "+ id+"); DELETE FROM chart WHERE id_chart=(SELECT id_chart FROM chart INNER JOIN patient ON chart.fk_id_patient = patient.id_patient WHERE patient.id_patient = "+id+"); DELETE FROM patient WHERE id_patient = " + id + "";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient with id=" + id + " successfully deleted!");
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                }
            }
        }
        #endregion
    }
}
