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
    public class AdminDAO
    {
        #region Constructor
        public AdminDAO()
        {

        }
        #endregion

        string ConnectionString = @" Data source = C:\Users\dev2\Documents\Shomy\DrOffice\DrOffice.db";

        #region AddDoctor
        public void AddDoctorDAO( Doctor d)
        {
            using(SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using(SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = String.Format(" INSERT INTO doctor (first_name, last_name, address, email, phone_number, birth_date, user_name, password) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}')",
                           d.First_name, d.Last_name, d.Address, d.Email, d.Phone_number, d.Birth_date, d.User_name, d.Password);
                         
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
        public ObservableCollection<Doctor> ReturnAllDoctorsDAO()
        {
            ObservableCollection<Doctor> list = new ObservableCollection<Doctor>();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM doctor 
                        ORDER BY first_name, last_name, birth_date";
                     
                        using(SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Doctor d = new Doctor();
                                d.Id_doctor = reader.GetInt32(0);
                                d.First_name = reader["first_name"].ToString();
                                d.Last_name = reader["last_name"].ToString();
                                d.Address = reader["address"].ToString();
                                d.Email = reader["email"].ToString();
                                d.Phone_number = reader["phone_number"].ToString();
                                d.Birth_date = Convert.ToDateTime(reader["birth_date"]).ToString("dd/MM/yyyy");
                                d.User_name = reader["user_name"].ToString();
                                list.Add(d);


                            }
                        }
                        for (int i = 0; i < list.Count(); i++)
                            Console.WriteLine(list[i].First_name);
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

        #region DeleteDoctor
        public void DeleteDoctorDAO( int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"DELETE FROM doctor WHERE id_doctor = "+id+"";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Doctor with id="+id+" successfully deleted!");
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
        public int AdminExistsDAO(string user_name, string password)
        {
            
            
            int id = -1;
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"SELECT * FROM admin WHERE user_name='"+user_name+"' and password='"+password+"'";

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

        #region UpdatePassword
        public void UpdatePassworDAO(string newPassword, int id)
        {
            
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = @"UPDATE admin
                                            SET password = '"+newPassword+"'WHERE id_admin = "+id+"";
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
