using MovieStore.DBUtility;
using MovieStore.Operation;
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
    public partial class MovieRentForm : Form
    {
        // Store the Home Form Reference
        public Form HomeForm { get; set; }
        // Create a reference of DBOperation for Database Operations
        public DBOperation operation;
        // Data Table For Display Details to Grid
        DataTable RentTable = new DataTable();
        // Store the Selected Issue ID for delete, return movie operation
        int rmid;


        public MovieRentForm()
        {
            InitializeComponent();
            // Create an Object of DB operation
            operation = new DBOperation();
            // Prepare the Combo with Database Records
            BindComboBox(); 
            // Prepare Data Table Columns to Display Data in Grid
            LoadDB();
        }

        // Load Database to Grid View
        private void LoadDB()
        {
            // Prepare Rent Table Columns
            DataTableColumns();
            // Fetch All Rented Movie Details
            DataSet dataset = operation.GetRentedMovieDetails();
            // Check User Selected Rented Out Movies
            if(radioOut.Checked)
            {
                // Fetch All Rent Out Movies
                dataset = operation.GetOutRentedMovieDetails();
            }
            if(dataset.Tables.Count > 0)
            {
                // Iterate All Records From Dataset 
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    // STore them into Rent Data Table
                    RentTable.Rows.Add(row["rmid"], row["name"], row["address"], row["phone_no"], row["title"], row["rented_cost"], row["date_rented"], row["date_returned"]);
                }
            }
            // Set Data Table to Data Grid
            dgvRentGrid.DataSource = RentTable;
        }

        private void DataTableColumns()
        {
            // Clear Previous Details
            RentTable.Clear();
            try
            {
                // Prepare Table Columns
                RentTable.Columns.Add("RMID");
                RentTable.Columns.Add("Customer Name");
                RentTable.Columns.Add("Address");                
                RentTable.Columns.Add("Phone No");
                RentTable.Columns.Add("Movie Title");
                RentTable.Columns.Add("Rented Cost");
                RentTable.Columns.Add("Rented Date");
                RentTable.Columns.Add("Return Date");
            }
            catch (Exception ex)
            {

            }
        }

        private void BindComboBox()
        {
            // Fetch Customer Details to Data Table
            DataTable tableCustomer = operation.ViewCustomerDetails();
            // Prepare Combo Box to Display Customer
            comboCustomer.ValueMember = "cust_id";
            comboCustomer.DisplayMember = "name";
            comboCustomer.DataSource = tableCustomer;
            // Fetch Movie Details to Data Table
            DataTable tableMovie = operation.ViewMovieDetails();
            // Prepare Combo Box to Display Movie
            comboMovie.ValueMember = "movie_id";
            comboMovie.DisplayMember = "title";
            comboMovie.DataSource = tableMovie;
        }

        private void MovieRentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // CLose the Database Connection
            operation.CloseConnection();
            // Display The Home Form
            this.HomeForm.Show();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            string message = "";
            bool all_valid = true;
            DateTime rented_date = dtpDate.Value;
            // Check User SElected a Customer
            if (comboCustomer.SelectedIndex == 0)
            {
                all_valid = false;
                message += "Please Choose Any Customer\n\n";
            }
            // Check User SElected a Movie
            if ( comboMovie.SelectedIndex == 0)
            {
                all_valid = false;
                message += "Please Choose Any Movie\n\n";
            }
            // If USer Selected All Value
            if (all_valid)
            {
                // Convert Movie Rental Cost to Float
                float rental_cost = float.Parse(labelRent.Text.Trim());
                // Convert Movie ID into int
                int movie_id = int.Parse(comboMovie.SelectedValue.ToString());
                // Convert Customer ID into into
                int cust_id = int.Parse(comboCustomer.SelectedValue.ToString());
                // Issue Movie To Customer
                if (operation.IssueMovieToCustomer(movie_id,cust_id,rental_cost,rented_date))
                {
                    message = "Movie is Issued and its Details are Saved in Database";
                    LoadDB();
                }
                else
                {
                    message = "There are some failure in Issue the Movie to Customer";
                }
            }
            // Display the Message
            MessageBox.Show(message);
        }

        private void comboMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check User Selected a VAlid Movie
            if(comboMovie.SelectedIndex!=0)
            {
                // Display the Movie Rent to Label
                labelRent.Text = operation.GetMovieRent(int.Parse(comboMovie.SelectedValue.ToString())).ToString();
            }
            else
            {
                // Display the None to Label
                labelRent.Text = "None";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadDB();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            // Check User Selected a Valid Record
            if (rmid!=0)
            {
                // Return Selected Movie with Current Date Time
                if(operation.ReturnMovie(rmid,DateTime.Now))
                {
                    // Display Message
                    MessageBox.Show("Movie is Successfuly returned");
                    LoadDB();
                }
                else
                {
                    // Display Message
                    MessageBox.Show(" There are some issued in Returning Movie");
                }
                rmid = 0;
            }
            else
            {
                // Display Message
                MessageBox.Show("Please Click on a Valid Row of Grid");
            }
        }

        private void dgvRentGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Select the Rent ID for selected record
                rmid = int.Parse(dgvRentGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                btnReturn.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check User Selected a Valid Record
            if (rmid != 0)
            {
                // Assure User WIll to Delete Record
                DialogResult result = MessageBox.Show("Are You Sure To Remove Record From Database?", "Movie Store", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string message = "";
                    // Delete the Record and prepare the message
                    if (operation.DeleteRentedDetails(rmid))
                    {
                        message = "Movie Rented Details are Removed from Database";
                        rmid = 0;
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in removing Movie Rented Details in Database";
                    }
                    // Display Message
                    MessageBox.Show(message);
                }
            }            
        }
    }
}
