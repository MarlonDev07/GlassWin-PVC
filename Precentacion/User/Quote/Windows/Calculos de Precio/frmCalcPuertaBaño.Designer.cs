namespace Precentacion.User.Quote.Windows.Calculos_de_Precio
{
    partial class frmCalcPuertaBaño
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbSupplier = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDesglose = new System.Windows.Forms.Button();
            this.PanelMedidas = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbColorLamina = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLaminaPlastica = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAnchoPanel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblAncho = new System.Windows.Forms.Label();
            this.txtAncho = new System.Windows.Forms.TextBox();
            this.cbVidrio = new System.Windows.Forms.ComboBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.lblVidrio = new System.Windows.Forms.Label();
            this.lblAlto = new System.Windows.Forms.Label();
            this.txtAlto = new System.Windows.Forms.TextBox();
            this.picPuertaBaño = new System.Windows.Forms.PictureBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelDesglose = new System.Windows.Forms.Panel();
            this.dgvAccesorios = new System.Windows.Forms.DataGridView();
            this.dgvVidrio = new System.Windows.Forms.DataGridView();
            this.btnOcultar = new System.Windows.Forms.Button();
            this.dgvAluminio = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.PanelMedidas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPuertaBaño)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panelDesglose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccesorios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVidrio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAluminio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.txtCantidad);
            this.panel2.Controls.Add(this.lblCantidad);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.txtTotalPrice);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.cbSupplier);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnDesglose);
            this.panel2.Location = new System.Drawing.Point(673, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(228, 416);
            this.panel2.TabIndex = 64;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(27, 313);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(167, 18);
            this.label14.TabIndex = 30;
            this.label14.Text = "Total";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.BackColor = System.Drawing.Color.White;
            this.txtTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrice.Location = new System.Drawing.Point(27, 339);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(167, 29);
            this.txtTotalPrice.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(228, 25);
            this.label12.TabIndex = 27;
            this.label12.Text = "Precios";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSupplier
            // 
            this.cbSupplier.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupplier.FormattingEnabled = true;
            this.cbSupplier.Items.AddRange(new object[] {
            "Extralum",
            "Macopa"});
            this.cbSupplier.Location = new System.Drawing.Point(30, 49);
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.Size = new System.Drawing.Size(167, 21);
            this.cbSupplier.TabIndex = 26;
            this.cbSupplier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Proveedor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDesglose
            // 
            this.btnDesglose.BackColor = System.Drawing.Color.Black;
            this.btnDesglose.FlatAppearance.BorderSize = 0;
            this.btnDesglose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDesglose.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesglose.ForeColor = System.Drawing.Color.White;
            this.btnDesglose.Location = new System.Drawing.Point(37, 382);
            this.btnDesglose.Name = "btnDesglose";
            this.btnDesglose.Size = new System.Drawing.Size(151, 30);
            this.btnDesglose.TabIndex = 13;
            this.btnDesglose.Text = "Ver Desglose";
            this.btnDesglose.UseVisualStyleBackColor = false;
            this.btnDesglose.Click += new System.EventHandler(this.btnDesglose_Click);
            // 
            // PanelMedidas
            // 
            this.PanelMedidas.BackColor = System.Drawing.Color.DarkGray;
            this.PanelMedidas.Controls.Add(this.btnGuardar);
            this.PanelMedidas.Controls.Add(this.label4);
            this.PanelMedidas.Controls.Add(this.cbColorLamina);
            this.PanelMedidas.Controls.Add(this.label2);
            this.PanelMedidas.Controls.Add(this.cbLaminaPlastica);
            this.PanelMedidas.Controls.Add(this.label1);
            this.PanelMedidas.Controls.Add(this.txtAnchoPanel);
            this.PanelMedidas.Controls.Add(this.label9);
            this.PanelMedidas.Controls.Add(this.btnCargar);
            this.PanelMedidas.Controls.Add(this.lblColor);
            this.PanelMedidas.Controls.Add(this.lblAncho);
            this.PanelMedidas.Controls.Add(this.txtAncho);
            this.PanelMedidas.Controls.Add(this.cbVidrio);
            this.PanelMedidas.Controls.Add(this.cbColor);
            this.PanelMedidas.Controls.Add(this.lblVidrio);
            this.PanelMedidas.Controls.Add(this.lblAlto);
            this.PanelMedidas.Controls.Add(this.txtAlto);
            this.PanelMedidas.Location = new System.Drawing.Point(408, 67);
            this.PanelMedidas.Name = "PanelMedidas";
            this.PanelMedidas.Size = new System.Drawing.Size(244, 416);
            this.PanelMedidas.TabIndex = 63;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Black;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(128, 382);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 30);
            this.btnGuardar.TabIndex = 48;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(26, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 20);
            this.label4.TabIndex = 47;
            this.label4.Text = "Color Lamina Plastica";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbColorLamina
            // 
            this.cbColorLamina.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbColorLamina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorLamina.FormattingEnabled = true;
            this.cbColorLamina.Items.AddRange(new object[] {
            "Blanco",
            "Whisky",
            "Celeste",
            "Verde"});
            this.cbColorLamina.Location = new System.Drawing.Point(26, 341);
            this.cbColorLamina.Name = "cbColorLamina";
            this.cbColorLamina.Size = new System.Drawing.Size(192, 21);
            this.cbColorLamina.TabIndex = 46;
            this.cbColorLamina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox_KeyPress);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(26, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "Lamina Plastica";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbLaminaPlastica
            // 
            this.cbLaminaPlastica.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbLaminaPlastica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLaminaPlastica.FormattingEnabled = true;
            this.cbLaminaPlastica.Items.AddRange(new object[] {
            "Cañaberal",
            "Florentino",
            "Delfin"});
            this.cbLaminaPlastica.Location = new System.Drawing.Point(26, 292);
            this.cbLaminaPlastica.Name = "cbLaminaPlastica";
            this.cbLaminaPlastica.Size = new System.Drawing.Size(192, 21);
            this.cbLaminaPlastica.TabIndex = 44;
            this.cbLaminaPlastica.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(30, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "Ancho Panel";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAnchoPanel
            // 
            this.txtAnchoPanel.BackColor = System.Drawing.Color.White;
            this.txtAnchoPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnchoPanel.Location = new System.Drawing.Point(26, 99);
            this.txtAnchoPanel.Name = "txtAnchoPanel";
            this.txtAnchoPanel.Size = new System.Drawing.Size(192, 23);
            this.txtAnchoPanel.TabIndex = 43;
            this.txtAnchoPanel.TextChanged += new System.EventHandler(this.PuntoDecimal_TextChanged);
            this.txtAnchoPanel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressTextBox_KeyPress);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(244, 25);
            this.label9.TabIndex = 41;
            this.label9.Text = "Medidas";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.Black;
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargar.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.ForeColor = System.Drawing.Color.White;
            this.btnCargar.Location = new System.Drawing.Point(26, 383);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(90, 30);
            this.btnCargar.TabIndex = 26;
            this.btnCargar.Text = "Calcular";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // lblColor
            // 
            this.lblColor.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.ForeColor = System.Drawing.Color.Black;
            this.lblColor.Location = new System.Drawing.Point(22, 175);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(196, 20);
            this.lblColor.TabIndex = 12;
            this.lblColor.Text = "Color Aluminio";
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAncho
            // 
            this.lblAncho.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAncho.ForeColor = System.Drawing.Color.Black;
            this.lblAncho.Location = new System.Drawing.Point(26, 25);
            this.lblAncho.Name = "lblAncho";
            this.lblAncho.Size = new System.Drawing.Size(192, 20);
            this.lblAncho.TabIndex = 39;
            this.lblAncho.Text = "Ancho Total";
            this.lblAncho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAncho
            // 
            this.txtAncho.BackColor = System.Drawing.Color.White;
            this.txtAncho.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAncho.Location = new System.Drawing.Point(26, 48);
            this.txtAncho.Name = "txtAncho";
            this.txtAncho.Size = new System.Drawing.Size(192, 23);
            this.txtAncho.TabIndex = 40;
            this.txtAncho.TextChanged += new System.EventHandler(this.PuntoDecimal_TextChanged);
            this.txtAncho.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressTextBox_KeyPress);
            // 
            // cbVidrio
            // 
            this.cbVidrio.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbVidrio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVidrio.FormattingEnabled = true;
            this.cbVidrio.Location = new System.Drawing.Point(26, 245);
            this.cbVidrio.Name = "cbVidrio";
            this.cbVidrio.Size = new System.Drawing.Size(192, 21);
            this.cbVidrio.TabIndex = 24;
            this.cbVidrio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox_KeyPress);
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
            this.cbColor.Location = new System.Drawing.Point(26, 198);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(192, 21);
            this.cbColor.TabIndex = 11;
            this.cbColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox_KeyPress);
            // 
            // lblVidrio
            // 
            this.lblVidrio.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVidrio.ForeColor = System.Drawing.Color.Black;
            this.lblVidrio.Location = new System.Drawing.Point(22, 222);
            this.lblVidrio.Name = "lblVidrio";
            this.lblVidrio.Size = new System.Drawing.Size(196, 20);
            this.lblVidrio.TabIndex = 17;
            this.lblVidrio.Text = "Vidrio";
            this.lblVidrio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlto
            // 
            this.lblAlto.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlto.ForeColor = System.Drawing.Color.Black;
            this.lblAlto.Location = new System.Drawing.Point(26, 126);
            this.lblAlto.Name = "lblAlto";
            this.lblAlto.Size = new System.Drawing.Size(192, 20);
            this.lblAlto.TabIndex = 37;
            this.lblAlto.Text = "Alto";
            this.lblAlto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAlto
            // 
            this.txtAlto.BackColor = System.Drawing.Color.White;
            this.txtAlto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlto.Location = new System.Drawing.Point(26, 149);
            this.txtAlto.Name = "txtAlto";
            this.txtAlto.Size = new System.Drawing.Size(192, 23);
            this.txtAlto.TabIndex = 38;
            this.txtAlto.TextChanged += new System.EventHandler(this.PuntoDecimal_TextChanged);
            this.txtAlto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressTextBox_KeyPress);
            // 
            // picPuertaBaño
            // 
            this.picPuertaBaño.Location = new System.Drawing.Point(6, 119);
            this.picPuertaBaño.Name = "picPuertaBaño";
            this.picPuertaBaño.Size = new System.Drawing.Size(396, 361);
            this.picPuertaBaño.TabIndex = 62;
            this.picPuertaBaño.TabStop = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // panelDesglose
            // 
            this.panelDesglose.Controls.Add(this.dgvAccesorios);
            this.panelDesglose.Controls.Add(this.dgvVidrio);
            this.panelDesglose.Controls.Add(this.btnOcultar);
            this.panelDesglose.Controls.Add(this.dgvAluminio);
            this.panelDesglose.Controls.Add(this.label5);
            this.panelDesglose.Location = new System.Drawing.Point(-2, 64);
            this.panelDesglose.Name = "panelDesglose";
            this.panelDesglose.Size = new System.Drawing.Size(903, 474);
            this.panelDesglose.TabIndex = 65;
            this.panelDesglose.Visible = false;
            // 
            // dgvAccesorios
            // 
            this.dgvAccesorios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccesorios.Location = new System.Drawing.Point(6, 276);
            this.dgvAccesorios.Name = "dgvAccesorios";
            this.dgvAccesorios.Size = new System.Drawing.Size(890, 150);
            this.dgvAccesorios.TabIndex = 46;
            // 
            // dgvVidrio
            // 
            this.dgvVidrio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVidrio.Location = new System.Drawing.Point(5, 201);
            this.dgvVidrio.Name = "dgvVidrio";
            this.dgvVidrio.Size = new System.Drawing.Size(891, 68);
            this.dgvVidrio.TabIndex = 45;
            // 
            // btnOcultar
            // 
            this.btnOcultar.BackColor = System.Drawing.Color.Black;
            this.btnOcultar.FlatAppearance.BorderSize = 0;
            this.btnOcultar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOcultar.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOcultar.ForeColor = System.Drawing.Color.White;
            this.btnOcultar.Location = new System.Drawing.Point(379, 441);
            this.btnOcultar.Name = "btnOcultar";
            this.btnOcultar.Size = new System.Drawing.Size(151, 30);
            this.btnOcultar.TabIndex = 44;
            this.btnOcultar.Text = "Ocultar";
            this.btnOcultar.UseVisualStyleBackColor = false;
            this.btnOcultar.Click += new System.EventHandler(this.btnOcultar_Click);
            // 
            // dgvAluminio
            // 
            this.dgvAluminio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAluminio.Location = new System.Drawing.Point(5, 44);
            this.dgvAluminio.Name = "dgvAluminio";
            this.dgvAluminio.Size = new System.Drawing.Size(891, 150);
            this.dgvAluminio.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(903, 38);
            this.label5.TabIndex = 42;
            this.label5.Text = "Desglose";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(30, 99);
            this.txtCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(167, 20);
            this.txtCantidad.TabIndex = 82;
            this.txtCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantidad.ValueChanged += new System.EventHandler(this.txtCantidad_ValueChanged);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.ForeColor = System.Drawing.Color.Black;
            this.lblCantidad.Location = new System.Drawing.Point(71, 76);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(67, 20);
            this.lblCantidad.TabIndex = 81;
            this.lblCantidad.Text = "Cantidad";
            // 
            // frmCalcPuertaBaño
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 544);
            this.Controls.Add(this.panelDesglose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelMedidas);
            this.Controls.Add(this.picPuertaBaño);
            this.Name = "frmCalcPuertaBaño";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cotizador Puerta Baño";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PanelMedidas.ResumeLayout(false);
            this.PanelMedidas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPuertaBaño)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panelDesglose.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccesorios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVidrio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAluminio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.ComboBox cbSupplier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDesglose;
        private System.Windows.Forms.Panel PanelMedidas;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtAnchoPanel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblAncho;
        public System.Windows.Forms.TextBox txtAncho;
        public System.Windows.Forms.ComboBox cbVidrio;
        public System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label lblVidrio;
        private System.Windows.Forms.Label lblAlto;
        public System.Windows.Forms.TextBox txtAlto;
        private System.Windows.Forms.PictureBox picPuertaBaño;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cbLaminaPlastica;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cbColorLamina;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel panelDesglose;
        private System.Windows.Forms.DataGridView dgvAluminio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOcultar;
        private System.Windows.Forms.DataGridView dgvVidrio;
        private System.Windows.Forms.DataGridView dgvAccesorios;
        private System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.Label lblCantidad;
    }
}