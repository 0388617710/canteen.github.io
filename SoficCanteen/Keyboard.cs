using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoficCanteen
{
    public partial class Keyboard : DevExpress.XtraEditors.XtraForm
    {
        Control ActiveControl;
        private bool checkFocus = false;
        public delegate void ButtonClick(string sKetQua);
        public event ButtonClick ButtonClickEvent;
        public Keyboard()
        {
            InitializeComponent();
        }

        private void txtResult_Enter(object sender, EventArgs e)
        {
            ActiveControl = (Control)sender;
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ActiveControl.Focus();
            SendKeys.Send(btn.Text);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_WOC11_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
        }

        private void button_WOC12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResult.Text) && txtResult.Text.All(Char.IsDigit))
            {
                ButtonClickEvent(txtResult.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Không đúng định dạng.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Keyboard_Load(object sender, EventArgs e)
            
        {
            timer.Start();
            txtResult.Focus();  
        }

        private void timer_Tick(object sender, EventArgs e)
        {

            if (!checkFocus)
            {
                txtResult.Focus();
                timer.Stop();
                checkFocus = true;
            }
        }
    }
    
}