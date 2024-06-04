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
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
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
            this.dgvQuotes.Location = new System.Drawing.Point(4, 151);
            this.dgvQuotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.dgvQuotes.Size = new System.Drawing.Size(1259, 477);
            this.dgvQuotes.TabIndex = 3;
            this.dgvQuotes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQuotes_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cotizarToolStripMenuItem,
            this.editarProformaToolStripMenuItem,
            this.facturarProformaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(227, 100);
            // 
            // cotizarToolStripMenuItem
            // 
            this.cotizarToolStripMenuItem.Name = "cotizarToolStripMenuItem";
            this.cotizarToolStripMenuItem.Size = new System.Drawing.Size(226, 32);
            this.cotizarToolStripMenuItem.Text = "Cotizar";
            this.cotizarToolStripMenuItem.Click += new System.EventHandler(this.cotizarToolStripMenuItem_Click);
            // 
            // editarProformaToolStripMenuItem
            // 
            this.editarProformaToolStripMenuItem.Name = "editarProformaToolStripMenuItem";
            this.editarProformaToolStripMenuItem.Size = new System.Drawing.Size(226, 32);
            this.editarProformaToolStripMenuItem.Text = "Editar Proforma";
            this.editarProformaToolStripMenuItem.Click += new System.EventHandler(this.editarProformaToolStripMenuItem_Click);
            // 
            // facturarProformaToolStripMenuItem
            // 
            this.facturarProformaToolStripMenuItem.Name = "facturarProformaToolStripMenuItem";
            this.facturarProformaToolStripMenuItem.Size = new System.Drawing.Size(226, 32);
            this.facturarProformaToolStripMenuItem.Text = "Facturar Proforma";
            this.facturarProformaToolStripMenuItem.Click += new System.EventHandler(this.facturarProformaToolStripMenuItem_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(8, 110);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(93, 29);
            this.lblBuscar.TabIndex = 4;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Location = new System.Drawing.Point(109, 110);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(595, 31);
            this.txtBuscar.TabIndex = 5;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // frmManagerQuotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1260, 663);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.dgvQuotes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmManagerQuotes";
            this.Padding = new System.Windows.Forms.Padding(4, 98, 4, 5);
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
    }
}