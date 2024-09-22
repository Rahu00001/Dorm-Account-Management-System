using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hostel
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CustomizeDesign();
        }
        private void CustomizeDesign()
        {
            submenu2.Visible = false;
            gensubmenupanel.Visible = false;
            //..
        }
        private void hideSubMenu()
        {
            if (submenu2.Visible == true)
                submenu2.Visible = false;
            if (gensubmenupanel.Visible == true)
                gensubmenupanel.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btngen_Click(object sender, EventArgs e)
        {
            showSubMenu(gensubmenupanel);
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new general());
            hideSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Form1());
            hideSubMenu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new AcademicYear());
            hideSubMenu();
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                TopMost = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new Course());
            hideSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new Report());
            hideSubMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new studentAdmission());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new bill());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            showSubMenu(submenu2);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            openChildForm(new PayBill());
            hideSubMenu();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
    }
}
