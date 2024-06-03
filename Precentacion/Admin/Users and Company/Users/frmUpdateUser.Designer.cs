namespace Precentacion.Admin.Users_and_Company.Users
{
    partial class frmUpdateUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateUser));
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblProductManager = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRestore = new System.Windows.Forms.PictureBox();
            this.btnMini = new System.Windows.Forms.PictureBox();
            this.btnMaxi = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.cbRoll = new System.Windows.Forms.ComboBox();
            this.lblRoll = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.PanelDatosAcceso = new System.Windows.Forms.Panel();
            this.lblDatosAcceso = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.PanelDatosUsuario = new System.Windows.Forms.Panel();
            this.lblDatosUsuario = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblCorreoUsuario = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.lblTelUsuario = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindUpdate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMini)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaxi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.PanelDatosAcceso.SuspendLayout();
            this.PanelDatosUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.Black;
            this.panelTitleBar.Controls.Add(this.lblProductManager);
            this.panelTitleBar.Controls.Add(this.pictureBox1);
            this.panelTitleBar.Controls.Add(this.btnRestore);
            this.panelTitleBar.Controls.Add(this.btnMini);
            this.panelTitleBar.Controls.Add(this.btnMaxi);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 30);
            this.panelTitleBar.TabIndex = 48;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // lblProductManager
            // 
            this.lblProductManager.AutoSize = true;
            this.lblProductManager.BackColor = System.Drawing.Color.Black;
            this.lblProductManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductManager.ForeColor = System.Drawing.Color.White;
            this.lblProductManager.Location = new System.Drawing.Point(56, 6);
            this.lblProductManager.Name = "lblProductManager";
            this.lblProductManager.Size = new System.Drawing.Size(110, 18);
            this.lblProductManager.TabIndex = 5;
            this.lblProductManager.Text = "Editar Usuarios";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
            this.btnRestore.Location = new System.Drawing.Point(728, 3);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(29, 25);
            this.btnRestore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnRestore.TabIndex = 3;
            this.btnRestore.TabStop = false;
            this.btnRestore.Visible = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnMini
            // 
            this.btnMini.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMini.Image = ((System.Drawing.Image)(resources.GetObject("btnMini.Image")));
            this.btnMini.Location = new System.Drawing.Point(693, 2);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(29, 25);
            this.btnMini.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMini.TabIndex = 2;
            this.btnMini.TabStop = false;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // btnMaxi
            // 
            this.btnMaxi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMaxi.Image = ((System.Drawing.Image)(resources.GetObject("btnMaxi.Image")));
            this.btnMaxi.Location = new System.Drawing.Point(729, 2);
            this.btnMaxi.Name = "btnMaxi";
            this.btnMaxi.Size = new System.Drawing.Size(29, 25);
            this.btnMaxi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMaxi.TabIndex = 1;
            this.btnMaxi.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(765, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(29, 25);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbRoll
            // 
            this.cbRoll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbRoll.FormattingEnabled = true;
            this.cbRoll.Items.AddRange(new object[] {
            "Seleccione un Roll",
            "Admin",
            "User"});
            this.cbRoll.Location = new System.Drawing.Point(545, 106);
            this.cbRoll.Name = "cbRoll";
            this.cbRoll.Size = new System.Drawing.Size(199, 21);
            this.cbRoll.TabIndex = 44;
            // 
            // lblRoll
            // 
            this.lblRoll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRoll.AutoSize = true;
            this.lblRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoll.ForeColor = System.Drawing.Color.Black;
            this.lblRoll.Location = new System.Drawing.Point(387, 102);
            this.lblRoll.Name = "lblRoll";
            this.lblRoll.Size = new System.Drawing.Size(143, 25);
            this.lblRoll.TabIndex = 45;
            this.lblRoll.Text = "Roll de Usuario";
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Black;
            this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 33);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(800, 59);
            this.lblTitulo.TabIndex = 43;
            this.lblTitulo.Text = "Editar Usuario";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAccept.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAccept.BackgroundImage")));
            this.btnAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAccept.Location = new System.Drawing.Point(467, 366);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(63, 57);
            this.btnAccept.TabIndex = 41;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // PanelDatosAcceso
            // 
            this.PanelDatosAcceso.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelDatosAcceso.BackColor = System.Drawing.Color.LightGray;
            this.PanelDatosAcceso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDatosAcceso.Controls.Add(this.lblDatosAcceso);
            this.PanelDatosAcceso.Controls.Add(this.lblPassword);
            this.PanelDatosAcceso.Controls.Add(this.txtPassWord);
            this.PanelDatosAcceso.Controls.Add(this.lblUserName);
            this.PanelDatosAcceso.Controls.Add(this.txtUserName);
            this.PanelDatosAcceso.Location = new System.Drawing.Point(388, 143);
            this.PanelDatosAcceso.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.PanelDatosAcceso.Name = "PanelDatosAcceso";
            this.PanelDatosAcceso.Size = new System.Drawing.Size(356, 179);
            this.PanelDatosAcceso.TabIndex = 47;
            // 
            // lblDatosAcceso
            // 
            this.lblDatosAcceso.BackColor = System.Drawing.Color.Black;
            this.lblDatosAcceso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDatosAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatosAcceso.ForeColor = System.Drawing.Color.White;
            this.lblDatosAcceso.Location = new System.Drawing.Point(-1, 0);
            this.lblDatosAcceso.Name = "lblDatosAcceso";
            this.lblDatosAcceso.Size = new System.Drawing.Size(356, 59);
            this.lblDatosAcceso.TabIndex = 26;
            this.lblDatosAcceso.Text = "Datos de Acceso";
            this.lblDatosAcceso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(15, 137);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(104, 25);
            this.lblPassword.TabIndex = 29;
            this.lblPassword.Text = "PassWord";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(177, 143);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(155, 20);
            this.txtPassWord.TabIndex = 6;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(15, 80);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(105, 25);
            this.lblUserName.TabIndex = 27;
            this.lblUserName.Text = "UserName";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(178, 85);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(155, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // PanelDatosUsuario
            // 
            this.PanelDatosUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelDatosUsuario.BackColor = System.Drawing.Color.LightGray;
            this.PanelDatosUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDatosUsuario.Controls.Add(this.lblDatosUsuario);
            this.PanelDatosUsuario.Controls.Add(this.lblNombre);
            this.PanelDatosUsuario.Controls.Add(this.txtEmail);
            this.PanelDatosUsuario.Controls.Add(this.lblCorreoUsuario);
            this.PanelDatosUsuario.Controls.Add(this.txtTel);
            this.PanelDatosUsuario.Controls.Add(this.lblTelUsuario);
            this.PanelDatosUsuario.Controls.Add(this.txtName);
            this.PanelDatosUsuario.Location = new System.Drawing.Point(17, 143);
            this.PanelDatosUsuario.Name = "PanelDatosUsuario";
            this.PanelDatosUsuario.Size = new System.Drawing.Size(356, 304);
            this.PanelDatosUsuario.TabIndex = 46;
            // 
            // lblDatosUsuario
            // 
            this.lblDatosUsuario.BackColor = System.Drawing.Color.Black;
            this.lblDatosUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDatosUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDatosUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatosUsuario.ForeColor = System.Drawing.Color.White;
            this.lblDatosUsuario.Location = new System.Drawing.Point(0, 0);
            this.lblDatosUsuario.Name = "lblDatosUsuario";
            this.lblDatosUsuario.Size = new System.Drawing.Size(354, 59);
            this.lblDatosUsuario.TabIndex = 25;
            this.lblDatosUsuario.Text = "Datos del Usuario";
            this.lblDatosUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.Black;
            this.lblNombre.Location = new System.Drawing.Point(3, 81);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(81, 25);
            this.lblNombre.TabIndex = 16;
            this.lblNombre.Text = "Nombre";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(113, 258);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(230, 20);
            this.txtEmail.TabIndex = 12;
            // 
            // lblCorreoUsuario
            // 
            this.lblCorreoUsuario.AutoSize = true;
            this.lblCorreoUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorreoUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblCorreoUsuario.Location = new System.Drawing.Point(6, 251);
            this.lblCorreoUsuario.Name = "lblCorreoUsuario";
            this.lblCorreoUsuario.Size = new System.Drawing.Size(72, 25);
            this.lblCorreoUsuario.TabIndex = 15;
            this.lblCorreoUsuario.Text = "Correo";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(113, 168);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(229, 20);
            this.txtTel.TabIndex = 10;
            // 
            // lblTelUsuario
            // 
            this.lblTelUsuario.AutoSize = true;
            this.lblTelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblTelUsuario.Location = new System.Drawing.Point(3, 162);
            this.lblTelUsuario.Name = "lblTelUsuario";
            this.lblTelUsuario.Size = new System.Drawing.Size(89, 25);
            this.lblTelUsuario.TabIndex = 21;
            this.lblTelUsuario.Text = "Telefono";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(620, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 57);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 25);
            this.label1.TabIndex = 27;
            this.label1.Text = "Numero Cedula";
            // 
            // btnFindUpdate
            // 
            this.btnFindUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFindUpdate.BackColor = System.Drawing.Color.SeaShell;
            this.btnFindUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFindUpdate.BackgroundImage")));
            this.btnFindUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFindUpdate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFindUpdate.Location = new System.Drawing.Point(345, 101);
            this.btnFindUpdate.Name = "btnFindUpdate";
            this.btnFindUpdate.Size = new System.Drawing.Size(28, 26);
            this.btnFindUpdate.TabIndex = 100;
            this.btnFindUpdate.Text = " ";
            this.btnFindUpdate.UseVisualStyleBackColor = false;
            this.btnFindUpdate.Click += new System.EventHandler(this.btnFindUpdate_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(113, 81);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(230, 20);
            this.txtName.TabIndex = 8;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(172, 104);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(167, 20);
            this.txtID.TabIndex = 26;
            // 
            // frmUpdateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFindUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.cbRoll);
            this.Controls.Add(this.lblRoll);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.PanelDatosAcceso);
            this.Controls.Add(this.PanelDatosUsuario);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUpdateUser";
            this.Text = "frmUpdateUser";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewSettingsPrice_Paint);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMini)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaxi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.PanelDatosAcceso.ResumeLayout(false);
            this.PanelDatosAcceso.PerformLayout();
            this.PanelDatosUsuario.ResumeLayout(false);
            this.PanelDatosUsuario.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblProductManager;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox btnRestore;
        private System.Windows.Forms.PictureBox btnMini;
        private System.Windows.Forms.PictureBox btnMaxi;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.ComboBox cbRoll;
        private System.Windows.Forms.Label lblRoll;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Panel PanelDatosAcceso;
        private System.Windows.Forms.Label lblDatosAcceso;
        private System.Windows.Forms.Label lblPassword;
        public System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblUserName;
        public System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Panel PanelDatosUsuario;
        private System.Windows.Forms.Label lblDatosUsuario;
        private System.Windows.Forms.Label lblNombre;
        public System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblCorreoUsuario;
        public System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label lblTelUsuario;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindUpdate;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtID;
    }
}