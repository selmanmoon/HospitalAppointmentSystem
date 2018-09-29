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
    public partial class DoctorForm : Form
    {
        public DoctorForm()
        {
            InitializeComponent();
        }
        OleDbConnection connect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb");
        private void btnDocOK_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void DoctorForm_Load(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                OleDbCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT ReservationForm.PatientName, ReservationForm.HospitalName, ReservationForm.ChosenHour, ReservationForm.RDate FROM ReservationForm WHERE doktorid=" + MainForm.id2;
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string PatientName = rdr[0].ToString();
                    string hospitalname = rdr[1].ToString();
                    string chosenhour = rdr[2].ToString();
                    string Rdate = rdr[3].ToString();
                    string[] arry = { PatientName, hospitalname, chosenhour, Rdate };
                    ListViewItem item = new ListViewItem(arry);
                    lvDoctors.Items.Add(item);
                }
                rdr.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            string a = Convert.ToString(MainForm.id2);
            string c;
            OleDbCommand cmd2 = new OleDbCommand("SELECT DoktorAdi FROM doktor WHERE doktorid=" + a + "", connect);
            OleDbDataReader rdr2;
            rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                c = Convert.ToString(rdr2["DoktorAdi"]);
                label1.Text = "Your Appointment List " +c +":";
            }
            
            connect.Close();

            label4.Text = DateTime.Now.ToShortDateString();
        }


    }
}
