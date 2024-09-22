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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtsid_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select st_name,gender,batch,hostel from studAd where studid=@id;";
            con.Open();
            txtsname.Text = txtgender.Text = txtbatch.Text = txthos.Text = String.Empty;
            if (txtsid.Text != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", txtsid.Text);
                SqlDataReader d = cmd.ExecuteReader();
                if (d.HasRows)
                {
                    while (d.Read())
                    {
                        txtsname.Text = d.GetValue(0).ToString();
                        txtgender.Text = d.GetValue(1).ToString();
                        txtbatch.Text = d.GetValue(2).ToString();
                        txthos.Text = d.GetValue(3).ToString();
                    }
                }

                con.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            print p = new print();
            p.Studid = txtsid.Text;
            p.Show();

        }
    }
}
