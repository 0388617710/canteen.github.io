using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoficCanteen
{
    public partial class ucOderItem : UserControl
    {
        public delegate void ButtonClick(string id, int quantity);
        public event ButtonClick ButtonClickEvent;
        public delegate void DeleteClick(string id);
        public event DeleteClick DeleteClickEvent;
        private bool isKhach;
        public ucOderItem()
        {
            InitializeComponent();
        }
        public ucOderItem(bool check, string id, string name, long price, int quantity, bool isdelete, Image imageFile)
        {
            InitializeComponent();
            this.isKhach = check;
            this.Tag = id;
            lblName.Text = name;
            lblPrice.Text = price.ToString("N0");
            lblQuantity.Text = quantity.ToString();
            lbtongtien.Text = (price * quantity).ToString("N0");
            btndelete.Visible = isdelete;
            /*if (imageFile !=null)
            {
                picFood.Image = imageFile;
            }
            else
            {
                picFood.Image = SoficCanteen.Properties.Resources._650_425_com_ga_nha_trang_bepcuoi_981;
            }*/

        }

       

       

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isKhach)
            {
                int quantity = Convert.ToInt16(lblQuantity.Text);
                quantity++;
                //lblQuantity.Text = quantity.ToString();
                ButtonClickEvent(this.Tag.ToString(), quantity);
            }
        }

       
        private void btndelete_Click(object sender, EventArgs e)
        {
            DeleteClickEvent(this.Tag.ToString());
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt16(lblQuantity.Text);
            if (quantity > 1)
            {
                quantity = quantity - 1;
                //lblQuantity.Text = quantity.ToString();
                ButtonClickEvent(this.Tag.ToString(), quantity);
            }
        }
    }
}
