namespace CAIGrupoG.EntregaGuíaAgencia
{
    partial class EntregaGuíaAgenciaForm
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
            RetirarBttn = new Button();
            CancelarBttn = new Button();
            GuiasListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            label2 = new Label();
            BuscarBttn = new Button();
            DNIText = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // RetirarBttn
            // 
            RetirarBttn.Location = new Point(570, 400);
            RetirarBttn.Name = "RetirarBttn";
            RetirarBttn.Size = new Size(97, 23);
            RetirarBttn.TabIndex = 13;
            RetirarBttn.Text = "Retirar";
            RetirarBttn.UseVisualStyleBackColor = true;
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(673, 400);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(97, 23);
            CancelarBttn.TabIndex = 12;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // GuiasListView
            // 
            GuiasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            GuiasListView.Location = new Point(31, 125);
            GuiasListView.Name = "GuiasListView";
            GuiasListView.Size = new Size(739, 238);
            GuiasListView.TabIndex = 11;
            GuiasListView.UseCompatibleStateImageBehavior = false;
            GuiasListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "N°Guia";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Estado";
            columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Tipo Paquete";
            columnHeader3.Width = 100;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 98);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 10;
            label2.Text = "Guías Asociadas";
            // 
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(147, 54);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(104, 23);
            BuscarBttn.TabIndex = 9;
            BuscarBttn.Text = "Buscar";
            BuscarBttn.UseVisualStyleBackColor = true;
            // 
            // DNIText
            // 
            DNIText.Location = new Point(31, 54);
            DNIText.Name = "DNIText";
            DNIText.Size = new Size(100, 23);
            DNIText.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 27);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 7;
            label1.Text = "DNI";
            // 
            // EntregaGuíaAgenciaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RetirarBttn);
            Controls.Add(CancelarBttn);
            Controls.Add(GuiasListView);
            Controls.Add(label2);
            Controls.Add(BuscarBttn);
            Controls.Add(DNIText);
            Controls.Add(label1);
            Name = "EntregaGuíaAgenciaForm";
            Text = "Entrega Encomienda - Agencia";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button RetirarBttn;
        private Button CancelarBttn;
        private ListView GuiasListView;
        private Label label2;
        private Button BuscarBttn;
        private TextBox DNIText;
        private Label label1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
    }
}