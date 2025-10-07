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
            textBox1 = new TextBox();
            button1 = new Button();
            label2 = new Label();
            listView1 = new ListView();
            button2 = new Button();
            button3 = new Button();
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
            // textBox1
            // 
            textBox1.Location = new Point(30, 46);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(153, 46);
            button1.Name = "button1";
            button1.Size = new Size(97, 23);
            button1.TabIndex = 2;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = true;
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
            // listView1
            // 
            listView1.Location = new Point(30, 117);
            listView1.Name = "listView1";
            listView1.Size = new Size(739, 238);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button2
            // 
            button2.Location = new Point(672, 392);
            button2.Name = "button2";
            button2.Size = new Size(97, 23);
            button2.TabIndex = 5;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(569, 392);
            button3.Name = "button3";
            button3.Size = new Size(97, 23);
            button3.TabIndex = 6;
            button3.Text = "Retirar";
            button3.UseVisualStyleBackColor = true;
            // 
            // EntregaGuíaCDForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(listView1);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "EntregaGuíaCDForm";
            Text = "Entrega Encomienda - CD";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private Label label2;
        private ListView listView1;
        private Button button2;
        private Button button3;
    }
}