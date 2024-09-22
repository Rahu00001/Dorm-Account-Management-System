using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using hostel.Properties;
using System.IO;

namespace hostel
{
    public partial class viewStudent : Form
    {
        private readonly studentAdmission _parent;
        public string id = "", name = "", pid = "", gen = "", add = "", course = "", phone = "", batch = "", quota = "", hostel = "", room = "";
        public byte[] photo;
        public viewStudent(studentAdmission parent)
        {
            InitializeComponent();
            fillcombox();
            fillcombox2();
            fillcombox1();
            _parent = parent;
        }
        public void change()
        {
            btnSave.Text = "Save";
            txtid.ReadOnly = false;
            txtid.Enabled = true;
            pictureBox1.Image = Resources.person;
        }
        public void fillcombox()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select sname from course";

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader = cmd.ExecuteReader();
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        txtcourse.Items.Add(myreader["sname"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        public void fillcombox1()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select sname from academic";
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader = cmd.ExecuteReader();
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        txtbatch.Items.Add(myreader["sname"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        public void getdata()
        {
            btnSave.Text = "Update";
            txtid.Text = id;
            txtname.Text = name;
            txtid.ReadOnly = true;
            txtid.Enabled = false;
            txtpid.Text = pid;
            txtgen.Text = gen;
            txtad.Text = add;
            txtph.Text = phone;
            txtcourse.Text = course;
            txtbatch.Text = batch;
            txtquota.Text = quota;
            pictureBox1.Image = GetImage(photo);
            txthos.Text = hostel;
            txtroom.Text = room;
        }

        private Image GetImage(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
        public void fillcombox2()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select hname from hostel";


            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader = cmd.ExecuteReader();
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        txthos.Items.Add(myreader["hname"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        public void Clear()
        {
            txtgen.SelectedIndex = 0;
            txtbatch.SelectedIndex = 0;
            txtcourse.SelectedIndex = 0;
            txtquota.SelectedIndex = 0;
            txthos.SelectedIndex = 0;
            /*txtgen.Items.Clear();
            txtbatch.Items.Clear();
            txtcourse.Items.Clear();
            txtquota.Items.Clear();*/
            txtid.Text = txtname.Text = txtroom.Text = txtpid.Text = txtgen.Text = txtad.Text = txtph.Text = txtcourse.Text = txtbatch.Text = txtquota.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            con.Open();
            if (btnSave.Text == "Save")
            {
                string sql = "insert into studAd values(@studid,@st_name,@phoneid,@gender,@addres,@phone,@course,@batch,@quota,@photo,@hostel,@room)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@studid", txtid.Text);
                cmd.Parameters.AddWithValue("@st_name", txtname.Text);
                cmd.Parameters.AddWithValue("@phoneid", txtpid.Text);
                cmd.Parameters.AddWithValue("@gender", txtgen.Text);
                cmd.Parameters.AddWithValue("@addres", txtad.Text);
                cmd.Parameters.AddWithValue("@phone", txtph.Text);
                cmd.Parameters.AddWithValue("@course", txtcourse.Text);
                cmd.Parameters.AddWithValue("@batch", txtbatch.Text);
                cmd.Parameters.AddWithValue("@quota", txtquota.Text);
                cmd.Parameters.AddWithValue("@photo", getPhoto());
                cmd.Parameters.AddWithValue("@hostel", txthos.Text);
                cmd.Parameters.AddWithValue("@room", txtroom.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                this.Hide();
            }
            if (btnSave.Text == "Update")
            {
                string sql = "Update studAd set st_name=@st_name,phoneid=@phoneid,gender=@gender,addres=@addres,phone=@phone,course=@course,batch=@batch,quota=@quota,photo=@photo where studid=@studid";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@st_name", txtname.Text);
                cmd.Parameters.AddWithValue("@phoneid", txtpid.Text);
                cmd.Parameters.AddWithValue("@gender", txtgen.Text);
                cmd.Parameters.AddWithValue("@addres", txtad.Text);
                cmd.Parameters.AddWithValue("@phone", txtph.Text);
                cmd.Parameters.AddWithValue("@course", txtcourse.Text);
                cmd.Parameters.AddWithValue("@batch", txtbatch.Text);
                cmd.Parameters.AddWithValue("@quota", txtquota.Text);
                cmd.Parameters.AddWithValue("@photo", getPhoto());
                cmd.Parameters.AddWithValue("@studid", txtid.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                this.Hide();
            }
        }
        private byte[] getPhoto()
        {
            MemoryStream stream = new MemoryStream();
            pictureBox1.Image.Save(stream, pictureBox1.Image.RawFormat);
            return stream.GetBuffer();
        }
    }
}
