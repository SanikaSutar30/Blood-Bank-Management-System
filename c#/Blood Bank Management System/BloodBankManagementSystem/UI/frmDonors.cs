using BloodBankManagementSystem.BLL;
using BloodBankManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class frmDonors : Form
    {
        public frmDonors()
        {
            InitializeComponent();
        }

        donorBLL d = new donorBLL();
        donorDAL dal = new donorDAL();
        userDAL udal = new userDAL();

        private void frmDonors_Load(object sender, EventArgs e)
        {
          
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;


        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
          
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            
            d.address = txtAddress.Text;
            d.added_date = DateTime.Now;


            if (txtContact.Text.Length != 10 || !txtContact.Text.All(char.IsDigit))
            {
                MessageBox.Show("Contact number must contain exactly 10 digits.", "Invalid Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                d.contact = txtContact.Text;
            }


            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            bool isSuccess = dal.Insert(d);

            if(isSuccess==true)
            {

                MessageBox.Show("New Donor Added Successfully");

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

    
                Clear();
            }
            else
            {
            
                MessageBox.Show("Failed to Add new Donor.");
            }
        }

        public void Clear()
        {

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtDonorID.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";

        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int RowIndex = e.RowIndex;

            txtDonorID.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            d.donor_id = int.Parse(txtDonorID.Text);
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
 
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            bool isSuccess = dal.Update(d);

            if (isSuccess == true)
            {
                MessageBox.Show("Donor updated Successfully.");
                Clear();
 
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

            }
            else
            {

                MessageBox.Show("Failed to update donors.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            d.donor_id = Convert.ToInt32(txtDonorID.Text);

            bool isSuccess = dal.Delete(d);

            if(isSuccess==true)
            {
                MessageBox.Show("Donor Deleted Successfully.");

                Clear();

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Delete Donor");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
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

        private void button1_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Show();
            this.Hide();
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
