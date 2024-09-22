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
using System.Xml.Linq;

namespace hostel
{

    public partial class PayBill : Form
    {
        Dictionary<string, double> arr = new Dictionary<string, double>();
        Dictionary<string, int> arr1 = new Dictionary<string, int>();
        public PayBill()
        {
            InitializeComponent();
        }

        public void Removeitem(string id)
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql2 = "select describe from bill where id=@id";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.Parameters.AddWithValue("@id", id);
                SqlDataReader r1 = cmd2.ExecuteReader();
                if (r1.HasRows)
                {
                    while (r1.Read())
                    {
                        bill.Items.Remove(r1["describe"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        public void addedremove()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql1 = "select billid from paid where studid=@id";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.Parameters.AddWithValue("@id", txtsid.Text);
                SqlDataReader r = cmd1.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        string id = r["billid"].ToString();
                        Removeitem(id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        public void fillcombox()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select describe,id,amt from bill where bid=@batch and gender=@gender or gender='Hostel'";

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@batch", txtbatch.Text);
                cmd.Parameters.AddWithValue("@gender", txtgender.Text);
                SqlDataReader myreader = cmd.ExecuteReader();
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        int id = int.Parse(myreader["id"].ToString());
                        string des = myreader["describe"].ToString();
                        double a = double.Parse(myreader["amt"].ToString());
                        arr.Add(des, a);
                        arr1.Add(des, id);
                        bill.Items.Add(myreader["describe"]);
                    }
                }
                addedremove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        private void txtsid_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            arr1.Clear();
            arr.Clear();
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select st_name,gender,batch,hostel from studAd where studid=@id;";
            con.Open();
            bill.Items.Clear();
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
                    fillcombox();
                }

                con.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (String s in bill.CheckedItems)
                if (arr.ContainsKey(s))
                {
                    SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
                    con.Open();
                    string sql = "insert into paid values(@studid,@billid,@stat,@paydate,@bill)";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@studid", txtsid.Text);
                    cmd.Parameters.AddWithValue("@billid", arr1[s]);
                    cmd.Parameters.AddWithValue("@stat", "Paid");
                    cmd.Parameters.AddWithValue("@paydate", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@bill", s);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            fees();
        }
        public void fees()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            try {
                con.Open();
                string sql = "insert into fees values(@studid,@amt,@paydate)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@studid", txtsid.Text);
                cmd.Parameters.AddWithValue("@amt", label9.Text);
                cmd.Parameters.AddWithValue("@paydate", dateTimePicker1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Paid Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (String s in bill.CheckedItems)
                if (arr.ContainsKey(s))
                    listBox1.Items.Add(arr[s]);
            tot();
        }
        public void tot()
        {
            int b = 0;
            for (int i = 0; i < bill.CheckedItems.Count; i++)
            {
                b += int.Parse(listBox1.Items[i].ToString());
            }
            label9.Text = b.ToString();
        }
        private void bill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
