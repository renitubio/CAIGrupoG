namespace CAIGrupoG.ResultadoCostosVSVentas
{
    partial class CostosVsVentasForm
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
            CancelarBttn = new Button();
            DesdeTimePicker = new DateTimePicker();
            HastaTimePicker = new DateTimePicker();
            ResultadoListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            groupBox2 = new GroupBox();
            BuscarBttn = new Button();
            label2 = new Label();
            label1 = new Label();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(702, 360);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(75, 23);
            CancelarBttn.TabIndex = 16;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // DesdeTimePicker
            // 
            DesdeTimePicker.Location = new Point(51, 44);
            DesdeTimePicker.Name = "DesdeTimePicker";
            DesdeTimePicker.Size = new Size(224, 23);
            DesdeTimePicker.TabIndex = 2;
            // 
            // HastaTimePicker
            // 
            HastaTimePicker.Location = new Point(281, 44);
            HastaTimePicker.Name = "HastaTimePicker";
            HastaTimePicker.Size = new Size(219, 23);
            HastaTimePicker.TabIndex = 3;
            // 
            // ResultadoListView
            // 
            ResultadoListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader5 });
            ResultadoListView.Location = new Point(50, 86);
            ResultadoListView.Name = "ResultadoListView";
            ResultadoListView.Size = new Size(657, 173);
            ResultadoListView.TabIndex = 4;
            ResultadoListView.UseCompatibleStateImageBehavior = false;
            ResultadoListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Resultado Empresa";
            columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Ventas";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Costos";
            columnHeader3.Width = 150;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Rentabilidad %";
            columnHeader5.Width = 130;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(BuscarBttn);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(ResultadoListView);
            groupBox2.Controls.Add(HastaTimePicker);
            groupBox2.Controls.Add(DesdeTimePicker);
            groupBox2.Location = new Point(35, 11);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2);
            groupBox2.Size = new Size(742, 326);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            // 
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(520, 40);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(75, 23);
            BuscarBttn.TabIndex = 7;
            BuscarBttn.Text = "Buscar";
            BuscarBttn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(281, 18);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 6;
            label2.Text = "Hasta:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 18);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 5;
            label1.Text = "Desde:";
            // 
            // CostosVsVentasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 401);
            Controls.Add(CancelarBttn);
            Controls.Add(groupBox2);
            Margin = new Padding(2);
            Name = "CostosVsVentasForm";
            Text = "Resultado Económico Tutasa";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button CancelarBttn;
        private DateTimePicker DesdeTimePicker;
        private DateTimePicker HastaTimePicker;
        private ListView ResultadoListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader5;
        private GroupBox groupBox2;
        private Label label2;
        private Label label1;
        private Button BuscarBttn;
    }
}