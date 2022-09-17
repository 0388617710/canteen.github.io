using DevExpress.Utils;
using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;


namespace SoficCanteen
{
    public partial class Form : DevExpress.XtraEditors.XtraForm
    {
        private Bitmap eImage;
        private string userId;
        private string hoTen;
        string MaNhanVien;

        private string conn;
        private string DiaDiemAn_Id;
        private string TenDiaDiemAn;
        private string LoaiThucDon_Id;
        private string Ngay;
        private string KhungGioAn_Id;
        private string MaKhungGioAn;
        private string TenLoaiThucDon;
        private string temporaryChuoi;
        private string apiserver;
        private long total = 0;
        private string parentId = null;
        private bool inputMode;
        private bool isPhatsinh;
        private bool isHopLe;
        private bool isKhach;
        private bool isDanhan;
        private string NhanVien_Id;
        private string newId;
        private string apiUrl;
       
        private string apiKey;
        private bool ketQua;
        private bool timerStatus;
        private bool isXacNhan;
        private bool isThuCong;
        private int loai = 1;
        private DataTable dtMenu = new DataTable();
        List<Food> lstReg = new List<Food>();
        List<Food> lstOrder = new List<Food>();
        List<Food> lstMenu = new List<Food>();
        GuestForm secondMornitor = new GuestForm();
        ThanhToan thanhToan = new ThanhToan();
        Keyboard keyPad = new Keyboard();
       
        public DataTable DanhSachMonAn;

        private BackgroundWorker _workerConnect;
        public Form()
        {
            InitializeComponent();

        }
        
        public Form(string guid, string hoTen, string dbConnect, string diadiemAnId, string tendiadiemAn, string loaithucdonId, string ngay, string khunggioId, string makhunggioAn, string tenloaithucDon)
        {
            InitializeComponent();
            this.userId = guid;
            this.hoTen = hoTen;
            this.conn = dbConnect;
            this.DiaDiemAn_Id = diadiemAnId;
            this.TenDiaDiemAn = tendiadiemAn;
            this.LoaiThucDon_Id = loaithucdonId;
            this.Ngay = ngay;
            this.KhungGioAn_Id = khunggioId;
            this.MaKhungGioAn = makhunggioAn;
            this.TenLoaiThucDon = tenloaithucDon;



            //Initial background worker
            _workerConnect = new BackgroundWorker();
            _workerConnect.WorkerSupportsCancellation = true;
            _workerConnect.WorkerReportsProgress = true;
            _workerConnect.DoWork += _workerConnect_DoWork;
            _workerConnect.ProgressChanged += _workerConnect_ProgressChanged;
            _workerConnect.RunWorkerCompleted += _workerConnect_RunWorkerCompleted;
            _workerConnect.RunWorkerAsync();

        }
        // Checking connectivity function
        private void _workerConnect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void _workerConnect_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bool connect = (bool)e.UserState;
            if (connect)
                picKetnoi.Image = SoficCanteen.Properties.Resources.greenfilled_circle_50px;
            else
                picKetnoi.Image = SoficCanteen.Properties.Resources.red_filled_circle_50px;
        }

        private void _workerConnect_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_workerConnect != null)
            {
                object connectState = SystemUti.PingFunction("8.8.8.8", 1000, "");
                _workerConnect.ReportProgress(0, connectState);
                Thread.Sleep(1000);
            }
        }
        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //string Date=String.Format("{0:dd/MM/yyyy}", Ngay);
            DateTime date = DateTime.Parse(Ngay);
            //string Date = date.ToString("dd/MM/yyyy");
            lblTenDiaDiemAn.Text = TenDiaDiemAn;
            lblNgay.Text = date.ToString("dd/MM/yyyy");
            //var config = DBUti.GetConfig(conn, KhungGioAn_Id,LoaiThucDon_Id, "SP_GetConfig");
            lblKhungGioAn.Text = "("+MaKhungGioAn+")";
            lblSuatAn.Text = TenLoaiThucDon;
            clock.Start();

            ClearNhanVien();

            ClearData();
            loai = 1;
            total = 0;
            if (!string.IsNullOrEmpty(NhanVien_Id))
                XuLyDuLieu(temporaryChuoi);
            
            //Check second monitor available
            if (Screen.AllScreens.Length > 1)
            {
                secondMornitor.StartPosition = FormStartPosition.Manual;
                secondMornitor.Location = Screen.AllScreens[1].WorkingArea.Location;
                secondMornitor.WindowState = FormWindowState.Maximized;
                secondMornitor.Show();
            }

            
            LoadThucDon();
        }

        
        
        private void Func_ButtonColor(string btnName)
        {
            ClearNhanVien();
            ClearData();
            switch (btnName)
            {
                case "btnDoan":
                    btnDoan.FillColor = Color.FromArgb(240, 118, 19);
                    btnDoan.ForeColor = Color.White;
                    btnThucuong.FillColor = Color.White;
                    btnThucuong.ForeColor = Color.Black;
                    loai = 1;
                    total = 0;
                    if (!string.IsNullOrEmpty(NhanVien_Id))
                        XuLyDuLieu(temporaryChuoi);
                    break;
                case "btnThucuong":
                    btnThucuong.FillColor = Color.FromArgb(240, 118, 19);
                    btnThucuong.ForeColor = Color.White;
                    btnDoan.FillColor = Color.White;
                    btnDoan.ForeColor = Color.Black;
                    loai = 2;
                    total = 0;
                    if (!string.IsNullOrEmpty(NhanVien_Id))
                        GetThongTinNhanVien(temporaryChuoi);
                    break;
            };

            LoadThucDon();
        }
        private void LoadThucDon()
        {
            lstMenu.Clear();
            menuPanel.Controls.Clear();
            ClearData();
            dtMenu = DBUti.GetThucDon(conn, KhungGioAn_Id, LoaiThucDon_Id, Ngay, "SP_GetMenu");
            if (dtMenu.Rows.Count > 0)
            {
                int stt = 0;
                foreach (DataRow row in dtMenu.Rows)
                {
                    
                    ucMenuItem menu = new ucMenuItem(
                                                row["MonAn_Id"].ToString(),
                                                row["TenMonAn"].ToString(),
                                                Convert.ToInt64(row["Gia"]),
                                                SoficCanteen.Properties.Resources.no_image_icon_23483
                                                );
                    menu.ButtonClickEvent += MenuItem_ButtonClickEvent;
                    string tmp = row["LoaiThucDon_Id"].ToString();
                    //if (loai == 2 && (stt == 0 || (dtMenu.Rows[stt - 1]["LoaiThucDon_Id"].ToString() != row["LoaiThucDon_Id"].ToString())))
                    /*{
                        Label lb = new Label();
                        lb.Width = Screen.PrimaryScreen.Bounds.Width;
                        lb.Font = new Font("Cambria", 12, FontStyle.Bold);
                        lb.Text = row["TenLoaiThucDon"].ToString();
                        menuPanel.Controls.Add(lb);

                    }*/
                    menuPanel.Controls.Add(menu);
                    stt++;
                }
                lstMenu = ConvertTableToList(dtMenu);
            }
        }
        private void MenuItem_ButtonClickEvent(string Id)
        {
            if ((isHopLe & !isKhach) || loai == 2)
            {
                var result = lstOrder.Find(x => x.id == Id);
                if (result == null)
                {
                    var item = lstMenu.Find(x => x.id == Id);
                    if (item != null)
                    {
                        item.quantity = 1;
                        if (loai == 2)
                        {
                            total += item.price;
                        }
                        item.total = 0;
                        item.isdelete = true;
                        lstOrder.Add(item);
                        //Tính tổng số mặc định
                        //int total = item.quantity * item.price;
                        lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
                        ucOderItem order = new ucOderItem(isKhach, item.id, item.name, item.price, item.quantity, item.isdelete, item.imageFile);
                        order.ButtonClickEvent += Order_ButtonClickEvent;
                        order.DeleteClickEvent += Order_DeleteClickEvent;
                        order.Name = Id;
                        orderPanel.Controls.Add(order);

                        //Pass data to second monitor
                        secondMornitor.ReciveOrder(false, lstOrder);
                    }
                }
            }
        }
        private void Order_ButtonClickEvent(string id, int quantity)
        {
            //throw new NotImplementedException();
            if (loai == 1)
            {
                if (!isXacNhan)
                {
                    DialogResult dialogResult;
                    dialogResult =
                            MessageBox.Show(
                                $@"Bạn đang thay đổi số lượng đặt món. Bạn có chắc chắn muốn thay đổi?", @"Cảnh báo",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);

                    if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                    {
                        ThayDoiSoLuong(id, quantity);
                    }
                }
            }
            else
            {
                ThayDoiSoLuong(id, quantity);
            }
        }
        private void ThayDoiSoLuong(string id, int quantity)
        {
            var item = lstOrder.Find(x => x.id == id);
            if (item != null)
            {
                item.quantity = quantity;
                item.total = quantity * item.price;
                //Update quantity of Order list
                foreach (Control ctr in orderPanel.Controls)
                {
                    if (ctr is UserControl)
                    {
                        if (string.Compare(ctr.Tag.ToString(), id) == 0)
                        {
                            foreach (Control lbl in ctr.Controls)
                            {
                                if (lbl is Label && string.Compare("lblQuantity", lbl.Name) == 0)
                                {
                                    lbl.Text = quantity.ToString();
                                }
                                if (lbl is Label && string.Compare("lbtongtien", lbl.Name) == 0)
                                {
                                    lbl.Text = item.total.ToString("N0");
                                }
                            }
                        }
                    }
                }
            }
            total = 0;
            foreach (var orderItem in lstOrder)
            {
                total += orderItem.price * orderItem.quantity;
            }
            lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";

            //Pass order data to second monitor
            secondMornitor.ReciveOrder(false, lstOrder);
        }
        private void Order_DeleteClickEvent(string id)
        {
            var index = lstOrder.FindIndex(x => x.id == id);
            lstOrder.RemoveAt(index);
            total = 0;
            orderPanel.Controls.Remove(orderPanel.Controls.Find(id, true)[0]);
            foreach (var item in lstOrder)
            {
                total += item.price * item.quantity;
            }
            lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";

            //Pass order data to second monitor
            secondMornitor.ReciveOrder(true, lstOrder);
        }
        private List<Food> ConvertTableToList(DataTable dt)
        {
            List<Food> lstResut = new List<Food>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new Food();
                item.id = row["MonAn_Id"].ToString();
                item.name = row["TenMonAn"].ToString();
                item.price = Convert.ToInt64(row["Gia"]);
                item.unit = "Phần";
                item.quantity = 0;
                Thread t = new Thread(() =>
                {
                    item.imageFile = SoficCanteen.Properties.Resources.no_image_icon_23483;
                });
                t.IsBackground = true;
                t.Start();
                //item.imageFile = SystemUti.DownloadImage(apiserver + row["thumb_File_Url"].ToString());
                lstResut.Add(item);
            }
            return lstResut;
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            keyPad = new Keyboard();
            keyPad.ButtonClickEvent += KeyPad_ButtonClickEvent;
            keyPad.ShowDialog();

        }
        private void KeyPad_ButtonClickEvent(string sKetQua)
        {
            SystemUti.WriteLogError("M-" + sKetQua);
            ClearNhanVien();
            if (loai == 1)
            {
                XuLyDuLieu(sKetQua);
                
            }
            else
            {
                GetThongTinNhanVien(sKetQua);
            }

        }
        
       
        private void XacNhan()
        {
            ketQua = false;
            timerReset.Start();
            timerStatus = true;
            
            if (lstOrder != null && lstOrder.Count > 0)
            {
                foreach (var item in lstOrder)
                {
                    if (string.IsNullOrEmpty(NhanVien_Id))
                    {
                        if (Convert.ToInt16(item.quantity) > 0)
                        {
                            ketQua = DBUti.UpdateOrderKH(conn, parentId, inputMode, item.id, item.quantity, userId, LoaiThucDon_Id, KhungGioAn_Id);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt16(item.quantity) > 0)
                        {
                            ketQua = DBUti.UpdateOrderNV(conn, isDanhan, parentId, inputMode, Ngay, userId, KhungGioAn_Id,NhanVien_Id);
                        }
                    }
                }
            }

            if (ketQua)
            {
                isXacNhan = true;
                guna2Button1.Text = "Đã xác nhận";
                guna2Button1.Enabled = false;
                guna2Button1.FillColor = Color.FromArgb(155, 145, 221);
                ClearNhanVien();
                ClearData();
                
                if (!string.IsNullOrEmpty(NhanVien_Id))
                    XuLyDuLieu(temporaryChuoi);
            }
            else
            {
                isXacNhan = false;
                guna2Button1.Text = "Chưa xác nhận";
                guna2Button1.Enabled = true;
                guna2Button1.FillColor = Color.FromArgb(240, 118, 19);
            }
        }
        public void XuLyDuLieu(string sChuoi)
        {
            temporaryChuoi = sChuoi;
            if (!isThuCong)
            {
                XacNhan();
            }
            if (sChuoi.Length >= 7 && sChuoi.Length < 12)
            {
                isThuCong = false;
                total = 0;
                parentId = null;
                lstReg.Clear();
                lstOrder.Clear();
                isDanhan = false;
                orderPanel.Controls.Clear();
                lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";

                guna2Button1.Text = "Chưa xác nhận";
                guna2Button1.Enabled = true;
                isXacNhan = false;
                guna2Button1.FillColor = Color.FromArgb(240, 118, 19);

                DataTable dtOrder = new DataTable();
                if (sChuoi.Length == 7 || sChuoi.Length == 8)
                {
                    inputMode = true;
                }
                else
                {
                    inputMode = false;
                }
                dtOrder = DBUti.GetData(conn, sChuoi,Ngay,LoaiThucDon_Id,KhungGioAn_Id, "SP_GetOrderNV");

                if (dtOrder.Rows.Count > 0)
                {
                    isHopLe = true;
                    guna2Button1.Enabled = true;

                    //--Process parentId--
                    if (!string.IsNullOrEmpty(dtOrder.Rows[0]["Id"].ToString()))
                    {
                        parentId = dtOrder.Rows[0][0].ToString();
                       
                    }
                    else
                    {
                        parentId = Guid.NewGuid().ToString();
                    }

                    //Process isDanhan
                    if (!string.IsNullOrEmpty(dtOrder.Rows[0]["ThoiGianNhan"].ToString()))
                    {
                        isDanhan = true;
                    }
                    else
                    {
                        isDanhan = false;
                    }

                    //--Process NhanVien_Id--
                    lbhoten.Text = dtOrder.Rows[0]["HoTen"].ToString();
                    //lbsodienthoai.Text = dtOrder.Rows[0]["sodienthoai"].ToString();
                    lbchucvu.Text = dtOrder.Rows[0]["ChucVu"].ToString();
                    lbphongban.Text = dtOrder.Rows[0]["TenDonVi"].ToString();
                    if (dtOrder.Rows[0]["NhanVien_Id"] != null)
                    {
                        NhanVien_Id = dtOrder.Rows[0]["NhanVien_Id"].ToString();
                      
                        newId = Guid.NewGuid().ToString();
                    }

                    //--Process Picture & name--
                    if (!string.IsNullOrEmpty(dtOrder.Rows[0]["Khach_Id"].ToString()))
                    {
                        isKhach = true;
                        picAvatar.Image = SoficCanteen.Properties.Resources._0e152f58c9b001ee58a1;

                        //--Pass person infor to second monitor--
                        secondMornitor.RecivePerson(1, "", dtOrder.Rows[0]["HoTen"].ToString());
                    }
                    else
                    {
                        isKhach = false;
                        string maNV = dtOrder.Rows[0]["MaNhanVien"].ToString();
                        Thread t = new Thread(() =>
                        {
                            GetImage(maNV);
                        });
                        t.IsBackground = true;
                        t.Start();

                        //--Pass person infor to second monitor--
                        secondMornitor.RecivePerson(2, maNV, dtOrder.Rows[0]["HoTen"].ToString());
                    }

                    foreach (DataRow row in dtOrder.Rows)
                    {
                        string monanId = row["MonAn_Id"].ToString();
                        int soluong = 0;
                        if (row["SoLuong"] != DBNull.Value)
                        {
                            soluong = Convert.ToInt16(row["SoLuong"]);
                        }

                        if (soluong > 0)
                        {
                            if (!isDanhan)
                            {
                                btnTrangthai.FillColor = Color.GreenYellow;
                                btnTrangthai.FillColor2 = Color.Lime;
                                btnTrangthai.Text = "Chưa nhận";

                                //--Pass status data to second monitor--
                                secondMornitor.ReciveStatus(1);
                            }
                            else
                            {
                                btnTrangthai.FillColor = Color.Violet;
                                btnTrangthai.FillColor2 = Color.DarkViolet;
                                btnTrangthai.Text = "Đã nhận";

                                //--Pass status data to second monitor--
                                secondMornitor.ReciveStatus(6);
                            }
                        }
                        else
                        {
                            btnTrangthai.FillColor = Color.Orange;
                            btnTrangthai.FillColor2 = Color.Red;
                            btnTrangthai.Text = "Không đăng ký";
                            btnTrangthai.ForeColor = Color.White;

                            //--Pass status data to second monitor--
                            secondMornitor.ReciveStatus(2);
                        }
                        
                        var item = lstMenu.Find(x => x.id == monanId);
                        if (item != null)

                        {    
                        item.quantity = soluong;
                            item.total = soluong * item.price;
                            //item.regQuantity = soluong;
                            //lstReg.Add(item);
                            if (!isDanhan)
                            {
                                lstOrder.Add(item);

                                //--Process order panel--
                                ucOderItem order = new ucOderItem(isKhach, item.id, item.name, item.price, item.quantity, item.isdelete, item.imageFile);
                                order.ButtonClickEvent += Order_ButtonClickEvent;
                                order.DeleteClickEvent += Order_DeleteClickEvent;
                                order.Name = item.id;
                                orderPanel.Controls.Add(order);
                            }
                        }

                        //--Pass status data to second monitor--
                        secondMornitor.ReciveOrder(true, lstOrder);


                        //--Checking time--
                        if (!isKhach && !string.IsNullOrEmpty(dtOrder.Rows[0]["BatDau"].ToString()) && monanId != "")
                        {
                            TimeSpan hourMinute = DateTime.Now.TimeOfDay;
                          
                            string[] batdau = dtOrder.Rows[0]["BatDau"].ToString().Split(' ');
                            string[] ketthuc = dtOrder.Rows[0]["KetThuc"].ToString().Split(' ');
                            string[] beginArr = batdau[1].Split(':');
                            string[] endArr = ketthuc[1].Split(':');
                            if (
                                hourMinute < new TimeSpan(Convert.ToInt16(beginArr[0]), Convert.ToInt16(beginArr[1]), 0)
                                || hourMinute > new TimeSpan(Convert.ToInt16(endArr[0]), Convert.ToInt16(endArr[1])+30, 0)
                                )
                            {
                                if (!isDanhan)
                                {
                                    btnTrangthai.FillColor = Color.LightBlue;
                                    btnTrangthai.FillColor2 = Color.DodgerBlue;
                                    btnTrangthai.Text = "Không đúng giờ";

                                    //--Pass status data to second monitor--
                                    secondMornitor.ReciveStatus(4);
                                }
                            }
                        }
                    }

                    if (lstOrder.Count > 0 && !isDanhan)
                    {
                        //--Pass data to guest form--
                        secondMornitor.ReciveOrder(true, lstOrder);

                        foreach (var orderItem in lstOrder)
                        {
                            total += orderItem.price * orderItem.quantity;
                        }
                        lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
                    }
                }
                else
                {
                    total = 0;
                    ketQua = false;
                    isHopLe = false;
                    parentId = null;
                    lstReg.Clear();
                    lstOrder.Clear();
                    isDanhan = false;
                    orderPanel.Controls.Clear();
                    guna2Button1.Enabled = false;

                    lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
                    lbhoten.Text = "Mã số " + sChuoi;
                    btnTrangthai.FillColor = Color.Orange;
                    btnTrangthai.FillColor2 = Color.Red;
                    btnTrangthai.Text = "Không đăng ký";
                    btnTrangthai.ForeColor = Color.White;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;

                    //--Pass status data to second monitor--
                    secondMornitor.ReciveStatus(3);
                    secondMornitor.ReciveOrder(true, lstOrder);

                    SystemUti.WriteLogError(sChuoi);
                }
            }
            else
            {
                NhanVien_Id = "";
                if (sChuoi.Length == 12)
                {
                    total = 0;
                    ketQua = false;
                    isHopLe = false;
                    parentId = null;
                    lstReg.Clear();
                    lstOrder.Clear();
                    isDanhan = false;
                    orderPanel.Controls.Clear();
                    guna2Button1.Enabled = false;

                    lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
                    lbhoten.Text = "Mã số " + sChuoi;
                    btnTrangthai.FillColor = Color.Orange;
                    btnTrangthai.FillColor2 = Color.Red;
                    btnTrangthai.Text = "Sai đầu đọc thẻ";
                    btnTrangthai.ForeColor = Color.White;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;

                    //--Pass status data to second monitor--
                    secondMornitor.ReciveStatus(5);
                }
                else
                {
                    total = 0;
                    ketQua = false;
                    isHopLe = false;
                    parentId = null;
                    lstReg.Clear();
                    lstOrder.Clear();
                    isDanhan = false;
                    orderPanel.Controls.Clear();
                    guna2Button1.Enabled = false;

                    lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
                    lbhoten.Text = "Mã số " + sChuoi;
                    btnTrangthai.FillColor = Color.PapayaWhip;
                    btnTrangthai.FillColor2 = Color.Orange;
                    btnTrangthai.Text = "Không tồn tại";
                    btnTrangthai.ForeColor = Color.White;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;

                    //--Pass status data to second monitor--
                    secondMornitor.ReciveStatus(3);
                }
            }
           
        }
        private void GetThongTinNhanVien(string sChuoi)
        {
            temporaryChuoi = sChuoi;
            if (sChuoi.Length >= 7 && sChuoi.Length < 12)
            {
                DataTable dtOrder = new DataTable();
                if (sChuoi.Length == 7 || sChuoi.Length == 8)
                {
                    inputMode = true;
                }
                else
                {
                    inputMode = false;
                }
                dtOrder = DBUti.GetInfo(conn, sChuoi, "SP_GetInfo");

                if (dtOrder.Rows.Count > 0)
                {
                    isHopLe = true;
                    guna2Button1.Enabled = true;
                    //--Process NhanVien_Id--
                    lbhoten.Text = dtOrder.Rows[0]["HoTen"].ToString();
                    //lbsodienthoai.Text = dtOrder.Rows[0]["sodienthoai"].ToString();
                    lbchucvu.Text = dtOrder.Rows[0]["ChucVu"].ToString();
                    lbphongban.Text = dtOrder.Rows[0]["TenDonVi"].ToString();
                    MaNhanVien = dtOrder.Rows[0]["MaNhanVien"].ToString();
                    if (dtOrder.Rows[0]["NhanVien_Id"] != null)
                    {
                        NhanVien_Id = dtOrder.Rows[0]["NhanVien_Id"].ToString();
                        newId = Guid.NewGuid().ToString();
                    }
                    isKhach = false;
                    string maNV = dtOrder.Rows[0]["MaNhanVien"].ToString();
                    Thread t = new Thread(() =>
                    {
                        GetImage(maNV);
                    });
                    t.IsBackground = true;
                    t.Start();

                    //--Pass person infor to second monitor--
                    secondMornitor.RecivePerson(2, maNV, dtOrder.Rows[0]["HoTen"].ToString());
                    if (total > 0)
                    {
                        thanhToan = new ThanhToan();
                        thanhToan.ButtonClickEvent += XacNhanDichVu;
                        thanhToan.Show();
                    }
                }
                else
                {
                    NhanVienKhongTonTai(sChuoi);
                }
            }
            else
            {
                if (sChuoi.Length == 12)
                {
                    lbhoten.Text = "Mã số " + sChuoi;
                    btnTrangthai.FillColor = Color.Orange;
                    btnTrangthai.FillColor2 = Color.Red;
                    btnTrangthai.Text = "Sai đầu đọc thẻ";
                    btnTrangthai.Visible = true;
                    btnTrangthai.ForeColor = Color.White;
                    picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;

                    //--Pass status data to second monitor--
                    secondMornitor.ReciveStatus(5);
                }
                else
                {
                    NhanVienKhongTonTai(sChuoi);
                }
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
        public void ClearNhanVien()
        {
            //NhanVien_Id = "";
            lbhoten.Text = "Họ tên";
            //lbsodienthoai.Text = "Số điện thoại";
            lbchucvu.Text = "Chức vụ";
            lbphongban.Text = "Phòng ban";
            btnTrangthai.FillColor = Color.PapayaWhip;
            btnTrangthai.FillColor2 = Color.Orange;
            btnTrangthai.Text = "";
            btnTrangthai.ForeColor = Color.White;
            picAvatar.Image = SoficCanteen.Properties.Resources.no_image_icon_23483;
            //--Pass status data to second monitor--
            //secondMornitor.ReciveStatus(0);
        }
        void NhanVienKhongTonTai(string sChuoi)
        {
            lbhoten.Text = "Mã số " + sChuoi;
            btnTrangthai.FillColor = Color.PapayaWhip;
            btnTrangthai.FillColor2 = Color.Orange;
            btnTrangthai.Text = "Không tồn tại";
            btnTrangthai.Visible = true;
            btnTrangthai.ForeColor = Color.White;
            picAvatar.Image = SoficCanteen.Properties.Resources.unavailable_200px;
            secondMornitor.ReciveStatus(3);
        }
        public void ClearData()
        {
            btnTrangthai.Visible = loai == 1;
            total = 0;
            parentId = null;
            lstReg.Clear();
            lstOrder.Clear();
            isDanhan = false;
            orderPanel.Controls.Clear();
            lblTotal.Text = "Tổng cộng: 0 VNĐ";
            secondMornitor.ReciveOrder(true, lstOrder);
            isXacNhan = false;
            guna2Button1.Text = "Xác nhận";
            guna2Button1.Enabled = true;
            guna2Button1.FillColor = Color.FromArgb(240, 118, 19);
        }
        private void XacNhanDichVu(int LoaiThanhToan)
        {
            ketQua = false;
            timerReset.Start();
            timerStatus = true;
            parentId = Guid.NewGuid().ToString();
            double tongtien = 0;
            int sltendai = 0;
            if (lstOrder != null && lstOrder.Count > 0)
            {
                foreach (var item in lstOrder)
                {
                    if (Convert.ToInt16(item.quantity) > 0)
                    {
                        ketQua = DBUti.UpdateDichVu(conn, isDanhan, newId, NhanVien_Id, parentId, inputMode, item.id, item.regQuantity, item.quantity, userId, DiaDiemAn_Id, LoaiThanhToan);
                    }
                }
            }
            if (ketQua)
            {
                thanhToan.Close();
                //InHoaDon(parentId, true);
                isXacNhan = true;
                guna2Button1.Text = "Đã xác nhận";
                guna2Button1.Enabled = false;
                guna2Button1.FillColor = Color.FromArgb(155, 145, 221);
                ClearData();
                ClearNhanVien();
            }
            else
            {
                isXacNhan = false;
                guna2Button1.Text = "Chưa xác nhận";
                guna2Button1.Enabled = true;
                guna2Button1.FillColor = Color.FromArgb(240, 118, 19);
            }
        }

        private void btnDoan_Click(object sender, EventArgs e)
        {
            Func_ButtonColor("btnDoan");
        }

        private void btnThucuong_Click(object sender, EventArgs e)
        {
            Func_ButtonColor("btnThucuong");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (loai == 1)
            {
                XacNhan();
                isThuCong = true;
            }
            else
            {
                if (total > 0)
                {
                    keyPad = new Keyboard();
                    keyPad.ButtonClickEvent += KeyPad_ButtonClickEvent;
                    keyPad.Show();
                   
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(conn);
            setting.StartPosition = FormStartPosition.Manual;
            setting.Top = 60;
            setting.Left = this.Width - 940;
            setting.ShowDialog();
        }
    }
    public class Food
    {
        public string id { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public int regQuantity { get; set; }
        public int quantity { get; set; }
        public long price { get; set; }
        public long total { get; set; }
        public bool isdelete { get; set; }
        public Image imageFile { get; set; }
    }
}
