namespace CAIGrupoG.Playero
{
    partial class PlayeroForm
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
            DescargaGpBox = new GroupBox();
            DescargarListView = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            label3 = new Label();
            AceptarBttn = new Button();
            CargaGpBox = new GroupBox();
            CargaListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            label2 = new Label();
            label1 = new Label();
            PatenteTxt = new TextBox();
            BuscarGuiasAsociadasBttn = new Button();
            DescargaGpBox.SuspendLayout();
            CargaGpBox.SuspendLayout();
            SuspendLayout();
            // 
            // DescargaGpBox
            // 
            DescargaGpBox.Controls.Add(DescargarListView);
            DescargaGpBox.Controls.Add(label3);
            DescargaGpBox.Location = new Point(12, 269);
            DescargaGpBox.Name = "DescargaGpBox";
            DescargaGpBox.Size = new Size(807, 181);
            DescargaGpBox.TabIndex = 17;
            DescargaGpBox.TabStop = false;
            DescargaGpBox.Text = "Descarga";
            // 
            // DescargarListView
            // 
            DescargarListView.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6, columnHeader9, columnHeader10 });
            DescargarListView.FullRowSelect = true;
            DescargarListView.Location = new Point(11, 44);
            DescargarListView.Margin = new Padding(3, 2, 3, 2);
            DescargarListView.Name = "DescargarListView";
            DescargarListView.Size = new Size(785, 92);
            DescargarListView.TabIndex = 9;
            DescargarListView.UseCompatibleStateImageBehavior = false;
            DescargarListView.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "N° Guía";
            columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Tipo Paquete";
            columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "CUIT";
            columnHeader6.Width = 100;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "CD Origen";
            columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "CD Destino";
            columnHeader10.Width = 100;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 5;
            label3.Text = "Detalle Guía";
            // 
            // AceptarBttn
            // 
            AceptarBttn.Location = new Point(711, 455);
            AceptarBttn.Margin = new Padding(3, 2, 3, 2);
            AceptarBttn.Name = "AceptarBttn";
            AceptarBttn.Size = new Size(108, 28);
            AceptarBttn.TabIndex = 8;
            AceptarBttn.Text = "Aceptar";
            AceptarBttn.UseVisualStyleBackColor = true;
            // 
            // CargaGpBox
            // 
            CargaGpBox.Controls.Add(CargaListView);
            CargaGpBox.Controls.Add(label2);
            CargaGpBox.Location = new Point(12, 73);
            CargaGpBox.Name = "CargaGpBox";
            CargaGpBox.Size = new Size(807, 181);
            CargaGpBox.TabIndex = 16;
            CargaGpBox.TabStop = false;
            CargaGpBox.Text = "Carga";
            // 
            // CargaListView
            // 
            CargaListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader11, columnHeader12 });
            CargaListView.FullRowSelect = true;
            CargaListView.Location = new Point(6, 43);
            CargaListView.Margin = new Padding(3, 2, 3, 2);
            CargaListView.Name = "CargaListView";
            CargaListView.Size = new Size(785, 92);
            CargaListView.TabIndex = 6;
            CargaListView.UseCompatibleStateImageBehavior = false;
            CargaListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "N° Guía";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Tipo Paquete";
            columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "CUIT";
            columnHeader3.Width = 100;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "CD Origen";
            columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "CD Destino";
            columnHeader12.Width = 100;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 5;
            label2.Text = "Detalle Guía";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 9);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 15;
            label1.Text = "Patente";
            // 
            // PatenteTxt
            // 
            PatenteTxt.Location = new Point(18, 31);
            PatenteTxt.Margin = new Padding(3, 2, 3, 2);
            PatenteTxt.Name = "PatenteTxt";
            PatenteTxt.Size = new Size(253, 23);
            PatenteTxt.TabIndex = 14;
            // 
            // BuscarGuiasAsociadasBttn
            // 
            BuscarGuiasAsociadasBttn.Location = new Point(298, 31);
            BuscarGuiasAsociadasBttn.Margin = new Padding(3, 2, 3, 2);
            BuscarGuiasAsociadasBttn.Name = "BuscarGuiasAsociadasBttn";
            BuscarGuiasAsociadasBttn.Size = new Size(145, 28);
            BuscarGuiasAsociadasBttn.TabIndex = 13;
            BuscarGuiasAsociadasBttn.Text = "Buscar Guías asociadas";
            BuscarGuiasAsociadasBttn.UseVisualStyleBackColor = true;
            // 
            // PlayeroForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(837, 495);
            Controls.Add(DescargaGpBox);
            Controls.Add(AceptarBttn);
            Controls.Add(CargaGpBox);
            Controls.Add(label1);
            Controls.Add(PatenteTxt);
            Controls.Add(BuscarGuiasAsociadasBttn);
            Name = "PlayeroForm";
            Text = "Carga/Descarga - Playero";
            DescargaGpBox.ResumeLayout(false);
            DescargaGpBox.PerformLayout();
            CargaGpBox.ResumeLayout(false);
            CargaGpBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox DescargaGpBox;
        private Button AceptarBttn;
        private Label label3;
        private GroupBox CargaGpBox;
        private ListView CargaListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label label2;
        private Label label1;
        private TextBox PatenteTxt;
        private Button BuscarGuiasAsociadasBttn;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ListView DescargarListView;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
    }
}