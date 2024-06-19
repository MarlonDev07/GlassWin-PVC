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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAccess = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.btnViewPassWord = new System.Windows.Forms.PictureBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.pbUserName = new System.Windows.Forms.PictureBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewPassWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccess
            // 
            this.btnAccess.BackColor = System.Drawing.Color.DarkGreen;
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
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassWord.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtPassWord.Location = new System.Drawing.Point(431, 158);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(222, 26);
            this.txtPassWord.TabIndex = 6;
            this.txtPassWord.Text = "Ingrese la Contraseña";
            this.toolTip1.SetToolTip(this.txtPassWord, "Ingrese la Contraseña");
            this.txtPassWord.Enter += new System.EventHandler(this.txtPassWord_Enter);
            this.txtPassWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.txtPassWord.Leave += new System.EventHandler(this.txtPassWord_Leave);
            // 
            // btnViewPassWord
            // 
            this.btnViewPassWord.BackColor = System.Drawing.Color.White;
            this.btnViewPassWord.Image = ((System.Drawing.Image)(resources.GetObject("btnViewPassWord.Image")));
            this.btnViewPassWord.Location = new System.Drawing.Point(626, 161);
            this.btnViewPassWord.Name = "btnViewPassWord";
            this.btnViewPassWord.Size = new System.Drawing.Size(26, 21);
            this.btnViewPassWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnViewPassWord.TabIndex = 10;
            this.btnViewPassWord.TabStop = false;
            this.toolTip1.SetToolTip(this.btnViewPassWord, "Ver Contraseña");
            this.btnViewPassWord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnViewPassWord_MouseDown);
            this.btnViewPassWord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnViewPassWord_MouseUp);
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.Location = new System.Drawing.Point(33, 30);
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
            this.lblUserName.Font = new System.Drawing.Font("Imprint MT Shadow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblUserName.Location = new System.Drawing.Point(431, 130);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(104, 25);
            this.lblUserName.TabIndex = 11;
            this.lblUserName.Text = "PassWord";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Imprint MT Shadow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(431, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "UserName:";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnAccess;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(686, 253);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.btnViewPassWord);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnAccess);
            this.Controls.Add(this.pbPassword);
            this.Controls.Add(this.pbUserName);
            this.Controls.Add(this.Logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BarraSuperior_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BarraSuperior_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.btnViewPassWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.PictureBox pbUserName;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.Button btnAccess;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.PictureBox btnViewPassWord;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label1;
    }
}