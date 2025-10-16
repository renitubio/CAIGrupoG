namespace CAIGrupoG.EmitirFactura
{
    partial class EmitirFacturaForm
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
            ListViewItem listViewItem2 = new ListViewItem("");
            label1 = new Label();
            CancelarBttn = new Button();
            EmitirBttn = new Button();
            GuiasListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            BuscarBttn = new Button();
            CuitText = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 103);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 1;
            label1.Text = "Listado de Guías";
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(474, 350);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(75, 23);
            CancelarBttn.TabIndex = 4;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // EmitirBttn
            // 
            EmitirBttn.Location = new Point(393, 350);
            EmitirBttn.Name = "EmitirBttn";
            EmitirBttn.Size = new Size(75, 23);
            EmitirBttn.TabIndex = 5;
            EmitirBttn.Text = "Emitir";
            EmitirBttn.UseVisualStyleBackColor = true;
            // 
            // GuiasListView
            // 
            GuiasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader3, columnHeader4, columnHeader5 });
            listViewItem2.StateImageIndex = 0;
            GuiasListView.Items.AddRange(new ListViewItem[] { listViewItem2 });
            GuiasListView.Location = new Point(38, 134);
            GuiasListView.Name = "GuiasListView";
            GuiasListView.Size = new Size(511, 184);
            GuiasListView.TabIndex = 6;
            GuiasListView.UseCompatibleStateImageBehavior = false;
            GuiasListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Guía";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Estado";
            columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Razon Social";
            columnHeader4.Width = 180;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Importe";
            columnHeader5.Width = 130;
            // 
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(245, 39);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(75, 23);
            BuscarBttn.TabIndex = 7;
            BuscarBttn.Text = "Buscar";
            BuscarBttn.UseVisualStyleBackColor = true;
            // 
            // CuitText
            // 
            CuitText.Location = new Point(76, 39);
            CuitText.Name = "CuitText";
            CuitText.Size = new Size(144, 23);
            CuitText.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 42);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 9;
            label2.Text = "CUIT";
            // 
            // EmitirFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(590, 385);
            Controls.Add(label2);
            Controls.Add(CuitText);
            Controls.Add(BuscarBttn);
            Controls.Add(GuiasListView);
            Controls.Add(EmitirBttn);
            Controls.Add(CancelarBttn);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "EmitirFactura";
            Text = "Facturación";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button CancelarBttn;
        private Button EmitirBttn;
        private ListView GuiasListView;
        private Button BuscarBttn;
        private TextBox CuitText;
        private Label label2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
    }
}