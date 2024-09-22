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
    public partial class studentAdmission : Form
    {
        viewStudent form;
        public studentAdmission()
        {
            InitializeComponent();
            Fill();
            form = new viewStudent(this);
        }
        public void Fill()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select studid,st_name,phoneid,gender,addres,phone,course,batch,quota,photo,hostel,room from studAd";
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

        private void button2_Click(object sender, EventArgs e)
        {
            form.change();
            form.Clear();
            form.ShowDialog();
            Fill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].HeaderText == "View")
            {
                form.id = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                form.name = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.pid = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.gen = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.add = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.phone = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.course = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
                form.batch = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
                form.quota = dgv.Rows[e.RowIndex].Cells[9].Value.ToString();
                form.hostel = dgv.Rows[e.RowIndex].Cells[11].Value.ToString();
                form.room = dgv.Rows[e.RowIndex].Cells[12].Value.ToString();
                form.photo = (byte[])dgv.Rows[e.RowIndex].Cells[10].Value;
                form.getdata();
                form.ShowDialog();
                form.Clear();
                Fill();
                return;
            }
        }
    }
}
