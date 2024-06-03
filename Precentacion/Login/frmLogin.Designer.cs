namespace Precentacion.Login
{
    partial class frmLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.IconLogin = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAccess = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.pbUserName = new System.Windows.Forms.PictureBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassWord = new System.Windows.Forms.Label();
            this.lblSlogan1 = new System.Windows.Forms.Label();
            this.slogan2 = new System.Windows.Forms.Label();
            this.lblForgetPassword = new System.Windows.Forms.LinkLabel();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panelTitleBar.Controls.Add(this.label3);
            this.panelTitleBar.Controls.Add(this.IconLogin);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(697, 25);
            this.panelTitleBar.TabIndex = 0;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Login GlasWin";
            // 
            // IconLogin
            // 
            this.IconLogin.Image = ((System.Drawing.Image)(resources.GetObject("IconLogin.Image")));
            this.IconLogin.Location = new System.Drawing.Point(10, 2);
            this.IconLogin.Name = "IconLogin";
            this.IconLogin.Size = new System.Drawing.Size(26, 21);
            this.IconLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IconLogin.TabIndex = 1;
            this.IconLogin.TabStop = false;
            this.toolTip1.SetToolTip(this.IconLogin, "GlassWin Sofware");
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(657, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 21);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.toolTip1.SetToolTip(this.btnClose, "Salir del Progrma");
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnAccess
            // 
            this.btnAccess.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAccess.ForeColor = System.Drawing.Color.White;
            this.btnAccess.Location = new System.Drawing.Point(452, 199);
            this.btnAccess.Name = "btnAccess";
            this.btnAccess.Size = new System.Drawing.Size(182, 38);
            this.btnAccess.TabIndex = 4;
            this.btnAccess.Text = "Ingresar";
            this.toolTip1.SetToolTip(this.btnAccess, "Boton de Ingresar al Sistema");
            this.btnAccess.UseVisualStyleBackColor = false;
            this.btnAccess.Click += new System.EventHandler(this.btnAccess_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtUserName.Location = new System.Drawing.Point(432, 75);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(222, 26);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.Text = "Ingrese el Usuario";
            this.toolTip1.SetToolTip(this.txtUserName, "Ingrese el Usuario");
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassWord.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtPassWord.Location = new System.Drawing.Point(431, 142);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(222, 26);
            this.txtPassWord.TabIndex = 6;
            this.txtPassWord.Text = "Ingrese la Contraseña";
            this.toolTip1.SetToolTip(this.txtPassWord, "Ingrese la Contraseña");
            this.txtPassWord.Enter += new System.EventHandler(this.txtPassWord_Enter);
            this.txtPassWord.Leave += new System.EventHandler(this.txtPassWord_Leave);
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.Location = new System.Drawing.Point(42, 49);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(247, 188);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 2;
            this.Logo.TabStop = false;
            // 
            // pbUserName
            // 
            this.pbUserName.Image = ((System.Drawing.Image)(resources.GetObject("pbUserName.Image")));
            this.pbUserName.Location = new System.Drawing.Point(370, 49);
            this.pbUserName.Name = "pbUserName";
            this.pbUserName.Size = new System.Drawing.Size(55, 53);
            this.pbUserName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbUserName.TabIndex = 2;
            this.pbUserName.TabStop = false;
            // 
            // pbPassword
            // 
            this.pbPassword.Image = ((System.Drawing.Image)(resources.GetObject("pbPassword.Image")));
            this.pbPassword.Location = new System.Drawing.Point(370, 130);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(55, 53);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPassword.TabIndex = 3;
            this.pbPassword.TabStop = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblUserName.Location = new System.Drawing.Point(501, 54);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(75, 17);
            this.lblUserName.TabIndex = 7;
            this.lblUserName.Text = "UserName";
            // 
            // lblPassWord
            // 
            this.lblPassWord.AutoSize = true;
            this.lblPassWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassWord.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPassWord.Location = new System.Drawing.Point(501, 119);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(73, 17);
            this.lblPassWord.TabIndex = 8;
            this.lblPassWord.Text = "PassWord";
            // 
            // lblSlogan1
            // 
            this.lblSlogan1.Font = new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlogan1.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblSlogan1.Location = new System.Drawing.Point(39, 240);
            this.lblSlogan1.Name = "lblSlogan1";
            this.lblSlogan1.Size = new System.Drawing.Size(250, 20);
            this.lblSlogan1.TabIndex = 10;
            this.lblSlogan1.Text = "GlassWin Maximiza su Tiempo ";
            this.lblSlogan1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // slogan2
            // 
            this.slogan2.Font = new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slogan2.ForeColor = System.Drawing.Color.SteelBlue;
            this.slogan2.Location = new System.Drawing.Point(39, 260);
            this.slogan2.Name = "slogan2";
            this.slogan2.Size = new System.Drawing.Size(250, 16);
            this.slogan2.TabIndex = 11;
            this.slogan2.Text = "Minimiza Errores";
            this.slogan2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblForgetPassword
            // 
            this.lblForgetPassword.AutoSize = true;
            this.lblForgetPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForgetPassword.Location = new System.Drawing.Point(475, 254);
            this.lblForgetPassword.Name = "lblForgetPassword";
            this.lblForgetPassword.Size = new System.Drawing.Size(137, 15);
            this.lblForgetPassword.TabIndex = 12;
            this.lblForgetPassword.TabStop = true;
            this.lblForgetPassword.Text = "¿Olvido su Contraseña?";
            this.lblForgetPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblForgetPassword_LinkClicked);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(698, 317);
            this.Controls.Add(this.lblForgetPassword);
            this.Controls.Add(this.slogan2);
            this.Controls.Add(this.lblSlogan1);
            this.Controls.Add(this.lblPassWord);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnAccess);
            this.Controls.Add(this.pbPassword);
            this.Controls.Add(this.pbUserName);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmLogin_Paint);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox IconLogin;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.PictureBox pbUserName;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAccess;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassWord;
        private System.Windows.Forms.Label lblSlogan1;
        private System.Windows.Forms.Label slogan2;
        private System.Windows.Forms.LinkLabel lblForgetPassword;
    }
}