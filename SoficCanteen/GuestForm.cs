using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace SoficCanteen
{
    public partial class GuestForm : System.Windows.Forms.Form
    {
        private long total = 0;
        private string userId;
        private string locationId;
        private string apiServer;
        private string conn;
        private string apiKey;
        private string[] arrConfig;
        private List<Food> lstData = new List<Food>();
        public GuestForm()
        {
            InitializeComponent();
            //Read config parameter
            //arrConfig = SystemUti.ReadConfig();
            //if (arrConfig != null)
            /*{
                this.apiKey = arrConfig[0];
                this.apiServer = arrConfig[1];
                this.locationId = arrConfig[2];
                this.conn = arrConfig[3];
              }*/
        }
        public GuestForm(List<Food> lst)
        {
            InitializeComponent();
            this.lstData = lst;
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    ucGuestOrder ucGuest = new ucGuestOrder(item.id, item.name, item.price, item.quantity, item.imageFile);
                    flowLayoutPanel1.Controls.Add(ucGuest);
                }
            }
        }
        public void ReciveOrder(bool init, List<Food> lst)
        {
            total = 0;
            if (init)
            {
                flowLayoutPanel1.Controls.Clear();
                lstData.Clear();
            }
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    var variant = lstData.Find(x => x.id == item.id);
                    if (variant == null)
                    {
                        lstData.Add(item);
                        ucGuestOrder ucGuest = new ucGuestOrder(item.id, item.name, item.price, item.quantity, item.imageFile);
                        flowLayoutPanel1.Controls.Add(ucGuest);
                    }
                    else
                    {
                        foreach (Control ctr in flowLayoutPanel1.Controls)
                        {
                            if (ctr is UserControl && string.Compare(ctr.Name, variant.id) == 0)
                            {
                                foreach (Control shadow in ctr.Controls)
                                {
                                    if (shadow is Guna.UI2.WinForms.Guna2ShadowPanel)
                                    {
                                        foreach (Control lbl in shadow.Controls)
                                        {
                                            if (string.Compare(lbl.Name, "lblSub") == 0 && lbl is Label)
                                            {
                                                lbl.Text = "Số lượng: " + variant.quantity.ToString() + " | " + "Thành tiền: " + (variant.quantity * variant.price).ToString("N0");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Process total label
                    total += item.price * item.quantity;
                }
                lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
            }

        }
        public void RecivePerson(int identity, string employeeCode, string personName)
        {
            lblPerson.Text = personName;
            switch (identity)
            {
                case 1:
                    picAvatar.Image = SoficCanteen.Properties.Resources._0e152f58c9b001ee58a1;
                    break;
                case 2:
                    Thread t = new Thread(() =>
                    {
                        GetImage(employeeCode);
                    });
                    t.IsBackground = true;
                    t.Start();
                    break;
                case 3:
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;
                    break;
            }
        }
        public void ReciveStatus(int reg)
        {
            switch (reg)
            {
                case 1:
                    guna2GradientButton.FillColor = Color.GreenYellow;
                    guna2GradientButton.FillColor2 = Color.Lime;
                    guna2GradientButton.Text = "Chưa nhận";
                    guna2GradientButton.ForeColor = Color.Black;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;
                    break;
                case 2:
                    guna2GradientButton.FillColor = Color.Orange;
                    guna2GradientButton.FillColor2 = Color.Red;
                    guna2GradientButton.Text = "Không đăng ký";
                    guna2GradientButton.ForeColor = Color.Black;
                    break;
                case 3:
                    guna2GradientButton.FillColor = Color.PapayaWhip;
                    guna2GradientButton.FillColor2 = Color.Orange;
                    guna2GradientButton.Text = "Không tồn tại";
                    lblPerson.Text = "Không tồn tại";
                    guna2GradientButton.ForeColor = Color.Black;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;
                    break;
                case 4:
                    guna2GradientButton.FillColor = Color.LightBlue;
                    guna2GradientButton.FillColor2 = Color.DodgerBlue;
                    guna2GradientButton.Text = "Không đúng giờ";
                    guna2GradientButton.ForeColor = Color.Black;
                    break;
                case 5:
                    guna2GradientButton.FillColor = Color.Orange;
                    guna2GradientButton.FillColor2 = Color.Red;
                    guna2GradientButton.Text = "Sai đầu đọc thẻ";
                    lblPerson.Text = "";
                    guna2GradientButton.ForeColor = Color.Black;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;
                    break;
                case 6:
                    guna2GradientButton.FillColor = Color.Violet;
                    guna2GradientButton.FillColor2 = Color.DarkViolet;
                    guna2GradientButton.Text = "Đã nhận";
                    guna2GradientButton.ForeColor = Color.Black;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;
                    break;
                default:
                    guna2GradientButton.FillColor = Color.PapayaWhip;
                    guna2GradientButton.FillColor2 = Color.Orange;
                    guna2GradientButton.Text = "Welcome";
                    lblPerson.Text = "Welcome";
                    guna2GradientButton.ForeColor = Color.Black;
                    picAvatar.Image = SoficCanteen.Properties.Resources.no_image_icon_23483;
                    break;
            }
        }
        private void GetImage(string maNhanVien)
        {
            byte[] bytes = DBUti.GetImage(conn, maNhanVien, "SP_GetImage");
            if (bytes != null)
            {
                picAvatar.Image = Image.FromStream(new MemoryStream(bytes));
            }
            else
            {
                picAvatar.Image = SoficCanteen.Properties.Resources.no_image_icon_23483;
            }
        }
        public void Reset()
        {
            flowLayoutPanel1.Controls.Clear();
            total = 0;
            lblTotal.Text = "Tổng cộng: ";

            picAvatar.Image = SoficCanteen.Properties.Resources.a10601;
            lblPerson.Text = "Welcome";
            guna2GradientButton.Text = "Welcome";
            guna2GradientButton.ForeColor = Color.Black;
            guna2GradientButton.FillColor = Color.GreenYellow;
            guna2GradientButton.FillColor2 = Color.Lime;
        }
    }
}
