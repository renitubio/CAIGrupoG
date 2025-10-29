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
            CancelarBttn.Location = new Point(1003, 600);
            CancelarBttn.Margin = new Padding(4, 5, 4, 5);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(107, 38);
            CancelarBttn.TabIndex = 16;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // DesdeTimePicker
            // 
            DesdeTimePicker.Location = new Point(73, 73);
            DesdeTimePicker.Margin = new Padding(4, 5, 4, 5);
            DesdeTimePicker.Name = "DesdeTimePicker";
            DesdeTimePicker.Size = new Size(318, 31);
            DesdeTimePicker.TabIndex = 2;
            // 
            // HastaTimePicker
            // 
            HastaTimePicker.Location = new Point(401, 73);
            HastaTimePicker.Margin = new Padding(4, 5, 4, 5);
            HastaTimePicker.Name = "HastaTimePicker";
            HastaTimePicker.Size = new Size(311, 31);
            HastaTimePicker.TabIndex = 3;
            // 
            // ResultadoListView
            // 
            ResultadoListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader5 });
            ResultadoListView.Location = new Point(73, 141);
            ResultadoListView.Margin = new Padding(4, 5, 4, 5);
            ResultadoListView.Name = "ResultadoListView";
            ResultadoListView.Size = new Size(777, 286);
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
            groupBox2.Location = new Point(50, 18);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1060, 543);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            // 
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(743, 66);
            BuscarBttn.Margin = new Padding(4, 5, 4, 5);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(107, 38);
            BuscarBttn.TabIndex = 7;
            BuscarBttn.Text = "Buscar";
            BuscarBttn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(401, 30);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(61, 25);
            label2.TabIndex = 6;
            label2.Text = "Hasta:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 30);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 5;
            label1.Text = "Desde:";
            // 
            // CostosVsVentasForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 668);
            Controls.Add(CancelarBttn);
            Controls.Add(groupBox2);
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