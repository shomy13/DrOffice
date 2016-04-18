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
    public class AdminDAO
    {
        public AdminDAO()
        {

        }

        string ConnectionString = @" Data source = C:\Users\dev2\Documents\Shomy\DrOffice\DrOffice.db";

        #region AddDoctor
        public void AddDoctorDAO()
        {
            using(SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using(SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"INSERT INTO doctor 
                        (first_name, last_name, address, email, phone_number, birth_date, user_name, password)
                        VALUES
                        ('Nikola', 'Ninkovic', 'Rajiceva 18 Cacak', 'dzigi@gmail.com', '0654565878', '1988-05-25', 'dzigi', 'dzigi')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Doctor successfully added!");
                        con.Close();
                    }
                    catch(Exception ex)
                    {
                        
                        MessageBox.Show(ex.Message);

                    }
                    finally
                    {
                        if(con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        
                    }
                }
            }
        }
        #endregion

        #region ReturnAllDoctors
        public void ReturnAllDoctorsDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM doctor";
                     
                        using(SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
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

        #region DeleteDoctor
        public void DeleteDoctorDAO()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"DELETE FROM doctor WHERE id_doctor = 3";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Doctor successfully deleted!");
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

        #region AdminExists
        public string AdminExistsDAO()
        {
            string returnString = "";
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM admin WHERE user_name='admin' and password='123456'";

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["id_admin"] + " " + reader["user_name"] + " " + reader["password"]);

                                returnString = reader["id_admin"].ToString();
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

        #region UpdatePassword
        public void UpdatePassworDAO()
        {
            
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"UPDATE admin
                                            SET password = 'admin'
                                            WHERE id_admin = 1";
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
    }
}
