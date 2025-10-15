namespace CAIGrupoG.Admisión
{
    partial class RendicionFleteroForm
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
            BuscarGuiasButton = new Button();
            DNIText = new TextBox();
            label1 = new Label();
            AdmisionGpBox = new GroupBox();
            label2 = new Label();
            RetiroGpBox = new GroupBox();
            RetiroListView = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            label3 = new Label();
            AceptarButton = new Button();
            AdmisionListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            AdmisionGpBox.SuspendLayout();
            RetiroGpBox.SuspendLayout();
            SuspendLayout();
            // 
            // BuscarGuiasButton
            // 
            BuscarGuiasButton.Location = new Point(279, 36);
            BuscarGuiasButton.Margin = new Padding(3, 2, 3, 2);
            BuscarGuiasButton.Name = "BuscarGuiasButton";
            BuscarGuiasButton.Size = new Size(145, 28);
            BuscarGuiasButton.TabIndex = 0;
            BuscarGuiasButton.Text = "Buscar Guías asociadas";
            BuscarGuiasButton.UseVisualStyleBackColor = true;
            // 
            // DNIText
            // 
            DNIText.Location = new Point(20, 40);
            DNIText.Margin = new Padding(3, 2, 3, 2);
            DNIText.Name = "DNIText";
            DNIText.Size = new Size(253, 23);
            DNIText.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 14);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 2;
            label1.Text = "DNI Fletero";
            // 
            // AdmisionGpBox
            // 
            AdmisionGpBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdmisionGpBox.Controls.Add(AdmisionListView);
            AdmisionGpBox.Controls.Add(label2);
            AdmisionGpBox.Location = new Point(12, 78);
            AdmisionGpBox.Name = "AdmisionGpBox";
            AdmisionGpBox.Size = new Size(758, 181);
            AdmisionGpBox.TabIndex = 11;
            AdmisionGpBox.TabStop = false;
            AdmisionGpBox.Text = "Admision - Encomiendas Entrantes desde Domicilios/ Agencias";
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
            // RetiroGpBox
            // 
            RetiroGpBox.Controls.Add(RetiroListView);
            RetiroGpBox.Controls.Add(label3);
            RetiroGpBox.Location = new Point(12, 265);
            RetiroGpBox.Name = "RetiroGpBox";
            RetiroGpBox.Size = new Size(758, 181);
            RetiroGpBox.TabIndex = 12;
            RetiroGpBox.TabStop = false;
            RetiroGpBox.Text = "Encomiendas Salientes hacia Domicilios/ Agencias";
            // 
            // RetiroListView
            // 
            RetiroListView.CheckBoxes = true;
            RetiroListView.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader8, columnHeader9, columnHeader10, columnHeader11 });
            RetiroListView.FullRowSelect = true;
            RetiroListView.Location = new Point(7, 44);
            RetiroListView.Margin = new Padding(3, 2, 3, 2);
            RetiroListView.Name = "RetiroListView";
            RetiroListView.Size = new Size(745, 92);
            RetiroListView.TabIndex = 7;
            RetiroListView.UseCompatibleStateImageBehavior = false;
            RetiroListView.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "N° Guía";
            columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Estado de Encomienda";
            columnHeader5.Width = 140;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Tipo Paquete";
            columnHeader8.Width = 80;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "CUIT";
            columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "Autorizado a retirar";
            columnHeader10.Width = 130;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Destino";
            columnHeader11.Width = 180;
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
            // AceptarButton
            // 
            AceptarButton.Location = new Point(662, 457);
            AceptarButton.Margin = new Padding(3, 2, 3, 2);
            AceptarButton.Name = "AceptarButton";
            AceptarButton.Size = new Size(108, 28);
            AceptarButton.TabIndex = 8;
            AceptarButton.Text = "Aceptar";
            AceptarButton.UseVisualStyleBackColor = true;
            // 
            // AdmisionListView
            // 
            AdmisionListView.CheckBoxes = true;
            AdmisionListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader6, columnHeader7, columnHeader12 });
            AdmisionListView.FullRowSelect = true;
            AdmisionListView.Location = new Point(6, 45);
            AdmisionListView.Margin = new Padding(3, 2, 3, 2);
            AdmisionListView.Name = "AdmisionListView";
            AdmisionListView.Size = new Size(745, 92);
            AdmisionListView.TabIndex = 8;
            AdmisionListView.UseCompatibleStateImageBehavior = false;
            AdmisionListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "N° Guía";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Estado de Encomienda";
            columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Tipo Paquete";
            columnHeader3.Width = 80;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "CUIT";
            columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Autorizado a retirar";
            columnHeader7.Width = 130;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "Destino";
            columnHeader12.Width = 180;
            // 
            // RendicionFleteroForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 492);
            Controls.Add(AceptarButton);
            Controls.Add(RetiroGpBox);
            Controls.Add(AdmisionGpBox);
            Controls.Add(label1);
            Controls.Add(DNIText);
            Controls.Add(BuscarGuiasButton);
            Margin = new Padding(1);
            Name = "RendicionFleteroForm";
            Text = "Rendición de Fletero";
            AdmisionGpBox.ResumeLayout(false);
            AdmisionGpBox.PerformLayout();
            RetiroGpBox.ResumeLayout(false);
            RetiroGpBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BuscarGuiasButton;
        private TextBox DNIText;
        private Label label1;
        private GroupBox AdmisionGpBox;
        private Label label2;
        private GroupBox RetiroGpBox;
        private Button AceptarButton;
        private Label label3;
        private ListView RetiroListView;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ListView AdmisionListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader12;
    }
}