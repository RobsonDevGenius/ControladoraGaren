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
            lstMensagem = new ListBox();
            lstMensagemDispositivosConectados = new ListBox();
            btnLimparLists = new Button();
            btnMinimize = new Button();
            btnSair = new Button();
            btnConfigurar = new Button();
            SuspendLayout();
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(434, 9);
            label4.Name = "label4";
            label4.Size = new Size(187, 30);
            label4.TabIndex = 8;
            label4.Text = "CONTROLADORA";
            // 
            // lstMensagem
            // 
            lstMensagem.FormattingEnabled = true;
            lstMensagem.HorizontalScrollbar = true;
            lstMensagem.ItemHeight = 15;
            lstMensagem.Location = new Point(533, 47);
            lstMensagem.Name = "lstMensagem";
            lstMensagem.ScrollAlwaysVisible = true;
            lstMensagem.Size = new Size(482, 454);
            lstMensagem.TabIndex = 11;
            // 
            // lstMensagemDispositivosConectados
            // 
            lstMensagemDispositivosConectados.FormattingEnabled = true;
            lstMensagemDispositivosConectados.HorizontalScrollbar = true;
            lstMensagemDispositivosConectados.ItemHeight = 15;
            lstMensagemDispositivosConectados.Location = new Point(24, 47);
            lstMensagemDispositivosConectados.Name = "lstMensagemDispositivosConectados";
            lstMensagemDispositivosConectados.ScrollAlwaysVisible = true;
            lstMensagemDispositivosConectados.Size = new Size(482, 454);
            lstMensagemDispositivosConectados.TabIndex = 23;
            // 
            // btnLimparLists
            // 
            btnLimparLists.Location = new Point(24, 527);
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
            btnConfigurar.Click += button1_Click;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1039, 611);
            ControlBox = false;
            Controls.Add(btnConfigurar);
            Controls.Add(btnSair);
            Controls.Add(btnMinimize);
            Controls.Add(btnLimparLists);
            Controls.Add(lstMensagemDispositivosConectados);
            Controls.Add(lstMensagem);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmPrincipal";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private Button btnLimparLists;
        private Button btnMinimize;
        private Button btnSair;
        private Button btnConfigurar;
        public static ListBox lstMensagemDispositivosConectados;
        public static ListBox lstMensagem;
    }
}
