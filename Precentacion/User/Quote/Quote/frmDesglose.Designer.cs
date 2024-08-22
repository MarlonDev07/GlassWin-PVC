namespace Precentacion.User.Quote.Quote
{
    partial class frmDesglose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesglose));
            this.dgvDesglose = new System.Windows.Forms.DataGridView();
            this.btnGenerarDesglose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbProveedorDesglose = new System.Windows.Forms.ComboBox();
            this.txtTotalC = new System.Windows.Forms.TextBox();
            this.txtTotalSP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvGlass = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesglose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlass)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDesglose
            // 
            this.dgvDesglose.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDesglose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDesglose.Location = new System.Drawing.Point(12, 12);
            this.dgvDesglose.Name = "dgvDesglose";
            this.dgvDesglose.Size = new System.Drawing.Size(729, 402);
            this.dgvDesglose.TabIndex = 1;
            // 
            // btnGenerarDesglose
            // 
            this.btnGenerarDesglose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarDesglose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerarDesglose.BackgroundImage")));
            this.btnGenerarDesglose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerarDesglose.Location = new System.Drawing.Point(881, 420);
            this.btnGenerarDesglose.Name = "btnGenerarDesglose";
            this.btnGenerarDesglose.Size = new System.Drawing.Size(57, 38);
            this.btnGenerarDesglose.TabIndex = 199;
            this.btnGenerarDesglose.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(755, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 25);
            this.label6.TabIndex = 198;
            this.label6.Text = "Proveedor";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbProveedorDesglose
            // 
            this.cbProveedorDesglose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbProveedorDesglose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedorDesglose.FormattingEnabled = true;
            this.cbProveedorDesglose.Location = new System.Drawing.Point(755, 47);
            this.cbProveedorDesglose.Name = "cbProveedorDesglose";
            this.cbProveedorDesglose.Size = new System.Drawing.Size(183, 21);
            this.cbProveedorDesglose.TabIndex = 197;
            this.cbProveedorDesglose.SelectedIndexChanged += new System.EventHandler(this.cbProveedorDesglose_SelectedIndexChanged);
            // 
            // txtTotalC
            // 
            this.txtTotalC.Enabled = false;
            this.txtTotalC.Location = new System.Drawing.Point(414, 438);
            this.txtTotalC.Name = "txtTotalC";
            this.txtTotalC.Size = new System.Drawing.Size(153, 20);
            this.txtTotalC.TabIndex = 204;
            // 
            // txtTotalSP
            // 
            this.txtTotalSP.Enabled = false;
            this.txtTotalSP.Location = new System.Drawing.Point(588, 438);
            this.txtTotalSP.Name = "txtTotalSP";
            this.txtTotalSP.Size = new System.Drawing.Size(153, 20);
            this.txtTotalSP.TabIndex = 203;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(586, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 202;
            this.label4.Text = "Total Precio Venta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 201;
            this.label2.Text = "Total Costo ";
            // 
            // dgvGlass
            // 
            this.dgvGlass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGlass.Location = new System.Drawing.Point(777, 206);
            this.dgvGlass.Name = "dgvGlass";
            this.dgvGlass.Size = new System.Drawing.Size(136, 132);
            this.dgvGlass.TabIndex = 205;
            this.dgvGlass.Visible = false;
            // 
            // frmDesglose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 484);
            this.Controls.Add(this.dgvGlass);
            this.Controls.Add(this.txtTotalC);
            this.Controls.Add(this.txtTotalSP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerarDesglose);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbProveedorDesglose);
            this.Controls.Add(this.dgvDesglose);
            this.Name = "frmDesglose";
            this.Text = "frmDesglose";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesglose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDesglose;
        private System.Windows.Forms.Button btnGenerarDesglose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbProveedorDesglose;
        private System.Windows.Forms.TextBox txtTotalC;
        private System.Windows.Forms.TextBox txtTotalSP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvGlass;
    }
}