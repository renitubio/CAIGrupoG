namespace CAIGrupoG.Imposicion.ImpCallCenter
{
    partial class ImposicionDeEncomiendaCallCenterForm
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
            groupBox1 = new GroupBox();
            textBox1 = new TextBox();
            label5 = new Label();
            button1 = new Button();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            button3 = new Button();
            label11 = new Label();
            Guías = new ListBox();
            EncomiendasListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            ConfirmarBttn = new Button();
            CancelarBttn = new Button();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label10 = new Label();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            numericUpDown1 = new NumericUpDown();
            label7 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            button6 = new Button();
            button7 = new Button();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 11);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(556, 103);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(49, 49);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(110, 23);
            textBox1.TabIndex = 7;
            // 
            // label5
            // 
            label5.Font = new Font("Arial", 12F, FontStyle.Bold);
            label5.Location = new Point(6, 17);
            label5.Name = "label5";
            label5.Size = new Size(226, 21);
            label5.TabIndex = 4;
            label5.Text = "Datos del Cliente";
            // 
            // button1
            // 
            button1.Location = new Point(178, 44);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(118, 32);
            button1.TabIndex = 0;
            button1.Text = "Buscar Cliente";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(6, 53);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 0;
            label1.Text = "CUIT:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(EncomiendasListView);
            groupBox2.Controls.Add(ConfirmarBttn);
            groupBox2.Controls.Add(CancelarBttn);
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Controls.Add(radioButton1);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(button7);
            groupBox2.Controls.Add(comboBox3);
            groupBox2.Controls.Add(comboBox2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(12, 118);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(556, 454);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(Guías);
            groupBox3.Location = new Point(461, 391);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(408, 368);
            groupBox3.TabIndex = 39;
            groupBox3.TabStop = false;
            groupBox3.Text = "Guías de Encomiendas";
            // 
            // button3
            // 
            button3.Location = new Point(265, 318);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(102, 25);
            button3.TabIndex = 32;
            button3.Text = "Finalizar";
            button3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(39, 29);
            label11.Name = "label11";
            label11.Size = new Size(179, 15);
            label11.TabIndex = 1;
            label11.Text = "Numeros de tracking generados:";
            // 
            // Guías
            // 
            Guías.FormattingEnabled = true;
            Guías.Location = new Point(39, 56);
            Guías.Name = "Guías";
            Guías.Size = new Size(317, 244);
            Guías.TabIndex = 0;
            // 
            // EncomiendasListView
            // 
            EncomiendasListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            EncomiendasListView.Location = new Point(26, 306);
            EncomiendasListView.Name = "EncomiendasListView";
            EncomiendasListView.Size = new Size(248, 97);
            EncomiendasListView.TabIndex = 38;
            EncomiendasListView.UseCompatibleStateImageBehavior = false;
            EncomiendasListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tipo de Caja";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Cantidad";
            columnHeader2.Width = 100;
            // 
            // ConfirmarBttn
            // 
            ConfirmarBttn.Location = new Point(323, 420);
            ConfirmarBttn.Margin = new Padding(3, 2, 3, 2);
            ConfirmarBttn.Name = "ConfirmarBttn";
            ConfirmarBttn.Size = new Size(102, 25);
            ConfirmarBttn.TabIndex = 31;
            ConfirmarBttn.Text = "Confirmar";
            ConfirmarBttn.UseVisualStyleBackColor = true;
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(430, 420);
            CancelarBttn.Margin = new Padding(3, 2, 3, 2);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(102, 25);
            CancelarBttn.TabIndex = 32;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(7, 91);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(95, 19);
            radioButton2.TabIndex = 36;
            radioButton2.TabStop = true;
            radioButton2.Text = "Agencia / CD";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(306, 91);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(76, 19);
            radioButton1.TabIndex = 35;
            radioButton1.TabStop = true;
            radioButton1.Text = "Domicilio";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(25, 283);
            label10.Name = "label10";
            label10.Size = new Size(135, 15);
            label10.TabIndex = 34;
            label10.Text = "Encomiendas a Imponer";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(163, 206);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(110, 23);
            comboBox1.TabIndex = 33;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(164, 169);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(110, 23);
            textBox2.TabIndex = 32;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(164, 238);
            numericUpDown1.Margin = new Padding(3, 2, 3, 2);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(109, 23);
            numericUpDown1.TabIndex = 31;
            // 
            // label7
            // 
            label7.Location = new Point(7, 171);
            label7.Name = "label7";
            label7.Size = new Size(149, 17);
            label7.TabIndex = 28;
            label7.Text = "DNI autorizado a retirar:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(387, 130);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(110, 23);
            textBox3.TabIndex = 27;
            // 
            // label4
            // 
            label4.Location = new Point(308, 132);
            label4.Name = "label4";
            label4.Size = new Size(74, 19);
            label4.TabIndex = 26;
            label4.Text = "Domicilio:";
            // 
            // button6
            // 
            button6.Location = new Point(291, 306);
            button6.Margin = new Padding(3, 2, 3, 2);
            button6.Name = "button6";
            button6.Size = new Size(101, 25);
            button6.TabIndex = 25;
            button6.Text = "Quitar";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(291, 236);
            button7.Margin = new Padding(3, 2, 3, 2);
            button7.Name = "button7";
            button7.Size = new Size(101, 25);
            button7.TabIndex = 24;
            button7.Text = "Añadir";
            button7.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(164, 132);
            comboBox3.Margin = new Padding(3, 2, 3, 2);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(110, 23);
            comboBox3.TabIndex = 18;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(164, 46);
            comboBox2.Margin = new Padding(3, 2, 3, 2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(110, 23);
            comboBox2.TabIndex = 17;
            // 
            // label3
            // 
            label3.Location = new Point(7, 44);
            label3.Name = "label3";
            label3.Size = new Size(132, 19);
            label3.TabIndex = 14;
            label3.Text = "Ciudad:";
            // 
            // label2
            // 
            label2.Location = new Point(10, 240);
            label2.Name = "label2";
            label2.Size = new Size(85, 16);
            label2.TabIndex = 12;
            label2.Text = "Cantidad:";
            // 
            // label9
            // 
            label9.Location = new Point(7, 132);
            label9.Name = "label9";
            label9.Size = new Size(132, 19);
            label9.TabIndex = 7;
            label9.Text = "Opciones de entrega:";
            // 
            // label8
            // 
            label8.Location = new Point(10, 206);
            label8.Name = "label8";
            label8.Size = new Size(85, 16);
            label8.TabIndex = 6;
            label8.Text = "Tipo de caja:";
            // 
            // label6
            // 
            label6.Font = new Font("Arial", 12F, FontStyle.Bold);
            label6.Location = new Point(7, 17);
            label6.Name = "label6";
            label6.Size = new Size(309, 27);
            label6.TabIndex = 5;
            label6.Text = "Datos de la encomienda";
            // 
            // ImposicionDeEncomiendaCallCenterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(580, 583);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(2);
            Name = "ImposicionDeEncomiendaCallCenterForm";
            Text = "Imposición de Encomienda - Call Center";
            Load += ImposicionCallCenterForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Label label5;
        private Button button1;
        private Label label1;
        private GroupBox groupBox2;
        private Button ConfirmarBttn;
        private Button CancelarBttn;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label10;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private NumericUpDown numericUpDown1;
        private Label label7;
        private TextBox textBox3;
        private Label label4;
        private Button button6;
        private Button button7;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private Label label3;
        private Label label2;
        private Label label9;
        private Label label8;
        private Label label6;
        private ListView EncomiendasListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private GroupBox groupBox3;
        private Button button3;
        private Label label11;
        private ListBox Guías;
    }
}