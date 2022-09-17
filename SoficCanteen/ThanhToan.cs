
using System;
using System.Data;
using System.Windows.Forms;

namespace SoficCanteen
{
    public partial class ThanhToan : System.Windows.Forms.Form
    {
        public delegate void ButtonClick(int loaithanhtoan);
        public event ButtonClick ButtonClickEvent;
        public ThanhToan()
        {
            InitializeComponent();

        }

       

        

        

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ButtonClickEvent(1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ButtonClickEvent(2);
        }
    }
}

