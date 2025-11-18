namespace ControladoraGaren
{
    partial class frmCadastroControladoraEquipamento
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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtDispositivo = new TextBox();
            txtIpControladora = new TextBox();
            txtTempo = new TextBox();
            btnIncluir = new Button();
            dgv = new DataGridView();
            cboDirecao = new ComboBox();
            cboRele = new ComboBox();
            cboIdControladora = new ComboBox();
            btnAtualizar = new Button();
            btnDeletar = new Button();
            btnNovo = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(32, 29);
            label1.Name = "label1";
            label1.Size = new Size(160, 19);
            label1.TabIndex = 0;
            label1.Text = "Numero Equipamento:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(32, 203);
            label2.Name = "label2";
            label2.Size = new Size(65, 19);
            label2.TabIndex = 1;
            label2.Text = "Direção:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(32, 167);
            label3.Name = "label3";
            label3.Size = new Size(59, 19);
            label3.TabIndex = 2;
            label3.Text = "Tempo:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(32, 135);
            label4.Name = "label4";
            label4.Size = new Size(42, 19);
            label4.TabIndex = 3;
            label4.Text = "Rele:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(32, 100);
            label5.Name = "label5";
            label5.Size = new Size(120, 19);
            label5.TabIndex = 4;
            label5.Text = "Ip Controladora:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label6.Location = new Point(32, 64);
            label6.Name = "label6";
            label6.Size = new Size(120, 19);
            label6.TabIndex = 5;
            label6.Text = "Id Controladora:";
            // 
            // txtDispositivo
            // 
            txtDispositivo.Location = new Point(232, 25);
            txtDispositivo.Name = "txtDispositivo";
            txtDispositivo.Size = new Size(100, 23);
            txtDispositivo.TabIndex = 0;
            txtDispositivo.KeyPress += txtDispositivo_KeyPress;
            // 
            // txtIpControladora
            // 
            txtIpControladora.Location = new Point(232, 96);
            txtIpControladora.Name = "txtIpControladora";
            txtIpControladora.Size = new Size(121, 23);
            txtIpControladora.TabIndex = 2;
            txtIpControladora.KeyPress += txtIpControladora_KeyPress;
            // 
            // txtTempo
            // 
            txtTempo.Location = new Point(232, 163);
            txtTempo.Name = "txtTempo";
            txtTempo.Size = new Size(100, 23);
            txtTempo.TabIndex = 4;
            txtTempo.KeyPress += txtTempo_KeyPress;
            // 
            // btnIncluir
            // 
            btnIncluir.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnIncluir.Location = new Point(147, 267);
            btnIncluir.Name = "btnIncluir";
            btnIncluir.Size = new Size(109, 30);
            btnIncluir.TabIndex = 6;
            btnIncluir.Text = "Incluir";
            btnIncluir.UseVisualStyleBackColor = true;
            btnIncluir.Click += btnIncluir_Click;
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(32, 314);
            dgv.Name = "dgv";
            dgv.Size = new Size(684, 197);
            dgv.TabIndex = 13;
            dgv.CellClick += dgv_CellClick;
            // 
            // cboDirecao
            // 
            cboDirecao.AutoCompleteCustomSource.AddRange(new string[] { "ENTRADA", "SAIDA" });
            cboDirecao.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDirecao.FormattingEnabled = true;
            cboDirecao.ItemHeight = 15;
            cboDirecao.Items.AddRange(new object[] { "ENTRADA", "SAIDA" });
            cboDirecao.Location = new Point(232, 202);
            cboDirecao.Name = "cboDirecao";
            cboDirecao.Size = new Size(121, 23);
            cboDirecao.TabIndex = 5;
            // 
            // cboRele
            // 
            cboRele.AutoCompleteCustomSource.AddRange(new string[] { "1", "2" });
            cboRele.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRele.FormattingEnabled = true;
            cboRele.ItemHeight = 15;
            cboRele.Items.AddRange(new object[] { "1", "2" });
            cboRele.Location = new Point(232, 131);
            cboRele.Name = "cboRele";
            cboRele.Size = new Size(121, 23);
            cboRele.TabIndex = 3;
            // 
            // cboIdControladora
            // 
            cboIdControladora.AutoCompleteCustomSource.AddRange(new string[] { "1", "2" });
            cboIdControladora.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdControladora.FormattingEnabled = true;
            cboIdControladora.ItemHeight = 15;
            cboIdControladora.Items.AddRange(new object[] { "1", "2" });
            cboIdControladora.Location = new Point(232, 60);
            cboIdControladora.Name = "cboIdControladora";
            cboIdControladora.Size = new Size(121, 23);
            cboIdControladora.TabIndex = 1;
            // 
            // btnAtualizar
            // 
            btnAtualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAtualizar.Location = new Point(261, 267);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(109, 30);
            btnAtualizar.TabIndex = 14;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
            // 
            // btnDeletar
            // 
            btnDeletar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDeletar.Location = new Point(376, 267);
            btnDeletar.Name = "btnDeletar";
            btnDeletar.Size = new Size(109, 30);
            btnDeletar.TabIndex = 15;
            btnDeletar.Text = "Deletar";
            btnDeletar.UseVisualStyleBackColor = true;
            btnDeletar.Click += btnDeletar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNovo.Location = new Point(32, 267);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(109, 30);
            btnNovo.TabIndex = 16;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // frmCadastroControladoraEquipamento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 554);
            Controls.Add(btnNovo);
            Controls.Add(btnDeletar);
            Controls.Add(btnAtualizar);
            Controls.Add(cboIdControladora);
            Controls.Add(cboRele);
            Controls.Add(cboDirecao);
            Controls.Add(dgv);
            Controls.Add(btnIncluir);
            Controls.Add(txtTempo);
            Controls.Add(txtIpControladora);
            Controls.Add(txtDispositivo);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            KeyPreview = true;
            Name = "frmCadastroControladoraEquipamento";
            Text = "frmCadastroControladoraEquipamento";
            KeyDown += frmCadastroControladoraEquipamento_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtDispositivo;
        private TextBox txtIpControladora;
        private TextBox txtTempo;
        private Button btnIncluir;
        private DataGridView dgv;
        private ComboBox cboDirecao;
        private ComboBox cboRele;
        private ComboBox cboIdControladora;
        private Button btnAtualizar;
        private Button btnDeletar;
        private Button btnNovo;
    }
}