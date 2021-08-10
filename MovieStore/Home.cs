using MovieStore.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieStore
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if(RestoreDatabase())
            {
                // Create a Customer Form object
                CustomerForm form = new CustomerForm();
                form.Show();
                form.HomeForm = this;
                // Hide the Parent
                this.Hide();
            }
            else
            {
                MessageBox.Show("Backup is Restore. Try Again.");
                Application.Exit();
            }
        }

        private void btnMovie_Click(object sender, EventArgs e)
        {
            if (RestoreDatabase())
            {
                // Create a Movie Form object
                MovieForm form = new MovieForm();
                form.Show();
                form.HomeForm = this;
                // Hide the Parent
                this.Hide();
            }
            else
            {
                MessageBox.Show("Backup is Restore. Try Again.");
                Application.Exit();
            }
        }

        private void btnMovieRent_Click(object sender, EventArgs e)
        {
            if(RestoreDatabase())
            {
                // Create a Movie Rent Form object
                MovieRentForm form = new MovieRentForm();
                form.Show();
                form.HomeForm = this;
                // Hide the Parent
                this.Hide();
            }
            else
            {
                MessageBox.Show("Backup is Restore. Try Again.");
                Application.Exit();
            }
        }   

        public bool RestoreDatabase()
        {
            BackupManager manager = new BackupManager();
            if(manager.CheckDB())
            {
                return true;
            }
            else
            {
                manager.GenerateDB();
                return false;
            }
        }
    }
}
