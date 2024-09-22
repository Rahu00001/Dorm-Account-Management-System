using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace hostel
{
    public partial class billUpdate : Form
    {
        private readonly bill _parent;
        public string des = "", date = "", amt = "",  batch= "",id="",gen="";
        public billUpdate(bill parent)
        {
            InitializeComponent();
            _parent = parent;
            fillcombox1();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            con.Open();
            if (btnSave.Text == "Save")
            {
                string sql = "insert into bill values(@aname,@sname,@syear,@gen,@eyear)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@aname", txtdes.Text);
                cmd.Parameters.AddWithValue("@sname", txtdate.Text);
                cmd.Parameters.AddWithValue("@syear", txtamt.Text);
                cmd.Parameters.AddWithValue("@gen", txtgen.Text);
                cmd.Parameters.AddWithValue("@eyear", txtbatch.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                this.Hide();
            }
            if (btnSave.Text == "Update")
            {
                string sql = "Update bill set describe=@des,billdate=@date,amt=@amt,gender=@gen,bid=@batch where id=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@des", txtdes.Text);
                cmd.Parameters.AddWithValue("@date", txtdate.Text);
                cmd.Parameters.AddWithValue("@amt", txtamt.Text);
                cmd.Parameters.AddWithValue("@gen", txtgen.Text);
                cmd.Parameters.AddWithValue("@batch", txtbatch.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                this.Hide();
            }
        }
        public void change()
        {
            btnSave.Text = "Save";
            txtdate.Enabled = true;
            txtdate.Value = DateTime.Now;
        }
        public void getdata()
        {
            btnSave.Text = "Update";
            txtdes.Text = des;
            txtdate.Text = date;
            txtdate.Enabled = false;
            txtamt.Text = amt;
            txtbatch.Text = batch;
            txtgen.Text = gen;
        }
        public void Clear()
        {
            txtdes.Text = txtamt.Text =  string.Empty;
        }
    }
}
