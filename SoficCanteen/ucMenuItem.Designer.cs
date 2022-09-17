namespace SoficCanteen
{
    partial class ucMenuItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.lblName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblPrice = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Button
            // 
            this.guna2Button.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(202)))), ((int)(((byte)(209)))));
            this.guna2Button.BorderRadius = 10;
            this.guna2Button.BorderThickness = 1;
            this.guna2Button.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button.FillColor = System.Drawing.Color.White;
            this.guna2Button.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button.ForeColor = System.Drawing.Color.White;
            this.guna2Button.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.HoverState.FillColor = System.Drawing.Color.White;
            this.guna2Button.Location = new System.Drawing.Point(0, 0);
            this.guna2Button.Name = "guna2Button";
            this.guna2Button.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(118)))), ((int)(((byte)(19)))));
            this.guna2Button.Size = new System.Drawing.Size(343, 153);
            this.guna2Button.TabIndex = 0;
            this.guna2Button.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2Button.Click += new System.EventHandler(this.guna2Button_Click);
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.White;
            this.lblName.Font = new System.Drawing.Font("Cambria", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(146, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(82, 22);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Cơm gà xé";
            // 
            // lblPrice
            // 
            this.lblPrice.BackColor = System.Drawing.Color.White;
            this.lblPrice.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblPrice.Location = new System.Drawing.Point(146, 47);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(102, 23);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "30,000 VNĐ";
            // 
            // guna2PictureBox
            // 
            this.guna2PictureBox.Image = global::SoficCanteen.Properties.Resources._650_425_com_ga_nha_trang_bepcuoi_981;
            this.guna2PictureBox.ImageRotate = 0F;
            this.guna2PictureBox.Location = new System.Drawing.Point(3, 12);
            this.guna2PictureBox.Name = "guna2PictureBox";
            this.guna2PictureBox.Size = new System.Drawing.Size(127, 125);
            this.guna2PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox.TabIndex = 1;
            this.guna2PictureBox.TabStop = false;
            // 
            // ucMenuItem
            // 
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.guna2PictureBox);
            this.Controls.Add(this.guna2Button);
            this.Name = "ucMenuItem";
            this.Size = new System.Drawing.Size(346, 153);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private Guna.UI2.WinForms.Guna2Button guna2Button;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPrice;
    }
}
