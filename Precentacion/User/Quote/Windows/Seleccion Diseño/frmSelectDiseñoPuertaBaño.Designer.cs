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
            this.panel1.SuspendLayout();
            this.PanelSeleccionDiseño.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.PanelSeleccionDiseño);
            this.panel1.Location = new System.Drawing.Point(30, 114);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1139, 343);
            this.panel1.TabIndex = 41;
            // 
            // PanelSeleccionDiseño
            // 
            this.PanelSeleccionDiseño.AutoScroll = true;
            this.PanelSeleccionDiseño.BackColor = System.Drawing.Color.White;
            this.PanelSeleccionDiseño.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelSeleccionDiseño.Controls.Add(this.FijoMovilMovil);
            this.PanelSeleccionDiseño.Controls.Add(this.btnMovilMovil);
            this.PanelSeleccionDiseño.Location = new System.Drawing.Point(12, 22);
            this.PanelSeleccionDiseño.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PanelSeleccionDiseño.Name = "PanelSeleccionDiseño";
            this.PanelSeleccionDiseño.Size = new System.Drawing.Size(1149, 280);
            this.PanelSeleccionDiseño.TabIndex = 42;
            // 
            // FijoMovilMovil
            // 
            this.FijoMovilMovil.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FijoMovilMovil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.FijoMovilMovil.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FijoMovilMovil.BackgroundImage")));
            this.FijoMovilMovil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FijoMovilMovil.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FijoMovilMovil.Location = new System.Drawing.Point(684, 6);
            this.FijoMovilMovil.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FijoMovilMovil.Name = "FijoMovilMovil";
            this.FijoMovilMovil.Size = new System.Drawing.Size(412, 268);
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
            this.btnMovilMovil.Location = new System.Drawing.Point(26, 6);
            this.btnMovilMovil.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMovilMovil.Name = "btnMovilMovil";
            this.btnMovilMovil.Size = new System.Drawing.Size(303, 268);
            this.btnMovilMovil.TabIndex = 35;
            this.btnMovilMovil.UseVisualStyleBackColor = false;
            this.btnMovilMovil.Click += new System.EventHandler(this.btnMovilMovil_Click);
            // 
            // frmSelectDiseñoPuertaBaño
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 562);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmSelectDiseñoPuertaBaño";
            this.Padding = new System.Windows.Forms.Padding(4, 98, 4, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccion de Diseño";
            this.panel1.ResumeLayout(false);
            this.PanelSeleccionDiseño.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelSeleccionDiseño;
        private System.Windows.Forms.Button FijoMovilMovil;
        private System.Windows.Forms.Button btnMovilMovil;
    }
}