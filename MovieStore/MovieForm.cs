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
    public partial class MovieForm : Form
    {
        // Reference of Home Form
        public Form HomeForm { get; set; }
        // Reference of DBOperation
        public DBOperation operation;
        // Data Table for Movie Details
        DataTable MovieTable = new DataTable();
        // Movie ID for Edit and Delete Operatiorn
        int movieID;

        public MovieForm()
        {
            InitializeComponent();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            // Create an Object of DBOPeration
            operation = new DBOperation();
            // Load Movie Details to Grid
            LoadDB();
        }

        private void LoadDB()
        {
            // Prepare Data Table Columns
            DataTableColumns();
            // Fetch Movie Details to Data Set
            DataSet dataset = operation.GetMovieDetails();
            // Check Data Set Fetch some table
            if(dataset.Tables.Count > 0 )
            {
                // Iterate to All Rows 
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    // Store Row with data in Movie Data Table
                    MovieTable.Rows.Add(row["movie_id"], row["title"], row["genre"], row["rating"], row["rental_cost"], row["release_year"]);
                }
            }
            // Set Movie Table to Data Grid
            dgvMovieGrid.DataSource = MovieTable;
        }

        private void DataTableColumns()
        {
            // Clear Previous details of Movie Table
            MovieTable.Clear();
            try
            {
                // Add Movie Column to Data Table
                MovieTable.Columns.Add("Movie ID");
                MovieTable.Columns.Add("Movie Title");
                MovieTable.Columns.Add("Genre");
                MovieTable.Columns.Add("Rating");
                MovieTable.Columns.Add("Rental Cost");
                MovieTable.Columns.Add("Release Year");                
            }
            catch(Exception ex)
            {

            }
        }

        private void MovieForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Close the Database Connection
            operation.CloseConnection();
            // Show Home Form
            this.HomeForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string message = "";
            bool all_valid = true;
            // Fetch Form Details
            string title = textMovieTitle.Text.Trim();
            string year = textReleaseYear.Text.Trim();
            string genre = textGenre.Text.Trim();
            float rating = (float)numericRating.Value;
            // Validate the Form Data
            if(Validation.IsEmpty(title))
            {
                all_valid = false;
                message += "Please Enter Some Value in Title\n\n";
            }

            if (Validation.IsEmpty(year))
            {
                all_valid = false;
                message += "Please Enter Some Value in Release Year\n\n";
            }
            else if( year.Length != 4 )
            {
                all_valid = false;
                message += "Please Enter Four Digit in Release Year\n\n";
            }
            else if(!Validation.IsNumber(year))
            {
                all_valid = false;
                message += "Please Enter Number in Release Year\n\n";
            }

            if (Validation.IsEmpty(genre))
            {
                all_valid = false;
                message += "Please Enter Some Value in Genre\n\n";
            }
            // Check All Validation are Successful
            if(all_valid)
            {
                int release_year = int.Parse(year);
                float rental_cost = 5;
                // Check Release Year Older
                if(release_year < DateTime.Now.Year - 5 )
                {
                    rental_cost = 2;
                }
                // Store Movie Details
                if(operation.InsertMovieDetails(title,rating,release_year,genre,rental_cost))
                {
                    message = "New Movie Details are Saved in Database";
                    LoadDB();
                }
                else
                {
                    message = "There are some failure in saveing Movie Details in Database";
                }
            }
            // Display Message
            MessageBox.Show(message);
        }

        private void dgvMovieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Collect All Information According to Selected Grid Row
                movieID = int.Parse(dgvMovieGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                textGenre.Text = dgvMovieGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                textMovieTitle.Text = dgvMovieGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                textReleaseYear.Text = dgvMovieGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                numericRating.Value = int.Parse(dgvMovieGrid.Rows[e.RowIndex].Cells[3].Value.ToString());
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check user selecte a Grid Row
            if(movieID!=0)
            {
                string message = "";
                bool all_valid = true;
                // Fetch All Form Details
                string title = textMovieTitle.Text.Trim();
                string year = textReleaseYear.Text.Trim();
                string genre = textGenre.Text.Trim();
                float rating = (float)numericRating.Value;
                // Validate the Form Data
                if (Validation.IsEmpty(title))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Title\n\n";
                }

                if (Validation.IsEmpty(year))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Release Year\n\n";
                }
                else if (year.Length != 4)
                {
                    all_valid = false;
                    message += "Please Enter Four Digit in Release Year\n\n";
                }
                else if (!Validation.IsNumber(year))
                {
                    all_valid = false;
                    message += "Please Enter Number in Release Year\n\n";
                }

                if (Validation.IsEmpty(genre))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Genre\n\n";
                }
                // Check All Validation are Successful
                if (all_valid)
                {
                    int release_year = int.Parse(year);
                    float rental_cost = 5;
                    // Check Release Year Older
                    if (release_year < DateTime.Now.Year - 5)
                    {
                        rental_cost = 2;
                    }
                    // Update the Movie Details
                    if (operation.UpdateMovieDetails(movieID, title, rating, release_year, genre, rental_cost))
                    {
                        message = "Movie Details are Updated in Database";
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in saving Movie Details in Database";
                    }
                }
                // Display Message
                MessageBox.Show(message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (movieID != 0)
            {
                // Assure User WIll to Delete Record
                DialogResult result = MessageBox.Show("Are You Sure To Remove Record From Database?", "Movie Store", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result==DialogResult.Yes)
                {
                    string message = "";
                    // Delete the Record and prepare the message
                    if (operation.DeleteMovieDetails(movieID))
                    {
                        message = "Movie Details are Removed from Database";
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in removing Movie Details in Database";
                    }
                    // Display Message
                    MessageBox.Show(message);
                }
            }
        }
    }
}
