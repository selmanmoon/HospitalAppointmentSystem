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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        OleDbConnection connect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb");
        OleDbCommand cmd = new OleDbCommand();
        DataTable dt = new DataTable();
        public static int id, id2;
        public static string s;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbSelect.SelectedItem.ToString() == "Patient")
            {
                try
                {
                    connect.Open();
                    string user = "SELECT * FROM Register WHERE P_ID=@P_ID AND P_Password=@P_Password";
                    OleDbParameter prms1 = new OleDbParameter("@P_ID", txtID.Text);
                    OleDbParameter prms2 = new OleDbParameter("@P_Password", txtPass.Text);
                    OleDbCommand cmd = new OleDbCommand(user, connect);
                    cmd.Parameters.Add(prms1);
                    cmd.Parameters.Add(prms2);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    
                    if (dt.Rows.Count > 0)
                    {
                        string i = Convert.ToString(txtID.Text);
                        id = Convert.ToInt32(txtID.Text);
                        OleDbCommand cmd2 = new OleDbCommand("SELECT P_NAME FROM Register WHERE P_ID="+i+"", connect);
                        OleDbDataReader rdr;
                        rdr = cmd2.ExecuteReader();
                        while (rdr.Read())
                        {
                             s = Convert.ToString(rdr["P_NAME"]);
                        }
                      
                         ReservationForm lgfrm = new ReservationForm();
                         lgfrm.Show();
                        
                       

                    }
                    else if (String.IsNullOrWhiteSpace(txtID.Text) || String.IsNullOrWhiteSpace(txtPass.Text))
                    {
                        MessageBox.Show("Boş alan bırakamazsınız");
                    }
                    else
                    {
                        txtID.Text = "";
                        txtPass.Text = "";
                        MessageBox.Show("Kullanıcı adı veya şifrenizi kontrol ediniz");
                    }
                    

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                connect.Close();

            }   
            else if (cmbSelect.SelectedIndex == -1)
            {
                MessageBox.Show("Please Choose Doctor/Patient");
            }
            else if (cmbSelect.SelectedItem.ToString() == "Doctor")
            {
                try
                {
                    connect.Open();
                    string doctor = "SELECT * FROM doktor WHERE doktorid=@doktorid and DoctorPass=@DoctorPass";
                    OleDbParameter prmd1 = new OleDbParameter("@doktorid", txtID.Text);
                    OleDbParameter prmd2 = new OleDbParameter("@DoctorPass", txtPass.Text);
                    OleDbCommand cmd = new OleDbCommand(doctor, connect);
                    cmd.Parameters.Add(prmd1);
                    cmd.Parameters.Add(prmd2);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        id2 = Convert.ToInt32(txtID.Text);
                        DoctorForm dcfrm = new DoctorForm();
                        dcfrm.Show();
                    }
                    else if (String.IsNullOrWhiteSpace(txtID.Text) || String.IsNullOrWhiteSpace(txtPass.Text))
                    {
                        MessageBox.Show("Boş alan bırakamazsınız");
                    }
                    else
                    {
                        txtID.Text = "";
                        txtPass.Text = "";
                        MessageBox.Show("Kullanıcı adı veya şifrenizi kontrol ediniz");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connect.Close();
            }
            
        }
        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            Register rgstr = new Register();
            rgstr.Show();
        }

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelect.SelectedItem.ToString() == "Doctor")
            {
                btnRegister.Enabled = false;
            }
            else
            {
                btnRegister.Enabled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToShortDateString();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("We will add the 'doctors rate system' totally in the next Update!!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Programı Kapatmak İstiyor Musunuz?","Dikkat",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }
    }
}
