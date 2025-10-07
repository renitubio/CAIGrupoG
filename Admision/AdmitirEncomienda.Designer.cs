namespace CAIGrupoG.Admisión
{
    partial class AdmitirEncomienda
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
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(29, 121);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(155, 46);
            button1.TabIndex = 0;
            button1.Text = "Buscar Guía";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(29, 66);
            textBox1.Margin = new Padding(4, 4, 4, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(360, 31);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 25);
            label1.TabIndex = 2;
            label1.Text = "Número de Guía";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 206);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(106, 25);
            label2.TabIndex = 3;
            label2.Text = "Detalle Guía";
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader6, columnHeader7, columnHeader8 });
            listView1.Location = new Point(29, 257);
            listView1.Margin = new Padding(4, 4, 4, 4);
            listView1.Name = "listView1";
            listView1.Size = new Size(950, 150);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "N° Guía";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Tipo";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "ID Cliente";
            columnHeader3.Width = 150;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Nombre Destinatario";
            columnHeader6.Width = 160;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Dirección Destinatario";
            columnHeader7.Width = 160;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Tel. Destinatario";
            columnHeader8.Width = 160;
            // 
            // button2
            // 
            button2.Location = new Point(498, 535);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(155, 46);
            button2.TabIndex = 5;
            button2.Text = "Admitir";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(824, 535);
            button3.Margin = new Padding(4, 4, 4, 4);
            button3.Name = "button3";
            button3.Size = new Size(155, 46);
            button3.TabIndex = 6;
            button3.Text = "Rechazar";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(661, 535);
            button4.Margin = new Padding(4);
            button4.Name = "button4";
            button4.Size = new Size(155, 46);
            button4.TabIndex = 7;
            button4.Text = "Modificar";
            button4.UseVisualStyleBackColor = true;
            // 
            // AdmitirEncomienda
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1041, 620);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(listView1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Margin = new Padding(2);
            Name = "AdmitirEncomienda";
            Text = "AdmitirEncomienda";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}