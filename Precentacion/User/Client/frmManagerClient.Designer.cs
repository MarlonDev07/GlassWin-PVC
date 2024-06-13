namespace Precentacion.User.Client
{
    partial class frmManagerClient
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagerClient));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabLista = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblBusquedaNombre = new System.Windows.Forms.Label();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cotizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabNew = new System.Windows.Forms.TabPage();
            this.PanelDataNew = new System.Windows.Forms.Panel();
            this.lblAgenda = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnAcceptNew = new System.Windows.Forms.Button();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTitleNew = new System.Windows.Forms.Label();
            this.tabEdit = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtNameEdit = new System.Windows.Forms.TextBox();
            this.txtEmailEdit = new System.Windows.Forms.TextBox();
            this.txtAddressEdit = new System.Windows.Forms.TextBox();
            this.txtPhoneEdit = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTitleEdit = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl.SuspendLayout();
            this.tabLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.tabNew.SuspendLayout();
            this.PanelDataNew.SuspendLayout();
            this.tabEdit.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabLista);
            this.tabControl.Controls.Add(this.tabNew);
            this.tabControl.Controls.Add(this.tabEdit);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.imageList;
            this.tabControl.Location = new System.Drawing.Point(3, 64);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(819, 495);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabLista
            // 
            this.tabLista.Controls.Add(this.textBox1);
            this.tabLista.Controls.Add(this.lblBusquedaNombre);
            this.tabLista.Controls.Add(this.dgvClient);
            this.tabLista.ImageIndex = 2;
            this.tabLista.Location = new System.Drawing.Point(4, 32);
            this.tabLista.Name = "tabLista";
            this.tabLista.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabLista.Size = new System.Drawing.Size(811, 459);
            this.tabLista.TabIndex = 0;
            this.tabLista.Text = "Lista";
            this.tabLista.ToolTipText = "Lista de Clientes";
            this.tabLista.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblBusquedaNombre
            // 
            this.lblBusquedaNombre.AutoSize = true;
            this.lblBusquedaNombre.Location = new System.Drawing.Point(3, 6);
            this.lblBusquedaNombre.Name = "lblBusquedaNombre";
            this.lblBusquedaNombre.Size = new System.Drawing.Size(113, 13);
            this.lblBusquedaNombre.TabIndex = 1;
            this.lblBusquedaNombre.Text = "Busqueda por Nombre";
            // 
            // dgvClient
            // 
            this.dgvClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Mongolian Baiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClient.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvClient.EnableHeadersVisualStyles = false;
            this.dgvClient.Location = new System.Drawing.Point(3, 45);
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.ReadOnly = true;
            this.dgvClient.RowHeadersWidth = 62;
            this.dgvClient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClient.Size = new System.Drawing.Size(806, 411);
            this.dgvClient.TabIndex = 0;
            this.dgvClient.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClient_CellDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cotizarToolStripMenuItem,
            this.editarClienteToolStripMenuItem,
            this.eliminarClienteToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(158, 70);
            // 
            // cotizarToolStripMenuItem
            // 
            this.cotizarToolStripMenuItem.Name = "cotizarToolStripMenuItem";
            this.cotizarToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cotizarToolStripMenuItem.Text = "Cotizar";
            this.cotizarToolStripMenuItem.Click += new System.EventHandler(this.cotizarToolStripMenuItem_Click);
            // 
            // editarClienteToolStripMenuItem
            // 
            this.editarClienteToolStripMenuItem.Name = "editarClienteToolStripMenuItem";
            this.editarClienteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.editarClienteToolStripMenuItem.Text = "Editar Cliente";
            this.editarClienteToolStripMenuItem.Click += new System.EventHandler(this.editarClienteToolStripMenuItem_Click);
            // 
            // eliminarClienteToolStripMenuItem
            // 
            this.eliminarClienteToolStripMenuItem.Name = "eliminarClienteToolStripMenuItem";
            this.eliminarClienteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.eliminarClienteToolStripMenuItem.Text = "Eliminar Cliente";
            this.eliminarClienteToolStripMenuItem.Click += new System.EventHandler(this.eliminarClienteToolStripMenuItem_Click);
            // 
            // tabNew
            // 
            this.tabNew.Controls.Add(this.PanelDataNew);
            this.tabNew.Controls.Add(this.lblTitleNew);
            this.tabNew.ImageIndex = 6;
            this.tabNew.Location = new System.Drawing.Point(4, 32);
            this.tabNew.Name = "tabNew";
            this.tabNew.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabNew.Size = new System.Drawing.Size(811, 459);
            this.tabNew.TabIndex = 1;
            this.tabNew.Text = "Nuevo";
            this.tabNew.UseVisualStyleBackColor = true;
            // 
            // PanelDataNew
            // 
            this.PanelDataNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelDataNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDataNew.Controls.Add(this.lblAgenda);
            this.PanelDataNew.Controls.Add(this.button1);
            this.PanelDataNew.Controls.Add(this.txtName);
            this.PanelDataNew.Controls.Add(this.txtEmail);
            this.PanelDataNew.Controls.Add(this.txtAddress);
            this.PanelDataNew.Controls.Add(this.txtPhone);
            this.PanelDataNew.Controls.Add(this.btnAcceptNew);
            this.PanelDataNew.Controls.Add(this.lblEmail);
            this.PanelDataNew.Controls.Add(this.lblAddress);
            this.PanelDataNew.Controls.Add(this.lblPhone);
            this.PanelDataNew.Controls.Add(this.lblName);
            this.PanelDataNew.Location = new System.Drawing.Point(3, 48);
            this.PanelDataNew.Name = "PanelDataNew";
            this.PanelDataNew.Size = new System.Drawing.Size(809, 405);
            this.PanelDataNew.TabIndex = 1;
            // 
            // lblAgenda
            // 
            this.lblAgenda.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAgenda.AutoSize = true;
            this.lblAgenda.Location = new System.Drawing.Point(352, 237);
            this.lblAgenda.Name = "lblAgenda";
            this.lblAgenda.Size = new System.Drawing.Size(123, 13);
            this.lblAgenda.TabIndex = 10;
            this.lblAgenda.TabStop = true;
            this.lblAgenda.Text = "Agendar cita con Cliente";
            this.lblAgenda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAgenda_LinkClicked);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(684, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 83);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtName.Location = new System.Drawing.Point(32, 44);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(357, 28);
            this.txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmail.Location = new System.Drawing.Point(418, 123);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(357, 28);
            this.txtEmail.TabIndex = 7;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAddress.Location = new System.Drawing.Point(32, 123);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(357, 87);
            this.txtAddress.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPhone.Location = new System.Drawing.Point(418, 44);
            this.txtPhone.Multiline = true;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(357, 28);
            this.txtPhone.TabIndex = 3;
            // 
            // btnAcceptNew
            // 
            this.btnAcceptNew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAcceptNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAcceptNew.BackgroundImage")));
            this.btnAcceptNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAcceptNew.FlatAppearance.BorderSize = 0;
            this.btnAcceptNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcceptNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAcceptNew.Image")));
            this.btnAcceptNew.Location = new System.Drawing.Point(33, 291);
            this.btnAcceptNew.Name = "btnAcceptNew";
            this.btnAcceptNew.Size = new System.Drawing.Size(88, 66);
            this.btnAcceptNew.TabIndex = 8;
            this.btnAcceptNew.UseVisualStyleBackColor = true;
            this.btnAcceptNew.Click += new System.EventHandler(this.btnNewClient_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEmail.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(363, 86);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(170, 34);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Correo";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAddress
            // 
            this.lblAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAddress.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(28, 89);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(102, 31);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Direccion";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPhone.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(413, 12);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(88, 31);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Telefono";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblName.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(28, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(91, 28);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Nombre";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleNew
            // 
            this.lblTitleNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleNew.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleNew.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleNew.Location = new System.Drawing.Point(3, 3);
            this.lblTitleNew.Name = "lblTitleNew";
            this.lblTitleNew.Size = new System.Drawing.Size(805, 42);
            this.lblTitleNew.TabIndex = 0;
            this.lblTitleNew.Text = "Nuevo Cliente";
            this.lblTitleNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabEdit
            // 
            this.tabEdit.Controls.Add(this.panel1);
            this.tabEdit.Controls.Add(this.lblTitleEdit);
            this.tabEdit.ImageIndex = 7;
            this.tabEdit.Location = new System.Drawing.Point(4, 32);
            this.tabEdit.Name = "tabEdit";
            this.tabEdit.Size = new System.Drawing.Size(811, 459);
            this.tabEdit.TabIndex = 2;
            this.tabEdit.Text = "Editar";
            this.tabEdit.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtNameEdit);
            this.panel1.Controls.Add(this.txtEmailEdit);
            this.panel1.Controls.Add(this.txtAddressEdit);
            this.panel1.Controls.Add(this.txtPhoneEdit);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(3, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 405);
            this.panel1.TabIndex = 3;
            // 
            // txtId
            // 
            this.txtId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(32, 47);
            this.txtId.Multiline = true;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(357, 28);
            this.txtId.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 28);
            this.label5.TabIndex = 10;
            this.label5.Text = "ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(685, 289);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 85);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtNameEdit
            // 
            this.txtNameEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNameEdit.Location = new System.Drawing.Point(418, 47);
            this.txtNameEdit.Multiline = true;
            this.txtNameEdit.Name = "txtNameEdit";
            this.txtNameEdit.Size = new System.Drawing.Size(357, 28);
            this.txtNameEdit.TabIndex = 1;
            // 
            // txtEmailEdit
            // 
            this.txtEmailEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmailEdit.Location = new System.Drawing.Point(418, 123);
            this.txtEmailEdit.Multiline = true;
            this.txtEmailEdit.Name = "txtEmailEdit";
            this.txtEmailEdit.Size = new System.Drawing.Size(357, 28);
            this.txtEmailEdit.TabIndex = 7;
            // 
            // txtAddressEdit
            // 
            this.txtAddressEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAddressEdit.Location = new System.Drawing.Point(32, 123);
            this.txtAddressEdit.Multiline = true;
            this.txtAddressEdit.Name = "txtAddressEdit";
            this.txtAddressEdit.Size = new System.Drawing.Size(357, 87);
            this.txtAddressEdit.TabIndex = 5;
            // 
            // txtPhoneEdit
            // 
            this.txtPhoneEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPhoneEdit.Location = new System.Drawing.Point(418, 184);
            this.txtPhoneEdit.Multiline = true;
            this.txtPhoneEdit.Name = "txtPhoneEdit";
            this.txtPhoneEdit.Size = new System.Drawing.Size(357, 28);
            this.txtPhoneEdit.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(22, 289);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 79);
            this.button3.TabIndex = 8;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(363, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 34);
            this.label1.TabIndex = 6;
            this.label1.Text = "Correo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Direccion";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(413, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "Telefono";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(413, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 28);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nombre";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleEdit
            // 
            this.lblTitleEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleEdit.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleEdit.Location = new System.Drawing.Point(0, 0);
            this.lblTitleEdit.Name = "lblTitleEdit";
            this.lblTitleEdit.Size = new System.Drawing.Size(811, 42);
            this.lblTitleEdit.TabIndex = 2;
            this.lblTitleEdit.Text = "Editar Cliente";
            this.lblTitleEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Create.png");
            this.imageList.Images.SetKeyName(1, "Delete.png");
            this.imageList.Images.SetKeyName(2, "List.png");
            this.imageList.Images.SetKeyName(3, "Update.png");
            this.imageList.Images.SetKeyName(4, "Create1.png");
            this.imageList.Images.SetKeyName(5, "Más o agregar.png");
            this.imageList.Images.SetKeyName(6, "Agregar.png");
            this.imageList.Images.SetKeyName(7, "Prueba_Editar.png");
            // 
            // frmManagerClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(825, 562);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(825, 483);
            this.Name = "frmManagerClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formclosing);
            this.tabControl.ResumeLayout(false);
            this.tabLista.ResumeLayout(false);
            this.tabLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.tabNew.ResumeLayout(false);
            this.PanelDataNew.ResumeLayout(false);
            this.PanelDataNew.PerformLayout();
            this.tabEdit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabLista;
        private System.Windows.Forms.TabPage tabNew;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.TabPage tabEdit;
        private System.Windows.Forms.Panel PanelDataNew;
        private System.Windows.Forms.Label lblTitleNew;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAcceptNew;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblTitleEdit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cotizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarClienteToolStripMenuItem;
        private System.Windows.Forms.LinkLabel lblAgenda;
        private System.Windows.Forms.Label lblBusquedaNombre;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem eliminarClienteToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtNameEdit;
        private System.Windows.Forms.TextBox txtEmailEdit;
        private System.Windows.Forms.TextBox txtAddressEdit;
        private System.Windows.Forms.TextBox txtPhoneEdit;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label5;
    }
}