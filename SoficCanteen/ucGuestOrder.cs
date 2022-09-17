using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoficCanteen
{
    public partial class ucGuestOrder : UserControl
    {
        public ucGuestOrder()
        {
            InitializeComponent();
        }
        public ucGuestOrder(string id, string name, long price, int quantity, Image imageFile)
        {
            InitializeComponent();
            this.Name = id;
            lblName.Text = name;
            lblPrice.Text = price.ToString("N0");
            lblSub.Text = "Số lượng: " + quantity.ToString() + " | " + "Thành tiền: " + (price * quantity).ToString("N0");
            picAvatar.Image = imageFile;
        }
    }
}
