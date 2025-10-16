namespace CAIGrupoG.ConsultaTracking
{
    partial class ConsultarTrackingForm
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
            BuscarBttn = new Button();
            GuiaText = new TextBox();
            EstadoText = new TextBox();
            label3 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // CancelarBttn
            // 
            CancelarBttn.Location = new Point(338, 143);
            CancelarBttn.Name = "CancelarBttn";
            CancelarBttn.Size = new Size(75, 23);
            CancelarBttn.TabIndex = 7;
            CancelarBttn.Text = "Cancelar";
            CancelarBttn.UseVisualStyleBackColor = true;
            // 
            // BuscarBttn
            // 
            BuscarBttn.Location = new Point(338, 37);
            BuscarBttn.Name = "BuscarBttn";
            BuscarBttn.Size = new Size(75, 23);
            BuscarBttn.TabIndex = 2;
            BuscarBttn.Text = "Buscar";
            BuscarBttn.UseVisualStyleBackColor = true;
            // 
            // GuiaText
            // 
            GuiaText.Location = new Point(12, 37);
            GuiaText.Name = "GuiaText";
            GuiaText.Size = new Size(320, 23);
            GuiaText.TabIndex = 1;
            // 
            // EstadoText
            // 
            EstadoText.Location = new Point(12, 101);
            EstadoText.Name = "EstadoText";
            EstadoText.ReadOnly = true;
            EstadoText.Size = new Size(320, 23);
            EstadoText.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 83);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 9;
            label3.Text = "Estado Encomienda";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 0;
            label1.Text = "Guía";
            // 
            // ConsultarTrackingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 178);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(CancelarBttn);
            Controls.Add(EstadoText);
            Controls.Add(GuiaText);
            Controls.Add(BuscarBttn);
            HelpButton = true;
            Name = "ConsultarTrackingForm";
            Text = "Consultar Tracking";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button CancelarBttn;
        private Button BuscarBttn;
        private TextBox GuiaText;
        private TextBox EstadoText;
        private Label label3;
        private Label label1;
    }
}