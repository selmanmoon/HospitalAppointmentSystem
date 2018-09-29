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
using System.Collections;

namespace HospitalAppointmentSystem
{
    public partial class ReservationForm : Form
    {
        public ReservationForm()
        {
            InitializeComponent();
        }
        OleDbConnection connect = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source =data.mdb");
        DataTable table = new DataTable();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        ArrayList hospID = new ArrayList();
        ArrayList docID = new ArrayList();
        ArrayList gunID = new ArrayList();
        ArrayList hourID = new ArrayList();
        ArrayList aktifID = new ArrayList();
        ArrayList clinID = new ArrayList();
        public int pol_id;

        void addCity()
        {
            DataTable dt = new DataTable();
            adtr = new OleDbDataAdapter("select * from iller", connect);
            adtr.Fill(dt);
            cmbCity.ValueMember = "id";
            cmbCity.DisplayMember = "ilAdi";
            cmbCity.DataSource = dt;
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbCity.Items.Count == -1 || cmbHospital.Items.Count == -1 || cmbDepartment.Items.Count == -1 || cmbClinic.Items.Count == -1 || cmbDoctors.Items.Count == -1 || textBox1.Text == "")
            {
                MessageBox.Show("Please Fill all boxes");
            }
            else
            {
                DateTime d_day = Convert.ToDateTime(dateTimePicker1.Text);
                string day = string.Empty;
                day = d_day.ToString("dddd");
                if (day != "Saturday" && day != "Sunday")
                {
                    listAll();
                    connect.Open();
                    OleDbCommand data = new OleDbCommand("SELECT Clinicid FROM ReservationForm WHERE Patientid =" + MainForm.id + "", connect);
                    OleDbDataReader rdr2 = null;
                    rdr2 = data.ExecuteReader();
                    listBox2.Items.Clear();
                    while (rdr2.Read())
                    {
                        listBox2.Items.Add(rdr2.GetInt32(0));
                    }
                    rdr2.Close();
                    connect.Close();
                    readhour();
                    int count = listBox2.Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (listBox2.Items[i].ToString() == pol_id.ToString())
                        {
                            value = listBox2.Items[i].ToString();
                        }
                    }
                    if (value == pol_id.ToString())
                    {
                        MessageBox.Show("Its already have");
                        listBox2.Items.Clear();
                    }
                    else
                    {
                        OleDbCommand cmd = connect.CreateCommand();
                        cmd.CommandText = "INSERT INTO ReservationForm(PatientName,TownName,HospitalName,DoctorName,doktorid,RDate,ClinicName,Clinicid,ChosenHour,Patientid) VALUES ('" + MainForm.s + "','" + cmbHospital.SelectedItem + "','" + cmbDepartment.SelectedItem + "','" + cmbDoctors.SelectedItem + "','" + dr_id + "','" + dateTimePicker1.Text + "','" + cmbClinic.SelectedItem + "','" + pol_id + "','" + textBox1.Text + "','" + MainForm.id + "')";
                        connect.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        MessageBox.Show("Reservation Successful!");
                        connect.Close();
                        readhour();
                        listAll();
                        this.Close();
                    }
            
                }
            }


 
            
        }
        
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbCommand cmd = connect.CreateCommand();
            cmd.CommandText = "select ilceAdi,id From ilceler where ilid=" + cmbCity.SelectedValue;
            connect.Open();
            OleDbDataReader read;
            read = cmd.ExecuteReader();
            cmbHospital.Items.Clear();
            cmbDepartment.Items.Clear();
            cmbDoctors.Items.Clear();
            cmbClinic.Items.Clear();
            cmbAvailableHours.Items.Clear();
            hospID.Clear();
            while (read.Read())
            {
                hospID.Add(read[1].ToString());
                cmbHospital.Items.Add(read[0].ToString());
            }
            connect.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();                                
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            addCity();
            label10.Text = DateTime.Now.ToShortDateString();
            string a = Convert.ToString(MainForm.id);
            string c,d;
            OleDbCommand cmd = new OleDbCommand("SELECT P_NAME,P_SURNAME FROM Register WHERE P_ID=" + a + "", connect);
            connect.Open();
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                c = Convert.ToString(rdr["P_NAME"]);
                d = Convert.ToString(rdr["P_SURNAME"]);
                label6.Text = "Make Reservation " + c + " " +d +"";   
            }
            connect.Close();
        }
        private void cmbHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = connect.CreateCommand();
                cmd.CommandText = @"SELECT hastane.HastaneAdi,hastane.hastaneid FROM ilceler INNER JOIN hastane ON ilceler.id = hastane.ilceid WHERE (((ilceler.id)=[?]))";
                cmd.Parameters.Add("ce", OleDbType.Integer).Value = hospID[cmbHospital.SelectedIndex];
                connect.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                cmbDepartment.Items.Clear();
                cmbDoctors.Items.Clear();
                cmbClinic.Items.Clear();
                cmbAvailableHours.Items.Clear();
                docID.Clear();
                while (rdr.Read())
                {
                    clinID.Add(rdr[1].ToString());
                    cmbDepartment.Items.Add(rdr[0].ToString());
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error! We couldnt add Hospitals");
            }
            connect.Close();
        }
        public int hastane_id;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect.Open();
            OleDbCommand cmd2 = new OleDbCommand("SELECT hastaneid FROM hastane WHERE HastaneAdi='" + cmbDepartment.Text + "'", connect);
            OleDbDataReader rdr3 = cmd2.ExecuteReader();
            while (rdr3.Read())
            {
                hastane_id = rdr3.GetInt32(0);
            }
            connect.Close();
            rdr3.Close();
            cmbClinic.Items.Clear();
            cmbDoctors.Items.Clear();
            connect.Open();
            OleDbCommand cmd3 = new OleDbCommand("SELECT clinic_Name FROM clinic WHERE hastaneid=" + hastane_id + "", connect);
            OleDbDataReader rdr5 = cmd3.ExecuteReader();
            cmbDoctors.Items.Clear();
            while (rdr5.Read())
            {
                cmbClinic.Items.Add(rdr5.GetString(0));
            }
            connect.Close();
            cmd3.Dispose();
            rdr5.Close();
        }
        private void cmbDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbCommand cmd2 = new OleDbCommand("SELECT doktorid FROM doktor WHERE DoktorAdi='" + cmbDoctors.Text + "'", connect);
            connect.Open();
            OleDbDataReader rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                dr_id = rdr2.GetInt32(0);
            }
            connect.Close();
            listAll();
        }
 
        int visibleCount = 0;
        private void btnHistory_Click(object sender, EventArgs e)
        {
            
            OleDbCommand cmd = new OleDbCommand("SELECT ReservationForm.DoctorName, ReservationForm.HospitalName, ReservationForm.ChosenHour, ReservationForm.RDate FROM ReservationForm WHERE(((ReservationForm.Patientid) =[?]))" , connect);
            cmd.Parameters.Add("Patientid", OleDbType.Integer).Value = MainForm.id;
            connect.Open();
            OleDbDataReader rdr = cmd.ExecuteReader();
            visibleCount++;
            if (visibleCount%2!=0)
            {
                lvPatient.Visible = true;
            }
            else
            {
                lvPatient.Visible = false;
            }
            lvPatient.Items.Clear();
            while (rdr.Read())
            {
                string DoctorName = rdr[0].ToString();
                string HospitalName = rdr[1].ToString();
                string ChosenHour = rdr[2].ToString();
                string Date = rdr[3].ToString();
                string[] arry = { DoctorName, HospitalName, ChosenHour, Date };
                ListViewItem item = new ListViewItem(arry);
                lvPatient.Items.Add(item);
            }
            connect.Close();
        }

        private void cmbClinic_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect.Open();
            OleDbCommand cmd2 = new OleDbCommand("SELECT clinic_id FROM clinic WHERE clinic_Name='" + cmbClinic.Text + "'", connect);
            OleDbDataReader rdr3 = cmd2.ExecuteReader();
            while (rdr3.Read())
            {
                pol_id = rdr3.GetInt32(0);
            }
            connect.Close();
            rdr3.Close();
            connect.Open();
            OleDbCommand cmd3 = new OleDbCommand("SELECT DoktorAdi FROM doktor WHERE clinicid=" + pol_id + "", connect);
            OleDbDataReader rdr5 = cmd3.ExecuteReader();
            cmbDoctors.Items.Clear();
            while (rdr5.Read())
            {
                cmbDoctors.Items.Add(rdr5.GetString(0));
            }
            connect.Close();
            cmd3.Dispose();
            rdr5.Close();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = button3.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = button12.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = button13.Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = button14.Text;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = button18.Text;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = button19.Text;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text = button20.Text;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text = button21.Text;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text = button22.Text;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox1.Text = button23.Text;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox1.Text = button24.Text;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox1.Text = button25.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = button11.Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = button15.Text;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = button16.Text;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = button17.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = button9.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = button8.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = button4.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = button6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = button7.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = button5.Text;
        }
        public int dr_id;
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = button10.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = button2.Text;
        }

        public void readhour()
        {
          
                connect.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT ChosenHour FROM ReservationForm WHERE doktorid=" + dr_id + " and RDate='" + dateTimePicker1.Text + "'", connect);
                OleDbDataReader rdr = cmd.ExecuteReader();
                listBox1.Items.Clear();
                while (rdr.Read())
                {
                    listBox1.Items.Add(rdr.GetString(0).ToString());
                }
                rdr.Close();
                connect.Close();
                
            
          
        }
        public void listAll()
        {
            button2.Enabled = true; button3.Enabled = true; button4.Enabled = true; button5.Enabled = true; button6.Enabled = true; button7.Enabled = true; button8.Enabled = true; button9.Enabled = true; button10.Enabled = true; button11.Enabled = true; button12.Enabled = true; button13.Enabled = true; button14.Enabled = true; button15.Enabled = true;
            button16.Enabled = true; button17.Enabled = true; button18.Enabled = true; button19.Enabled = true; button20.Enabled = true; button21.Enabled = true; button22.Enabled = true; button23.Enabled = true; button24.Enabled = true; button25.Enabled = true;

            button2.BackColor = Color.DarkGreen; button3.BackColor = Color.DarkGreen; button4.BackColor = Color.DarkGreen; button5.BackColor = Color.DarkGreen;
            button6.BackColor = Color.DarkGreen; button7.BackColor = Color.DarkGreen; button8.BackColor = Color.DarkGreen; button9.BackColor = Color.DarkGreen;
            button10.BackColor = Color.DarkGreen; button11.BackColor = Color.DarkGreen; button12.BackColor = Color.DarkGreen; button13.BackColor = Color.DarkGreen;
            button14.BackColor = Color.DarkGreen; button15.BackColor = Color.DarkGreen; button16.BackColor = Color.DarkGreen; button17.BackColor = Color.DarkGreen;
            button18.BackColor = Color.DarkGreen; button19.BackColor = Color.DarkGreen; button20.BackColor = Color.DarkGreen; button21.BackColor = Color.DarkGreen;
            button22.BackColor = Color.DarkGreen; button23.BackColor = Color.DarkGreen; button24.BackColor = Color.DarkGreen; button25.BackColor = Color.DarkGreen;

            listBox1.Items.Clear();
            readhour();
            int count = listBox1.Items.Count;
            string value = null;
            for (int i = 0; i < count; i++)
            {
                if (listBox1.Items[i].ToString()==button2.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button2.BackColor = Color.Brown;
                    button2.Enabled = false;
                }
                else if (listBox1.Items[i].ToString()==button3.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button3.BackColor = Color.Brown;
                    button3.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button4.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button4.BackColor = Color.Brown;
                    button4.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button3.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button3.BackColor = Color.Brown;
                    button3.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button4.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button4.BackColor = Color.Brown;
                    button4.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button5.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button5.BackColor = Color.Brown;
                    button5.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button6.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button6.BackColor = Color.Brown;
                    button6.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button7.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button7.BackColor = Color.Brown;
                    button7.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button8.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button8.BackColor = Color.Brown;
                    button8.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button9.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button9.BackColor = Color.Brown;
                    button9.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button10.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button10.BackColor = Color.Brown;
                    button10.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button11.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button11.BackColor = Color.Brown;
                    button11.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button12.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button12.BackColor = Color.Brown;
                    button12.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button13.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button13.BackColor = Color.Brown;
                    button13.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button14.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button14.BackColor = Color.Brown;
                    button14.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button15.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button15.BackColor = Color.Brown;
                    button15.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button16.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button16.BackColor = Color.Brown;
                    button16.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button17.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button17.BackColor = Color.Brown;
                    button17.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button18.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button18.BackColor = Color.Brown;
                    button18.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button19.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button19.BackColor = Color.Brown;
                    button19.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button20.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button20.BackColor = Color.Brown;
                    button20.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button21.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button21.BackColor = Color.Brown;
                    button21.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button22.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button22.BackColor = Color.Brown;
                    button22.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button23.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button23.BackColor = Color.Brown;
                    button23.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button24.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button24.BackColor = Color.Brown;
                    button24.Enabled = false;
                }
                else if (listBox1.Items[i].ToString() == button25.Text)
                {
                    value = listBox1.Items[i].ToString();
                    button25.BackColor = Color.Brown;
                    button25.Enabled = false;
                }
                readhour();
            }

        }
        public string value;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime d_date = Convert.ToDateTime(dateTimePicker1.Text);
            string day = string.Empty;
            day = d_date.ToString("dddd");
            if (day!= "Cumartesi" && day!= "Pazar")
            {
                listAll();
            }
            else
            {
                MessageBox.Show("Cumartesi ve pazar alamazsın");
            }
        }
    }
}
