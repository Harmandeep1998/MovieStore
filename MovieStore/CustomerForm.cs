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
    public partial class CustomerForm : Form
    {
        // Reference of Home Form
        public Form HomeForm { get; set; }
        // Reference of DBOperation
        public DBOperation operation;
        // Customer Data Table for Grid
        DataTable CustomerTable = new DataTable();
        // Customer ID for Edit and Delete Operation
        int cust_id;

        public CustomerForm()
        {
            InitializeComponent();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            // create an object of DBOperation
            operation = new DBOperation();
            // Load Database Details
            LoadDB();
        }

        private void LoadDB()
        {
            // Prepare Customer Data Table Structure
            DataTableColumns();
            // Fetch Customer Details
            DataSet dataset = operation.GetCustomerDetails();
            // Check dataset contains any tabel
            if(dataset.Tables.Count > 0)
            {
                // Iterate all Table Rows
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    // Save Row with data in Customer Data Table
                    CustomerTable.Rows.Add(row["cust_id"], row["first_name"], row["last_name"], row["address"], row["phone_no"]);
                }
            }
            // set data table to grid
            dgvCustomerGrid.DataSource = CustomerTable;
        }

        private void DataTableColumns()
        {
            // Clear previous details of Customer Data Table
            CustomerTable.Clear();
            try
            {
                // Add Customer Collumn to Customer Data Table
                CustomerTable.Columns.Add("Customer ID");
                CustomerTable.Columns.Add("First Name");
                CustomerTable.Columns.Add("Last Name");
                CustomerTable.Columns.Add("Address");
                CustomerTable.Columns.Add("Phone No");
            }
            catch (Exception ex)
            {

            }
        }

        private void CustomerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Close the Database Connection
            operation.CloseConnection();
            // Show the Home Form
            this.HomeForm.Show();
        }

        private void dgvCustomerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Fetch all Customer Details according to Selected Grid Row
                cust_id = int.Parse(dgvCustomerGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                textFirstName.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                textLastName.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                textAddress.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                textPhoneNo.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string message = "";
            bool all_valid = true;
            // Collect Form Data
            string first_name = textFirstName.Text.Trim();
            string last_name = textLastName.Text.Trim();
            string address = textAddress.Text.Trim();
            string phone_no = textPhoneNo.Text.Trim();
            // Validate the Form Data
            if (Validation.IsEmpty(first_name))
            {
                all_valid = false;
                message += "Please Enter Some Value in First Name\n\n";
            }

            if (Validation.IsEmpty(last_name))
            {
                all_valid = false;
                message += "Please Enter Some Value in Last Name\n\n";
            }

            if (Validation.IsEmpty(address))
            {
                all_valid = false;
                message += "Please Enter Some Value in Address\n\n";
            }

            if (Validation.IsEmpty(phone_no))
            {
                all_valid = false;
                message += "Please Enter Some Value in Phone No\n\n";
            }
            else if(!Validation.IsOnlyDigitInString(phone_no))
            {
                all_valid = false;
                message += "Phone No Only Contains Digit\n\n";

            }
            // Check all Validation are successful
            if (all_valid)
            {
                // Insert Customer Details
                if (operation.InsertCustomerDetails(first_name,last_name,address,phone_no))
                {
                    message = "New Customer Details are Saved in Database";
                    LoadDB();
                }
                else
                {
                    message = "There are some failure in Saving Customer Details in Database";
                }
            }
            // Display Message
            MessageBox.Show(message);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check User Selected a Valid Record
            if (cust_id != 0)
            {
                string message = "";
                bool all_valid = true;
                // Fetch Form Data
                string first_name = textFirstName.Text.Trim();
                string last_name = textLastName.Text.Trim();
                string address = textAddress.Text.Trim();
                string phone_no = textPhoneNo.Text.Trim();

                // Validate Form Data
                if (Validation.IsEmpty(first_name))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in First Name\n\n";
                }

                if (Validation.IsEmpty(last_name))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Last Name\n\n";
                }

                if (Validation.IsEmpty(address))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Address\n\n";
                }

                if (Validation.IsEmpty(phone_no))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Phone No\n\n";
                }
                else if (!Validation.IsOnlyDigitInString(phone_no))
                {
                    all_valid = false;
                    message += "Phone No Only Contains Digit\n\n";

                }
                // Check All Validation are Successful
                if (all_valid)
                {
                    // Update Customer Details
                    if (operation.UpdateCustomerDetails(cust_id,first_name, last_name, address, phone_no))
                    {
                        message = "Customer Details are Updated in Database";
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in Saving Customer Details in Database";
                    }
                }
                // Display Message
                MessageBox.Show(message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check User Selected a Valid Record
            if (cust_id != 0)
            {
                // Assure User WIll to Delete Record
                DialogResult result = MessageBox.Show("Are You Sure To Remove Record From Database?", "Movie System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string message = "";
                    // Delete the Record and prepare the message
                    if (operation.DeleteCustomerDetails(cust_id))
                    {
                        message = "Customer Details are Removed from Database";
                        cust_id = 0;
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in removing Customer Details in Database";
                    }
                    // Display Message
                    MessageBox.Show(message);
                }
            }
        }
    }
}
