namespace Precentacion.User.Accounts
{
    partial class frmManagerCxC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagerCxC));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabClient = new System.Windows.Forms.TabPage();
            this.txtBusquedaNombre2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBusquedaNombre = new System.Windows.Forms.TextBox();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.contextMenuStripClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verCuentasPorCobrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabCxC = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnnewCxC = new System.Windows.Forms.Button();
            this.txtPendiente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.Paneldgv = new System.Windows.Forms.Panel();
            this.dgvCxC = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abonarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarCuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelDataClient = new System.Windows.Forms.Panel();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.TabControl.SuspendLayout();
            this.tabClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            this.contextMenuStripClient.SuspendLayout();
            this.tabCxC.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Paneldgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCxC)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.PanelDataClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabClient);
            this.TabControl.Controls.Add(this.tabCxC);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.ImageList = this.imageList;
            this.TabControl.Location = new System.Drawing.Point(4, 98);
            this.TabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1231, 903);
            this.TabControl.TabIndex = 0;
            this.TabControl.Tag = "";
            // 
            // tabClient
            // 
            this.tabClient.Controls.Add(this.button1);
            this.tabClient.Controls.Add(this.txtBusquedaNombre2);
            this.tabClient.Controls.Add(this.label11);
            this.tabClient.Controls.Add(this.txtBusquedaNombre);
            this.tabClient.Controls.Add(this.dgvClient);
            this.tabClient.ImageIndex = 0;
            this.tabClient.Location = new System.Drawing.Point(4, 39);
            this.tabClient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabClient.Name = "tabClient";
            this.tabClient.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabClient.Size = new System.Drawing.Size(1223, 860);
            this.tabClient.TabIndex = 0;
            this.tabClient.Text = "Clientes";
            this.tabClient.ToolTipText = "Clientes con Cuentas por Cobrar";
            this.tabClient.UseVisualStyleBackColor = true;
            // 
            // txtBusquedaNombre2
            // 
            this.txtBusquedaNombre2.Location = new System.Drawing.Point(45, 52);
            this.txtBusquedaNombre2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBusquedaNombre2.Name = "txtBusquedaNombre2";
            this.txtBusquedaNombre2.Size = new System.Drawing.Size(288, 26);
            this.txtBusquedaNombre2.TabIndex = 183;
            this.txtBusquedaNombre2.TextChanged += new System.EventHandler(this.txtBusquedaNombre2_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 20);
            this.label11.TabIndex = 182;
            this.label11.Text = "Busqueda por Nombre";
            // 
            // txtBusquedaNombre
            // 
            this.txtBusquedaNombre.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusquedaNombre.Location = new System.Drawing.Point(876, 28);
            this.txtBusquedaNombre.Multiline = true;
            this.txtBusquedaNombre.Name = "txtBusquedaNombre";
            this.txtBusquedaNombre.Size = new System.Drawing.Size(288, 36);
            this.txtBusquedaNombre.TabIndex = 181;
            this.txtBusquedaNombre.Visible = false;
            // 
            // dgvClient
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Mongolian Baiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.ContextMenuStrip = this.contextMenuStripClient;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClient.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvClient.EnableHeadersVisualStyles = false;
            this.dgvClient.Location = new System.Drawing.Point(45, 92);
            this.dgvClient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.ReadOnly = true;
            this.dgvClient.RowHeadersWidth = 62;
            this.dgvClient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClient.Size = new System.Drawing.Size(1150, 692);
            this.dgvClient.TabIndex = 180;
            // 
            // contextMenuStripClient
            // 
            this.contextMenuStripClient.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verCuentasPorCobrarToolStripMenuItem});
            this.contextMenuStripClient.Name = "contextMenuStrip";
            this.contextMenuStripClient.Size = new System.Drawing.Size(270, 36);
            // 
            // verCuentasPorCobrarToolStripMenuItem
            // 
            this.verCuentasPorCobrarToolStripMenuItem.Name = "verCuentasPorCobrarToolStripMenuItem";
            this.verCuentasPorCobrarToolStripMenuItem.Size = new System.Drawing.Size(269, 32);
            this.verCuentasPorCobrarToolStripMenuItem.Text = "Ver Cuentas por Cobrar";
            this.verCuentasPorCobrarToolStripMenuItem.Click += new System.EventHandler(this.verCuentasPorCobrarToolStripMenuItem_Click);
            // 
            // tabCxC
            // 
            this.tabCxC.Controls.Add(this.groupBox1);
            this.tabCxC.Controls.Add(this.Paneldgv);
            this.tabCxC.Controls.Add(this.PanelDataClient);
            this.tabCxC.Controls.Add(this.lblTitle);
            this.tabCxC.ImageIndex = 1;
            this.tabCxC.Location = new System.Drawing.Point(4, 39);
            this.tabCxC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabCxC.Name = "tabCxC";
            this.tabCxC.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabCxC.Size = new System.Drawing.Size(1223, 860);
            this.tabCxC.TabIndex = 1;
            this.tabCxC.Text = "Cuentas x Cobrar ";
            this.tabCxC.ToolTipText = "Cuentas por Cobrar ";
            this.tabCxC.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnnewCxC);
            this.groupBox1.Controls.Add(this.txtPendiente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTotal);
            this.groupBox1.Location = new System.Drawing.Point(4, 675);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1170, 135);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // btnnewCxC
            // 
            this.btnnewCxC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnnewCxC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnnewCxC.BackgroundImage")));
            this.btnnewCxC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnnewCxC.FlatAppearance.BorderSize = 2;
            this.btnnewCxC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnewCxC.Location = new System.Drawing.Point(4, 54);
            this.btnnewCxC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnnewCxC.Name = "btnnewCxC";
            this.btnnewCxC.Size = new System.Drawing.Size(72, 58);
            this.btnnewCxC.TabIndex = 18;
            this.btnnewCxC.UseVisualStyleBackColor = true;
            this.btnnewCxC.Click += new System.EventHandler(this.btnnewCxC_Click);
            // 
            // txtPendiente
            // 
            this.txtPendiente.Location = new System.Drawing.Point(860, 82);
            this.txtPendiente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPendiente.Name = "txtPendiente";
            this.txtPendiente.Size = new System.Drawing.Size(228, 26);
            this.txtPendiente.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(698, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "Saldo Total";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(916, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Saldo Pendiente";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(602, 82);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(217, 26);
            this.txtTotal.TabIndex = 6;
            // 
            // Paneldgv
            // 
            this.Paneldgv.Controls.Add(this.dgvCxC);
            this.Paneldgv.Location = new System.Drawing.Point(4, 128);
            this.Paneldgv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Paneldgv.Name = "Paneldgv";
            this.Paneldgv.Size = new System.Drawing.Size(1170, 538);
            this.Paneldgv.TabIndex = 4;
            // 
            // dgvCxC
            // 
            this.dgvCxC.AllowDrop = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Mongolian Baiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCxC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCxC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCxC.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCxC.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCxC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCxC.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvCxC.EnableHeadersVisualStyles = false;
            this.dgvCxC.Location = new System.Drawing.Point(0, 0);
            this.dgvCxC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvCxC.Name = "dgvCxC";
            this.dgvCxC.RowHeadersWidth = 62;
            this.dgvCxC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCxC.Size = new System.Drawing.Size(1170, 538);
            this.dgvCxC.TabIndex = 181;
            this.dgvCxC.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCxC_CellContentDoubleClick);
            this.dgvCxC.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCxC_CellDoubleClick);
            this.dgvCxC.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCxC_CellEndEdit_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abonarToolStripMenuItem,
            this.eliminarCuentaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(207, 68);
            // 
            // abonarToolStripMenuItem
            // 
            this.abonarToolStripMenuItem.Name = "abonarToolStripMenuItem";
            this.abonarToolStripMenuItem.Size = new System.Drawing.Size(206, 32);
            this.abonarToolStripMenuItem.Text = "Abonar";
            this.abonarToolStripMenuItem.Click += new System.EventHandler(this.abonarToolStripMenuItem_Click);
            // 
            // eliminarCuentaToolStripMenuItem
            // 
            this.eliminarCuentaToolStripMenuItem.Name = "eliminarCuentaToolStripMenuItem";
            this.eliminarCuentaToolStripMenuItem.Size = new System.Drawing.Size(206, 32);
            this.eliminarCuentaToolStripMenuItem.Text = "Eliminar Cuenta";
            this.eliminarCuentaToolStripMenuItem.Click += new System.EventHandler(this.eliminarCuentaToolStripMenuItem_Click);
            // 
            // PanelDataClient
            // 
            this.PanelDataClient.Controls.Add(this.txtCount);
            this.PanelDataClient.Controls.Add(this.pictureBox3);
            this.PanelDataClient.Controls.Add(this.txtName);
            this.PanelDataClient.Controls.Add(this.pictureBox2);
            this.PanelDataClient.Controls.Add(this.txtId);
            this.PanelDataClient.Controls.Add(this.pictureBox1);
            this.PanelDataClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelDataClient.Location = new System.Drawing.Point(4, 39);
            this.PanelDataClient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PanelDataClient.Name = "PanelDataClient";
            this.PanelDataClient.Size = new System.Drawing.Size(1215, 88);
            this.PanelDataClient.TabIndex = 2;
            // 
            // txtCount
            // 
            this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCount.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.Location = new System.Drawing.Point(1011, 20);
            this.txtCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(217, 44);
            this.txtCount.TabIndex = 6;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(906, 6);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(96, 77);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtName.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(492, 20);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(330, 44);
            this.txtName.TabIndex = 4;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(387, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(96, 77);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // txtId
            // 
            this.txtId.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(110, 20);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(196, 44);
            this.txtId.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(4, 5);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1215, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cuentas por Cobrar";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "client.png");
            this.imageList.Images.SetKeyName(1, "finances.png");
            this.imageList.Images.SetKeyName(2, "credit.png");
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip";
            this.contextMenuStrip2.Size = new System.Drawing.Size(144, 36);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(143, 32);
            this.toolStripMenuItem1.Text = "Abonar";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Create.png");
            this.imageList1.Images.SetKeyName(1, "List.png");
            this.imageList1.Images.SetKeyName(2, "Update.png");
            this.imageList1.Images.SetKeyName(3, "Agregar.png");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(390, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(254, 41);
            this.button1.TabIndex = 184;
            this.button1.Text = "Ver Cuentas por Cobrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmManagerCxC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 1006);
            this.Controls.Add(this.TabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmManagerCxC";
            this.Padding = new System.Windows.Forms.Padding(4, 98, 4, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuentas por Cobrar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManagerCxC_FormClosing);
            this.TabControl.ResumeLayout(false);
            this.tabClient.ResumeLayout(false);
            this.tabClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            this.contextMenuStripClient.ResumeLayout(false);
            this.tabCxC.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Paneldgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCxC)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.PanelDataClient.ResumeLayout(false);
            this.PanelDataClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabClient;
        private System.Windows.Forms.TabPage tabCxC;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel PanelDataClient;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Panel Paneldgv;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripClient;
        private System.Windows.Forms.ToolStripMenuItem verCuentasPorCobrarToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPendiente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem abonarToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button btnnewCxC;
        private System.Windows.Forms.ToolStripMenuItem eliminarCuentaToolStripMenuItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBusquedaNombre;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBusquedaNombre2;
        public System.Windows.Forms.DataGridView dgvCxC;
        private System.Windows.Forms.Button button1;
    }
}