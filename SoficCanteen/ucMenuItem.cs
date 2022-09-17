using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoficCanteen
{
    public partial class ucMenuItem : UserControl
    {
        public delegate void ButtonClick(string guid);
        public event ButtonClick ButtonClickEvent;
        public ucMenuItem()
        {
            InitializeComponent();
        }
        public ucMenuItem(string Id, string name, long price, Image imageFile)
        {
            InitializeComponent();
            this.Tag = Id;
            guna2Button.Name = Id;
            lblName.Text = name;
            lblPrice.Text = price.ToString("N0");
            if (imageFile != null)
            {
                guna2PictureBox.Image = imageFile;
            }
            else
            {
                guna2PictureBox.Image = SoficCanteen.Properties.Resources._650_425_com_ga_nha_trang_bepcuoi_981;
            }
        }

        

        private void guna2Button_Click(object sender, EventArgs e)
        {
            ButtonClickEvent(guna2Button.Name);
        }
    }
}
