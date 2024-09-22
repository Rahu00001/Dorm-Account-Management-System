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
    public partial class print : Form
    {
        public print()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=hostel;Data Source=LENOVO\\SQLEXPRESS");
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataAdapter dr;
        SqlDataAdapter dr1;
        SqlDataReader read;




        private String studid;

        public String Studid
        {
            get { return studid; }
            set { studid = value; }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void print_Load(object sender, EventArgs e)
        {
            con.Open();
            DataTable dt = new DataTable();
            string sql = "select * from paid where studid= '"+Studid+"'";
            cmd = new SqlCommand(sql,con);
            dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);

            DataTable dt1 = new DataTable();
            string sql1 = "select * from fees where studid= '" + Studid + "'";
            cmd1 = new SqlCommand(sql1, con);
            dr1 = new SqlDataAdapter(cmd1);
            dr1.Fill(dt1);
            con.Close();

            CrystalReport1 cr = new CrystalReport1();
            cr.Database.Tables["paid"].SetDataSource(dt);
            cr.Database.Tables["fees"].SetDataSource(dt1);

            this.crystalReportViewer1.ReportSource = cr;

        }
    }
}
