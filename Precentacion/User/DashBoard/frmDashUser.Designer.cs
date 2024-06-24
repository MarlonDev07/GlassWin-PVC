namespace Precentacion.User.DashBoard
{
    partial class frmDashUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashUser));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnProduccion = new System.Windows.Forms.PictureBox();
            this.btnFactProveedor = new System.Windows.Forms.PictureBox();
            this.btnProveedor = new System.Windows.Forms.PictureBox();
            this.btnCalendario = new System.Windows.Forms.PictureBox();
            this.btnProyecto = new System.Windows.Forms.PictureBox();
            this.btnEmpleado = new System.Windows.Forms.PictureBox();
            this.btnCxC = new System.Windows.Forms.PictureBox();
            this.btnFactura = new System.Windows.Forms.PictureBox();
            this.btnOrden = new System.Windows.Forms.PictureBox();
            this.btnCliente = new System.Windows.Forms.PictureBox();
            this.pbAdmin = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.BarraSuperior = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnProduccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFactProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalendario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProyecto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCxC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFactura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.BarraSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProduccion
            // 
            this.btnProduccion.Image = ((System.Drawing.Image)(resources.GetObject("btnProduccion.Image")));
            this.btnProduccion.Location = new System.Drawing.Point(882, 31);
            this.btnProduccion.Name = "btnProduccion";
            this.btnProduccion.Size = new System.Drawing.Size(67, 63);
            this.btnProduccion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnProduccion.TabIndex = 46;
            this.btnProduccion.TabStop = false;
            this.toolTip1.SetToolTip(this.btnProduccion, "Produccion");
            this.btnProduccion.Click += new System.EventHandler(this.btnOrdenProd_Click);
            // 
            // btnFactProveedor
            // 
            this.btnFactProveedor.Image = ((System.Drawing.Image)(resources.GetObject("btnFactProveedor.Image")));
            this.btnFactProveedor.Location = new System.Drawing.Point(784, 31);
            this.btnFactProveedor.Name = "btnFactProveedor";
            this.btnFactProveedor.Size = new System.Drawing.Size(67, 63);
            this.btnFactProveedor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnFactProveedor.TabIndex = 45;
            this.btnFactProveedor.TabStop = false;
            this.toolTip1.SetToolTip(this.btnFactProveedor, "Facturas de Proveedores");
            this.btnFactProveedor.Click += new System.EventHandler(this.btnFactura_Click);
            this.btnFactProveedor.MouseEnter += new System.EventHandler(this.btnFactProveedor_MouseEnter);
            this.btnFactProveedor.MouseLeave += new System.EventHandler(this.btnFactProveedor_MouseLeave);
            // 
            // btnProveedor
            // 
            this.btnProveedor.Image = ((System.Drawing.Image)(resources.GetObject("btnProveedor.Image")));
            this.btnProveedor.Location = new System.Drawing.Point(686, 31);
            this.btnProveedor.Name = "btnProveedor";
            this.btnProveedor.Size = new System.Drawing.Size(67, 63);
            this.btnProveedor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnProveedor.TabIndex = 44;
            this.btnProveedor.TabStop = false;
            this.toolTip1.SetToolTip(this.btnProveedor, "Registro de Proveedores");
            this.btnProveedor.Click += new System.EventHandler(this.btnRegProveedor_Click);
            // 
            // btnCalendario
            // 
            this.btnCalendario.Image = ((System.Drawing.Image)(resources.GetObject("btnCalendario.Image")));
            this.btnCalendario.Location = new System.Drawing.Point(588, 31);
            this.btnCalendario.Name = "btnCalendario";
            this.btnCalendario.Size = new System.Drawing.Size(67, 63);
            this.btnCalendario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCalendario.TabIndex = 43;
            this.btnCalendario.TabStop = false;
            this.toolTip1.SetToolTip(this.btnCalendario, "Calendario Google");
            this.btnCalendario.Click += new System.EventHandler(this.btnCalendar_Click);
            this.btnCalendario.MouseEnter += new System.EventHandler(this.btnCalendario_MouseEnter);
            this.btnCalendario.MouseLeave += new System.EventHandler(this.btnCalendario_MouseLeave);
            // 
            // btnProyecto
            // 
            this.btnProyecto.Image = ((System.Drawing.Image)(resources.GetObject("btnProyecto.Image")));
            this.btnProyecto.Location = new System.Drawing.Point(490, 31);
            this.btnProyecto.Name = "btnProyecto";
            this.btnProyecto.Size = new System.Drawing.Size(67, 63);
            this.btnProyecto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnProyecto.TabIndex = 42;
            this.btnProyecto.TabStop = false;
            this.toolTip1.SetToolTip(this.btnProyecto, "Administración de Proyectos");
            this.btnProyecto.Click += new System.EventHandler(this.btnAdmProyecto_Click);
            this.btnProyecto.MouseEnter += new System.EventHandler(this.btnProyecto_MouseEnter);
            this.btnProyecto.MouseLeave += new System.EventHandler(this.btnProyecto_MouseLeave);
            // 
            // btnEmpleado
            // 
            this.btnEmpleado.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpleado.Image")));
            this.btnEmpleado.Location = new System.Drawing.Point(392, 31);
            this.btnEmpleado.Name = "btnEmpleado";
            this.btnEmpleado.Size = new System.Drawing.Size(67, 63);
            this.btnEmpleado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnEmpleado.TabIndex = 41;
            this.btnEmpleado.TabStop = false;
            this.toolTip1.SetToolTip(this.btnEmpleado, "Control de Empleados");
            this.btnEmpleado.Click += new System.EventHandler(this.btnEmployer_Click);
            this.btnEmpleado.MouseEnter += new System.EventHandler(this.btnEmpleado_MouseHover);
            this.btnEmpleado.MouseLeave += new System.EventHandler(this.btnEmpleado_MouseLeave);
            // 
            // btnCxC
            // 
            this.btnCxC.Image = ((System.Drawing.Image)(resources.GetObject("btnCxC.Image")));
            this.btnCxC.Location = new System.Drawing.Point(294, 31);
            this.btnCxC.Name = "btnCxC";
            this.btnCxC.Size = new System.Drawing.Size(67, 63);
            this.btnCxC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCxC.TabIndex = 40;
            this.btnCxC.TabStop = false;
            this.toolTip1.SetToolTip(this.btnCxC, "Cuentas por Cobrar");
            this.btnCxC.Click += new System.EventHandler(this.btnCxC_Click);
            this.btnCxC.MouseEnter += new System.EventHandler(this.btnCxC_MouseEnter);
            this.btnCxC.MouseLeave += new System.EventHandler(this.btnCxC_MouseLeave);
            // 
            // btnFactura
            // 
            this.btnFactura.Image = ((System.Drawing.Image)(resources.GetObject("btnFactura.Image")));
            this.btnFactura.Location = new System.Drawing.Point(196, 31);
            this.btnFactura.Name = "btnFactura";
            this.btnFactura.Size = new System.Drawing.Size(67, 63);
            this.btnFactura.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnFactura.TabIndex = 38;
            this.btnFactura.TabStop = false;
            this.toolTip1.SetToolTip(this.btnFactura, "Gestión de Proformas y Facturas");
            this.btnFactura.Click += new System.EventHandler(this.ManagerQuotes_Click);
            this.btnFactura.MouseEnter += new System.EventHandler(this.btnFactura_MouseHover);
            this.btnFactura.MouseLeave += new System.EventHandler(this.btnFactura_MouseLeave);
            // 
            // btnOrden
            // 
            this.btnOrden.Image = ((System.Drawing.Image)(resources.GetObject("btnOrden.Image")));
            this.btnOrden.Location = new System.Drawing.Point(98, 31);
            this.btnOrden.Name = "btnOrden";
            this.btnOrden.Size = new System.Drawing.Size(67, 63);
            this.btnOrden.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOrden.TabIndex = 37;
            this.btnOrden.TabStop = false;
            this.toolTip1.SetToolTip(this.btnOrden, "Nueva Proforma");
            this.btnOrden.Click += new System.EventHandler(this.btnNewQuote_Click);
            this.btnOrden.MouseEnter += new System.EventHandler(this.btnOrden_MouseEnter);
            this.btnOrden.MouseLeave += new System.EventHandler(this.btnOrden_MouseLeave);
            // 
            // btnCliente
            // 
            this.btnCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnCliente.Image")));
            this.btnCliente.Location = new System.Drawing.Point(2, 31);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(67, 63);
            this.btnCliente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCliente.TabIndex = 36;
            this.btnCliente.TabStop = false;
            this.toolTip1.SetToolTip(this.btnCliente, "Clientes");
            this.btnCliente.Click += new System.EventHandler(this.btnClient_Click);
            this.btnCliente.MouseEnter += new System.EventHandler(this.btnCliente_MouseEnter);
            this.btnCliente.MouseLeave += new System.EventHandler(this.btnCliente_MouseLeave);
            // 
            // pbAdmin
            // 
            this.pbAdmin.Image = ((System.Drawing.Image)(resources.GetObject("pbAdmin.Image")));
            this.pbAdmin.Location = new System.Drawing.Point(970, 31);
            this.pbAdmin.Name = "pbAdmin";
            this.pbAdmin.Size = new System.Drawing.Size(67, 63);
            this.pbAdmin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAdmin.TabIndex = 48;
            this.pbAdmin.TabStop = false;
            this.toolTip1.SetToolTip(this.pbAdmin, "Administracion de Precios");
            this.pbAdmin.Visible = false;
            this.pbAdmin.Click += new System.EventHandler(this.pictureBox4_Click_1);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(-91, 55);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(67, 63);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 47;
            this.pictureBox3.TabStop = false;
            // 
            // BarraSuperior
            // 
            this.BarraSuperior.BackColor = System.Drawing.Color.Orange;
            this.BarraSuperior.Controls.Add(this.btnMinimizar);
            this.BarraSuperior.Controls.Add(this.label1);
            this.BarraSuperior.Controls.Add(this.btnCerrar);
            this.BarraSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraSuperior.Location = new System.Drawing.Point(0, 0);
            this.BarraSuperior.Name = "BarraSuperior";
            this.BarraSuperior.Size = new System.Drawing.Size(1043, 27);
            this.BarraSuperior.TabIndex = 39;
            this.BarraSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraSuperior_MouseDown);
            this.BarraSuperior.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BarraSuperior_MouseMove);
            this.BarraSuperior.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BarraSuperior_MouseUp);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(962, 3);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(36, 21);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMinimizar.TabIndex = 30;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            this.btnMinimizar.MouseEnter += new System.EventHandler(this.btnMinimizar_MouseEnter);
            this.btnMinimizar.MouseLeave += new System.EventHandler(this.btnMinimizar_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "GlassWin";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1004, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(36, 21);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 28;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.MouseEnter += new System.EventHandler(this.btnCerrar_MouseEnter);
            this.btnCerrar.MouseLeave += new System.EventHandler(this.btnCerrar_MouseLeave);
            // 
            // frmDashUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1043, 97);
            this.Controls.Add(this.pbAdmin);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnProduccion);
            this.Controls.Add(this.btnFactProveedor);
            this.Controls.Add(this.btnProveedor);
            this.Controls.Add(this.btnCalendario);
            this.Controls.Add(this.btnProyecto);
            this.Controls.Add(this.btnEmpleado);
            this.Controls.Add(this.btnCxC);
            this.Controls.Add(this.BarraSuperior);
            this.Controls.Add(this.btnFactura);
            this.Controls.Add(this.btnOrden);
            this.Controls.Add(this.btnCliente);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDashUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.btnProduccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFactProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalendario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProyecto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCxC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFactura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.BarraSuperior.ResumeLayout(false);
            this.BarraSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox btnProduccion;
        private System.Windows.Forms.PictureBox btnFactProveedor;
        private System.Windows.Forms.PictureBox btnProveedor;
        private System.Windows.Forms.PictureBox btnCalendario;
        private System.Windows.Forms.PictureBox btnProyecto;
        private System.Windows.Forms.PictureBox btnEmpleado;
        private System.Windows.Forms.PictureBox btnCxC;
        private System.Windows.Forms.Panel BarraSuperior;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnFactura;
        private System.Windows.Forms.PictureBox btnOrden;
        private System.Windows.Forms.PictureBox btnCliente;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox pbAdmin;
    }
}