namespace Precentacion.User.Quote.Windows.Seleccion_Diseño
{
    partial class frmSelectDiseñoPuertaBaño
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectDiseñoPuertaBaño));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelSeleccionDiseño = new System.Windows.Forms.Panel();
            this.FijoMovilMovil = new System.Windows.Forms.Button();
            this.btnMovilMovil = new System.Windows.Forms.Button();
            this.btnBackSistema = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.PanelSeleccionDiseño.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.PanelSeleccionDiseño);
            this.panel1.Location = new System.Drawing.Point(20, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 223);
            this.panel1.TabIndex = 41;
            // 
            // PanelSeleccionDiseño
            // 
            this.PanelSeleccionDiseño.AutoScroll = true;
            this.PanelSeleccionDiseño.BackColor = System.Drawing.Color.White;
            this.PanelSeleccionDiseño.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelSeleccionDiseño.Controls.Add(this.FijoMovilMovil);
            this.PanelSeleccionDiseño.Controls.Add(this.btnMovilMovil);
            this.PanelSeleccionDiseño.Location = new System.Drawing.Point(8, 14);
            this.PanelSeleccionDiseño.Name = "PanelSeleccionDiseño";
            this.PanelSeleccionDiseño.Size = new System.Drawing.Size(767, 183);
            this.PanelSeleccionDiseño.TabIndex = 42;
            // 
            // FijoMovilMovil
            // 
            this.FijoMovilMovil.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FijoMovilMovil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.FijoMovilMovil.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FijoMovilMovil.BackgroundImage")));
            this.FijoMovilMovil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FijoMovilMovil.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FijoMovilMovil.Location = new System.Drawing.Point(456, 4);
            this.FijoMovilMovil.Name = "FijoMovilMovil";
            this.FijoMovilMovil.Size = new System.Drawing.Size(275, 174);
            this.FijoMovilMovil.TabIndex = 41;
            this.FijoMovilMovil.UseVisualStyleBackColor = false;
            this.FijoMovilMovil.Click += new System.EventHandler(this.FijoMovilMovil_Click);
            // 
            // btnMovilMovil
            // 
            this.btnMovilMovil.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMovilMovil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnMovilMovil.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMovilMovil.BackgroundImage")));
            this.btnMovilMovil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMovilMovil.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovilMovil.Location = new System.Drawing.Point(17, 4);
            this.btnMovilMovil.Name = "btnMovilMovil";
            this.btnMovilMovil.Size = new System.Drawing.Size(202, 174);
            this.btnMovilMovil.TabIndex = 35;
            this.btnMovilMovil.UseVisualStyleBackColor = false;
            this.btnMovilMovil.Click += new System.EventHandler(this.btnMovilMovil_Click);
            // 
            // btnBackSistema
            // 
            this.btnBackSistema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackSistema.BackColor = System.Drawing.Color.Transparent;
            this.btnBackSistema.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBackSistema.BackgroundImage")));
            this.btnBackSistema.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBackSistema.FlatAppearance.BorderSize = 0;
            this.btnBackSistema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackSistema.Location = new System.Drawing.Point(732, 303);
            this.btnBackSistema.Name = "btnBackSistema";
            this.btnBackSistema.Size = new System.Drawing.Size(47, 46);
            this.btnBackSistema.TabIndex = 43;
            this.btnBackSistema.UseVisualStyleBackColor = false;
            this.btnBackSistema.Click += new System.EventHandler(this.btnBackSistema_Click);
            // 
            // frmSelectDiseñoPuertaBaño
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 365);
            this.Controls.Add(this.btnBackSistema);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmSelectDiseñoPuertaBaño";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccion de Diseño";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectDiseñoPuertaBaño_FormClosing);
            this.panel1.ResumeLayout(false);
            this.PanelSeleccionDiseño.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelSeleccionDiseño;
        private System.Windows.Forms.Button FijoMovilMovil;
        private System.Windows.Forms.Button btnMovilMovil;
        private System.Windows.Forms.Button btnBackSistema;
    }
}