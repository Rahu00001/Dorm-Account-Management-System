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

namespace hostel
{
    public partial class AcademicUpate : Form
    {
        private readonly AcademicYear _parent;
        public string name = "", s = "", start = "", end = "";

        private void txtshort_TextChanged(object sender, EventArgs e)
        {

        }

        public AcademicUpate(AcademicYear parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtend.Text.Trim().Length < 10)
            {
                MessageBox.Show("EndYear is incorrect");
                Clear();
            }
            else if (txtname.Text.Trim().Length < 9)
            {
                MessageBox.Show("Academic Year is incorrect");
                Clear();
            }
            else if (txtstart.Text.Trim().Length < 10)
            {
                MessageBox.Show("Start Year is incorrect");
                Clear();
            }
            else if (txtshort.Text.Trim().Length < 4)
            {
                MessageBox.Show("Short Name is incorrect");
                Clear();
            }
            else
            {
                SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
                con.Open();
                if (btnSave.Text == "Save")
                {
                    string sql = "insert into academic values(@aname,@sname,@syear,@eyear)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@aname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sname", txtshort.Text);
                    cmd.Parameters.AddWithValue("@syear", txtstart.Text);
                    cmd.Parameters.AddWithValue("@eyear", txtend.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    this.Hide();
                }
                if (btnSave.Text == "Update")
                {
                    string sql = "Update academic set aname=@aname,syear=@syear,eyear=@eyear where sname=@sname";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@aname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sname", txtshort.Text);
                    cmd.Parameters.AddWithValue("@syear", txtstart.Text);
                    cmd.Parameters.AddWithValue("@eyear", txtend.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    this.Hide();
                }
            }
        }

        public void change()
        {
            btnSave.Text = "Save";
            txtshort.ReadOnly = false;
            txtshort.Enabled = true;
        }
        public void getdata()
        {
            btnSave.Text = "Update";
            txtname.Text = name;
            txtshort.Text = s;
            txtshort.ReadOnly = true;
            txtshort.Enabled = false;
            txtstart.Text = start;
            txtend.Text = end;
        }
        public void Clear()
        {
            txtend.Text = txtname.Text = txtstart.Text = txtshort.Text = string.Empty;
        }
    }
}
