namespace HospitalAppointmentSystem
{
    partial class DoctorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnDocOK = new System.Windows.Forms.Button();
            this.lvDoctors = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(86, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "APPOINTMENTS LIST";
            // 
            // btnDocOK
            // 
            this.btnDocOK.BackColor = System.Drawing.Color.Transparent;
            this.btnDocOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDocOK.BackgroundImage")));
            this.btnDocOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDocOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDocOK.FlatAppearance.BorderSize = 2;
            this.btnDocOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDocOK.Location = new System.Drawing.Point(164, 243);
            this.btnDocOK.Name = "btnDocOK";
            this.btnDocOK.Size = new System.Drawing.Size(50, 50);
            this.btnDocOK.TabIndex = 2;
            this.btnDocOK.UseVisualStyleBackColor = false;
            this.btnDocOK.Click += new System.EventHandler(this.btnDocOK_Click);
            // 
            // lvDoctors
            // 
            this.lvDoctors.BackColor = System.Drawing.Color.CadetBlue;
            this.lvDoctors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvDoctors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.Date});
            this.lvDoctors.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
            this.lvDoctors.FullRowSelect = true;
            this.lvDoctors.GridLines = true;
            this.lvDoctors.LabelWrap = false;
            this.lvDoctors.Location = new System.Drawing.Point(29, 58);
            this.lvDoctors.MultiSelect = false;
            this.lvDoctors.Name = "lvDoctors";
            this.lvDoctors.Scrollable = false;
            this.lvDoctors.Size = new System.Drawing.Size(369, 179);
            this.lvDoctors.TabIndex = 1;
            this.lvDoctors.UseCompatibleStateImageBehavior = false;
            this.lvDoctors.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PatientName";
            this.columnHeader1.Width = 96;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "HospitalName";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Hour";
            this.columnHeader3.Width = 80;
            // 
            // Date
            // 
            this.Date.Text = "Date";
            this.Date.Width = 112;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(326, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "tarih";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button1.Location = new System.Drawing.Point(220, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // DoctorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(425, 305);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lvDoctors);
            this.Controls.Add(this.btnDocOK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DoctorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppointmentForm";
            this.Load += new System.EventHandler(this.DoctorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDocOK;
        private System.Windows.Forms.ListView lvDoctors;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.Button button1;
    }
}