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
        ConsultaTrackingBttn = new Button();
        CdCombo = new ComboBox();
        AgenciaCombo = new ComboBox();
        label1 = new Label();
        label2 = new Label();
        EmitirFacturaButton = new Button();
        EntregaGuiaAgenciaButton = new Button();
        EntregaGuiaCDButton = new Button();
        ImpAgenciaButton = new Button();
        ImpCallCenterButton = new Button();
        PlayeroButton = new Button();
        RendicionFleteroButton = new Button();
        CostosVentasButton = new Button();
        ImpCentroDistribucionButton = new Button();
        SuspendLayout();
        // 
        // ConsultaTrackingBttn
        // 
        ConsultaTrackingButton.Location = new Point(238, 208);
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
        label2.Location = new Point(30, 53);
        label2.Name = "label2";
        label2.Size = new Size(88, 15);
        label2.TabIndex = 4;
        label2.Text = "Agencia actual:";
        // 
        // EmitirFacturaButton
        // 
        EmitirFacturaButton.Location = new Point(238, 300);
        EmitirFacturaButton.Name = "EmitirFacturaButton";
        EmitirFacturaButton.Size = new Size(167, 23);
        EmitirFacturaButton.TabIndex = 5;
        EmitirFacturaButton.Text = "Emitir factura";
        EmitirFacturaButton.UseVisualStyleBackColor = true;
        EmitirFacturaButton.Click += EmitirFacturaButton_Click;
        // 
        // EntregaGuiaAgenciaButton
        // 
        EntregaGuiaAgenciaButton.Location = new Point(238, 154);
        EntregaGuiaAgenciaButton.Name = "EntregaGuiaAgenciaButton";
        EntregaGuiaAgenciaButton.Size = new Size(167, 23);
        EntregaGuiaAgenciaButton.TabIndex = 6;
        EntregaGuiaAgenciaButton.Text = "Entrega guia agencia";
        EntregaGuiaAgenciaButton.UseVisualStyleBackColor = true;
        EntregaGuiaAgenciaButton.Click += EntregaGuiaAgenciaButton_Click;
        // 
        // EntregaGuiaCDButton
        // 
        EntregaGuiaCDButton.Location = new Point(238, 110);
        EntregaGuiaCDButton.Name = "EntregaGuiaCDButton";
        EntregaGuiaCDButton.Size = new Size(167, 23);
        EntregaGuiaCDButton.TabIndex = 7;
        EntregaGuiaCDButton.Text = "Entrega guia CD";
        EntregaGuiaCDButton.UseVisualStyleBackColor = true;
        EntregaGuiaCDButton.Click += EntregaGuiaCDButton_Click;
        // 
        // ImpAgenciaButton
        // 
        ImpAgenciaButton.Location = new Point(24, 110);
        ImpAgenciaButton.Name = "ImpAgenciaButton";
        ImpAgenciaButton.Size = new Size(167, 23);
        ImpAgenciaButton.TabIndex = 8;
        ImpAgenciaButton.Text = "Imposicion agencia";
        ImpAgenciaButton.UseVisualStyleBackColor = true;
        ImpAgenciaButton.Click += ImpAgenciaButton_Click;
        // 
        // ImpCallCenterButton
        // 
        ImpCallCenterButton.Location = new Point(24, 157);
        ImpCallCenterButton.Name = "ImpCallCenterButton";
        ImpCallCenterButton.Size = new Size(167, 23);
        ImpCallCenterButton.TabIndex = 9;
        ImpCallCenterButton.Text = "Imposicion call center";
        ImpCallCenterButton.UseVisualStyleBackColor = true;
        ImpCallCenterButton.Click += ImpCallCenterButton_Click;
        // 
        // PlayeroButton
        // 
        PlayeroButton.Location = new Point(24, 252);
        PlayeroButton.Name = "PlayeroButton";
        PlayeroButton.Size = new Size(167, 23);
        PlayeroButton.TabIndex = 10;
        PlayeroButton.Text = "Playero";
        PlayeroButton.UseVisualStyleBackColor = true;
        PlayeroButton.Click += PlayeroButton_Click;
        // 
        // RendicionFleteroButton
        // 
        RendicionFleteroButton.Location = new Point(24, 300);
        RendicionFleteroButton.Name = "RendicionFleteroButton";
        RendicionFleteroButton.Size = new Size(167, 23);
        RendicionFleteroButton.TabIndex = 11;
        RendicionFleteroButton.Text = "Rendicion fletero";
        RendicionFleteroButton.UseVisualStyleBackColor = true;
        RendicionFleteroButton.Click += RendicionFleteroButton_Click;
        // 
        // CostosVentasButton
        // 
        CostosVentasButton.Location = new Point(238, 252);
        CostosVentasButton.Name = "CostosVentasButton";
        CostosVentasButton.Size = new Size(167, 23);
        CostosVentasButton.TabIndex = 12;
        CostosVentasButton.Text = "Costos vs ventas";
        CostosVentasButton.UseMnemonic = false;
        CostosVentasButton.UseVisualStyleBackColor = true;
        CostosVentasButton.UseWaitCursor = true;
        CostosVentasButton.Click += CostosVentasButton_Click;
        // 
        // ImpCentroDistribucionButton
        // 
        ImpCentroDistribucionButton.Location = new Point(24, 208);
        ImpCentroDistribucionButton.Name = "ImpCentroDistribucionButton";
        ImpCentroDistribucionButton.Size = new Size(167, 23);
        ImpCentroDistribucionButton.TabIndex = 13;
        ImpCentroDistribucionButton.Text = "Imposicion CD";
        ImpCentroDistribucionButton.UseVisualStyleBackColor = true;
        ImpCentroDistribucionButton.Click += ImpCentroDistribucionButton_Click;
        // 
        // MenuPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(431, 362);
        Controls.Add(ImpCentroDistribucionButton);
        Controls.Add(CostosVentasButton);
        Controls.Add(RendicionFleteroButton);
        Controls.Add(PlayeroButton);
        Controls.Add(ImpCallCenterButton);
        Controls.Add(ImpAgenciaButton);
        Controls.Add(EntregaGuiaCDButton);
        Controls.Add(EntregaGuiaAgenciaButton);
        Controls.Add(EmitirFacturaButton);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(AgenciaCombo);
        Controls.Add(CdCombo);
        Controls.Add(ConsultaTrackingBttn);
        Name = "MenuPrincipal";
        Text = "Menu Principal";
        Load += MenuPrincipal_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button ConsultaTrackingBttn;
    private ComboBox CdCombo;
    private ComboBox AgenciaCombo;
    private Label label1;
    private Label label2;
    private Button EmitirFacturaButton;
    private Button EntregaGuiaAgenciaButton;
    private Button EntregaGuiaCDButton;
    private Button ImpAgenciaButton;
    private Button ImpCallCenterButton;
    private Button PlayeroButton;
    private Button RendicionFleteroButton;
    private Button CostosVentasButton;
    private Button ImpCentroDistribucionButton;
}