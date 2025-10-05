namespace CAIGrupoG.RegistroRecepcion
{
    partial class RegistrarRecepcionForm
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
            label2 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            groupBox1 = new GroupBox();
            label3 = new Label();
            comboBox3 = new ComboBox();
            button1 = new Button();
            checkedListBox1 = new CheckedListBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 32);
            label1.Name = "label1";
            label1.Size = new Size(183, 25);
            label1.TabIndex = 0;
            label1.Text = "Selección de empresa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 81);
            label2.Name = "label2";
            label2.Size = new Size(197, 25);
            label2.TabIndex = 1;
            label2.Text = "Selección de transporte";
            label2.Click += label2_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(232, 24);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(320, 33);
            comboBox1.TabIndex = 2;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(232, 73);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(320, 33);
            comboBox2.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkedListBox1);
            groupBox1.Location = new Point(29, 129);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(523, 239);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encomiendas recibidas:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 395);
            label3.Name = "label3";
            label3.Size = new Size(169, 25);
            label3.TabIndex = 5;
            label3.Text = "Ubicación asignada:";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(232, 387);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(320, 33);
            comboBox3.TabIndex = 6;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(356, 451);
            button1.Name = "button1";
            button1.Size = new Size(196, 34);
            button1.TabIndex = 7;
            button1.Text = "Confirmar recepción";
            button1.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(17, 40);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(483, 172);
            checkedListBox1.TabIndex = 0;
            // 
            // RegistrarRecepcionForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 516);
            Controls.Add(button1);
            Controls.Add(comboBox3);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegistrarRecepcionForm";
            Text = "RegistrarRecepcionForm";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private GroupBox groupBox1;
        private Label label3;
        private ComboBox comboBox3;
        private Button button1;
        private CheckedListBox checkedListBox1;
    }
}