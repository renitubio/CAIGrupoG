namespace CAIGrupoG.Imposicion.ImpCentroDistribucion
{
    partial class ImposicionDeEncomiendaCDForm
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
            groupBox1 = new GroupBox();
            CuitTxtBox = new TextBox();
            label5 = new Label();
            BuscarClienteBttn = new Button();
            label1 = new Label();
            GuiasAsignadasGroupBox = new GroupBox();
            FinalizaBttn = new Button();
            GuiasGeneradasListView = new ListView();
            columnHeader3 = new ColumnHeader();
            label6 = new Label();
            label8 = new Label();
            label9 = new Label();
            label2 = new Label();
            label3 = new Label();
            CiudadCmb = new ComboBox();
            OpcionesDeEntregaCmb = new ComboBox();
            EncomiendasListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            AñadirBttn = new Button();
            QuitarBttn = new Button();
            label4 = new Label();
            DomicilioTxtBox = new TextBox();
            label7 = new Label();
            CantidadNum = new NumericUpDown();
            DNITxtBox = new TextBox();
            TipoDeCajaCmb = new ComboBox();
            label10 = new Label();
            DomicilioRadioBttn = new RadioButton();
            AgenciaRadioBttn = new RadioButton();
            CancelarBttn = new Button();
            ConfirmarBttn = new Button();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            GuiasAsignadasGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CantidadNum).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CuitTxtBox);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(BuscarClienteBttn);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(7, 11);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(556, 83);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            // 
            // CuitTxtBox
            // 
            CuitTxtBox.Location = new Point(49, 49);
            CuitTxtBox.Margin = new Padding(3, 2, 3, 2);
            CuitTxtBox.Name = "CuitTxtBox";
            CuitTxtBox.Size = new Size(110, 23);
            CuitTxtBox.TabIndex = 7;
            // 
            // label5
            // 
            label5.Font = new Font("Arial", 12F, FontStyle.Bold);
            label5.Location = new Point(6, 17);
            label5.Name = "label5";
            label5.Size = new Size(226, 21);
            label5.TabIndex = 4;
            label5.Text = "Datos del Cliente";
            // 
            // BuscarClienteBttn
            // 
            BuscarClienteBttn.Location = new Point(178, 44);
            BuscarClienteBttn.Margin = new Padding(3, 2, 3, 2);
            BuscarClienteBttn.Name = "BuscarClienteBttn";
            BuscarClienteBttn.Size = new Size(118, 32);
            BuscarClienteBttn.TabIndex = 0;
            BuscarClienteBttn.Text = "Buscar Cliente";
            BuscarClienteBttn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(6, 53);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 0;
            label1.Text = "CUIT:";
            // 
            // GuiasAsignadasGroupBox
            // 
            GuiasAsignadasGroupBox.Controls.Add(FinalizaBttn);
            GuiasAsignadasGroupBox.Controls.Add(GuiasGeneradasListView);
            GuiasAsignadasGroupBox.Location = new Point(569, 284);
            GuiasAsignadasGroupBox.Name = "GuiasAsignadasGroupBox";
            GuiasAsignadasGroupBox.Size = new Size(408, 243);
            GuiasAsignadasGroupBox.TabIndex = 38;
            GuiasAsignadasGroupBox.TabStop = false;
            GuiasAsignadasGroupBox.Text = "Guias Generadas";
            // 
            // FinalizaBttn
            // 
            FinalizaBttn.Location = new Point(277, 203);
            FinalizaBttn.Margin = new Padding(3, 2, 3, 2);
            FinalizaBttn.Name = "FinalizaBttn";
            FinalizaBttn.Size = new Size(102, 25);
            FinalizaBttn.TabIndex = 32;
            FinalizaBttn.Text = "Finalizar";
            FinalizaBttn.UseVisualStyleBackColor = true;
            // 
            // GuiasGeneradasListView
            // 
            GuiasGeneradasListView.Columns.AddRange(new ColumnHeader[] { columnHeader3 });
            GuiasGeneradasListView.Location = new Point(24, 27);
            GuiasGeneradasListView.Name = "GuiasGeneradasListView";
            GuiasGeneradasListView.Size = new Size(355, 160);
            GuiasGeneradasListView.TabIndex = 33;
            GuiasGeneradasListView.UseCompatibleStateImageBehavior = false;
            GuiasGeneradasListView.View = View.Details;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Guias";
            columnHeader3.Width = 200;
            // 
            // label6
            // 
            label6.Font = new Font("Arial", 12F, FontStyle.Bold);
            label6.Location = new Point(7, 17);
            label6.Name = "label6";
            label6.Size = new Size(309, 27);
            label6.TabIndex = 5;
            label6.Text = "Datos de la encomienda";
            // 
            // label8
            // 
            label8.Location = new Point(10, 206);
            label8.Name = "label8";
            label8.Size = new Size(85, 16);
            label8.TabIndex = 6;
            label8.Text = "Tipo de caja:";
            // 
            // label9
            // 
            label9.Location = new Point(7, 132);
            label9.Name = "label9";
            label9.Size = new Size(132, 19);
            label9.TabIndex = 7;
            label9.Text = "Opciones de entrega:";
            // 
            // label2
            // 
            label2.Location = new Point(10, 240);
            label2.Name = "label2";
            label2.Size = new Size(85, 16);
            label2.TabIndex = 12;
            label2.Text = "Cantidad:";
            // 
            // label3
            // 
            label3.Location = new Point(7, 44);
            label3.Name = "label3";
            label3.Size = new Size(132, 19);
            label3.TabIndex = 14;
            label3.Text = "Ciudad:";
            // 
            // CiudadCmb
            // 
            CiudadCmb.FormattingEnabled = true;
            CiudadCmb.Location = new Point(164, 46);
            CiudadCmb.Margin = new Padding(3, 2, 3, 2);
            CiudadCmb.Name = "CiudadCmb";
            CiudadCmb.Size = new Size(110, 23);
            CiudadCmb.TabIndex = 17;
            // 
            // OpcionesDeEntregaCmb
            // 
            OpcionesDeEntregaCmb.FormattingEnabled = true;
            OpcionesDeEntregaCmb.Location = new Point(164, 132);
            OpcionesDeEntregaCmb.Margin = new Padding(3, 2, 3, 2);
            OpcionesDeEntregaCmb.Name = "OpcionesDeEntregaCmb";
            OpcionesDeEntregaCmb.Size = new Size(110, 23);
            OpcionesDeEntregaCmb.TabIndex = 18;
            OpcionesDeEntregaCmb.SelectedIndexChanged += OpcionesDeEntregaCmb_SelectedIndexChanged;
            // 
            // EncomiendasListView
            // 
            EncomiendasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            EncomiendasListView.Location = new Point(25, 306);
            EncomiendasListView.Name = "EncomiendasListView";
            EncomiendasListView.Size = new Size(248, 97);
            EncomiendasListView.TabIndex = 19;
            EncomiendasListView.UseCompatibleStateImageBehavior = false;
            EncomiendasListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tipo de Caja";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Cantidad";
            columnHeader2.Width = 100;
            // 
            // AñadirBttn
            // 
            AñadirBttn.Location = new Point(291, 236);
            AñadirBttn.Margin = new Padding(3, 2, 3, 2);
            AñadirBttn.Name = "AñadirBttn";
            AñadirBttn.Size = new Size(101, 25);
            AñadirBttn.TabIndex = 24;
            AñadirBttn.Text = "Añadir";
            AñadirBttn.UseVisualStyleBackColor = true;
            // 
            // QuitarBttn
            // 
            QuitarBttn.Location = new Point(291, 306);
            QuitarBttn.Margin = new Padding(3, 2, 3, 2);
            QuitarBttn.Name = "QuitarBttn";
            QuitarBttn.Size = new Size(101, 25);
            QuitarBttn.TabIndex = 25;
            QuitarBttn.Text = "Quitar";
            QuitarBttn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Location = new Point(308, 132);
            label4.Name = "label4";
            label4.Size = new Size(74, 19);
            label4.TabIndex = 26;
            label4.Text = "Domicilio:";
            // 
            // DomicilioTxtBox
            // 
            DomicilioTxtBox.Location = new Point(387, 130);
            DomicilioTxtBox.Margin = new Padding(3, 2, 3, 2);
            DomicilioTxtBox.Name = "DomicilioTxtBox";
            DomicilioTxtBox.Size = new Size(110, 23);
            DomicilioTxtBox.TabIndex = 27;
            // 
            // label7
            // 
            label7.Location = new Point(7, 171);
            label7.Name = "label7";
            label7.Size = new Size(149, 17);
            label7.TabIndex = 28;
            label7.Text = "DNI autorizado a retirar:";
            // 
            // CantidadNum
            // 
            CantidadNum.Location = new Point(164, 238);
            CantidadNum.Margin = new Padding(3, 2, 3, 2);
            CantidadNum.Name = "CantidadNum";
            CantidadNum.Size = new Size(109, 23);
            CantidadNum.TabIndex = 31;
            // 
            // DNITxtBox
            // 
            DNITxtBox.Location = new Point(164, 169);
            DNITxtBox.Margin = new Padding(3, 2, 3, 2);
            DNITxtBox.Name = "DNITxtBox";
            DNITxtBox.Size = new Size(110, 23);
            DNITxtBox.TabIndex = 32;
            // 
            // TipoDeCajaCmb
            // 
            TipoDeCajaCmb.FormattingEnabled = true;
            TipoDeCajaCmb.Location = new Point(163, 206);
            TipoDeCajaCmb.Margin = new Padding(3, 2, 3, 2);
            TipoDeCajaCmb.Name = "TipoDeCajaCmb";
            TipoDeCajaCmb.Size = new Size(110, 23);
            TipoDeCajaCmb.TabIndex = 33;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(25, 283);
            label10.Name = "label10";
            label10.Size = new Size(135, 15);
            label10.TabIndex = 34;
            label10.Text = "Encomiendas a Imponer";
            // 
            // DomicilioRadioBttn
            // 
            DomicilioRadioBttn.AutoSize = true;
            DomicilioRadioBttn.Location = new Point(306, 91);
            DomicilioRadioBttn.Name = "DomicilioRadioBttn";
            DomicilioRadioBttn.Size = new Size(76, 19);
            DomicilioRadioBttn.TabIndex = 35;
            DomicilioRadioBttn.TabStop = true;
            DomicilioRadioBttn.Text = "Domicilio";
            DomicilioRadioBttn.UseVisualStyleBackColor = true;
            // 
            // AgenciaRadioBttn
            // 
            AgenciaRadioBttn.AutoSize = true;
            AgenciaRadioBttn.Location = new Point(7, 91);
            AgenciaRadioBttn.Name = "AgenciaRadioBttn";
            AgenciaRadioBttn.Size = new Size(95, 19);
            AgenciaRadioBttn.TabIndex = 36;
            AgenciaRadioBttn.TabStop = true;
            AgenciaRadioBttn.Text = "Agencia / CD";
            AgenciaRadioBttn.UseVisualStyleBackColor = true;
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(430, 420);
            CancelarBttn.Margin = new Padding(3, 2, 3, 2);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(102, 25);
            CancelarBttn.TabIndex = 32;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // ConfirmarBttn
            // 
            ConfirmarBttn.Location = new Point(306, 420);
            ConfirmarBttn.Margin = new Padding(3, 2, 3, 2);
            ConfirmarBttn.Name = "ConfirmarBttn";
            ConfirmarBttn.Size = new Size(119, 25);
            ConfirmarBttn.TabIndex = 31;
            ConfirmarBttn.Text = "Confirmar/ Admitir";
            ConfirmarBttn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ConfirmarBttn);
            groupBox2.Controls.Add(CancelarBttn);
            groupBox2.Controls.Add(AgenciaRadioBttn);
            groupBox2.Controls.Add(DomicilioRadioBttn);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(TipoDeCajaCmb);
            groupBox2.Controls.Add(DNITxtBox);
            groupBox2.Controls.Add(CantidadNum);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(DomicilioTxtBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(QuitarBttn);
            groupBox2.Controls.Add(AñadirBttn);
            groupBox2.Controls.Add(EncomiendasListView);
            groupBox2.Controls.Add(OpcionesDeEntregaCmb);
            groupBox2.Controls.Add(CiudadCmb);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(7, 98);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(556, 454);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            // 
            // ImposicionDeEncomiendaCDForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(570, 561);
            Controls.Add(GuiasAsignadasGroupBox);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(2);
            Name = "ImposicionDeEncomiendaCDForm";
            Text = "Imposición de Encomienda - CD";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            GuiasAsignadasGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CantidadNum).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox CuitTxtBox;
        private Label label5;
        private Button BuscarClienteBttn;
        private Label label1;
        private GroupBox GuiasAsignadasGroupBox;
        private ListView GuiasGeneradasListView;
        private ColumnHeader columnHeader3;
        private Button FinalizaBttn;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label2;
        private Label label3;
        private ComboBox CiudadCmb;
        private ComboBox OpcionesDeEntregaCmb;
        private ListView EncomiendasListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button AñadirBttn;
        private Button QuitarBttn;
        private Label label4;
        private TextBox DomicilioTxtBox;
        private Label label7;
        private NumericUpDown CantidadNum;
        private TextBox DNITxtBox;
        private ComboBox TipoDeCajaCmb;
        private Label label10;
        private RadioButton DomicilioRadioBttn;
        private RadioButton AgenciaRadioBttn;
        private Button CancelarBttn;
        private Button ConfirmarBttn;
        private GroupBox groupBox2;
    }
}