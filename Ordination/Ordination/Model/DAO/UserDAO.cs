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
                                    + " " + reader["address"] + " " + reader["email"] + " " + reader["phone_number"] + " " + reader["birth_date"]);
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

        public void ReturnAllPatientsDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM patient";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["id_patient"] + " " + reader["first_name"] + " " + reader["last_name"]
                                    + " " + reader["address"] + " " + reader["email"] + " " + reader["phone_number"] + " " + reader["birth_date"]); 
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
                                Console.WriteLine(reader["id_appointment"] + " " + reader["date"] + " " + reader["last_name"]
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
        public void ReturnDoctorDAO()
        {
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
                                Console.WriteLine(reader["id_doctor"] + " " + reader["first_name"] + " " + reader["last_name"]
                                    + " " + reader["address"] + " " + reader["email"] + " " + reader["phone_number"] + " " + reader["birth_date"]
                                    + " " + reader["user_name"] + " " + reader["password"]);
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

        #region UpdateDoctor
        public void UpdateDoctorDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"UPDATE doctor
                        SET first_name = 'Uros', last_name = 'Petrovic', address = 'Kuzeljeva 13 Cacak',
                        email = 'pera@gmail.com', phone_number = '0656516469',birth_date = '1977-03-23'
                        WHERE id_doctor = 1 ";
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
        public void UserChangePasswordDAO()
        {

            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"UPDATE doctor
                                            SET password = 'doctor'
                                            WHERE id_doctor = 5";
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
        public string UserExistsDAO()
        {
            string returnString = "";
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM doctor WHERE user_name='dzigi' and password='dzigi'";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["id_doctor"] + " " + reader["first_name"] + " " + reader["last_name"]
                                     + " " + reader["address"] + " " + reader["email"] + " " + reader["phone_number"] + " " + reader["birth_date"]);

                                returnString = reader["id_doctor"].ToString();
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
            return returnString;
        }
        #endregion
    }
}
