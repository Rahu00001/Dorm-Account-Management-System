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
    public partial class bill : Form
    {
        billUpdate form;
        public bill()
        {
            InitializeComponent();
            Fill();
            form = new billUpdate(this);
        }
        public void Fill()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select id,describe,billdate,amt,gender,bid from bill";
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable tb1 = new DataTable();
                adp.Fill(tb1);
                dgv.DataSource = tb1;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.change();
            form.Clear();
            form.ShowDialog();
            Fill();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                form.id = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                form.des = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.date = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.amt = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.gen = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.batch = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.getdata();
                form.ShowDialog();
                form.Clear();
                Fill();
                return;
            }
        }
    }
}
