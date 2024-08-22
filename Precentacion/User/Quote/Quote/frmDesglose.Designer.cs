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
            this.dgvDesglose = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesglose)).BeginInit();
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
            // frmDesglose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDesglose);
            this.Name = "frmDesglose";
            this.Text = "frmDesglose";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesglose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDesglose;
    }
}