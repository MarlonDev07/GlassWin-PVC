namespace Precentacion.User.Quote.Accesorios
{
    partial class frmTablaViaticos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTablaViaticos));
            this.lblGasolina = new System.Windows.Forms.Label();
            this.lblCantV = new System.Windows.Forms.Label();
            this.numericVeiculos = new System.Windows.Forms.NumericUpDown();
            this.lblDistK = new System.Windows.Forms.Label();
            this.txtDistancia = new System.Windows.Forms.TextBox();
            this.txtPrecioxKM = new System.Windows.Forms.TextBox();
            this.lblPPK = new System.Windows.Forms.Label();
            this.txtTotalGasolina = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnAbrirMaps = new System.Windows.Forms.Button();
            this.lblComida = new System.Windows.Forms.Label();
            this.txtCena = new System.Windows.Forms.TextBox();
            this.lblCena = new System.Windows.Forms.Label();
            this.txtAlmuerzo = new System.Windows.Forms.TextBox();
            this.lblAlmuerzo = new System.Windows.Forms.Label();
            this.txtDesayuno = new System.Windows.Forms.TextBox();
            this.lblDesayuna = new System.Windows.Forms.Label();
            this.NumericEmpleados = new System.Windows.Forms.NumericUpDown();
            this.lblCantEmpleados = new System.Windows.Forms.Label();
            this.NumericDias = new System.Windows.Forms.NumericUpDown();
            this.lblCantDias = new System.Windows.Forms.Label();
            this.txtTotalComida = new System.Windows.Forms.TextBox();
            this.lblTotal2 = new System.Windows.Forms.Label();
            this.lblTotalViaticos = new System.Windows.Forms.Label();
            this.txtTotalViaticos = new System.Windows.Forms.TextBox();
            this.lblTotalT = new System.Windows.Forms.Label();
            this.lblHospedaje = new System.Windows.Forms.Label();
            this.txtPrecioHabitacion = new System.Windows.Forms.TextBox();
            this.lblPPH = new System.Windows.Forms.Label();
            this.NumericHabitaciones = new System.Windows.Forms.NumericUpDown();
            this.lblCantHabitaciones = new System.Windows.Forms.Label();
            this.NumericNoches = new System.Windows.Forms.NumericUpDown();
            this.lblNoches = new System.Windows.Forms.Label();
            this.btnHospedaje = new System.Windows.Forms.Button();
            this.txtTotalHospedaje = new System.Windows.Forms.TextBox();
            this.lblTotal3 = new System.Windows.Forms.Label();
            this.lblSalarios = new System.Windows.Forms.Label();
            this.lblListaEmpleados = new System.Windows.Forms.Label();
            this.cmbEmpleados = new System.Windows.Forms.ComboBox();
            this.btnSistemas = new System.Windows.Forms.Button();
            this.txtSalarios = new System.Windows.Forms.TextBox();
            this.lblSalariosxHora = new System.Windows.Forms.Label();
            this.lblCantHoras = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblTotal4 = new System.Windows.Forms.Label();
            this.txtHoras = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericVeiculos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHabitaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericNoches)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGasolina
            // 
            this.lblGasolina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblGasolina.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGasolina.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGasolina.ForeColor = System.Drawing.Color.White;
            this.lblGasolina.Location = new System.Drawing.Point(4, 98);
            this.lblGasolina.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGasolina.Name = "lblGasolina";
            this.lblGasolina.Size = new System.Drawing.Size(1226, 52);
            this.lblGasolina.TabIndex = 0;
            this.lblGasolina.Text = "Gasolina";
            this.lblGasolina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCantV
            // 
            this.lblCantV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantV.Location = new System.Drawing.Point(9, 155);
            this.lblCantV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantV.Name = "lblCantV";
            this.lblCantV.Size = new System.Drawing.Size(221, 25);
            this.lblCantV.TabIndex = 1;
            this.lblCantV.Text = "Cantidad de Vehiculos";
            this.lblCantV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericVeiculos
            // 
            this.numericVeiculos.Location = new System.Drawing.Point(9, 186);
            this.numericVeiculos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericVeiculos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericVeiculos.Name = "numericVeiculos";
            this.numericVeiculos.Size = new System.Drawing.Size(221, 26);
            this.numericVeiculos.TabIndex = 2;
            this.numericVeiculos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericVeiculos.ValueChanged += new System.EventHandler(this.txtGasolina_TextChanged);
            // 
            // lblDistK
            // 
            this.lblDistK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistK.Location = new System.Drawing.Point(278, 155);
            this.lblDistK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistK.Name = "lblDistK";
            this.lblDistK.Size = new System.Drawing.Size(239, 25);
            this.lblDistK.TabIndex = 3;
            this.lblDistK.Text = "Distancia en Kilomentros";
            this.lblDistK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDistancia
            // 
            this.txtDistancia.Location = new System.Drawing.Point(278, 185);
            this.txtDistancia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDistancia.Name = "txtDistancia";
            this.txtDistancia.Size = new System.Drawing.Size(239, 26);
            this.txtDistancia.TabIndex = 4;
            this.txtDistancia.TextChanged += new System.EventHandler(this.txtGasolina_TextChanged);
            // 
            // txtPrecioxKM
            // 
            this.txtPrecioxKM.Location = new System.Drawing.Point(584, 185);
            this.txtPrecioxKM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrecioxKM.Name = "txtPrecioxKM";
            this.txtPrecioxKM.Size = new System.Drawing.Size(224, 26);
            this.txtPrecioxKM.TabIndex = 6;
            this.txtPrecioxKM.TextChanged += new System.EventHandler(this.txtGasolina_TextChanged);
            // 
            // lblPPK
            // 
            this.lblPPK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPPK.Location = new System.Drawing.Point(586, 155);
            this.lblPPK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPPK.Name = "lblPPK";
            this.lblPPK.Size = new System.Drawing.Size(222, 25);
            this.lblPPK.TabIndex = 5;
            this.lblPPK.Text = "Precio por Kilomentros";
            this.lblPPK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalGasolina
            // 
            this.txtTotalGasolina.Location = new System.Drawing.Point(874, 185);
            this.txtTotalGasolina.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalGasolina.Name = "txtTotalGasolina";
            this.txtTotalGasolina.ReadOnly = true;
            this.txtTotalGasolina.Size = new System.Drawing.Size(148, 26);
            this.txtTotalGasolina.TabIndex = 8;
            this.txtTotalGasolina.TextChanged += new System.EventHandler(this.CalcularTotal_TextChange);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(874, 155);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(148, 25);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAbrirMaps
            // 
            this.btnAbrirMaps.Location = new System.Drawing.Point(1062, 155);
            this.btnAbrirMaps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAbrirMaps.Name = "btnAbrirMaps";
            this.btnAbrirMaps.Size = new System.Drawing.Size(158, 62);
            this.btnAbrirMaps.TabIndex = 9;
            this.btnAbrirMaps.Text = "Kilometraje";
            this.btnAbrirMaps.UseVisualStyleBackColor = true;
            this.btnAbrirMaps.Click += new System.EventHandler(this.btnAbrirMaps_Click);
            // 
            // lblComida
            // 
            this.lblComida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblComida.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComida.ForeColor = System.Drawing.Color.White;
            this.lblComida.Location = new System.Drawing.Point(-1, 237);
            this.lblComida.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComida.Name = "lblComida";
            this.lblComida.Size = new System.Drawing.Size(1240, 55);
            this.lblComida.TabIndex = 10;
            this.lblComida.Text = "Comida";
            this.lblComida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCena
            // 
            this.txtCena.Location = new System.Drawing.Point(651, 331);
            this.txtCena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(154, 26);
            this.txtCena.TabIndex = 18;
            this.txtCena.TextChanged += new System.EventHandler(this.txtComida_TextChanged);
            // 
            // lblCena
            // 
            this.lblCena.AutoSize = true;
            this.lblCena.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCena.Location = new System.Drawing.Point(693, 302);
            this.lblCena.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCena.Name = "lblCena";
            this.lblCena.Size = new System.Drawing.Size(60, 25);
            this.lblCena.TabIndex = 17;
            this.lblCena.Text = "Cena";
            // 
            // txtAlmuerzo
            // 
            this.txtAlmuerzo.Location = new System.Drawing.Point(458, 331);
            this.txtAlmuerzo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAlmuerzo.Name = "txtAlmuerzo";
            this.txtAlmuerzo.Size = new System.Drawing.Size(154, 26);
            this.txtAlmuerzo.TabIndex = 16;
            this.txtAlmuerzo.TextChanged += new System.EventHandler(this.txtComida_TextChanged);
            // 
            // lblAlmuerzo
            // 
            this.lblAlmuerzo.AutoSize = true;
            this.lblAlmuerzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlmuerzo.Location = new System.Drawing.Point(489, 302);
            this.lblAlmuerzo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlmuerzo.Name = "lblAlmuerzo";
            this.lblAlmuerzo.Size = new System.Drawing.Size(95, 25);
            this.lblAlmuerzo.TabIndex = 15;
            this.lblAlmuerzo.Text = "Almuerzo";
            // 
            // txtDesayuno
            // 
            this.txtDesayuno.Location = new System.Drawing.Point(268, 331);
            this.txtDesayuno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDesayuno.Name = "txtDesayuno";
            this.txtDesayuno.Size = new System.Drawing.Size(154, 26);
            this.txtDesayuno.TabIndex = 14;
            this.txtDesayuno.TextChanged += new System.EventHandler(this.txtComida_TextChanged);
            // 
            // lblDesayuna
            // 
            this.lblDesayuna.AutoSize = true;
            this.lblDesayuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesayuna.Location = new System.Drawing.Point(294, 302);
            this.lblDesayuna.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesayuna.Name = "lblDesayuna";
            this.lblDesayuna.Size = new System.Drawing.Size(101, 25);
            this.lblDesayuna.TabIndex = 13;
            this.lblDesayuna.Text = "Desayuno";
            // 
            // NumericEmpleados
            // 
            this.NumericEmpleados.Location = new System.Drawing.Point(14, 332);
            this.NumericEmpleados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumericEmpleados.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericEmpleados.Name = "NumericEmpleados";
            this.NumericEmpleados.Size = new System.Drawing.Size(225, 26);
            this.NumericEmpleados.TabIndex = 12;
            this.NumericEmpleados.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericEmpleados.ValueChanged += new System.EventHandler(this.txtComida_TextChanged);
            // 
            // lblCantEmpleados
            // 
            this.lblCantEmpleados.AutoSize = true;
            this.lblCantEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantEmpleados.Location = new System.Drawing.Point(9, 303);
            this.lblCantEmpleados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantEmpleados.Name = "lblCantEmpleados";
            this.lblCantEmpleados.Size = new System.Drawing.Size(221, 25);
            this.lblCantEmpleados.TabIndex = 11;
            this.lblCantEmpleados.Text = "Cantidad de Empleados";
            // 
            // NumericDias
            // 
            this.NumericDias.Location = new System.Drawing.Point(848, 332);
            this.NumericDias.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumericDias.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericDias.Name = "NumericDias";
            this.NumericDias.Size = new System.Drawing.Size(160, 26);
            this.NumericDias.TabIndex = 20;
            this.NumericDias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericDias.ValueChanged += new System.EventHandler(this.txtComida_TextChanged);
            // 
            // lblCantDias
            // 
            this.lblCantDias.AutoSize = true;
            this.lblCantDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantDias.Location = new System.Drawing.Point(843, 302);
            this.lblCantDias.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantDias.Name = "lblCantDias";
            this.lblCantDias.Size = new System.Drawing.Size(162, 25);
            this.lblCantDias.TabIndex = 19;
            this.lblCantDias.Text = "Cantidad de Dias";
            // 
            // txtTotalComida
            // 
            this.txtTotalComida.Location = new System.Drawing.Point(1050, 332);
            this.txtTotalComida.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalComida.Name = "txtTotalComida";
            this.txtTotalComida.ReadOnly = true;
            this.txtTotalComida.Size = new System.Drawing.Size(148, 26);
            this.txtTotalComida.TabIndex = 22;
            this.txtTotalComida.TextChanged += new System.EventHandler(this.CalcularTotal_TextChange);
            // 
            // lblTotal2
            // 
            this.lblTotal2.AutoSize = true;
            this.lblTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal2.Location = new System.Drawing.Point(1092, 303);
            this.lblTotal2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal2.Name = "lblTotal2";
            this.lblTotal2.Size = new System.Drawing.Size(56, 25);
            this.lblTotal2.TabIndex = 21;
            this.lblTotal2.Text = "Total";
            // 
            // lblTotalViaticos
            // 
            this.lblTotalViaticos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblTotalViaticos.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalViaticos.ForeColor = System.Drawing.Color.White;
            this.lblTotalViaticos.Location = new System.Drawing.Point(-6, 706);
            this.lblTotalViaticos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalViaticos.Name = "lblTotalViaticos";
            this.lblTotalViaticos.Size = new System.Drawing.Size(1245, 66);
            this.lblTotalViaticos.TabIndex = 23;
            this.lblTotalViaticos.Text = "Total de Viaticos";
            this.lblTotalViaticos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalViaticos
            // 
            this.txtTotalViaticos.Location = new System.Drawing.Point(515, 847);
            this.txtTotalViaticos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalViaticos.Name = "txtTotalViaticos";
            this.txtTotalViaticos.ReadOnly = true;
            this.txtTotalViaticos.Size = new System.Drawing.Size(188, 26);
            this.txtTotalViaticos.TabIndex = 25;
            this.txtTotalViaticos.TextChanged += new System.EventHandler(this.txtTotalViaticos_TextChanged);
            // 
            // lblTotalT
            // 
            this.lblTotalT.AutoSize = true;
            this.lblTotalT.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalT.Location = new System.Drawing.Point(565, 801);
            this.lblTotalT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalT.Name = "lblTotalT";
            this.lblTotalT.Size = new System.Drawing.Size(106, 51);
            this.lblTotalT.TabIndex = 24;
            this.lblTotalT.Text = "Total";
            this.lblTotalT.Click += new System.EventHandler(this.lblTotalT_Click);
            // 
            // lblHospedaje
            // 
            this.lblHospedaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblHospedaje.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospedaje.ForeColor = System.Drawing.Color.White;
            this.lblHospedaje.Location = new System.Drawing.Point(-1, 386);
            this.lblHospedaje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHospedaje.Name = "lblHospedaje";
            this.lblHospedaje.Size = new System.Drawing.Size(1240, 55);
            this.lblHospedaje.TabIndex = 27;
            this.lblHospedaje.Text = "Hospedaje";
            this.lblHospedaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrecioHabitacion
            // 
            this.txtPrecioHabitacion.Location = new System.Drawing.Point(382, 486);
            this.txtPrecioHabitacion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrecioHabitacion.Name = "txtPrecioHabitacion";
            this.txtPrecioHabitacion.Size = new System.Drawing.Size(194, 26);
            this.txtPrecioHabitacion.TabIndex = 29;
            this.txtPrecioHabitacion.TextChanged += new System.EventHandler(this.txtHospedaje_TextChanged);
            // 
            // lblPPH
            // 
            this.lblPPH.AutoSize = true;
            this.lblPPH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPPH.Location = new System.Drawing.Point(378, 457);
            this.lblPPH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPPH.Name = "lblPPH";
            this.lblPPH.Size = new System.Drawing.Size(197, 25);
            this.lblPPH.TabIndex = 28;
            this.lblPPH.Text = "Precio por Habitacion";
            // 
            // NumericHabitaciones
            // 
            this.NumericHabitaciones.Location = new System.Drawing.Point(9, 486);
            this.NumericHabitaciones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumericHabitaciones.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericHabitaciones.Name = "NumericHabitaciones";
            this.NumericHabitaciones.Size = new System.Drawing.Size(244, 26);
            this.NumericHabitaciones.TabIndex = 31;
            this.NumericHabitaciones.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericHabitaciones.ValueChanged += new System.EventHandler(this.txtHospedaje_TextChanged);
            // 
            // lblCantHabitaciones
            // 
            this.lblCantHabitaciones.AutoSize = true;
            this.lblCantHabitaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantHabitaciones.Location = new System.Drawing.Point(9, 457);
            this.lblCantHabitaciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantHabitaciones.Name = "lblCantHabitaciones";
            this.lblCantHabitaciones.Size = new System.Drawing.Size(236, 25);
            this.lblCantHabitaciones.TabIndex = 30;
            this.lblCantHabitaciones.Text = "Cantidad de Habitaciones";
            // 
            // NumericNoches
            // 
            this.NumericNoches.Location = new System.Drawing.Point(693, 486);
            this.NumericNoches.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumericNoches.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericNoches.Name = "NumericNoches";
            this.NumericNoches.Size = new System.Drawing.Size(81, 26);
            this.NumericNoches.TabIndex = 33;
            this.NumericNoches.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericNoches.ValueChanged += new System.EventHandler(this.txtHospedaje_TextChanged);
            // 
            // lblNoches
            // 
            this.lblNoches.AutoSize = true;
            this.lblNoches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoches.Location = new System.Drawing.Point(693, 457);
            this.lblNoches.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoches.Name = "lblNoches";
            this.lblNoches.Size = new System.Drawing.Size(79, 25);
            this.lblNoches.TabIndex = 32;
            this.lblNoches.Text = "Noches";
            // 
            // btnHospedaje
            // 
            this.btnHospedaje.Location = new System.Drawing.Point(1062, 458);
            this.btnHospedaje.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHospedaje.Name = "btnHospedaje";
            this.btnHospedaje.Size = new System.Drawing.Size(158, 62);
            this.btnHospedaje.TabIndex = 34;
            this.btnHospedaje.Text = "Hospedajes";
            this.btnHospedaje.UseVisualStyleBackColor = true;
            this.btnHospedaje.Click += new System.EventHandler(this.btnHospedaje_Click);
            // 
            // txtTotalHospedaje
            // 
            this.txtTotalHospedaje.Location = new System.Drawing.Point(874, 485);
            this.txtTotalHospedaje.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalHospedaje.Name = "txtTotalHospedaje";
            this.txtTotalHospedaje.ReadOnly = true;
            this.txtTotalHospedaje.Size = new System.Drawing.Size(148, 26);
            this.txtTotalHospedaje.TabIndex = 36;
            this.txtTotalHospedaje.TextChanged += new System.EventHandler(this.CalcularTotal_TextChange);
            // 
            // lblTotal3
            // 
            this.lblTotal3.AutoSize = true;
            this.lblTotal3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal3.Location = new System.Drawing.Point(916, 455);
            this.lblTotal3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal3.Name = "lblTotal3";
            this.lblTotal3.Size = new System.Drawing.Size(56, 25);
            this.lblTotal3.TabIndex = 35;
            this.lblTotal3.Text = "Total";
            // 
            // lblSalarios
            // 
            this.lblSalarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblSalarios.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalarios.ForeColor = System.Drawing.Color.White;
            this.lblSalarios.Location = new System.Drawing.Point(-4, 529);
            this.lblSalarios.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalarios.Name = "lblSalarios";
            this.lblSalarios.Size = new System.Drawing.Size(1243, 55);
            this.lblSalarios.TabIndex = 37;
            this.lblSalarios.Text = "Salarios";
            this.lblSalarios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblListaEmpleados
            // 
            this.lblListaEmpleados.AutoSize = true;
            this.lblListaEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaEmpleados.Location = new System.Drawing.Point(117, 610);
            this.lblListaEmpleados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListaEmpleados.Name = "lblListaEmpleados";
            this.lblListaEmpleados.Size = new System.Drawing.Size(156, 25);
            this.lblListaEmpleados.TabIndex = 38;
            this.lblListaEmpleados.Text = "Lista Empleados";
            // 
            // cmbEmpleados
            // 
            this.cmbEmpleados.FormattingEnabled = true;
            this.cmbEmpleados.Location = new System.Drawing.Point(81, 639);
            this.cmbEmpleados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEmpleados.Name = "cmbEmpleados";
            this.cmbEmpleados.Size = new System.Drawing.Size(238, 28);
            this.cmbEmpleados.TabIndex = 39;
            // 
            // btnSistemas
            // 
            this.btnSistemas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSistemas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSistemas.BackgroundImage")));
            this.btnSistemas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSistemas.FlatAppearance.BorderSize = 2;
            this.btnSistemas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSistemas.Location = new System.Drawing.Point(329, 639);
            this.btnSistemas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSistemas.Name = "btnSistemas";
            this.btnSistemas.Size = new System.Drawing.Size(45, 38);
            this.btnSistemas.TabIndex = 40;
            this.btnSistemas.UseVisualStyleBackColor = true;
            this.btnSistemas.Click += new System.EventHandler(this.ObtenerSalario_Click);
            // 
            // txtSalarios
            // 
            this.txtSalarios.Location = new System.Drawing.Point(449, 639);
            this.txtSalarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSalarios.Name = "txtSalarios";
            this.txtSalarios.Size = new System.Drawing.Size(194, 26);
            this.txtSalarios.TabIndex = 42;
            this.txtSalarios.TextChanged += new System.EventHandler(this.txtSalarios_TextChanged);
            // 
            // lblSalariosxHora
            // 
            this.lblSalariosxHora.AutoSize = true;
            this.lblSalariosxHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalariosxHora.Location = new System.Drawing.Point(463, 610);
            this.lblSalariosxHora.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalariosxHora.Name = "lblSalariosxHora";
            this.lblSalariosxHora.Size = new System.Drawing.Size(163, 25);
            this.lblSalariosxHora.TabIndex = 41;
            this.lblSalariosxHora.Text = "Salarios por Hora";
            // 
            // lblCantHoras
            // 
            this.lblCantHoras.AutoSize = true;
            this.lblCantHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantHoras.Location = new System.Drawing.Point(727, 610);
            this.lblCantHoras.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantHoras.Name = "lblCantHoras";
            this.lblCantHoras.Size = new System.Drawing.Size(175, 25);
            this.lblCantHoras.TabIndex = 43;
            this.lblCantHoras.Text = "Cantidad de Horas";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(941, 639);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(148, 26);
            this.textBox2.TabIndex = 46;
            this.textBox2.TextChanged += new System.EventHandler(this.CalcularTotal_TextChange);
            // 
            // lblTotal4
            // 
            this.lblTotal4.AutoSize = true;
            this.lblTotal4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal4.Location = new System.Drawing.Point(983, 610);
            this.lblTotal4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal4.Name = "lblTotal4";
            this.lblTotal4.Size = new System.Drawing.Size(56, 25);
            this.lblTotal4.TabIndex = 45;
            this.lblTotal4.Text = "Total";
            // 
            // txtHoras
            // 
            this.txtHoras.Location = new System.Drawing.Point(731, 637);
            this.txtHoras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHoras.Name = "txtHoras";
            this.txtHoras.Size = new System.Drawing.Size(174, 26);
            this.txtHoras.TabIndex = 47;
            this.txtHoras.TextChanged += new System.EventHandler(this.txtSalarios_TextChanged);
            // 
            // frmTablaViaticos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 908);
            this.Controls.Add(this.txtHoras);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lblTotal4);
            this.Controls.Add(this.lblCantHoras);
            this.Controls.Add(this.txtSalarios);
            this.Controls.Add(this.lblSalariosxHora);
            this.Controls.Add(this.btnSistemas);
            this.Controls.Add(this.cmbEmpleados);
            this.Controls.Add(this.lblListaEmpleados);
            this.Controls.Add(this.lblSalarios);
            this.Controls.Add(this.txtTotalHospedaje);
            this.Controls.Add(this.lblTotal3);
            this.Controls.Add(this.btnHospedaje);
            this.Controls.Add(this.NumericNoches);
            this.Controls.Add(this.lblNoches);
            this.Controls.Add(this.NumericHabitaciones);
            this.Controls.Add(this.lblCantHabitaciones);
            this.Controls.Add(this.txtPrecioHabitacion);
            this.Controls.Add(this.lblPPH);
            this.Controls.Add(this.lblHospedaje);
            this.Controls.Add(this.txtTotalViaticos);
            this.Controls.Add(this.lblTotalT);
            this.Controls.Add(this.lblTotalViaticos);
            this.Controls.Add(this.txtTotalComida);
            this.Controls.Add(this.lblTotal2);
            this.Controls.Add(this.NumericDias);
            this.Controls.Add(this.lblCantDias);
            this.Controls.Add(this.txtCena);
            this.Controls.Add(this.lblCena);
            this.Controls.Add(this.txtAlmuerzo);
            this.Controls.Add(this.lblAlmuerzo);
            this.Controls.Add(this.txtDesayuno);
            this.Controls.Add(this.lblDesayuna);
            this.Controls.Add(this.NumericEmpleados);
            this.Controls.Add(this.lblCantEmpleados);
            this.Controls.Add(this.lblComida);
            this.Controls.Add(this.btnAbrirMaps);
            this.Controls.Add(this.txtTotalGasolina);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtPrecioxKM);
            this.Controls.Add(this.lblPPK);
            this.Controls.Add(this.txtDistancia);
            this.Controls.Add(this.lblDistK);
            this.Controls.Add(this.numericVeiculos);
            this.Controls.Add(this.lblCantV);
            this.Controls.Add(this.lblGasolina);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1234, 908);
            this.MinimumSize = new System.Drawing.Size(1234, 908);
            this.Name = "frmTablaViaticos";
            this.Padding = new System.Windows.Forms.Padding(4, 98, 4, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tabla Viaticos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numericVeiculos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHabitaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericNoches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGasolina;
        private System.Windows.Forms.Label lblCantV;
        private System.Windows.Forms.NumericUpDown numericVeiculos;
        private System.Windows.Forms.Label lblDistK;
        private System.Windows.Forms.TextBox txtDistancia;
        private System.Windows.Forms.TextBox txtPrecioxKM;
        private System.Windows.Forms.Label lblPPK;
        private System.Windows.Forms.TextBox txtTotalGasolina;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnAbrirMaps;
        private System.Windows.Forms.Label lblComida;
        private System.Windows.Forms.TextBox txtCena;
        private System.Windows.Forms.Label lblCena;
        private System.Windows.Forms.TextBox txtAlmuerzo;
        private System.Windows.Forms.Label lblAlmuerzo;
        private System.Windows.Forms.TextBox txtDesayuno;
        private System.Windows.Forms.Label lblDesayuna;
        private System.Windows.Forms.NumericUpDown NumericEmpleados;
        private System.Windows.Forms.Label lblCantEmpleados;
        private System.Windows.Forms.NumericUpDown NumericDias;
        private System.Windows.Forms.Label lblCantDias;
        private System.Windows.Forms.TextBox txtTotalComida;
        private System.Windows.Forms.Label lblTotal2;
        private System.Windows.Forms.Label lblTotalViaticos;
        private System.Windows.Forms.TextBox txtTotalViaticos;
        private System.Windows.Forms.Label lblTotalT;
        private System.Windows.Forms.Label lblHospedaje;
        private System.Windows.Forms.TextBox txtPrecioHabitacion;
        private System.Windows.Forms.Label lblPPH;
        private System.Windows.Forms.NumericUpDown NumericHabitaciones;
        private System.Windows.Forms.Label lblCantHabitaciones;
        private System.Windows.Forms.NumericUpDown NumericNoches;
        private System.Windows.Forms.Label lblNoches;
        private System.Windows.Forms.Button btnHospedaje;
        private System.Windows.Forms.TextBox txtTotalHospedaje;
        private System.Windows.Forms.Label lblTotal3;
        private System.Windows.Forms.Label lblSalarios;
        private System.Windows.Forms.Label lblListaEmpleados;
        private System.Windows.Forms.ComboBox cmbEmpleados;
        private System.Windows.Forms.Button btnSistemas;
        private System.Windows.Forms.TextBox txtSalarios;
        private System.Windows.Forms.Label lblSalariosxHora;
        private System.Windows.Forms.Label lblCantHoras;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblTotal4;
        private System.Windows.Forms.TextBox txtHoras;
    }
}