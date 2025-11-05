namespace CAIGrupoG;

partial class MenuPrincipal
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
        ConsultaTrackingButton = new Button();
        CdCombo = new ComboBox();
        AgenciaCombo = new ComboBox();
        label1 = new Label();
        label2 = new Label();
        SuspendLayout();
        // 
        // ConsultaTrackingButton
        // 
        ConsultaTrackingButton.Location = new Point(24, 110);
        ConsultaTrackingButton.Name = "ConsultaTrackingButton";
        ConsultaTrackingButton.Size = new Size(167, 23);
        ConsultaTrackingButton.TabIndex = 0;
        ConsultaTrackingButton.Text = "Consulta Tracking";
        ConsultaTrackingButton.UseVisualStyleBackColor = true;
        ConsultaTrackingButton.Click += ConsultaTrackingButton_Click;
        // 
        // CdCombo
        // 
        CdCombo.FormattingEnabled = true;
        CdCombo.Location = new Point(154, 12);
        CdCombo.Name = "CdCombo";
        CdCombo.Size = new Size(251, 23);
        CdCombo.TabIndex = 1;
        CdCombo.SelectedIndexChanged += CdCombo_SelectedIndexChanged;
        // 
        // AgenciaCombo
        // 
        AgenciaCombo.FormattingEnabled = true;
        AgenciaCombo.Location = new Point(154, 50);
        AgenciaCombo.Name = "AgenciaCombo";
        AgenciaCombo.Size = new Size(251, 23);
        AgenciaCombo.TabIndex = 2;
        AgenciaCombo.SelectedIndexChanged += AgenciaCombo_SelectedIndexChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(30, 21);
        label1.Name = "label1";
        label1.Size = new Size(106, 15);
        label1.TabIndex = 3;
        label1.Text = "Centro dist. actual:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(48, 53);
        label2.Name = "label2";
        label2.Size = new Size(88, 15);
        label2.TabIndex = 4;
        label2.Text = "Agencia actual:";
        // 
        // MenuPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(AgenciaCombo);
        Controls.Add(CdCombo);
        Controls.Add(ConsultaTrackingButton);
        Name = "MenuPrincipal";
        Text = "MenuPrincipal";
        Load += MenuPrincipal_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button ConsultaTrackingButton;
    private ComboBox CdCombo;
    private ComboBox AgenciaCombo;
    private Label label1;
    private Label label2;
}