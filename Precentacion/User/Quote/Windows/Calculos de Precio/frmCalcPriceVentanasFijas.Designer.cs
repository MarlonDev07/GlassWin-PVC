namespace Precentacion.User.Quote.Windows
{
    partial class frmCalcPriceVentanasFijas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalcPriceVentanasFijas));
            this.PanelMedidas = new System.Windows.Forms.Panel();
            this.lblAluminio = new System.Windows.Forms.Label();
            this.cbAluminio = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblAncho = new System.Windows.Forms.Label();
            this.txtAncho = new System.Windows.Forms.TextBox();
            this.cbVidrio = new System.Windows.Forms.ComboBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.lblVidrio = new System.Windows.Forms.Label();
            this.lblAlto = new System.Windows.Forms.Label();
            this.txtAlto = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnAgregarCotizacion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbSupplier = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDesglose = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.pbVentana = new System.Windows.Forms.PictureBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvAccesorios = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Vidriodt = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.Aluminiodt = new System.Windows.Forms.DataGridView();
            this.PanelMedidas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVentana)).BeginInit();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccesorios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vidriodt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aluminiodt)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelMedidas
            // 
            this.PanelMedidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelMedidas.BackColor = System.Drawing.Color.DarkGray;
            this.PanelMedidas.Controls.Add(this.lblAluminio);
            this.PanelMedidas.Controls.Add(this.cbAluminio);
            this.PanelMedidas.Controls.Add(this.label9);
            this.PanelMedidas.Controls.Add(this.btnCargar);
            this.PanelMedidas.Controls.Add(this.txtCantidad);
            this.PanelMedidas.Controls.Add(this.lblColor);
            this.PanelMedidas.Controls.Add(this.lblAncho);
            this.PanelMedidas.Controls.Add(this.txtAncho);
            this.PanelMedidas.Controls.Add(this.cbVidrio);
            this.PanelMedidas.Controls.Add(this.cbColor);
            this.PanelMedidas.Controls.Add(this.lblVidrio);
            this.PanelMedidas.Controls.Add(this.lblAlto);
            this.PanelMedidas.Controls.Add(this.txtAlto);
            this.PanelMedidas.Controls.Add(this.lblCantidad);
            this.PanelMedidas.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelMedidas.Location = new System.Drawing.Point(473, 108);
            this.PanelMedidas.Name = "PanelMedidas";
            this.PanelMedidas.Size = new System.Drawing.Size(179, 414);
            this.PanelMedidas.TabIndex = 69;
            // 
            // lblAluminio
            // 
            this.lblAluminio.AutoSize = true;
            this.lblAluminio.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAluminio.ForeColor = System.Drawing.Color.Black;
            this.lblAluminio.Location = new System.Drawing.Point(52, 208);
            this.lblAluminio.Name = "lblAluminio";
            this.lblAluminio.Size = new System.Drawing.Size(64, 20);
            this.lblAluminio.TabIndex = 44;
            this.lblAluminio.Text = "Aluminio";
            // 
            // cbAluminio
            // 
            this.cbAluminio.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbAluminio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAluminio.FormattingEnabled = true;
            this.cbAluminio.Items.AddRange(new object[] {
            "1x2",
            "1 3/4x3",
            "1 3/4x4"});
            this.cbAluminio.Location = new System.Drawing.Point(13, 231);
            this.cbAluminio.Name = "cbAluminio";
            this.cbAluminio.Size = new System.Drawing.Size(150, 23);
            this.cbAluminio.TabIndex = 43;
            this.cbAluminio.Enter += new System.EventHandler(this.cbAluminio_Enter);
            this.cbAluminio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbAluminio_KeyPress);
            this.cbAluminio.Leave += new System.EventHandler(this.cbAluminio_Leave);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 25);
            this.label9.TabIndex = 41;
            this.label9.Text = "Medidas";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.Black;
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargar.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.ForeColor = System.Drawing.Color.White;
            this.btnCargar.Location = new System.Drawing.Point(28, 380);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(121, 30);
            this.btnCargar.TabIndex = 26;
            this.btnCargar.Text = "Calcular";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(15, 349);
            this.txtCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(148, 21);
            this.txtCantidad.TabIndex = 25;
            this.txtCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantidad.ValueChanged += new System.EventHandler(this.txtCantidad_ValueChanged);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.ForeColor = System.Drawing.Color.Black;
            this.lblColor.Location = new System.Drawing.Point(37, 148);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(102, 20);
            this.lblColor.TabIndex = 12;
            this.lblColor.Text = "Color Aluminio";
            // 
            // lblAncho
            // 
            this.lblAncho.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAncho.ForeColor = System.Drawing.Color.Black;
            this.lblAncho.Location = new System.Drawing.Point(64, 33);
            this.lblAncho.Name = "lblAncho";
            this.lblAncho.Size = new System.Drawing.Size(55, 20);
            this.lblAncho.TabIndex = 39;
            this.lblAncho.Text = "Ancho";
            this.lblAncho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAncho
            // 
            this.txtAncho.BackColor = System.Drawing.Color.White;
            this.txtAncho.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAncho.Location = new System.Drawing.Point(15, 56);
            this.txtAncho.Name = "txtAncho";
            this.txtAncho.Size = new System.Drawing.Size(150, 23);
            this.txtAncho.TabIndex = 40;
            this.txtAncho.TextChanged += new System.EventHandler(this.txtAncho_textChanged);
            this.txtAncho.Enter += new System.EventHandler(this.txtAncho_Enter);
            this.txtAncho.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAncho_KeyPress);
            this.txtAncho.Leave += new System.EventHandler(this.txtAncho_Leave);
            // 
            // cbVidrio
            // 
            this.cbVidrio.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbVidrio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVidrio.FormattingEnabled = true;
            this.cbVidrio.Location = new System.Drawing.Point(14, 290);
            this.cbVidrio.Name = "cbVidrio";
            this.cbVidrio.Size = new System.Drawing.Size(150, 23);
            this.cbVidrio.TabIndex = 24;
            this.cbVidrio.Enter += new System.EventHandler(this.cbVidrio_Enter);
            this.cbVidrio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVidrio_KeyPress);
            this.cbVidrio.Leave += new System.EventHandler(this.cbVidrio_Leave);
            // 
            // cbColor
            // 
            this.cbColor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Items.AddRange(new object[] {
            "Natural",
            "Bronce",
            "Bronce Texturisado",
            "Negro",
            "Blanco",
            "Madera",
            "Inox"});
            this.cbColor.Location = new System.Drawing.Point(15, 171);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(150, 23);
            this.cbColor.TabIndex = 11;
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor.Enter += new System.EventHandler(this.cbColor_Enter);
            this.cbColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbColor_KeyPress);
            this.cbColor.Leave += new System.EventHandler(this.cbColor_Leave);
            // 
            // lblVidrio
            // 
            this.lblVidrio.AutoSize = true;
            this.lblVidrio.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVidrio.ForeColor = System.Drawing.Color.Black;
            this.lblVidrio.Location = new System.Drawing.Point(62, 265);
            this.lblVidrio.Name = "lblVidrio";
            this.lblVidrio.Size = new System.Drawing.Size(46, 20);
            this.lblVidrio.TabIndex = 17;
            this.lblVidrio.Text = "Vidrio";
            // 
            // lblAlto
            // 
            this.lblAlto.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlto.ForeColor = System.Drawing.Color.Black;
            this.lblAlto.Location = new System.Drawing.Point(60, 89);
            this.lblAlto.Name = "lblAlto";
            this.lblAlto.Size = new System.Drawing.Size(55, 20);
            this.lblAlto.TabIndex = 37;
            this.lblAlto.Text = "Alto";
            this.lblAlto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAlto
            // 
            this.txtAlto.BackColor = System.Drawing.Color.White;
            this.txtAlto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlto.Location = new System.Drawing.Point(15, 112);
            this.txtAlto.Name = "txtAlto";
            this.txtAlto.Size = new System.Drawing.Size(150, 23);
            this.txtAlto.TabIndex = 38;
            this.txtAlto.TextChanged += new System.EventHandler(this.txtAlto_textChanged);
            this.txtAlto.Enter += new System.EventHandler(this.txtAlto_Enter);
            this.txtAlto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlto_KeyPress);
            this.txtAlto.Leave += new System.EventHandler(this.txtAlto_Leave);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.ForeColor = System.Drawing.Color.Black;
            this.lblCantidad.Location = new System.Drawing.Point(48, 327);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(67, 20);
            this.lblCantidad.TabIndex = 19;
            this.lblCantidad.Text = "Cantidad";
            // 
            // btnAgregarCotizacion
            // 
            this.btnAgregarCotizacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgregarCotizacion.BackColor = System.Drawing.Color.Black;
            this.btnAgregarCotizacion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregarCotizacion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarCotizacion.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCotizacion.ForeColor = System.Drawing.Color.White;
            this.btnAgregarCotizacion.Location = new System.Drawing.Point(32, 464);
            this.btnAgregarCotizacion.Name = "btnAgregarCotizacion";
            this.btnAgregarCotizacion.Size = new System.Drawing.Size(129, 58);
            this.btnAgregarCotizacion.TabIndex = 67;
            this.btnAgregarCotizacion.Text = "Guardar Cotizacion";
            this.btnAgregarCotizacion.UseVisualStyleBackColor = false;
            this.btnAgregarCotizacion.Click += new System.EventHandler(this.btnAgregarCotizacion_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 23);
            this.label1.TabIndex = 65;
            this.label1.Text = "Ubicacion";
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUbicacion.BackColor = System.Drawing.Color.White;
            this.txtUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUbicacion.Location = new System.Drawing.Point(106, 127);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(242, 23);
            this.txtUbicacion.TabIndex = 66;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.Black;
            this.lblDescripcion.Location = new System.Drawing.Point(243, 84);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(105, 23);
            this.lblDescripcion.TabIndex = 63;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // lblDetalle
            // 
            this.lblDetalle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.ForeColor = System.Drawing.Color.Black;
            this.lblDetalle.Location = new System.Drawing.Point(6, 84);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(221, 23);
            this.lblDetalle.TabIndex = 62;
            this.lblDetalle.Text = "Descripcion de la Ventana :";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 25);
            this.label12.TabIndex = 27;
            this.label12.Text = "Precios";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.cbSupplier);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnDesglose);
            this.panel2.Controls.Add(this.lblTotal);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Location = new System.Drawing.Point(673, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 414);
            this.panel2.TabIndex = 70;
            // 
            // cbSupplier
            // 
            this.cbSupplier.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupplier.FormattingEnabled = true;
            this.cbSupplier.Items.AddRange(new object[] {
            "Extralum",
            "Macopa"});
            this.cbSupplier.Location = new System.Drawing.Point(13, 157);
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.Size = new System.Drawing.Size(121, 21);
            this.cbSupplier.TabIndex = 26;
            this.cbSupplier.Enter += new System.EventHandler(this.cbSupplier_Enter);
            this.cbSupplier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbSupplier_KeyPress);
            this.cbSupplier.Leave += new System.EventHandler(this.cbSupplier_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(35, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Proveedor";
            // 
            // btnDesglose
            // 
            this.btnDesglose.BackColor = System.Drawing.Color.Black;
            this.btnDesglose.FlatAppearance.BorderSize = 0;
            this.btnDesglose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDesglose.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesglose.ForeColor = System.Drawing.Color.White;
            this.btnDesglose.Location = new System.Drawing.Point(26, 379);
            this.btnDesglose.Name = "btnDesglose";
            this.btnDesglose.Size = new System.Drawing.Size(100, 30);
            this.btnDesglose.TabIndex = 13;
            this.btnDesglose.Text = "Ver Desglose";
            this.btnDesglose.UseVisualStyleBackColor = false;
            this.btnDesglose.Click += new System.EventHandler(this.btnDesglose_Click_1);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(10, 327);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(120, 20);
            this.lblTotal.TabIndex = 11;
            this.lblTotal.Text = "SubTotal Ventana";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.Location = new System.Drawing.Point(11, 348);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(121, 20);
            this.txtTotal.TabIndex = 12;
            // 
            // pbVentana
            // 
            this.pbVentana.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbVentana.Image = ((System.Drawing.Image)(resources.GetObject("pbVentana.Image")));
            this.pbVentana.Location = new System.Drawing.Point(16, 174);
            this.pbVentana.Name = "pbVentana";
            this.pbVentana.Size = new System.Drawing.Size(332, 272);
            this.pbVentana.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVentana.TabIndex = 64;
            this.pbVentana.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSalir.BackColor = System.Drawing.Color.Black;
            this.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSalir.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(201, 464);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(129, 58);
            this.btnSalir.TabIndex = 71;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panelDetalle
            // 
            this.panelDetalle.Controls.Add(this.label4);
            this.panelDetalle.Controls.Add(this.dgvAccesorios);
            this.panelDetalle.Controls.Add(this.label7);
            this.panelDetalle.Controls.Add(this.label8);
            this.panelDetalle.Controls.Add(this.label10);
            this.panelDetalle.Controls.Add(this.Vidriodt);
            this.panelDetalle.Controls.Add(this.button2);
            this.panelDetalle.Controls.Add(this.Aluminiodt);
            this.panelDetalle.Location = new System.Drawing.Point(6, 67);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Size = new System.Drawing.Size(826, 487);
            this.panelDetalle.TabIndex = 72;
            this.panelDetalle.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 78;
            this.label4.Text = "Accesorios";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvAccesorios
            // 
            this.dgvAccesorios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccesorios.Location = new System.Drawing.Point(90, 175);
            this.dgvAccesorios.Name = "dgvAccesorios";
            this.dgvAccesorios.Size = new System.Drawing.Size(675, 117);
            this.dgvAccesorios.TabIndex = 77;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(16, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 76;
            this.label7.Text = "Vidrio";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(16, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 20);
            this.label8.TabIndex = 75;
            this.label8.Text = "Aluminio";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(826, 25);
            this.label10.TabIndex = 74;
            this.label10.Text = "Detalle";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Vidriodt
            // 
            this.Vidriodt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Vidriodt.Location = new System.Drawing.Point(90, 321);
            this.Vidriodt.Name = "Vidriodt";
            this.Vidriodt.Size = new System.Drawing.Size(675, 89);
            this.Vidriodt.TabIndex = 73;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(350, 433);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 45);
            this.button2.TabIndex = 72;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnAbrirDesglose_Click);
            // 
            // Aluminiodt
            // 
            this.Aluminiodt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Aluminiodt.Location = new System.Drawing.Point(90, 29);
            this.Aluminiodt.Name = "Aluminiodt";
            this.Aluminiodt.Size = new System.Drawing.Size(675, 117);
            this.Aluminiodt.TabIndex = 0;
            // 
            // frmCalcPriceVentanasFijas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 561);
            this.Controls.Add(this.panelDetalle);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.PanelMedidas);
            this.Controls.Add(this.btnAgregarCotizacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUbicacion);
            this.Controls.Add(this.pbVentana);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.panel2);
            this.Name = "frmCalcPriceVentanasFijas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cotizador de Ventanas Fijas";
            this.PanelMedidas.ResumeLayout(false);
            this.PanelMedidas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVentana)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccesorios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vidriodt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aluminiodt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelMedidas;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCargar;
        public System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblAncho;
        public System.Windows.Forms.TextBox txtAncho;
        public System.Windows.Forms.ComboBox cbVidrio;
        public System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label lblVidrio;
        private System.Windows.Forms.Label lblAlto;
        public System.Windows.Forms.TextBox txtAlto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnAgregarCotizacion;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cbSupplier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDesglose;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.ComboBox cbAluminio;
        private System.Windows.Forms.Label lblAluminio;
        private System.Windows.Forms.PictureBox pbVentana;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panelDetalle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView Vidriodt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView Aluminiodt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvAccesorios;
    }
}