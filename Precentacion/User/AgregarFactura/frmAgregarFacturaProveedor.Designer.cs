namespace Precentacion.User.AgregarFactura
{
    partial class frmAgregarFacturaProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgregarFacturaProveedor));
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabPageLista = new System.Windows.Forms.TabPage();
            this.txtTotalAbonar = new System.Windows.Forms.TextBox();
            this.btnFactCanceladas = new System.Windows.Forms.Button();
            this.btnCancelBuscar = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAbonarLista = new System.Windows.Forms.Button();
            this.btnAbonar = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.cbBuscarProveedor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abonarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageConsulta = new System.Windows.Forms.TabPage();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.dtpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.txtNumFactura = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProyecto = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlPrincipal.SuspendLayout();
            this.tabPageLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPageConsulta.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Controls.Add(this.tabPageLista);
            this.tabControlPrincipal.Controls.Add(this.tabPageConsulta);
            this.tabControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipal.Location = new System.Drawing.Point(3, 64);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(684, 439);
            this.tabControlPrincipal.TabIndex = 0;
            // 
            // tabPageLista
            // 
            this.tabPageLista.Controls.Add(this.txtTotalAbonar);
            this.tabPageLista.Controls.Add(this.btnFactCanceladas);
            this.tabPageLista.Controls.Add(this.btnCancelBuscar);
            this.tabPageLista.Controls.Add(this.btnCancel);
            this.tabPageLista.Controls.Add(this.btnAbonarLista);
            this.tabPageLista.Controls.Add(this.btnAbonar);
            this.tabPageLista.Controls.Add(this.txtTotal);
            this.tabPageLista.Controls.Add(this.lblMontoTotal);
            this.tabPageLista.Controls.Add(this.cbBuscarProveedor);
            this.tabPageLista.Controls.Add(this.label8);
            this.tabPageLista.Controls.Add(this.txtBuscar);
            this.tabPageLista.Controls.Add(this.label7);
            this.tabPageLista.Controls.Add(this.dgvFacturas);
            this.tabPageLista.Location = new System.Drawing.Point(4, 22);
            this.tabPageLista.Name = "tabPageLista";
            this.tabPageLista.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLista.Size = new System.Drawing.Size(676, 413);
            this.tabPageLista.TabIndex = 0;
            this.tabPageLista.Text = "Facturas";
            this.tabPageLista.UseVisualStyleBackColor = true;
            // 
            // txtTotalAbonar
            // 
            this.txtTotalAbonar.Enabled = false;
            this.txtTotalAbonar.Location = new System.Drawing.Point(339, 383);
            this.txtTotalAbonar.Name = "txtTotalAbonar";
            this.txtTotalAbonar.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAbonar.TabIndex = 15;
            this.txtTotalAbonar.Visible = false;
            // 
            // btnFactCanceladas
            // 
            this.btnFactCanceladas.Location = new System.Drawing.Point(572, 5);
            this.btnFactCanceladas.Name = "btnFactCanceladas";
            this.btnFactCanceladas.Size = new System.Drawing.Size(98, 23);
            this.btnFactCanceladas.TabIndex = 13;
            this.btnFactCanceladas.Text = "Fact.Canceladas";
            this.btnFactCanceladas.UseVisualStyleBackColor = true;
            this.btnFactCanceladas.Click += new System.EventHandler(this.btnFactCanceladas_Click);
            // 
            // btnCancelBuscar
            // 
            this.btnCancelBuscar.Location = new System.Drawing.Point(409, 6);
            this.btnCancelBuscar.Name = "btnCancelBuscar";
            this.btnCancelBuscar.Size = new System.Drawing.Size(119, 23);
            this.btnCancelBuscar.TabIndex = 12;
            this.btnCancelBuscar.Text = "Cancelar Busqueda";
            this.btnCancelBuscar.UseVisualStyleBackColor = true;
            this.btnCancelBuscar.Click += new System.EventHandler(this.btnCancelBuscar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 381);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAbonarLista
            // 
            this.btnAbonarLista.Location = new System.Drawing.Point(110, 381);
            this.btnAbonarLista.Name = "btnAbonarLista";
            this.btnAbonarLista.Size = new System.Drawing.Size(98, 23);
            this.btnAbonarLista.TabIndex = 10;
            this.btnAbonarLista.Text = "Abonar";
            this.btnAbonarLista.UseVisualStyleBackColor = true;
            this.btnAbonarLista.Visible = false;
            this.btnAbonarLista.Click += new System.EventHandler(this.btnAbonarLista_Click);
            // 
            // btnAbonar
            // 
            this.btnAbonar.Location = new System.Drawing.Point(6, 381);
            this.btnAbonar.Name = "btnAbonar";
            this.btnAbonar.Size = new System.Drawing.Size(98, 23);
            this.btnAbonar.TabIndex = 9;
            this.btnAbonar.Text = "Abonar Varias";
            this.btnAbonar.UseVisualStyleBackColor = true;
            this.btnAbonar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(572, 383);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 8;
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblMontoTotal.Font = new System.Drawing.Font("Imprint MT Shadow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(484, 379);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(82, 25);
            this.lblMontoTotal.TabIndex = 7;
            this.lblMontoTotal.Text = "Monto Total";
            this.lblMontoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbBuscarProveedor
            // 
            this.cbBuscarProveedor.FormattingEnabled = true;
            this.cbBuscarProveedor.Location = new System.Drawing.Point(98, 6);
            this.cbBuscarProveedor.Name = "cbBuscarProveedor";
            this.cbBuscarProveedor.Size = new System.Drawing.Size(121, 21);
            this.cbBuscarProveedor.TabIndex = 6;
            this.cbBuscarProveedor.SelectedIndexChanged += new System.EventHandler(this.cbBuscarProveedor_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 23);
            this.label8.TabIndex = 5;
            this.label8.Text = "Proveedor:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(300, 7);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(100, 20);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Imprint MT Shadow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(227, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 25);
            this.label7.TabIndex = 1;
            this.label7.Text = "Factura:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvFacturas.Location = new System.Drawing.Point(3, 31);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.Size = new System.Drawing.Size(670, 342);
            this.dgvFacturas.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.abonarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 70);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // abonarToolStripMenuItem
            // 
            this.abonarToolStripMenuItem.Name = "abonarToolStripMenuItem";
            this.abonarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.abonarToolStripMenuItem.Text = "Abonar";
            this.abonarToolStripMenuItem.Click += new System.EventHandler(this.abonarToolStripMenuItem_Click);
            // 
            // tabPageConsulta
            // 
            this.tabPageConsulta.Controls.Add(this.lblTitulo);
            this.tabPageConsulta.Controls.Add(this.panel);
            this.tabPageConsulta.Location = new System.Drawing.Point(4, 22);
            this.tabPageConsulta.Name = "tabPageConsulta";
            this.tabPageConsulta.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsulta.Size = new System.Drawing.Size(676, 413);
            this.tabPageConsulta.TabIndex = 1;
            this.tabPageConsulta.Text = "Mant.Facturas";
            this.tabPageConsulta.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.BackColor = System.Drawing.Color.Black;
            this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitulo.Font = new System.Drawing.Font("Imprint MT Shadow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(3, 3);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(670, 45);
            this.lblTitulo.TabIndex = 13;
            this.lblTitulo.Text = "Registro de Factura de Compra ";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.btnEliminar);
            this.panel.Controls.Add(this.btnEditar);
            this.panel.Controls.Add(this.btnCrear);
            this.panel.Controls.Add(this.dtpFechaCompra);
            this.panel.Controls.Add(this.dtpFechaVencimiento);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.label5);
            this.panel.Controls.Add(this.txtMonto);
            this.panel.Controls.Add(this.txtNumFactura);
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.cbProveedor);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.cbProyecto);
            this.panel.Location = new System.Drawing.Point(6, 51);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(664, 351);
            this.panel.TabIndex = 12;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Location = new System.Drawing.Point(162, 282);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(60, 52);
            this.btnEliminar.TabIndex = 16;
            this.toolTip.SetToolTip(this.btnEliminar, "Eliminar Factura");
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditar.BackgroundImage")));
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Location = new System.Drawing.Point(86, 282);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(58, 52);
            this.btnEditar.TabIndex = 15;
            this.toolTip.SetToolTip(this.btnEditar, "Editar Factura");
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCrear.BackgroundImage")));
            this.btnCrear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Location = new System.Drawing.Point(7, 282);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(58, 52);
            this.btnCrear.TabIndex = 14;
            this.toolTip.SetToolTip(this.btnCrear, "Añadir Factura");
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // dtpFechaCompra
            // 
            this.dtpFechaCompra.Location = new System.Drawing.Point(98, 131);
            this.dtpFechaCompra.Name = "dtpFechaCompra";
            this.dtpFechaCompra.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaCompra.TabIndex = 13;
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(421, 131);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(205, 20);
            this.dtpFechaVencimiento.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Factura:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(304, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Vencimiento:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(458, 70);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(168, 20);
            this.txtMonto.TabIndex = 11;
            // 
            // txtNumFactura
            // 
            this.txtNumFactura.Location = new System.Drawing.Point(98, 70);
            this.txtNumFactura.Name = "txtNumFactura";
            this.txtNumFactura.Size = new System.Drawing.Size(200, 20);
            this.txtNumFactura.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(298, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Monto de la Factura";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Proveedor:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbProveedor
            // 
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Location = new System.Drawing.Point(98, 9);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(528, 21);
            this.cbProveedor.TabIndex = 5;
            this.cbProveedor.SelectedIndexChanged += new System.EventHandler(this.cbProveedor_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Fecha:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Imprint MT Shadow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proyecto:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbProyecto
            // 
            this.cbProyecto.FormattingEnabled = true;
            this.cbProyecto.Location = new System.Drawing.Point(98, 188);
            this.cbProyecto.Name = "cbProyecto";
            this.cbProyecto.Size = new System.Drawing.Size(200, 21);
            this.cbProyecto.TabIndex = 3;
            this.cbProyecto.SelectedIndexChanged += new System.EventHandler(this.cbProyecto_SelectedIndexChanged);
            // 
            // frmAgregarFacturaProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 506);
            this.Controls.Add(this.tabControlPrincipal);
            this.Name = "frmAgregarFacturaProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modulo de Cuentas  por Pagar";
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabPageLista.ResumeLayout(false);
            this.tabPageLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPageConsulta.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPrincipal;
        private System.Windows.Forms.TabPage tabPageLista;
        private System.Windows.Forms.TabPage tabPageConsulta;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.ComboBox cbProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbProyecto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumFactura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.DateTimePicker dtpFechaCompra;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abonarToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbBuscarProveedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Button btnAbonar;
        private System.Windows.Forms.Button btnAbonarLista;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCancelBuscar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFactCanceladas;
        private System.Windows.Forms.TextBox txtTotalAbonar;
    }
}