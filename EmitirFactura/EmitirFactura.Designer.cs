namespace CAIGrupoG.EmitirFactura
{
    partial class EmitirFactura
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
            button2 = new Button();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewComboBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(701, 300);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Atrás";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(493, 300);
            button2.Name = "button2";
            button2.Size = new Size(202, 23);
            button2.TabIndex = 1;
            button2.Text = "Generar e Imprimir Comprobante";
            button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8 });
            dataGridView1.Location = new Point(0, 147);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(799, 82);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Código";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Servicio";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Cantidad";
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "Precio Unitario";
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            Column5.HeaderText = "Subtotal";
            Column5.Name = "Column5";
            // 
            // Column6
            // 
            Column6.HeaderText = "Alícuota IVA";
            Column6.Name = "Column6";
            // 
            // Column7
            // 
            Column7.HeaderText = "Importe IVA";
            Column7.Name = "Column7";
            // 
            // Column8
            // 
            Column8.HeaderText = "Total";
            Column8.Name = "Column8";
            // 
            // button3
            // 
            button3.Location = new Point(296, 247);
            button3.Name = "button3";
            button3.Size = new Size(202, 23);
            button3.TabIndex = 3;
            button3.Text = "Agregar línea descripción";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 12);
            button4.Name = "button4";
            button4.Size = new Size(172, 23);
            button4.TabIndex = 4;
            button4.Text = "Fecha Comprobante";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(12, 41);
            button5.Name = "button5";
            button5.Size = new Size(172, 23);
            button5.TabIndex = 5;
            button5.Text = "Conceptos a Incluir";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(12, 70);
            button6.Name = "button6";
            button6.Size = new Size(172, 23);
            button6.TabIndex = 6;
            button6.Text = "Condición frente al IVA";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(12, 99);
            button7.Name = "button7";
            button7.Size = new Size(172, 23);
            button7.TabIndex = 7;
            button7.Text = "CUIT/CUIL";
            // 
            // button8
            // 
            button8.Location = new Point(513, 12);
            button8.Name = "button8";
            button8.Size = new Size(170, 23);
            button8.TabIndex = 8;
            button8.Text = "Condiciones de Venta";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(211, 41);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(153, 23);
            comboBox1.TabIndex = 9;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(211, 70);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(153, 23);
            // 
            // textBox1
            // 
            textBox1.Location = new Point(211, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(153, 23);
            textBox1.TabIndex = 11;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(211, 100);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(153, 23);
            textBox2.TabIndex = 12;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(400, 43);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(72, 19);
            checkBox1.TabIndex = 13;
            checkBox1.Text = "Contado";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // EmitirFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "EmitirFactura";
            Text = "EmitirFactura";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewComboBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private TextBox textBox1;
        private TextBox textBox2;
        private CheckBox checkBox1;
    }
}