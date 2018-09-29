using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace HospitalAppointmentSystem
{
    public partial class Register : Form
    { 
        public Register()
        {
            InitializeComponent();
        }
        OleDbConnection connect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb");
        DataTable table = new DataTable();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        OleDbCommand cmd = new OleDbCommand();
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtRegisterPass.Text != txtRegisterRepass.Text)
            {
                MessageBox.Show("Passwords didnt match");
            }
            else if (txtRegisterID.Text == "" || txtRegisterName.Text == "" || txtRegisterPass.Text == "" || txtRegisterRepass.Text == "" || txtRegisterSurname.Text == "" )
            {
                MessageBox.Show("Please fill them all!");
            }
            else
            {
                try
                {
                    connect.Open();
                    cmd.Connection = connect;
                    cmd.CommandText = "INSERT INTO Register(P_ID,P_NAME,P_SURNAME,P_PASSWORD) VALUES ('" + txtRegisterID.Text + "','" + txtRegisterName.Text + "','" + txtRegisterSurname.Text + "','" + txtRegisterPass.Text + "')";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connect.Close();
                    MessageBox.Show("Registration Succeed");
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("There is already a registration with this ID number");
                }
                
            }
            

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {

            label5.Text = DateTime.Now.ToShortDateString();
        }
    }
}
