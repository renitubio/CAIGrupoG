namespace CAIGrupoG.Imposicion.ImpAgencia
{
    partial class ImposicionDeEncomiendaAgenciaForm
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
            ConfirmarBttn = new Button();
            CancelarBttn = new Button();
            DatosClienteGpBox = new GroupBox();
            CuitText = new TextBox();
            label5 = new Label();
            BuscarBttn = new Button();
            label1 = new Label();
            EncomiendaGpBox = new GroupBox();
            EncomiendasListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            AgenciaRdBttn = new RadioButton();
            DomicilioRdBttn = new RadioButton();
            label10 = new Label();
            TipoCajaCmb = new ComboBox();
            DNIText = new TextBox();
            CantidadNum = new NumericUpDown();
            label7 = new Label();
            DomicilioText = new TextBox();
            label4 = new Label();
            QuitarBttn = new Button();
            AñadirBttn = new Button();
            EntregaCmb = new ComboBox();
            CiudadCmb = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            GuiasGeneradasGpBox = new GroupBox();
            button6 = new Button();
            label11 = new Label();
            GuíasGeneradasListView = new ListBox();
            DatosClienteGpBox.SuspendLayout();
            EncomiendaGpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CantidadNum).BeginInit();
            GuiasGeneradasGpBox.SuspendLayout();
            SuspendLayout();
            // 
            // ConfirmarBttn
            // 
            ConfirmarBttn.Location = new Point(461, 700);
            ConfirmarBttn.Margin = new Padding(4, 3, 4, 3);
            ConfirmarBttn.Name = "ConfirmarBttn";
            ConfirmarBttn.Size = new Size(146, 42);
            ConfirmarBttn.TabIndex = 31;
            ConfirmarBttn.Text = "Confirmar";
            ConfirmarBttn.UseVisualStyleBackColor = true;
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
            // DatosClienteGpBox
            // 
            DatosClienteGpBox.Controls.Add(CuitText);
            DatosClienteGpBox.Controls.Add(label5);
            DatosClienteGpBox.Controls.Add(BuscarBttn);
            DatosClienteGpBox.Controls.Add(label1);
            DatosClienteGpBox.Location = new Point(17, 15);
            DatosClienteGpBox.Margin = new Padding(4, 3, 4, 3);
            DatosClienteGpBox.Name = "DatosClienteGpBox";
            DatosClienteGpBox.Padding = new Padding(4, 3, 4, 3);
            DatosClienteGpBox.Size = new Size(786, 165);
            DatosClienteGpBox.TabIndex = 29;
            DatosClienteGpBox.TabStop = false;
            // 
            // CuitText
            // 
            CuitText.Location = new Point(70, 82);
            CuitText.Margin = new Padding(4, 3, 4, 3);
            CuitText.Name = "CuitText";
            CuitText.Size = new Size(155, 31);
            CuitText.TabIndex = 7;
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
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(254, 73);
            BuscarBttn.Margin = new Padding(4, 3, 4, 3);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(169, 53);
            BuscarBttn.TabIndex = 0;
            BuscarBttn.Text = "Buscar Cliente";
            BuscarBttn.UseVisualStyleBackColor = true;
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
            // EncomiendaGpBox
            // 
            EncomiendaGpBox.Controls.Add(EncomiendasListView);
            EncomiendaGpBox.Controls.Add(ConfirmarBttn);
            EncomiendaGpBox.Controls.Add(CancelarBttn);
            EncomiendaGpBox.Controls.Add(AgenciaRdBttn);
            EncomiendaGpBox.Controls.Add(DomicilioRdBttn);
            EncomiendaGpBox.Controls.Add(label10);
            EncomiendaGpBox.Controls.Add(TipoCajaCmb);
            EncomiendaGpBox.Controls.Add(DNIText);
            EncomiendaGpBox.Controls.Add(CantidadNum);
            EncomiendaGpBox.Controls.Add(label7);
            EncomiendaGpBox.Controls.Add(DomicilioText);
            EncomiendaGpBox.Controls.Add(label4);
            EncomiendaGpBox.Controls.Add(QuitarBttn);
            EncomiendaGpBox.Controls.Add(AñadirBttn);
            EncomiendaGpBox.Controls.Add(EntregaCmb);
            EncomiendaGpBox.Controls.Add(CiudadCmb);
            EncomiendaGpBox.Controls.Add(label3);
            EncomiendaGpBox.Controls.Add(label2);
            EncomiendaGpBox.Controls.Add(label9);
            EncomiendaGpBox.Controls.Add(label8);
            EncomiendaGpBox.Controls.Add(label6);
            EncomiendaGpBox.Location = new Point(17, 197);
            EncomiendaGpBox.Margin = new Padding(4, 3, 4, 3);
            EncomiendaGpBox.Name = "EncomiendaGpBox";
            EncomiendaGpBox.Padding = new Padding(4, 3, 4, 3);
            EncomiendaGpBox.Size = new Size(786, 757);
            EncomiendaGpBox.TabIndex = 33;
            EncomiendaGpBox.TabStop = false;
            // 
            // EncomiendasListView
            // 
            EncomiendasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            EncomiendasListView.Location = new Point(34, 510);
            EncomiendasListView.Margin = new Padding(4, 5, 4, 5);
            EncomiendasListView.Name = "EncomiendasListView";
            EncomiendasListView.Size = new Size(353, 159);
            EncomiendasListView.TabIndex = 37;
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
            // AgenciaRdBttn
            // 
            AgenciaRdBttn.AutoSize = true;
            AgenciaRdBttn.Location = new Point(10, 153);
            AgenciaRdBttn.Margin = new Padding(4, 5, 4, 5);
            AgenciaRdBttn.Name = "AgenciaRdBttn";
            AgenciaRdBttn.Size = new Size(141, 29);
            AgenciaRdBttn.TabIndex = 36;
            AgenciaRdBttn.TabStop = true;
            AgenciaRdBttn.Text = "Agencia / CD";
            AgenciaRdBttn.UseVisualStyleBackColor = true;
            // 
            // DomicilioRdBttn
            // 
            DomicilioRdBttn.AutoSize = true;
            DomicilioRdBttn.Location = new Point(437, 153);
            DomicilioRdBttn.Margin = new Padding(4, 5, 4, 5);
            DomicilioRdBttn.Name = "DomicilioRdBttn";
            DomicilioRdBttn.Size = new Size(112, 29);
            DomicilioRdBttn.TabIndex = 35;
            DomicilioRdBttn.TabStop = true;
            DomicilioRdBttn.Text = "Domicilio";
            DomicilioRdBttn.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(36, 473);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(204, 25);
            label10.TabIndex = 34;
            label10.Text = "Encomiendas a Imponer";
            // 
            // TipoCajaCmb
            // 
            TipoCajaCmb.FormattingEnabled = true;
            TipoCajaCmb.Location = new Point(233, 343);
            TipoCajaCmb.Margin = new Padding(4, 3, 4, 3);
            TipoCajaCmb.Name = "TipoCajaCmb";
            TipoCajaCmb.Size = new Size(155, 33);
            TipoCajaCmb.TabIndex = 33;
            // 
            // DNIText
            // 
            DNIText.Location = new Point(234, 282);
            DNIText.Margin = new Padding(4, 3, 4, 3);
            DNIText.Name = "DNIText";
            DNIText.Size = new Size(155, 31);
            DNIText.TabIndex = 32;
            // 
            // CantidadNum
            // 
            CantidadNum.Location = new Point(234, 397);
            CantidadNum.Margin = new Padding(4, 3, 4, 3);
            CantidadNum.Name = "CantidadNum";
            CantidadNum.Size = new Size(156, 31);
            CantidadNum.TabIndex = 31;
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
            // DomicilioText
            // 
            DomicilioText.Location = new Point(553, 217);
            DomicilioText.Margin = new Padding(4, 3, 4, 3);
            DomicilioText.Name = "DomicilioText";
            DomicilioText.Size = new Size(155, 31);
            DomicilioText.TabIndex = 27;
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
            // EntregaCmb
            // 
            EntregaCmb.FormattingEnabled = true;
            EntregaCmb.Location = new Point(234, 220);
            EntregaCmb.Margin = new Padding(4, 3, 4, 3);
            EntregaCmb.Name = "EntregaCmb";
            EntregaCmb.Size = new Size(155, 33);
            EntregaCmb.TabIndex = 18;
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
            // label3
            // 
            label3.Location = new Point(10, 73);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(189, 32);
            label3.TabIndex = 14;
            label3.Text = "Ciudad:";
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
            // label9
            // 
            label9.Location = new Point(10, 220);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(189, 32);
            label9.TabIndex = 7;
            label9.Text = "Opciones de entrega:";
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
            // GuiasGeneradasGpBox
            // 
            GuiasGeneradasGpBox.Controls.Add(button6);
            GuiasGeneradasGpBox.Controls.Add(label11);
            GuiasGeneradasGpBox.Controls.Add(GuíasGeneradasListView);
            GuiasGeneradasGpBox.Location = new Point(811, 721);
            GuiasGeneradasGpBox.Margin = new Padding(4, 5, 4, 5);
            GuiasGeneradasGpBox.Name = "GuiasGeneradasGpBox";
            GuiasGeneradasGpBox.Padding = new Padding(4, 5, 4, 5);
            GuiasGeneradasGpBox.Size = new Size(583, 613);
            GuiasGeneradasGpBox.TabIndex = 38;
            GuiasGeneradasGpBox.TabStop = false;
            GuiasGeneradasGpBox.Text = "Guías de Encomiendas";
            // 
            // button6
            // 
            button6.Location = new Point(379, 530);
            button6.Margin = new Padding(4, 3, 4, 3);
            button6.Name = "button6";
            button6.Size = new Size(146, 42);
            button6.TabIndex = 32;
            button6.Text = "Finalizar";
            button6.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(70, 48);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(271, 25);
            label11.TabIndex = 1;
            label11.Text = "Numeros de tracking generados:";
            // 
            // GuíasGeneradasListView
            // 
            GuíasGeneradasListView.FormattingEnabled = true;
            GuíasGeneradasListView.Location = new Point(56, 93);
            GuíasGeneradasListView.Margin = new Padding(4, 5, 4, 5);
            GuíasGeneradasListView.Name = "GuíasGeneradasListView";
            GuíasGeneradasListView.Size = new Size(451, 404);
            GuíasGeneradasListView.TabIndex = 0;
            // 
            // ImposicionDeEncomiendaAgenciaForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(817, 972);
            Controls.Add(GuiasGeneradasGpBox);
            Controls.Add(EncomiendaGpBox);
            Controls.Add(DatosClienteGpBox);
            Name = "ImposicionDeEncomiendaAgenciaForm";
            Text = "Imposicion de Encomienda - Agencia";
            Load += ImposicionAgenciaForm_Load;
            DatosClienteGpBox.ResumeLayout(false);
            DatosClienteGpBox.PerformLayout();
            EncomiendaGpBox.ResumeLayout(false);
            EncomiendaGpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CantidadNum).EndInit();
            GuiasGeneradasGpBox.ResumeLayout(false);
            GuiasGeneradasGpBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button ConfirmarBttn;
        private Button CancelarBttn;
        private GroupBox DatosClienteGpBox;
        private TextBox CuitText;
        private Label label5;
        private Button BuscarBttn;
        private Label label1;
        private GroupBox EncomiendaGpBox;
        private NumericUpDown CantidadNum;
        private Label label7;
        private TextBox DomicilioText;
        private Label label4;
        private Button QuitarBttn;
        private Button AñadirBttn;
        private ComboBox EntregaCmb;
        private ComboBox CiudadCmb;
        private Label label3;
        private Label label2;
        private Label label9;
        private Label label8;
        private Label label6;
        private TextBox DNIText;
        private ComboBox TipoCajaCmb;
        private Label label10;
        private RadioButton AgenciaRdBttn;
        private RadioButton DomicilioRdBttn;
        private ListView EncomiendasListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ListBox GuíasGeneradasListView;
        private Label label11;
        private Button button6;
        private GroupBox GuiasGeneradasGpBox;
    }
}