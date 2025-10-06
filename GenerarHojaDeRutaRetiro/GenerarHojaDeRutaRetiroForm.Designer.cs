namespace CAIGrupoG.GenerarHojaDeRutaRetiro
{
    partial class GenerarHojaDeRutaRetiroForm
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
            listView1 = new ListView();
            listView2 = new ListView();
            label1 = new Label();
            GenerarBttn = new Button();
            CancelarBttn = new Button();
            AñadirBttn = new Button();
            QuitarBttn = new Button();
            NroGuia = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { NroGuia, columnHeader1, columnHeader2 });
            listView1.Location = new Point(81, 65);
            listView1.Name = "listView1";
            listView1.Size = new Size(238, 294);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // listView2
            // 
            listView2.Location = new Point(438, 65);
            listView2.Name = "listView2";
            listView2.Size = new Size(238, 294);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(81, 32);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 2;
            label1.Text = "Encomiendas Impuestas";
            // 
            // GenerarBttn
            // 
            GenerarBttn.Location = new Point(561, 399);
            GenerarBttn.Name = "GenerarBttn";
            GenerarBttn.Size = new Size(75, 23);
            GenerarBttn.TabIndex = 3;
            GenerarBttn.Text = "Generar";
            GenerarBttn.UseVisualStyleBackColor = true;
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(653, 399);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(75, 23);
            CancelarBttn.TabIndex = 4;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // AñadirBttn
            // 
            AñadirBttn.Location = new Point(342, 127);
            AñadirBttn.Name = "AñadirBttn";
            AñadirBttn.Size = new Size(75, 23);
            AñadirBttn.TabIndex = 5;
            AñadirBttn.Text = "Añadir";
            AñadirBttn.UseVisualStyleBackColor = true;
            // 
            // QuitarBttn
            // 
            QuitarBttn.Location = new Point(342, 183);
            QuitarBttn.Name = "QuitarBttn";
            QuitarBttn.Size = new Size(75, 23);
            QuitarBttn.TabIndex = 6;
            QuitarBttn.Text = "Quitar";
            QuitarBttn.UseVisualStyleBackColor = true;
            // 
            // NroGuia
            // 
            NroGuia.Text = "N°Guia";
            NroGuia.Width = 50;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Direccion de Retiro";
            columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Cliente";
            // 
            // GenerarHojaDeRutaRetiroForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(QuitarBttn);
            Controls.Add(AñadirBttn);
            Controls.Add(CancelarBttn);
            Controls.Add(GenerarBttn);
            Controls.Add(label1);
            Controls.Add(listView2);
            Controls.Add(listView1);
            Name = "GenerarHojaDeRutaRetiroForm";
            Text = "Generar Hoja De Ruta Retiro";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ListView listView2;
        private Label label1;
        private Button GenerarBttn;
        private Button CancelarBttn;
        private Button AñadirBttn;
        private Button QuitarBttn;
        private ColumnHeader NroGuia;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}