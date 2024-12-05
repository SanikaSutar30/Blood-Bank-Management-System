using BloodBankManagementSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.DAL
{
    class userDAL
    {
       
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT data from database
        public DataTable Select()
        {
           
            SqlConnection conn = new SqlConnection(myconnstrng);

 
            DataTable dt = new DataTable();

            try
            {
        
                String sql = "SELECT * FROM tbl_users";

    
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

     
                conn.Open();

    
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
 
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region Insert Data into Database for User Module
        public bool Insert(userBLL u)
        {

            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {

                String sql = "INSERT INTO tbl_users(username, password, full_name, contact, address) VALUES (@username, @password, @full_name, @contact, @address)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();


                if(rows>0)
                {
                    
                    isSuccess = true;
                }
                else
                {

                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region UPDATE data in database (User Module)
        public bool Update(userBLL u)
        {

            bool isSuccess = false;


            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
               
                string sql = "UPDATE tbl_users SET username=@username, password=@password, full_name=@full_name, contact=@contact, address=@address WHERE user_id=@user_id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();



                if(rows>0)
                {
                   
                    isSuccess = true;

                }
                else
                {
         
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
          
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Delete Data from Database (User Module)
        public bool Delete(userBLL u)
        {

            bool isSuccess = false;


            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {

                String sql = "DELETE FROM tbl_users WHERE user_id=@user_id";

   
                SqlCommand cmd = new SqlCommand(sql, conn);

    
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                conn.Open();


                int rows = cmd.ExecuteNonQuery();

     

                if(rows>0)
                {
               
                    isSuccess = true;
                }
                else
                {
         
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region SEARCH
        public DataTable Search(string keywords)
        {
  
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();


            try
            {

                String sql = "SELECT * FROM tbl_users WHERE user_id LIKE '%" + keywords + "%' OR full_name LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%'";


                SqlCommand cmd = new SqlCommand(sql, conn);


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }

            return dt;
        }
        #endregion


        #region
        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();

            SqlConnection conn = new SqlConnection(myconnstrng);


            DataTable dt = new DataTable();

            try
            {
    
                string sql = "SELECT user_id FROM tbl_users WHERE username='"+ username +"'";

     
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
   
                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count>0)
                {
                    u.user_id = int.Parse(dt.Rows[0]["user_id"].ToString());
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
               
                conn.Close();
            }

            return u;
        }
        #endregion
    }
}
