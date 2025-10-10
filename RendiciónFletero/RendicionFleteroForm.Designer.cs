namespace CAIGrupoG.Admisión
{
    partial class RendicionFleteroForm
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
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "33123123", "Pendiente entrega a domicilio" }, -1);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "441212", "Pendiente retiro" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "6612312", "Pendiente entrega en agencia" }, -1);
            ListViewItem listViewItem4 = new ListViewItem(new string[] { "33123123", "Pendiente entrega a domicilio" }, -1);
            ListViewItem listViewItem5 = new ListViewItem(new string[] { "441212", "Pendiente retiro" }, -1);
            ListViewItem listViewItem6 = new ListViewItem(new string[] { "6612312", "Pendiente entrega en agencia" }, -1);
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader13 = new ColumnHeader();
            label2 = new Label();
            groupBox2 = new GroupBox();
            listView2 = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader14 = new ColumnHeader();
            label3 = new Label();
            button5 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(279, 36);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(145, 28);
            button1.TabIndex = 0;
            button1.Text = "Buscar Guías asociadas";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(20, 40);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(253, 23);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 14);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 2;
            label1.Text = "DNI Fletero";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listView1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 78);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(912, 181);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Admision - Marque las tareas completadas por el fletero";
            // 
            // listView1
            // 
            listView1.CheckBoxes = true;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader12, columnHeader2, columnHeader3, columnHeader6, columnHeader7, columnHeader13 });
            listView1.FullRowSelect = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3 });
            listView1.Location = new Point(6, 43);
            listView1.Margin = new Padding(3, 2, 3, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(898, 92);
            listView1.TabIndex = 6;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "N° Guía";
            columnHeader1.Width = 100;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "Estado de Encomienda";
            columnHeader12.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Tipo Paquete";
            columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "CUIT";
            columnHeader3.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Autorizado a retirar";
            columnHeader6.Width = 140;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Dirección Destinatario";
            columnHeader7.Width = 140;
            // 
            // columnHeader13
            // 
            columnHeader13.Text = "CD";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 5;
            label2.Text = "Detalle Guía";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(listView2);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 265);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(912, 181);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Guias para entrega a domicilio y en agencia";
            // 
            // listView2
            // 
            listView2.CheckBoxes = true;
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader8, columnHeader9, columnHeader10, columnHeader11, columnHeader14 });
            listView2.FullRowSelect = true;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listView2.Items.AddRange(new ListViewItem[] { listViewItem4, listViewItem5, listViewItem6 });
            listView2.Location = new Point(7, 44);
            listView2.Margin = new Padding(3, 2, 3, 2);
            listView2.Name = "listView2";
            listView2.Size = new Size(898, 92);
            listView2.TabIndex = 7;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            listView2.SelectedIndexChanged += listView2_SelectedIndexChanged;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "N° Guía";
            columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Estado de Encomienda";
            columnHeader5.Width = 250;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Tipo Paquete";
            columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "CUIT";
            columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "Autorizado a retirar";
            columnHeader10.Width = 140;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Dirección Destinatario";
            columnHeader11.Width = 140;
            // 
            // columnHeader14
            // 
            columnHeader14.Text = "CD";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 5;
            label3.Text = "Detalle Guía";
            // 
            // button5
            // 
            button5.Location = new Point(816, 451);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(108, 28);
            button5.TabIndex = 8;
            button5.Text = "Aceptar";
            button5.UseVisualStyleBackColor = true;
            // 
            // RendicionFleteroForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(943, 492);
            Controls.Add(button5);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Margin = new Padding(1);
            Name = "RendicionFleteroForm";
            Text = "Rendición de Fletero";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private GroupBox groupBox1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Label label2;
        private GroupBox groupBox2;
        private Button button5;
        private Label label3;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ListView listView2;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader14;
    }
}