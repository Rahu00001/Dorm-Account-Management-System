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
namespace hostel
{
    public partial class CourseUpdate : Form
    {
        private readonly Course _parent;
        public string name = "", s = "", dur = "", tot = "", gq = "", mq = "", late = "", max = "";
        public CourseUpdate(Course parent)
        {
            InitializeComponent();
            _parent = parent;
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
            txtdur.Text = dur;
            txttot.Text = tot;
            txtgq.Text = gq;
            txtmq.Text = mq;
            txtlate.Text = late;
            txtmax.Text = max;
        }
        public void Clear()
        {
            txtname.Text = txtshort.Text = txtdur.Text = txttot.Text = txtgq.Text = txtmq.Text = txtlate.Text = txtmax.Text = string.Empty;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtname.Text.Trim().Length <= 0)
            {
                MessageBox.Show("lateral is incorrect");
                Clear();
            }
            else if (txtshort.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Short Name is incorrect");
                Clear();
            }
            else if (txtdur.Text.Trim().Length <= 0)
            {
                MessageBox.Show("duration is incorrect");
                Clear();
            }
            else if (txttot.Text.Trim().Length <= 0)
            {
                MessageBox.Show("total seats is incorrect");
                Clear();
            }
            else if (txtgq.Text.Trim().Length < 0)
            {
                MessageBox.Show("govt seats is incorrect");
                Clear();
            }
            else if (txtmq.Text.Trim().Length < 0)
            {
                MessageBox.Show("mgnt seats is incorrect");
                Clear();
            }
            else if (txtlate.Text.Trim().Length < 0)
            {
                MessageBox.Show("course name is incorrect");
                Clear();
            }
            else if (txtmax.Text.Trim().Length <= 0)
            {
                MessageBox.Show("max duration is incorrect");
                Clear();
            }
            else
            {
                SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
                con.Open();
                if (btnSave.Text == "Save")
                {
                    string sql = "insert into course values(@cname,@sname,@dur,@tot,@gq,@mq,@late,@maxi)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@cname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sname", txtshort.Text);
                    cmd.Parameters.AddWithValue("@dur", txtdur.Text);
                    cmd.Parameters.AddWithValue("@tot", txttot.Text);
                    cmd.Parameters.AddWithValue("@gq", txtgq.Text);
                    cmd.Parameters.AddWithValue("@mq", txtmq.Text);
                    cmd.Parameters.AddWithValue("@late", txtlate.Text);
                    cmd.Parameters.AddWithValue("@maxi", txtmax.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    this.Hide();
                }
                if (btnSave.Text == "Update")
                {
                    string sql = "Update course set cname=@cname,dur=@dur,tot=@tot,gq=@gq,mq=@mq,late=@late,maxi=@maxi where sname=@sname";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@cname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sname", txtshort.Text);
                    cmd.Parameters.AddWithValue("@dur", txtdur.Text);
                    cmd.Parameters.AddWithValue("@tot", txttot.Text);
                    cmd.Parameters.AddWithValue("@gq", txtgq.Text);
                    cmd.Parameters.AddWithValue("@mq", txtmq.Text);
                    cmd.Parameters.AddWithValue("@late", txtlate.Text);
                    cmd.Parameters.AddWithValue("@maxi", txtmax.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    this.Hide();
                }
            }
        }
    }
}
