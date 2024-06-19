namespace Precentacion.User.Quote.Prefabricado
{
    partial class frmPrefabricado
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
            this.btnCargarImagen = new System.Windows.Forms.Button();
            this.pbPrefabricado = new System.Windows.Forms.PictureBox();
            this.dgvPrefabricado = new System.Windows.Forms.DataGridView();
            this.lblAlto = new System.Windows.Forms.Label();
            this.lblAncho = new System.Windows.Forms.Label();
            this.txtAlto = new System.Windows.Forms.TextBox();
            this.txtAncho = new System.Windows.Forms.TextBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrefabricado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrefabricado)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCargarImagen
            // 
            this.btnCargarImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarImagen.Location = new System.Drawing.Point(24, 104);
            this.btnCargarImagen.Name = "btnCargarImagen";
            this.btnCargarImagen.Size = new System.Drawing.Size(225, 31);
            this.btnCargarImagen.TabIndex = 0;
            this.btnCargarImagen.Text = "Cargar Imagen";
            this.btnCargarImagen.UseVisualStyleBackColor = true;
            this.btnCargarImagen.Click += new System.EventHandler(this.btnCargarImagen_Click);
            // 
            // pbPrefabricado
            // 
            this.pbPrefabricado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPrefabricado.Location = new System.Drawing.Point(6, 141);
            this.pbPrefabricado.Name = "pbPrefabricado";
            this.pbPrefabricado.Size = new System.Drawing.Size(272, 263);
            this.pbPrefabricado.TabIndex = 1;
            this.pbPrefabricado.TabStop = false;
            // 
            // dgvPrefabricado
            // 
            this.dgvPrefabricado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrefabricado.Location = new System.Drawing.Point(284, 141);
            this.dgvPrefabricado.Name = "dgvPrefabricado";
            this.dgvPrefabricado.Size = new System.Drawing.Size(778, 231);
            this.dgvPrefabricado.TabIndex = 2;
            this.dgvPrefabricado.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrefabricado_CellValueChanged);
            this.dgvPrefabricado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPrefabricado_KeyDown);
            // 
            // lblAlto
            // 
            this.lblAlto.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlto.Location = new System.Drawing.Point(616, 84);
            this.lblAlto.Name = "lblAlto";
            this.lblAlto.Size = new System.Drawing.Size(100, 23);
            this.lblAlto.TabIndex = 3;
            this.lblAlto.Text = "Alto";
            this.lblAlto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAncho
            // 
            this.lblAncho.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAncho.Location = new System.Drawing.Point(284, 84);
            this.lblAncho.Name = "lblAncho";
            this.lblAncho.Size = new System.Drawing.Size(100, 23);
            this.lblAncho.TabIndex = 4;
            this.lblAncho.Text = "Ancho";
            this.lblAncho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAlto
            // 
            this.txtAlto.Location = new System.Drawing.Point(616, 115);
            this.txtAlto.Name = "txtAlto";
            this.txtAlto.Size = new System.Drawing.Size(100, 20);
            this.txtAlto.TabIndex = 7;
            this.txtAlto.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.txtAlto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlto_KeyPress);
            // 
            // txtAncho
            // 
            this.txtAncho.Location = new System.Drawing.Point(284, 115);
            this.txtAncho.Name = "txtAncho";
            this.txtAncho.Size = new System.Drawing.Size(100, 20);
            this.txtAncho.TabIndex = 8;
            this.txtAncho.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.txtAncho.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAncho_KeyPress);
            // 
            // btnCargar
            // 
            this.btnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.Location = new System.Drawing.Point(946, 84);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(116, 51);
            this.btnCargar.TabIndex = 9;
            this.btnCargar.Text = "Lista de Articulos";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(284, 373);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(225, 31);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar Prefabricados";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(962, 379);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 12;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(901, 375);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(55, 29);
            this.lblTotal.TabIndex = 11;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(722, 380);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(149, 20);
            this.txtNombre.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(528, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nombre Prefabricado";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPrefabricado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 450);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.txtAncho);
            this.Controls.Add(this.txtAlto);
            this.Controls.Add(this.lblAncho);
            this.Controls.Add(this.lblAlto);
            this.Controls.Add(this.dgvPrefabricado);
            this.Controls.Add(this.pbPrefabricado);
            this.Controls.Add(this.btnCargarImagen);
            this.Name = "frmPrefabricado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CMB Artículos";
            ((System.ComponentModel.ISupportInitialize)(this.pbPrefabricado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrefabricado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCargarImagen;
        private System.Windows.Forms.PictureBox pbPrefabricado;
        private System.Windows.Forms.DataGridView dgvPrefabricado;
        private System.Windows.Forms.Label lblAlto;
        private System.Windows.Forms.Label lblAncho;
        private System.Windows.Forms.TextBox txtAlto;
        private System.Windows.Forms.TextBox txtAncho;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
    }
}