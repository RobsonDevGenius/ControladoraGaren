namespace ControladoraGaren
{
    partial class frmConfiguracao
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
            btnSalvar = new Button();
            label1 = new Label();
            label2 = new Label();
            txtPorta = new TextBox();
            txtIp = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnSalvar
            // 
            btnSalvar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSalvar.Location = new Point(461, 137);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 31);
            btnSalvar.TabIndex = 0;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(127, 21);
            label1.TabIndex = 1;
            label1.Text = "IP Gerenciador:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(153, 21);
            label2.TabIndex = 2;
            label2.Text = "Porta Gerenciador:";
            // 
            // txtPorta
            // 
            txtPorta.Location = new Point(236, 58);
            txtPorta.Name = "txtPorta";
            txtPorta.Size = new Size(133, 23);
            txtPorta.TabIndex = 3;
            // 
            // txtIp
            // 
            txtIp.Location = new Point(236, 18);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(133, 23);
            txtIp.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(12, 99);
            label3.Name = "label3";
            label3.Size = new Size(459, 21);
            label3.TabIndex = 5;
            label3.Text = "Para que as configurações sejam salvas reinicie a aplicação";
            // 
            // frmConfiguracao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(548, 180);
            Controls.Add(label3);
            Controls.Add(txtIp);
            Controls.Add(txtPorta);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSalvar);
            Name = "frmConfiguracao";
            Text = "frmConfiguracao";
            Load += frmConfiguracao_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalvar;
        private Label label1;
        private Label label2;
        private TextBox txtPorta;
        private TextBox txtIp;
        private Label label3;
    }
}