namespace Precentacion.User.Quote.Quote
{
    partial class frmManagerQuotes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagerQuotes));
            this.dgvQuotes = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cotizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarProformaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturarProformaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarFacturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.dtFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVerEstadosProforma = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuotes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvQuotes
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Salmon;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvQuotes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQuotes.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvQuotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQuotes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQuotes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQuotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuotes.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvQuotes.EnableHeadersVisualStyles = false;
            this.dgvQuotes.GridColor = System.Drawing.Color.Black;
            this.dgvQuotes.Location = new System.Drawing.Point(9, 136);
            this.dgvQuotes.Name = "dgvQuotes";
            this.dgvQuotes.ReadOnly = true;
            this.dgvQuotes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQuotes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvQuotes.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvQuotes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvQuotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuotes.Size = new System.Drawing.Size(903, 351);
            this.dgvQuotes.TabIndex = 3;
            this.dgvQuotes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQuotes_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cotizarToolStripMenuItem,
            this.editarProformaToolStripMenuItem,
            this.facturarProformaToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.eliminarFacturaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 114);
            // 
            // cotizarToolStripMenuItem
            // 
            this.cotizarToolStripMenuItem.Name = "cotizarToolStripMenuItem";
            this.cotizarToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.cotizarToolStripMenuItem.Text = "Cotizar";
            this.cotizarToolStripMenuItem.Click += new System.EventHandler(this.cotizarToolStripMenuItem_Click);
            // 
            // editarProformaToolStripMenuItem
            // 
            this.editarProformaToolStripMenuItem.Name = "editarProformaToolStripMenuItem";
            this.editarProformaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.editarProformaToolStripMenuItem.Text = "Editar Proforma";
            this.editarProformaToolStripMenuItem.Click += new System.EventHandler(this.editarProformaToolStripMenuItem_Click);
            // 
            // facturarProformaToolStripMenuItem
            // 
            this.facturarProformaToolStripMenuItem.Name = "facturarProformaToolStripMenuItem";
            this.facturarProformaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.facturarProformaToolStripMenuItem.Text = "Facturar Proforma";
            this.facturarProformaToolStripMenuItem.Click += new System.EventHandler(this.facturarProformaToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar Proforma";
            this.eliminarToolStripMenuItem.Visible = false;
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // eliminarFacturaToolStripMenuItem
            // 
            this.eliminarFacturaToolStripMenuItem.Name = "eliminarFacturaToolStripMenuItem";
            this.eliminarFacturaToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.eliminarFacturaToolStripMenuItem.Text = "Eliminar Factura";
            this.eliminarFacturaToolStripMenuItem.Visible = false;
            this.eliminarFacturaToolStripMenuItem.Click += new System.EventHandler(this.eliminarFacturaToolStripMenuItem_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(49, 75);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(63, 20);
            this.lblBuscar.TabIndex = 4;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Location = new System.Drawing.Point(10, 98);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(147, 21);
            this.txtBuscar.TabIndex = 5;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(325, 100);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(103, 20);
            this.dtInicio.TabIndex = 6;
            this.dtInicio.ValueChanged += new System.EventHandler(this.dtInicio_ValueChanged);
            // 
            // dtFin
            // 
            this.dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFin.Location = new System.Drawing.Point(455, 100);
            this.dtFin.Name = "dtFin";
            this.dtFin.Size = new System.Drawing.Size(106, 20);
            this.dtFin.TabIndex = 7;
            this.dtFin.ValueChanged += new System.EventHandler(this.dtInicio_ValueChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(325, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Buscar por Fechas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVerEstadosProforma
            // 
            this.btnVerEstadosProforma.Location = new System.Drawing.Point(755, 68);
            this.btnVerEstadosProforma.Name = "btnVerEstadosProforma";
            this.btnVerEstadosProforma.Size = new System.Drawing.Size(157, 64);
            this.btnVerEstadosProforma.TabIndex = 9;
            this.btnVerEstadosProforma.Text = "Ver Facturas";
            this.btnVerEstadosProforma.UseVisualStyleBackColor = true;
            this.btnVerEstadosProforma.Click += new System.EventHandler(this.btnVerEstadosProforma_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(795, 493);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 10;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIva
            // 
            this.txtIva.Enabled = false;
            this.txtIva.Location = new System.Drawing.Point(689, 493);
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(100, 20);
            this.txtIva.TabIndex = 11;
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Enabled = false;
            this.txtSubtotal.Location = new System.Drawing.Point(583, 493);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubtotal.TabIndex = 12;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefrescar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefrescar.BackgroundImage")));
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefrescar.FlatAppearance.BorderSize = 2;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Location = new System.Drawing.Point(689, 78);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(36, 41);
            this.btnRefrescar.TabIndex = 19;
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // frmManagerQuotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(918, 530);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.txtIva);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnVerEstadosProforma);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtFin);
            this.Controls.Add(this.dtInicio);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.dgvQuotes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmManagerQuotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proformas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManagerQuotes_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuotes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQuotes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cotizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarProformaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturarProformaToolStripMenuItem;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.DateTimePicker dtFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVerEstadosProforma;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarFacturaToolStripMenuItem;
        private System.Windows.Forms.Button btnRefrescar;
    }
}