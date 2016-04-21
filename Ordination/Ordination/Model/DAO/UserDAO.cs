using System;
using System.Collections.Generic;
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

        public void ReturnOnePatientDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM patient WHERE id_patient = 1";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine(reader["id_patient"] + " " + reader["first_name"] + " " + reader["last_name"]
                                    + " " + reader["address"] + " " + reader["email"] + " " + reader["phone_number"] + " " + Convert.ToDateTime(reader["Birth_date"]).ToString("dd/MM/yyyy"));
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

        public void ReturnAllAppointmentsDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT appointment.id_appointment, appointment.date, patient.last_name, patient.first_name
                                            FROM appointment
                                            INNER JOIN chart
                                            ON appointment.fk_id_chart = chart.id_chart
                                            INNER JOIN patient
                                            ON chart.fk_id_patient = patient.id_patient
                                            ORDER BY date, last_name";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["id_appointment"] + " " + Convert.ToDateTime(reader["date"]).ToString("dd/MM/yyyy") + " " + reader["last_name"]
                                    + " " + reader["first_name"]);
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

        #region ReturnDoctor
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
        public void AddAppointmentDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"INSERT INTO appointment
                                          (fk_id_chart, symptoms, diagnosis, treatment)
                                          VALUES
                                          (3, 'bol u kolenu', 'upala mekog tkiva', 'hladne obloge')";
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
    }
}
