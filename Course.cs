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
    public partial class Course : Form
    {
        CourseUpdate form;
        public Course()
        {
            InitializeComponent();
            Fill();
            form = new CourseUpdate(this);
        }
        public void Fill()
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
            string sql = "select cname,sname,dur,tot,gq,mq,late,maxi from course";
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

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                form.name = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                form.s = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.dur = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.tot = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.gq = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.mq = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.late = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
                form.max = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
                form.getdata();
                form.ShowDialog();
                form.Clear();
                Fill();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
