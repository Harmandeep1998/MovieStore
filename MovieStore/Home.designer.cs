namespace MovieStore
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnMovie = new System.Windows.Forms.Button();
            this.btnMovieRent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.Teal;
            this.btnCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnCustomer.Location = new System.Drawing.Point(120, 26);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(555, 112);
            this.btnCustomer.TabIndex = 0;
            this.btnCustomer.Text = "Customer Operations";
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnMovie
            // 
            this.btnMovie.BackColor = System.Drawing.Color.Teal;
            this.btnMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovie.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnMovie.Location = new System.Drawing.Point(120, 169);
            this.btnMovie.Name = "btnMovie";
            this.btnMovie.Size = new System.Drawing.Size(555, 112);
            this.btnMovie.TabIndex = 1;
            this.btnMovie.Text = "Movie Operations";
            this.btnMovie.UseVisualStyleBackColor = false;
            this.btnMovie.Click += new System.EventHandler(this.btnMovie_Click);
            // 
            // btnMovieRent
            // 
            this.btnMovieRent.BackColor = System.Drawing.Color.Teal;
            this.btnMovieRent.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovieRent.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnMovieRent.Location = new System.Drawing.Point(120, 326);
            this.btnMovieRent.Name = "btnMovieRent";
            this.btnMovieRent.Size = new System.Drawing.Size(555, 112);
            this.btnMovieRent.TabIndex = 2;
            this.btnMovieRent.Text = "Movie Rent Operations";
            this.btnMovieRent.UseVisualStyleBackColor = false;
            this.btnMovieRent.Click += new System.EventHandler(this.btnMovieRent_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMovieRent);
            this.Controls.Add(this.btnMovie);
            this.Controls.Add(this.btnCustomer);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movie Store Operations";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnMovie;
        private System.Windows.Forms.Button btnMovieRent;
    }
}

