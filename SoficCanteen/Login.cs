using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoficCanteen
{
    
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        Control ActiveControl;
        private bool shift;
        private string[] arrConfig;
        private string conn;
        private string DiaDiemAn_Id;
        private string TenDiaDiemAn;
        private string LoaiThucDon_Id;
        private string Ngay;
        private string KhungGioAn_Id;
        private string MaKhungGioAn;
        private string TenLoaiThucDon;


        private string userId;
        private bool remind;
       
        private string apiServer;
        private string authorize;
        private string updateServer;
        private string apiKey;
        public Login()
        {
            InitializeComponent();
            //Read config parameter
            arrConfig = SystemUti.ReadConfig();
            if (arrConfig != null)
            {
                this.conn = arrConfig[0];
                this.DiaDiemAn_Id = arrConfig[1];
                this.TenDiaDiemAn = arrConfig[2];
                this.LoaiThucDon_Id = arrConfig[3];
                this.Ngay = arrConfig[4];
                this.KhungGioAn_Id = arrConfig[5];

                
                

            }
            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            gunaLabel3.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            timer.Start();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Authentication();
        }
        private void Authentication()
        {
            if (!txtUser.Text.Trim().Equals("") //&& !txtPassword.Text.Trim().Equals("")
                )
            {
                //Read config parameter
                arrConfig = SystemUti.ReadConfig();
                if (arrConfig != null)
                {
                    this.conn = arrConfig[0];
                    this.DiaDiemAn_Id = arrConfig[1];
                    this.TenDiaDiemAn = arrConfig[2];
                    this.LoaiThucDon_Id = arrConfig[3];
                    this.Ngay = arrConfig[4];
                    this.KhungGioAn_Id = arrConfig[5];
                    this.MaKhungGioAn = arrConfig[6];
                    this.TenLoaiThucDon = arrConfig[7];

                }
                //DBUti.UpdateConfig(conn, arrConfig[0], false);
                LoginAuth loginUser = new LoginAuth();
                loginUser.username = txtUser.Text;
                //loginUser.password = txtPassword.Text;
                loginUser.domain = "";
                loginUser.diaDiemAn_Id = DiaDiemAn_Id;
                Form mainForm = new Form(txtUser.Text, "", conn, DiaDiemAn_Id, TenDiaDiemAn, LoaiThucDon_Id, Ngay, KhungGioAn_Id,MaKhungGioAn,TenLoaiThucDon);
                mainForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên");
            }
            #region VPCL Method

            /*
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiServer + "token/App");
                httpWebRequest.ContentType = "application/json";


                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(loginUser);
                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        JsonAuth jsonSample = JsonConvert.DeserializeObject<JsonAuth>(result);
                        userId = jsonSample.id;
                        Form mainForm = new Form(userId, locationId, apiServer, apiKey, conn);
                        mainForm.ShowDialog();
                        //this.Hide();
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    MessageBox.Show("Thông tin đăng nhập không đúng. \n Vui lòng thử lại.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
            #endregion
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(conn);
            setting.StartPosition = FormStartPosition.Manual;
            setting.Top = 50;
            setting.Left = this.Width - 430;
            setting.ShowDialog();
        }

        private void guna2Button41_Click(object sender, EventArgs e)
        {
            if (!shift)
            {
                guna2Button41.FillColor = Color.FromArgb(151, 143, 255);
                guna2Button41.ForeColor = Color.White;
                guna2Button43.FillColor = Color.FromArgb(151, 143, 255);
                guna2Button43.ForeColor = Color.White;
                shift = true;
            }
            else
            {
                guna2Button41.FillColor = Color.White;
                guna2Button41.ForeColor = Color.Black;
                guna2Button43.FillColor = Color.White;
                guna2Button43.ForeColor = Color.Black;
                shift = false;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;
            ActiveControl.Focus();
            if (!shift)
            {
                SendKeys.Send(btn.Text.ToLower());
            }
            else
            {
                SendKeys.Send(btn.Text.ToUpper());
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (ActiveControl is Guna.UI2.WinForms.Guna2TextBox)
            {
                Guna.UI2.WinForms.Guna2TextBox txt = ActiveControl as Guna.UI2.WinForms.Guna2TextBox;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    string sChuoi = txt.Text;
                    string sKetQua = sChuoi.TrimEnd(sChuoi[sChuoi.Length - 1]);
                    txt.Text = sKetQua;
                }
            }
        }

        private void guna2Button45_Click(object sender, EventArgs e)
        {
            if (ActiveControl is Guna.UI2.WinForms.Guna2TextBox)
            {
                Guna.UI2.WinForms.Guna2TextBox txt = ActiveControl as Guna.UI2.WinForms.Guna2TextBox;
                txt.Clear();
            }
        }

        

        private void txtUser_Enter(object sender, EventArgs e)
        {
            ActiveControl = (Control)sender;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }
    }
    #region JSON LOGIN
    public class JsonAuth
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public DateTime expires { get; set; }
        public bool mustChangePass { get; set; }
    }
    public class LoginAuth
    {
        public string username { get; set; }
        public string password { get; set; }
        public string domain { get; set; }
        public string diaDiemAn_Id { get; set; }
    }
    #endregion
}