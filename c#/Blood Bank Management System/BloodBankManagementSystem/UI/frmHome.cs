using BloodBankManagementSystem.DAL;
using BloodBankManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        donorDAL dal = new donorDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            frmUsers users = new frmUsers();
            users.Show();
            this.Hide();
        }

        private void donorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            frmDonors donors = new frmDonors();
            donors.Show();
            this.Hide();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

            allDonorCount();

            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            lblUser.Text = frmLogin.loggedInUser;
        }

        public void allDonorCount()
        {
            lblOpositiveCount.Text = dal.countDonors("O+");
            lblOnegativeCount.Text = dal.countDonors("O-");
            lblApositiveCount.Text = dal.countDonors("A+");
            lblAnegativeCount.Text = dal.countDonors("A-");
            lblBpositiveCount.Text = dal.countDonors("B+");
            lblBnegativeCount.Text = dal.countDonors("B-");
            lblABpositiveCount.Text = dal.countDonors("AB+");
            lblABnegativeCount.Text = dal.countDonors("AB-");
        }

        private void frmHome_Activated(object sender, EventArgs e)
        {

            allDonorCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string keywords = txtSearch.Text;


            if(keywords != null)
            {
 
                DataTable dt = dal.Search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
        }

        private void lblAppName_Click(object sender, EventArgs e)
        {

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void lblBloodGroup_Click(object sender, EventArgs e)
        {

        }
    }
}
