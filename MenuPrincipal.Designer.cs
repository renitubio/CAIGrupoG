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
        ImposicionAgenciaBttn = new Button();
        ImpCDButton = new Button();
        PlayeroButton = new Button();
        CostosVentasButton = new Button();
        EntregaCDButton = new Button();
        EntregaAgenciaButton = new Button();
        ImpCallCenterButton = new Button();
        rendicionFleteroButton = new Button();
        EmitirFacturaButton = new Button();
        SuspendLayout();
        // 
        // ConsultaTrackingBttn
        // 
        ConsultaTrackingBttn.Location = new Point(238, 196);
        ConsultaTrackingBttn.Name = "ConsultaTrackingBttn";
        ConsultaTrackingBttn.Size = new Size(167, 23);
        ConsultaTrackingBttn.TabIndex = 0;
        ConsultaTrackingBttn.Text = "Consulta Tracking";
        ConsultaTrackingBttn.UseVisualStyleBackColor = true;
        ConsultaTrackingBttn.Click += ConsultaTrackingBttn_Click;
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
        // ImposicionAgenciaBttn
        // 
        ImposicionAgenciaBttn.Location = new Point(30, 116);
        ImposicionAgenciaBttn.Name = "ImposicionAgenciaBttn";
        ImposicionAgenciaBttn.Size = new Size(167, 23);
        ImposicionAgenciaBttn.TabIndex = 5;
        ImposicionAgenciaBttn.Text = "Imposicion en Agencia";
        ImposicionAgenciaBttn.UseVisualStyleBackColor = true;
        ImposicionAgenciaBttn.Click += ImposicionAgenciaBttn_Click;
        // 
        // ImpCDButton
        // 
        ImpCDButton.Location = new Point(30, 196);
        ImpCDButton.Name = "ImpCDButton";
        ImpCDButton.Size = new Size(167, 23);
        ImpCDButton.TabIndex = 6;
        ImpCDButton.Text = "Imposicion CD";
        ImpCDButton.UseVisualStyleBackColor = true;
        ImpCDButton.Click += ImpCDButton_Click;
        // 
        // PlayeroButton
        // 
        PlayeroButton.Location = new Point(238, 157);
        PlayeroButton.Name = "PlayeroButton";
        PlayeroButton.Size = new Size(167, 23);
        PlayeroButton.TabIndex = 7;
        PlayeroButton.Text = "Playero";
        PlayeroButton.UseVisualStyleBackColor = true;
        PlayeroButton.Click += PlayeroButton_Click;
        // 
        // CostosVentasButton
        // 
        CostosVentasButton.Location = new Point(238, 290);
        CostosVentasButton.Name = "CostosVentasButton";
        CostosVentasButton.Size = new Size(167, 23);
        CostosVentasButton.TabIndex = 8;
        CostosVentasButton.Text = "Costos vs Ventas";
        CostosVentasButton.UseVisualStyleBackColor = true;
        CostosVentasButton.Click += CostosVentasButton_Click;
        // 
        // EntregaCDButton
        // 
        EntregaCDButton.Location = new Point(30, 246);
        EntregaCDButton.Name = "EntregaCDButton";
        EntregaCDButton.Size = new Size(167, 23);
        EntregaCDButton.TabIndex = 9;
        EntregaCDButton.Text = "Entrega CD";
        EntregaCDButton.UseVisualStyleBackColor = true;
        EntregaCDButton.Click += EntregaCDButton_Click;
        // 
        // EntregaAgenciaButton
        // 
        EntregaAgenciaButton.Location = new Point(30, 290);
        EntregaAgenciaButton.Name = "EntregaAgenciaButton";
        EntregaAgenciaButton.Size = new Size(167, 23);
        EntregaAgenciaButton.TabIndex = 10;
        EntregaAgenciaButton.Text = "Entrega agencia";
        EntregaAgenciaButton.UseVisualStyleBackColor = true;
        EntregaAgenciaButton.Click += EntregaAgenciaButton_Click;
        // 
        // ImpCallCenterButton
        // 
        ImpCallCenterButton.Location = new Point(30, 157);
        ImpCallCenterButton.Name = "ImpCallCenterButton";
        ImpCallCenterButton.Size = new Size(167, 23);
        ImpCallCenterButton.TabIndex = 11;
        ImpCallCenterButton.Text = "Imposicion call center";
        ImpCallCenterButton.UseVisualStyleBackColor = true;
        ImpCallCenterButton.Click += ImpCallCenterButton_Click;
        // 
        // rendicionFleteroButton
        // 
        rendicionFleteroButton.Location = new Point(238, 116);
        rendicionFleteroButton.Name = "rendicionFleteroButton";
        rendicionFleteroButton.Size = new Size(167, 23);
        rendicionFleteroButton.TabIndex = 12;
        rendicionFleteroButton.Text = "Rendicion fletero";
        rendicionFleteroButton.UseVisualStyleBackColor = true;
        rendicionFleteroButton.Click += rendicionFleteroButton_Click;
        // 
        // EmitirFacturaButton
        // 
        EmitirFacturaButton.Location = new Point(238, 246);
        EmitirFacturaButton.Name = "EmitirFacturaButton";
        EmitirFacturaButton.Size = new Size(167, 23);
        EmitirFacturaButton.TabIndex = 13;
        EmitirFacturaButton.Text = "Emitir factura";
        EmitirFacturaButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        EmitirFacturaButton.UseVisualStyleBackColor = true;
        EmitirFacturaButton.Click += EmitirFacturaButton_Click;
        // 
        // MenuPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(424, 344);
        Controls.Add(EmitirFacturaButton);
        Controls.Add(rendicionFleteroButton);
        Controls.Add(ImpCallCenterButton);
        Controls.Add(EntregaAgenciaButton);
        Controls.Add(EntregaCDButton);
        Controls.Add(CostosVentasButton);
        Controls.Add(PlayeroButton);
        Controls.Add(ImpCDButton);
        Controls.Add(ImposicionAgenciaBttn);
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
    private Button ImposicionAgenciaBttn;
    private Button ImpCDButton;
    private Button PlayeroButton;
    private Button CostosVentasButton;
    private Button EntregaCDButton;
    private Button EntregaAgenciaButton;
    private Button ImpCallCenterButton;
    private Button rendicionFleteroButton;
    private Button EmitirFacturaButton;
}