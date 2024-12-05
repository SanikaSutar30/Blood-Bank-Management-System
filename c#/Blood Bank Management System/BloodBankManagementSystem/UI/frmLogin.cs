using BloodBankManagementSystem.BLL;
using BloodBankManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        public static string loggedInUser;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            l.username = txtUsername.Text;
            l.password = txtPassword.Text;

 
            bool isSuccess = dal.loginCheck(l);

            if(isSuccess==true)
            {

                MessageBox.Show("Login Successful.");

                loggedInUser = l.username;

                frmHome home = new frmHome();
                home.Show();
                this.Hide(); 
            }
            else
            {

                MessageBox.Show("Login Failed. Try Again.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmUsers frmUsers = new frmUsers();
            frmUsers.Show();
            this.Hide();
        }
    }
}
