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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        static string constr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS";
        static SqlConnection con = new SqlConnection(constr);
        private void label2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string q = "select psw from Admin where u_name='" + usertxt.Text + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    if (sdr["psw"].Equals(pwdtxt.Text))
                    {
                        MessageBox.Show("Login Successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        new Form2().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Password is Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usertxt.Clear();
                        pwdtxt.Clear();
                        usertxt.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Username is Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usertxt.Clear();
                    pwdtxt.Clear();
                    usertxt.Focus();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usertxt.Clear();
                pwdtxt.Clear();
                usertxt.Focus();
            }
        }
    }
}
