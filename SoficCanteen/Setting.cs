using DevComponents.Editors;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace SoficCanteen
{
    public partial class Setting : System.Windows.Forms.Form 
    {
        private string conn;
        string DiaDiemAn_Id = "";
        string TenDiaDiemAn = "";
        string LoaiThucDon_Id = "";
        string Ngay = "";
        
        string KhungGioAn_Id = "";
        string MaKhungGioAn = "";
        string TenLoaiThucDon = "";
        private long total = 0;
        private long loai = 1;
        public delegate void ButtonClick();
        public event ButtonClick ButtonClickEvent;
        public Setting(string dbConnect)
        {
            InitializeComponent();
            this.conn = dbConnect;

            //Read config parameter

            string[] arrConfig = SystemUti.ReadConfig();
            if (arrConfig != null)
            {
                conn = arrConfig[0];
                DiaDiemAn_Id = arrConfig[1];
                TenDiaDiemAn = arrConfig[2];
                LoaiThucDon_Id = arrConfig[3];
                Ngay = arrConfig[4];
                KhungGioAn_Id = arrConfig[5];
                MaKhungGioAn= arrConfig[6];
                TenLoaiThucDon = arrConfig[7];

            }
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
       
        private void gunaButton1_Click(object sender, EventArgs e)
        {

        }
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (SystemUti.UpdateConfig(lookUpEdit2.EditValue.ToString(),dateEdit.Text.ToString(), lookUpEdit1.EditValue.ToString(), lookUpEdit1.Text, lookUpEdit2.Text))
            {
                
                MessageBox.Show("Cập nhật thành công", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                
                
               

                string[] arrConfig = SystemUti.ReadConfig();
                if (arrConfig != null)
                {
                    conn = arrConfig[0];
                    DiaDiemAn_Id = arrConfig[1];
                    TenDiaDiemAn = arrConfig[2];
                    LoaiThucDon_Id = arrConfig[3];
                    Ngay = arrConfig[4];
                    KhungGioAn_Id = arrConfig[5];
                    MaKhungGioAn = arrConfig[6];
                    TenLoaiThucDon = arrConfig[7];

                }
                Form obj = (Form)Application.OpenForms["Form"];
                if (obj != null)
                {
                    obj.Close();
                    LoginAuth loginAuth = new LoginAuth();
                    Form mainForm = new Form(loginAuth.username, "", conn, DiaDiemAn_Id, TenDiaDiemAn, LoaiThucDon_Id, Ngay, KhungGioAn_Id, MaKhungGioAn, TenLoaiThucDon);
                    mainForm.ShowDialog();
                }
            }

            else
                MessageBox.Show("Cập nhật không thành công", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Setting_Load(object sender, EventArgs e)
        {
            dateEdit.Text = Ngay;
        
            DataTable dtKhungGioAn = new DataTable();
            dtKhungGioAn = DBUti.GetKhungGioAn(conn, KhungGioAn_Id, "SP_GetKhungGioAn");
            if (dtKhungGioAn != null && dtKhungGioAn.Rows.Count > 0)
            {
                lookUpEdit1.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                lookUpEdit1.Properties.DataSource = dtKhungGioAn;
                lookUpEdit1.Properties.DisplayMember = "MaKhungGioAn";

                lookUpEdit1.Properties.ValueMember = "Id";
                lookUpEdit1.Properties.PopulateColumns();
                lookUpEdit1.Properties.NullText = "Vui lòng chọn khung giờ ăn";
                lookUpEdit1.Properties.ForceInitialize();
                if (!string.IsNullOrEmpty(KhungGioAn_Id))
                {
                    lookUpEdit1.EditValue = Guid.Parse(KhungGioAn_Id);
                }
                
                lookUpEdit1.Properties.Columns[0].Visible = false;
               
            }

            DataTable dtSuatAn = new DataTable();
            dtSuatAn = DBUti.GetSuatAn(conn, LoaiThucDon_Id, "SP_GetSuatAn");
            if (dtSuatAn != null && dtSuatAn.Rows.Count > 0)
            {
                lookUpEdit2.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                lookUpEdit2.Properties.DataSource = dtSuatAn;
                lookUpEdit2.Properties.DisplayMember = "TenLoaiThucDon";

                lookUpEdit2.Properties.ValueMember = "Id";
                lookUpEdit2.Properties.PopulateColumns();
                lookUpEdit2.Properties.NullText = "Vui lòng chọn suất ăn";
                lookUpEdit2.Properties.ForceInitialize();
                if (!string.IsNullOrEmpty(LoaiThucDon_Id))
                {
                    lookUpEdit2.EditValue = Guid.Parse(LoaiThucDon_Id);
                }
               
                lookUpEdit2.Properties.Columns[0].Visible = false;
                
            }
        }

        

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

