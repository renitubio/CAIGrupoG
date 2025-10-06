namespace CAIGrupoG.GenerarHojaDeRutaDistribucion
{
    partial class GenerarHojaDeRutaDistribucionForm
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
            QuitarBttn = new Button();
            AñadirBttn = new Button();
            CancelarBttn = new Button();
            GenerarBttn = new Button();
            label1 = new Label();
            listView2 = new ListView();
            listView1 = new ListView();
            NroGuia = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            SuspendLayout();
            // 
            // QuitarBttn
            // 
            QuitarBttn.Location = new Point(338, 181);
            QuitarBttn.Name = "QuitarBttn";
            QuitarBttn.Size = new Size(75, 23);
            QuitarBttn.TabIndex = 13;
            QuitarBttn.Text = "Quitar";
            QuitarBttn.UseVisualStyleBackColor = true;
            // 
            // AñadirBttn
            // 
            AñadirBttn.Location = new Point(338, 125);
            AñadirBttn.Name = "AñadirBttn";
            AñadirBttn.Size = new Size(75, 23);
            AñadirBttn.TabIndex = 12;
            AñadirBttn.Text = "Añadir";
            AñadirBttn.UseVisualStyleBackColor = true;
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(649, 397);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(75, 23);
            CancelarBttn.TabIndex = 11;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // GenerarBttn
            // 
            GenerarBttn.Location = new Point(557, 397);
            GenerarBttn.Name = "GenerarBttn";
            GenerarBttn.Size = new Size(75, 23);
            GenerarBttn.TabIndex = 10;
            GenerarBttn.Text = "Generar";
            GenerarBttn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(77, 30);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 9;
            label1.Text = "Encomiendas a Distribuir";
            label1.Click += label1_Click;
            // 
            // listView2
            // 
            listView2.Location = new Point(434, 63);
            listView2.Name = "listView2";
            listView2.Size = new Size(238, 294);
            listView2.TabIndex = 8;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { NroGuia, columnHeader1, columnHeader2 });
            listView1.Location = new Point(77, 63);
            listView1.Name = "listView1";
            listView1.Size = new Size(238, 294);
            listView1.TabIndex = 7;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // NroGuia
            // 
            NroGuia.Text = "N°Guia";
            NroGuia.Width = 50;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Direccion de Destino";
            columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Cliente";
            columnHeader2.Width = 50;
            // 
            // GenerarHojaDeRutaDistribucionForm
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
            Name = "GenerarHojaDeRutaDistribucionForm";
            Text = "Generar Hoja De Ruta Distribucion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button QuitarBttn;
        private Button AñadirBttn;
        private Button CancelarBttn;
        private Button GenerarBttn;
        private Label label1;
        private ListView listView2;
        private ListView listView1;
        private ColumnHeader NroGuia;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}