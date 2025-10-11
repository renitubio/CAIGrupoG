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
            groupBox1.Location = new Point(10, 18);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(794, 138);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            // 
            // CuitTxtBox
            // 
            CuitTxtBox.Location = new Point(70, 82);
            CuitTxtBox.Margin = new Padding(4, 3, 4, 3);
            CuitTxtBox.Name = "CuitTxtBox";
            CuitTxtBox.Size = new Size(155, 31);
            CuitTxtBox.TabIndex = 7;
            // 
            // label5
            // 
            label5.Font = new Font("Arial", 12F, FontStyle.Bold);
            label5.Location = new Point(9, 28);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(323, 35);
            label5.TabIndex = 4;
            label5.Text = "Datos del Cliente";
            // 
            // BuscarClienteBttn
            // 
            BuscarClienteBttn.Location = new Point(254, 73);
            BuscarClienteBttn.Margin = new Padding(4, 3, 4, 3);
            BuscarClienteBttn.Name = "BuscarClienteBttn";
            BuscarClienteBttn.Size = new Size(169, 53);
            BuscarClienteBttn.TabIndex = 0;
            BuscarClienteBttn.Text = "Buscar Cliente";
            BuscarClienteBttn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(9, 88);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(99, 32);
            label1.TabIndex = 0;
            label1.Text = "CUIT:";
            // 
            // GuiasAsignadasGroupBox
            // 
            GuiasAsignadasGroupBox.Controls.Add(FinalizaBttn);
            GuiasAsignadasGroupBox.Controls.Add(GuiasGeneradasListView);
            GuiasAsignadasGroupBox.Location = new Point(17, 10);
            GuiasAsignadasGroupBox.Margin = new Padding(4, 5, 4, 5);
            GuiasAsignadasGroupBox.Name = "GuiasAsignadasGroupBox";
            GuiasAsignadasGroupBox.Padding = new Padding(4, 5, 4, 5);
            GuiasAsignadasGroupBox.Size = new Size(583, 405);
            GuiasAsignadasGroupBox.TabIndex = 38;
            GuiasAsignadasGroupBox.TabStop = false;
            GuiasAsignadasGroupBox.Text = "Guias Generadas";
            // 
            // FinalizaBttn
            // 
            FinalizaBttn.Location = new Point(396, 338);
            FinalizaBttn.Margin = new Padding(4, 3, 4, 3);
            FinalizaBttn.Name = "FinalizaBttn";
            FinalizaBttn.Size = new Size(146, 42);
            FinalizaBttn.TabIndex = 32;
            FinalizaBttn.Text = "Finalizar";
            FinalizaBttn.UseVisualStyleBackColor = true;
            // 
            // GuiasGeneradasListView
            // 
            GuiasGeneradasListView.Columns.AddRange(new ColumnHeader[] { columnHeader3 });
            GuiasGeneradasListView.Location = new Point(34, 45);
            GuiasGeneradasListView.Margin = new Padding(4, 5, 4, 5);
            GuiasGeneradasListView.Name = "GuiasGeneradasListView";
            GuiasGeneradasListView.Size = new Size(505, 264);
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
            label6.Location = new Point(10, 28);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(441, 45);
            label6.TabIndex = 5;
            label6.Text = "Datos de la encomienda";
            // 
            // label8
            // 
            label8.Location = new Point(14, 343);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(121, 27);
            label8.TabIndex = 6;
            label8.Text = "Tipo de caja:";
            // 
            // label9
            // 
            label9.Location = new Point(10, 220);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(189, 32);
            label9.TabIndex = 7;
            label9.Text = "Opciones de entrega:";
            // 
            // label2
            // 
            label2.Location = new Point(14, 400);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(121, 27);
            label2.TabIndex = 12;
            label2.Text = "Cantidad:";
            // 
            // label3
            // 
            label3.Location = new Point(10, 73);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(189, 32);
            label3.TabIndex = 14;
            label3.Text = "Ciudad:";
            // 
            // CiudadCmb
            // 
            CiudadCmb.FormattingEnabled = true;
            CiudadCmb.Location = new Point(234, 77);
            CiudadCmb.Margin = new Padding(4, 3, 4, 3);
            CiudadCmb.Name = "CiudadCmb";
            CiudadCmb.Size = new Size(155, 33);
            CiudadCmb.TabIndex = 17;
            // 
            // OpcionesDeEntregaCmb
            // 
            OpcionesDeEntregaCmb.FormattingEnabled = true;
            OpcionesDeEntregaCmb.Location = new Point(234, 220);
            OpcionesDeEntregaCmb.Margin = new Padding(4, 3, 4, 3);
            OpcionesDeEntregaCmb.Name = "OpcionesDeEntregaCmb";
            OpcionesDeEntregaCmb.Size = new Size(155, 33);
            OpcionesDeEntregaCmb.TabIndex = 18;
            // 
            // EncomiendasListView
            // 
            EncomiendasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            EncomiendasListView.Location = new Point(36, 510);
            EncomiendasListView.Margin = new Padding(4, 5, 4, 5);
            EncomiendasListView.Name = "EncomiendasListView";
            EncomiendasListView.Size = new Size(353, 159);
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
            AñadirBttn.Location = new Point(416, 393);
            AñadirBttn.Margin = new Padding(4, 3, 4, 3);
            AñadirBttn.Name = "AñadirBttn";
            AñadirBttn.Size = new Size(144, 42);
            AñadirBttn.TabIndex = 24;
            AñadirBttn.Text = "Añadir";
            AñadirBttn.UseVisualStyleBackColor = true;
            // 
            // QuitarBttn
            // 
            QuitarBttn.Location = new Point(416, 510);
            QuitarBttn.Margin = new Padding(4, 3, 4, 3);
            QuitarBttn.Name = "QuitarBttn";
            QuitarBttn.Size = new Size(144, 42);
            QuitarBttn.TabIndex = 25;
            QuitarBttn.Text = "Quitar";
            QuitarBttn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Location = new Point(440, 220);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(106, 32);
            label4.TabIndex = 26;
            label4.Text = "Domicilio:";
            // 
            // DomicilioTxtBox
            // 
            DomicilioTxtBox.Location = new Point(553, 217);
            DomicilioTxtBox.Margin = new Padding(4, 3, 4, 3);
            DomicilioTxtBox.Name = "DomicilioTxtBox";
            DomicilioTxtBox.Size = new Size(155, 31);
            DomicilioTxtBox.TabIndex = 27;
            // 
            // label7
            // 
            label7.Location = new Point(10, 285);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(213, 28);
            label7.TabIndex = 28;
            label7.Text = "DNI autorizado a retirar:";
            // 
            // CantidadNum
            // 
            CantidadNum.Location = new Point(234, 397);
            CantidadNum.Margin = new Padding(4, 3, 4, 3);
            CantidadNum.Name = "CantidadNum";
            CantidadNum.Size = new Size(156, 31);
            CantidadNum.TabIndex = 31;
            // 
            // DNITxtBox
            // 
            DNITxtBox.Location = new Point(234, 282);
            DNITxtBox.Margin = new Padding(4, 3, 4, 3);
            DNITxtBox.Name = "DNITxtBox";
            DNITxtBox.Size = new Size(155, 31);
            DNITxtBox.TabIndex = 32;
            // 
            // TipoDeCajaCmb
            // 
            TipoDeCajaCmb.FormattingEnabled = true;
            TipoDeCajaCmb.Location = new Point(233, 343);
            TipoDeCajaCmb.Margin = new Padding(4, 3, 4, 3);
            TipoDeCajaCmb.Name = "TipoDeCajaCmb";
            TipoDeCajaCmb.Size = new Size(155, 33);
            TipoDeCajaCmb.TabIndex = 33;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(36, 472);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(204, 25);
            label10.TabIndex = 34;
            label10.Text = "Encomiendas a Imponer";
            // 
            // DomicilioRadioBttn
            // 
            DomicilioRadioBttn.AutoSize = true;
            DomicilioRadioBttn.Location = new Point(437, 152);
            DomicilioRadioBttn.Margin = new Padding(4, 5, 4, 5);
            DomicilioRadioBttn.Name = "DomicilioRadioBttn";
            DomicilioRadioBttn.Size = new Size(112, 29);
            DomicilioRadioBttn.TabIndex = 35;
            DomicilioRadioBttn.TabStop = true;
            DomicilioRadioBttn.Text = "Domicilio";
            DomicilioRadioBttn.UseVisualStyleBackColor = true;
            // 
            // AgenciaRadioBttn
            // 
            AgenciaRadioBttn.AutoSize = true;
            AgenciaRadioBttn.Location = new Point(10, 152);
            AgenciaRadioBttn.Margin = new Padding(4, 5, 4, 5);
            AgenciaRadioBttn.Name = "AgenciaRadioBttn";
            AgenciaRadioBttn.Size = new Size(141, 29);
            AgenciaRadioBttn.TabIndex = 36;
            AgenciaRadioBttn.TabStop = true;
            AgenciaRadioBttn.Text = "Agencia / CD";
            AgenciaRadioBttn.UseVisualStyleBackColor = true;
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(614, 700);
            CancelarBttn.Margin = new Padding(4, 3, 4, 3);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(146, 42);
            CancelarBttn.TabIndex = 32;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // ConfirmarBttn
            // 
            ConfirmarBttn.Location = new Point(437, 700);
            ConfirmarBttn.Margin = new Padding(4, 3, 4, 3);
            ConfirmarBttn.Name = "ConfirmarBttn";
            ConfirmarBttn.Size = new Size(170, 42);
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
            groupBox2.Location = new Point(10, 163);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(794, 757);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            // 
            // ImposicionDeEncomiendaCDForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 935);
            Controls.Add(GuiasAsignadasGroupBox);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
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