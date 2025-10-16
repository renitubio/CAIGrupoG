namespace CAIGrupoG.EntregaGuíaCD
{
    partial class EntregaGuíaCDForm
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
            label1 = new Label();
            DNIText = new TextBox();
            BuscarBttn = new Button();
            label2 = new Label();
            CancelarBttn = new Button();
            RetirarBttn = new Button();
            GuiasListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 19);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 0;
            label1.Text = "DNI";
            // 
            // DNIText
            // 
            DNIText.Location = new Point(30, 46);
            DNIText.Name = "DNIText";
            DNIText.Size = new Size(100, 23);
            DNIText.TabIndex = 1;
            // 
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(153, 46);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(97, 23);
            BuscarBttn.TabIndex = 2;
            BuscarBttn.Text = "Buscar";
            BuscarBttn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 90);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 3;
            label2.Text = "Guías Asociadas";
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(672, 392);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(97, 23);
            CancelarBttn.TabIndex = 5;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // RetirarBttn
            // 
            RetirarBttn.Location = new Point(569, 392);
            RetirarBttn.Name = "RetirarBttn";
            RetirarBttn.Size = new Size(97, 23);
            RetirarBttn.TabIndex = 6;
            RetirarBttn.Text = "Retirar";
            RetirarBttn.UseVisualStyleBackColor = true;
            // 
            // GuiasListView
            // 
            GuiasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            GuiasListView.Location = new Point(30, 117);
            GuiasListView.Name = "GuiasListView";
            GuiasListView.Size = new Size(739, 238);
            GuiasListView.TabIndex = 12;
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
            columnHeader2.Width = 280;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Tipo Paquete";
            columnHeader3.Width = 100;
            // 
            // EntregaGuíaCDForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GuiasListView);
            Controls.Add(RetirarBttn);
            Controls.Add(CancelarBttn);
            Controls.Add(label2);
            Controls.Add(BuscarBttn);
            Controls.Add(DNIText);
            Controls.Add(label1);
            Name = "EntregaGuíaCDForm";
            Text = "Entrega Encomienda - CD";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox DNIText;
        private Button BuscarBttn;
        private Label label2;
        private Button CancelarBttn;
        private Button RetirarBttn;
        private ListView GuiasListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
    }
}