using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DBUtility
{
    public class DBOperation
    {
        // Connection String of 
        private string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=moviestore;Integrated Security=True;";

        // Hold reference of Open Connection
        private SqlConnection conn;

        public DBOperation()
        {
            // Initialize the Sql Connection with Connection STring
            conn = new SqlConnection(connectionString);
            // Open the Sql Connection
            conn.Open();
        }

        // Return Current Connection State
        public ConnectionState GetConnectionState()
        {
            return conn.State;
        }

        // Check Connection is Open or Not
        public bool CheckConnectionState()
        {
            if(conn != null && GetConnectionState() == ConnectionState.Open )
            {
                return true;
            }
            return false;
        }

        // Close The Connection
        public void CloseConnection()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        // Insertion Movie Details
        public bool InsertMovieDetails(string title,float rating,int release_year,string genre,float rental_cost)
        {
            try
            {
                // Prepare Query
                string query = "insert into movies(title,rating,release_year,genre,rental_cost) values(@title,@rating,@release_year,@genre,@rental_cost)";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@title", title));
                cmd.Parameters.Add(new SqlParameter("@rating", rating));
                cmd.Parameters.Add(new SqlParameter("@release_year", release_year));
                cmd.Parameters.Add(new SqlParameter("@genre", genre));
                cmd.Parameters.Add(new SqlParameter("@rental_cost", rental_cost));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        // Update Movie Details
        public bool UpdateMovieDetails(int movie_id,string title, float rating, int release_year, string genre, float rental_cost)
        {
            try
            {
                // Prepare Query
                string query = "update movies set title=@title,rating=@rating,release_year=@release_year,genre=@genre,rental_cost=@rental_cost where movie_id = @movie_id ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@title", title));
                cmd.Parameters.Add(new SqlParameter("@rating", rating));
                cmd.Parameters.Add(new SqlParameter("@release_year", release_year));
                cmd.Parameters.Add(new SqlParameter("@genre", genre));
                cmd.Parameters.Add(new SqlParameter("@rental_cost", rental_cost));
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Delete Movie Details
        public bool DeleteMovieDetails(int movie_id)
        {
            try
            {
                // Prepare Query
                string query = "delete from movies where movie_id = @movie_id ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Fetch All Movie Details in DataSet Form
        public DataSet GetMovieDetails()
        {
            // Create an Empty Dataset
            DataSet ds = new DataSet();
            try
            {
                // Prepare Query
                string query = "select * from movies";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Fetch Data and Fill Data SEt
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        // Get Movie Rent According to Given Movie ID
        public float GetMovieRent(int movie_id)
        {
            // Initialize with 0
            float rental_cost = 0;
            try
            {
                // Prepare Query
                string query = "select rental_cost from movies where movie_id = @movie_id";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                // Execute the Query
                SqlDataReader reader = cmd.ExecuteReader();
                // Check Data is Available or not
                if(reader.Read())
                {
                    // Store Rental Cost value 
                    rental_cost = float.Parse(reader[0].ToString());
                }
                // CLose The Reader resource in memory
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            return rental_cost;
        }

        // Insert Customer Details
        public bool InsertCustomerDetails(string first_name, string last_name, string address, string phone_no)
        {
            try
            {
                string query = "insert into customer(first_name,last_name,address,phone_no) values(@first_name,@last_name,@address,@phone_no)";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                cmd.Parameters.Add(new SqlParameter("@address", address));
                cmd.Parameters.Add(new SqlParameter("@phone_no", phone_no));
                // Execute the Query
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Update Customer Details
        public bool UpdateCustomerDetails(int cust_id, string first_name, string last_name,  string address, string phone_no)
        {
            try
            {
                string query = "update customer set first_name=@first_name,last_name=@last_name,address=@address,phone_no=@phone_no  where cust_id = @cust_id ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                cmd.Parameters.Add(new SqlParameter("@address", address));
                cmd.Parameters.Add(new SqlParameter("@phone_no", phone_no));
                cmd.Parameters.Add(new SqlParameter("@cust_id", cust_id));
                // Execute the Query
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Delete Customer Details
        public bool DeleteCustomerDetails(int cust_id)
        {
            try
            {
                // Prepare Query
                string query = "delete from customer where cust_id = @cust_id ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@cust_id", cust_id));
                // Execute the Query
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Fetch Customer Details
        public DataSet GetCustomerDetails()
        {
            // Create an empty data set
            DataSet ds = new DataSet();
            try
            {
                // Prepare Query
                string query = "select * from customer";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // File Data into Data Set
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        // Fetch Customer details
        public DataTable ViewCustomerDetails()
        {
            //Create an empty data table
            DataTable dt = new DataTable();
            try
            {
                // Prepare Query
                string query = "select * from viewCustomer ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // File data to data table
                adapter.Fill(dt);
                // Create a new row
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "Customer" };
                // Add New Row to Data Table
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        // Fetch Details of All Movies
        public DataTable ViewMovieDetails()
        {
            // Create an empty data table
            DataTable dt = new DataTable();
            try
            {
                // Prepare Query
                string query = "select * from viewMovie ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // File Data to Data Table
                adapter.Fill(dt);
                // Create a New Row
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "Movie" };
                // Add New Row to Data Table
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        // Issue Movie to Customer
        public bool IssueMovieToCustomer(int movie_id,int cust_id,float rented_cost,DateTime date_rented)
        {
            try
            {
                // Prepare Query
                string query = "insert into rented_movies(movie_id_fk,cust_id_fk,rented_cost,date_rented,date_returned) values(@movie_id,@cust_id,@rented_cost,@date_rented,null)";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                cmd.Parameters.Add(new SqlParameter("@cust_id", cust_id));
                cmd.Parameters.Add(new SqlParameter("@rented_cost", rented_cost));
                cmd.Parameters.Add(new SqlParameter("@date_rented", date_rented));
                // Execute The Query
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Fetch All Movie which are Rented
        public DataSet GetRentedMovieDetails()
        {
            // Create an empty data set
            DataSet ds = new DataSet();
            try
            {
                // Prepare Procedure Name
                string query = "prcShowRentedMovies";
                // Create an object of Command for Connection
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                // Set the Command Type for Procedure Execution
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Fill the Dataset
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        // Fetch All Movie which are Rented Out
        public DataSet GetOutRentedMovieDetails()
        {
            // Create an empty data set
            DataSet ds = new DataSet();
            try
            {
                // Prepare Procedure Name
                string query = "prcShowOutRentedMovies";
                // Create an object of Command for Connection
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                // Set the Command Type for Procedure Execution
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Fill the Dataset
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        // Return Movie to Store
        public bool ReturnMovie(int rmid, DateTime date_returned)
        {
            try
            {
                // Prepare Query
                string query = "update rented_movies set date_returned = @date_returned where rmid = @rmid";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@date_returned", date_returned));
                cmd.Parameters.Add(new SqlParameter("@rmid", rmid));
                // Execute the Query
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Delete Rent Movie Details
        public bool DeleteRentedDetails(int rmid)
        {
            try
            {
                // Prepare Query
                string query = "delete from rented_movies where rmid = @rmid ";
                // Create Command For Connection
                SqlCommand cmd = conn.CreateCommand();
                // Set Command Query
                cmd.CommandText = query;
                // Set Parameter for Query
                cmd.Parameters.Add(new SqlParameter("@rmid", rmid));
                // Execute the Query
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
