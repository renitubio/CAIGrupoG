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
            DniFleteroText = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            AdmisiónListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            label2 = new Label();
            groupBox2 = new GroupBox();
            RetiroListView = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            label3 = new Label();
            AceptarButton = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
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
            // DniFleteroText
            // 
            DniFleteroText.Location = new Point(20, 40);
            DniFleteroText.Margin = new Padding(3, 2, 3, 2);
            DniFleteroText.Name = "DniFleteroText";
            DniFleteroText.Size = new Size(253, 23);
            DniFleteroText.TabIndex = 1;
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
            // groupBox1
            // 
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(AdmisiónListView);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 78);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(758, 181);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Admision - Encomiendas entrantes desde Domicilios particulares y Agencias";
            // 
            // AdmisiónListView
            // 
            AdmisiónListView.CheckBoxes = true;
            AdmisiónListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader12, columnHeader2, columnHeader3, columnHeader6, columnHeader7 });
            AdmisiónListView.FullRowSelect = true;
            AdmisiónListView.Location = new Point(6, 43);
            AdmisiónListView.Margin = new Padding(3, 2, 3, 2);
            AdmisiónListView.Name = "AdmisiónListView";
            AdmisiónListView.Size = new Size(705, 92);
            AdmisiónListView.TabIndex = 6;
            AdmisiónListView.UseCompatibleStateImageBehavior = false;
            AdmisiónListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "N° Guía";
            columnHeader1.Width = 100;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "Estado de Encomienda";
            columnHeader12.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Tipo Paquete";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "CUIT";
            columnHeader3.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Autorizado a retirar";
            columnHeader6.Width = 200;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Dirección Destinatario";
            columnHeader7.Width = 200;
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
            // groupBox2
            // 
            groupBox2.Controls.Add(RetiroListView);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 265);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(758, 181);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Encomiendas salientes, entregadas en Domicilios particulaes y Agencias";
            // 
            // RetiroListView
            // 
            RetiroListView.CheckBoxes = true;
            RetiroListView.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader8, columnHeader9, columnHeader10, columnHeader11 });
            RetiroListView.FullRowSelect = true;
            RetiroListView.Location = new Point(7, 44);
            RetiroListView.Margin = new Padding(3, 2, 3, 2);
            RetiroListView.Name = "RetiroListView";
            RetiroListView.Size = new Size(705, 92);
            RetiroListView.TabIndex = 7;
            RetiroListView.UseCompatibleStateImageBehavior = false;
            RetiroListView.View = View.Details;
            RetiroListView.SelectedIndexChanged += listView2_SelectedIndexChanged;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "N° Guía";
            columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Estado de Encomienda";
            columnHeader5.Width = 200;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Tipo Paquete";
            columnHeader8.Width = 150;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "CUIT";
            columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "Autorizado a retirar";
            columnHeader10.Width = 200;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Dirección Destinatario";
            columnHeader11.Width = 200;
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
            // RendicionFleteroForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 492);
            Controls.Add(AceptarButton);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(DniFleteroText);
            Controls.Add(BuscarGuiasButton);
            Margin = new Padding(1, 1, 1, 1);
            Name = "RendicionFleteroForm";
            Text = "Rendición de Fletero";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BuscarGuiasButton;
        private TextBox DniFleteroText;
        private Label label1;
        private GroupBox groupBox1;
        private ListView AdmisiónListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Label label2;
        private GroupBox groupBox2;
        private Button AceptarButton;
        private Label label3;
        private ColumnHeader columnHeader12;
        private ListView RetiroListView;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
    }
}