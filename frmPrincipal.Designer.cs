namespace WinFormsApp1
{
    partial class frmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label4 = new Label();
            btnLimparLists = new Button();
            btnMinimize = new Button();
            btnSair = new Button();
            btnConfigurar = new Button();
            mnu = new MenuStrip();
            menu = new ToolStripMenuItem();
            mnuCadastrarEquipamento = new ToolStripMenuItem();
            mnuConfiguracao = new ToolStripMenuItem();
            mnuMinimizar = new ToolStripMenuItem();
            mnuSair = new ToolStripMenuItem();
            tabControl = new TabControl();
            tbpDispConectados = new TabPage();
            lstMensagem = new ListBox();
            lstMensagemDispositivosConectados = new ListBox();
            tbpMensagensTrocadas = new TabPage();
            btnLimparHistorico = new Button();
            btnCopiarHistorico = new Button();
            lstMensagensTrocadas = new ListBox();
            mnu.SuspendLayout();
            tabControl.SuspendLayout();
            tbpDispConectados.SuspendLayout();
            tbpMensagensTrocadas.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(435, 34);
            label4.Name = "label4";
            label4.Size = new Size(187, 30);
            label4.TabIndex = 8;
            label4.Text = "CONTROLADORA";
            // 
            // btnLimparLists
            // 
            btnLimparLists.Location = new Point(16, 527);
            btnLimparLists.Name = "btnLimparLists";
            btnLimparLists.Size = new Size(131, 23);
            btnLimparLists.TabIndex = 24;
            btnLimparLists.Text = "Limpar ListBoxes";
            btnLimparLists.UseVisualStyleBackColor = true;
            btnLimparLists.Click += btnLimparLists_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Location = new Point(844, 9);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(75, 23);
            btnMinimize.TabIndex = 25;
            btnMinimize.Text = "Minimize";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Visible = false;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnSair
            // 
            btnSair.Location = new Point(940, 9);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(75, 23);
            btnSair.TabIndex = 26;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Visible = false;
            btnSair.Click += btnSair_Click;
            // 
            // btnConfigurar
            // 
            btnConfigurar.Location = new Point(722, 9);
            btnConfigurar.Name = "btnConfigurar";
            btnConfigurar.Size = new Size(105, 23);
            btnConfigurar.TabIndex = 27;
            btnConfigurar.Text = "Configurar";
            btnConfigurar.UseVisualStyleBackColor = true;
            btnConfigurar.Visible = false;
            btnConfigurar.Click += button1_Click;
            // 
            // mnu
            // 
            mnu.Items.AddRange(new ToolStripItem[] { menu });
            mnu.Location = new Point(0, 0);
            mnu.Name = "mnu";
            mnu.Size = new Size(1039, 24);
            mnu.TabIndex = 29;
            mnu.Text = "menuStrip1";
            // 
            // menu
            // 
            menu.DropDownItems.AddRange(new ToolStripItem[] { mnuCadastrarEquipamento, mnuConfiguracao, mnuMinimizar, mnuSair });
            menu.Name = "menu";
            menu.Size = new Size(50, 20);
            menu.Text = "Menu";
            // 
            // mnuCadastrarEquipamento
            // 
            mnuCadastrarEquipamento.Name = "mnuCadastrarEquipamento";
            mnuCadastrarEquipamento.Size = new Size(198, 22);
            mnuCadastrarEquipamento.Text = "Cadastrar Equipamento";
            mnuCadastrarEquipamento.Click += mnuCadastrarEquipamento_Click;
            // 
            // mnuConfiguracao
            // 
            mnuConfiguracao.Name = "mnuConfiguracao";
            mnuConfiguracao.Size = new Size(198, 22);
            mnuConfiguracao.Text = "Configuração";
            mnuConfiguracao.Click += mnuConfiguracao_Click;
            // 
            // mnuMinimizar
            // 
            mnuMinimizar.Name = "mnuMinimizar";
            mnuMinimizar.Size = new Size(198, 22);
            mnuMinimizar.Text = "Minimizar";
            mnuMinimizar.Click += mnuMinimizar_Click;
            // 
            // mnuSair
            // 
            mnuSair.Name = "mnuSair";
            mnuSair.Size = new Size(198, 22);
            mnuSair.Text = "Sair";
            mnuSair.Click += mnuSair_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tbpDispConectados);
            tabControl.Controls.Add(tbpMensagensTrocadas);
            tabControl.Location = new Point(12, 67);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1003, 454);
            tabControl.TabIndex = 30;
            // 
            // tbpDispConectados
            // 
            tbpDispConectados.Controls.Add(lstMensagem);
            tbpDispConectados.Controls.Add(lstMensagemDispositivosConectados);
            tbpDispConectados.Location = new Point(4, 24);
            tbpDispConectados.Name = "tbpDispConectados";
            tbpDispConectados.Padding = new Padding(3);
            tbpDispConectados.Size = new Size(995, 426);
            tbpDispConectados.TabIndex = 0;
            tbpDispConectados.Text = "Dispositivos Conectados";
            tbpDispConectados.UseVisualStyleBackColor = true;
            // 
            // lstMensagem
            // 
            lstMensagem.FormattingEnabled = true;
            lstMensagem.HorizontalScrollbar = true;
            lstMensagem.ItemHeight = 15;
            lstMensagem.Location = new Point(496, 6);
            lstMensagem.Name = "lstMensagem";
            lstMensagem.ScrollAlwaysVisible = true;
            lstMensagem.Size = new Size(482, 409);
            lstMensagem.TabIndex = 25;
            // 
            // lstMensagemDispositivosConectados
            // 
            lstMensagemDispositivosConectados.FormattingEnabled = true;
            lstMensagemDispositivosConectados.HorizontalScrollbar = true;
            lstMensagemDispositivosConectados.ItemHeight = 15;
            lstMensagemDispositivosConectados.Location = new Point(8, 6);
            lstMensagemDispositivosConectados.Name = "lstMensagemDispositivosConectados";
            lstMensagemDispositivosConectados.ScrollAlwaysVisible = true;
            lstMensagemDispositivosConectados.Size = new Size(482, 409);
            lstMensagemDispositivosConectados.TabIndex = 24;
            // 
            // tbpMensagensTrocadas
            // 
            tbpMensagensTrocadas.Controls.Add(btnLimparHistorico);
            tbpMensagensTrocadas.Controls.Add(btnCopiarHistorico);
            tbpMensagensTrocadas.Controls.Add(lstMensagensTrocadas);
            tbpMensagensTrocadas.Location = new Point(4, 24);
            tbpMensagensTrocadas.Name = "tbpMensagensTrocadas";
            tbpMensagensTrocadas.Padding = new Padding(3);
            tbpMensagensTrocadas.Size = new Size(995, 426);
            tbpMensagensTrocadas.TabIndex = 1;
            tbpMensagensTrocadas.Text = "Mensagens Trocadas";
            tbpMensagensTrocadas.UseVisualStyleBackColor = true;
            // 
            // btnLimparHistorico
            // 
            btnLimparHistorico.Location = new Point(167, 6);
            btnLimparHistorico.Name = "btnLimparHistorico";
            btnLimparHistorico.Size = new Size(139, 23);
            btnLimparHistorico.TabIndex = 34;
            btnLimparHistorico.Text = "Limpar Historico";
            btnLimparHistorico.UseVisualStyleBackColor = true;
            btnLimparHistorico.Click += btnLimparHistorico_Click;
            // 
            // btnCopiarHistorico
            // 
            btnCopiarHistorico.Location = new Point(7, 6);
            btnCopiarHistorico.Name = "btnCopiarHistorico";
            btnCopiarHistorico.Size = new Size(137, 23);
            btnCopiarHistorico.TabIndex = 33;
            btnCopiarHistorico.Text = "Copiar Historico";
            btnCopiarHistorico.UseVisualStyleBackColor = true;
            btnCopiarHistorico.Click += btnCopiarHistorico_Click;
            // 
            // lstMensagensTrocadas
            // 
            lstMensagensTrocadas.DrawMode = DrawMode.OwnerDrawFixed;
            lstMensagensTrocadas.Font = new Font("Courier New", 9F);
            lstMensagensTrocadas.FormattingEnabled = true;
            lstMensagensTrocadas.HorizontalScrollbar = true;
            lstMensagensTrocadas.ItemHeight = 15;
            lstMensagensTrocadas.Location = new Point(5, 35);
            lstMensagensTrocadas.Name = "lstMensagensTrocadas";
            lstMensagensTrocadas.ScrollAlwaysVisible = true;
            lstMensagensTrocadas.Size = new Size(967, 349);
            lstMensagensTrocadas.TabIndex = 32;
            lstMensagensTrocadas.DrawItem += lstMensagensTrocadas_DrawItem;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1039, 611);
            ControlBox = false;
            Controls.Add(tabControl);
            Controls.Add(btnConfigurar);
            Controls.Add(btnSair);
            Controls.Add(btnMinimize);
            Controls.Add(btnLimparLists);
            Controls.Add(label4);
            Controls.Add(mnu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = mnu;
            Name = "frmPrincipal";
            Text = "Form1";
            Load += Form1_Load;
            mnu.ResumeLayout(false);
            mnu.PerformLayout();
            tabControl.ResumeLayout(false);
            tbpDispConectados.ResumeLayout(false);
            tbpMensagensTrocadas.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private Button btnLimparLists;
        private Button btnMinimize;
        private Button btnSair;
        private Button btnConfigurar;
        private MenuStrip mnu;
        private ToolStripMenuItem menu;
        private ToolStripMenuItem mnuCadastrarEquipamento;
        private ToolStripMenuItem mnuConfiguracao;
        private ToolStripMenuItem mnuMinimizar;
        private ToolStripMenuItem mnuSair;
        private TabControl tabControl;
        private TabPage tbpDispConectados;
        private TabPage tbpMensagensTrocadas;
        private Button btnLimparHistorico;
        private Button btnCopiarHistorico;
        public static ListBox lstMensagem;
        public static ListBox lstMensagemDispositivosConectados;
        public static ListBox lstMensagensTrocadas;
    }
}
