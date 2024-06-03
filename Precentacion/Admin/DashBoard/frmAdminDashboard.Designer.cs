namespace Precentacion.Admin
{
    partial class frmAdminDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminDashboard));
            this.PanelOption = new System.Windows.Forms.Panel();
            this.txtUserandCompany = new System.Windows.Forms.Button();
            this.btnSettingsPrice = new System.Windows.Forms.Button();
            this.lblNameUser = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnProductsManager = new System.Windows.Forms.Button();
            this.pbBrand = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PanelOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrand)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelOption
            // 
            this.PanelOption.BackColor = System.Drawing.Color.SteelBlue;
            this.PanelOption.Controls.Add(this.button1);
            this.PanelOption.Controls.Add(this.txtUserandCompany);
            this.PanelOption.Controls.Add(this.btnSettingsPrice);
            this.PanelOption.Controls.Add(this.lblNameUser);
            this.PanelOption.Controls.Add(this.lblWelcome);
            this.PanelOption.Controls.Add(this.btnProductsManager);
            this.PanelOption.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelOption.Location = new System.Drawing.Point(0, 0);
            this.PanelOption.Name = "PanelOption";
            this.PanelOption.Size = new System.Drawing.Size(207, 670);
            this.PanelOption.TabIndex = 1;
            // 
            // txtUserandCompany
            // 
            this.txtUserandCompany.Location = new System.Drawing.Point(13, 362);
            this.txtUserandCompany.Name = "txtUserandCompany";
            this.txtUserandCompany.Size = new System.Drawing.Size(180, 36);
            this.txtUserandCompany.TabIndex = 10;
            this.txtUserandCompany.Text = "Usuarios y Empresas";
            this.txtUserandCompany.UseVisualStyleBackColor = true;
            this.txtUserandCompany.Click += new System.EventHandler(this.txtUserandCompany_Click);
            // 
            // btnSettingsPrice
            // 
            this.btnSettingsPrice.Location = new System.Drawing.Point(13, 302);
            this.btnSettingsPrice.Name = "btnSettingsPrice";
            this.btnSettingsPrice.Size = new System.Drawing.Size(180, 36);
            this.btnSettingsPrice.TabIndex = 9;
            this.btnSettingsPrice.Text = "Ajustes de Precio";
            this.btnSettingsPrice.UseVisualStyleBackColor = true;
            this.btnSettingsPrice.Click += new System.EventHandler(this.btnSettingsPrice_Click);
            // 
            // lblNameUser
            // 
            this.lblNameUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameUser.Location = new System.Drawing.Point(12, 185);
            this.lblNameUser.Name = "lblNameUser";
            this.lblNameUser.Size = new System.Drawing.Size(180, 23);
            this.lblNameUser.TabIndex = 8;
            this.lblNameUser.Text = "AdminName";
            this.lblNameUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(12, 153);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(180, 23);
            this.lblWelcome.TabIndex = 7;
            this.lblWelcome.Text = "Bienvenido";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnProductsManager
            // 
            this.btnProductsManager.Location = new System.Drawing.Point(12, 240);
            this.btnProductsManager.Name = "btnProductsManager";
            this.btnProductsManager.Size = new System.Drawing.Size(180, 36);
            this.btnProductsManager.TabIndex = 1;
            this.btnProductsManager.Text = "Productos";
            this.btnProductsManager.UseVisualStyleBackColor = true;
            this.btnProductsManager.Click += new System.EventHandler(this.btnProductsManager_Click);
            // 
            // pbBrand
            // 
            this.pbBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBrand.Image = ((System.Drawing.Image)(resources.GetObject("pbBrand.Image")));
            this.pbBrand.Location = new System.Drawing.Point(224, 47);
            this.pbBrand.Name = "pbBrand";
            this.pbBrand.Size = new System.Drawing.Size(894, 611);
            this.pbBrand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBrand.TabIndex = 0;
            this.pbBrand.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 622);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 36);
            this.button1.TabIndex = 11;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1130, 670);
            this.Controls.Add(this.PanelOption);
            this.Controls.Add(this.pbBrand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminDashboard";
            this.PanelOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBrand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelOption;
        private System.Windows.Forms.Label lblNameUser;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnProductsManager;
        private System.Windows.Forms.Button btnSettingsPrice;
        private System.Windows.Forms.Button txtUserandCompany;
        private System.Windows.Forms.PictureBox pbBrand;
        private System.Windows.Forms.Button button1;
    }
}