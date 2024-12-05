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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private bool IsPasswordValid(string password)
        {

            if (password.Length < 5)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
                return false;

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            u.full_name = txtFullName.Text;
            u.username = txtUsername.Text;

            u.contact = txtContact.Text;
            u.address = txtAddress.Text;



            if (!IsPasswordValid(txtPassword.Text))
            {
                MessageBox.Show("Password must be at least 5 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 special symbol.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                u.password = txtPassword.Text;
            }
          


            bool success = dal.Insert(u);

            if(success==true)
            {
                MessageBox.Show("New User added Successfully.");

                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                Clear();
            }
            else
            {
              
                MessageBox.Show("Failed to Add New User.");
            }
        }

     
        public void Clear()
        {
            txtFullName.Text = "";
            txtUsername.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtPassword.Text = "";
            txtUserID.Text = "";

        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int RowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[RowIndex].Cells[0].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[RowIndex].Cells[1].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[RowIndex].Cells[3].Value.ToString();
            txtFullName.Text = dgvUsers.Rows[RowIndex].Cells[4].Value.ToString();
            txtContact.Text = dgvUsers.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[RowIndex].Cells[6].Value.ToString();

        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
 
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
      
            u.user_id = int.Parse(txtUserID.Text);
            u.full_name = txtFullName.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;

            bool success = dal.Update(u);

            if (success==true)
            {
                MessageBox.Show("User Updated Successfully.");

                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            u.user_id = int.Parse(txtUserID.Text);

            bool success = dal.Delete(u);

            if(success==true)
            {
                MessageBox.Show("User Deleted Successfully.");
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String keywords = txtSearch.Text;

            if(keywords!=null)
            {
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
        }
    }
}
