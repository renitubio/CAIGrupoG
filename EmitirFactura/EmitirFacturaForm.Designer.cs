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
            CancelarBttn = new Button();
            GenerarBttn = new Button();
            listView1 = new ListView();
            label1 = new Label();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            SuspendLayout();
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(661, 401);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(75, 23);
            CancelarBttn.TabIndex = 20;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // GenerarBttn
            // 
            GenerarBttn.Location = new Point(555, 401);
            GenerarBttn.Name = "GenerarBttn";
            GenerarBttn.Size = new Size(75, 23);
            GenerarBttn.TabIndex = 19;
            GenerarBttn.Text = "Emitir";
            GenerarBttn.UseVisualStyleBackColor = true;
            GenerarBttn.Click += GenerarBttn_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader5, columnHeader6 });
            listView1.Location = new Point(65, 62);
            listView1.Name = "listView1";
            listView1.Size = new Size(671, 262);
            listView1.TabIndex = 21;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 29);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 22;
            label1.Text = "Guias Rendidas ";
            label1.Click += label1_Click;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Seleccion";
            columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "N°Guia de Seguimiento";
            columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Fecha";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Cliente";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Monto a Facturar";
            columnHeader6.Width = 130;
            // 
            // EmitirFacturaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(CancelarBttn);
            Controls.Add(GenerarBttn);
            Name = "EmitirFacturaForm";
            Text = "Emitir Factura";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CancelarBttn;
        private Button GenerarBttn;
        private ListView listView1;
        private Label label1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
    }
}