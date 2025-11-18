using ControladoraGaren.CLASSES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.CLASSES;
using static System.Net.Mime.MediaTypeNames;

namespace ControladoraGaren
{
    public partial class frmCadastroControladoraEquipamento : Form
    {
        public frmCadastroControladoraEquipamento()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Cadastro Equipamento";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            carregaListaEquipamento();



        }

        private bool verificaSeAlgumaCaixaTextoVazia()
        {
            foreach (Control controle in this.Controls)
            {
                // 1. Verificar se o controle atual é um TextBox
                if (controle is TextBox)
                {
                    // 2. Fazer o casting (conversão) para o tipo TextBox
                    TextBox txt = (TextBox)controle;

                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        // A caixa de texto está vazia, nula ou contém apenas espaços em branco.

                        MessageBox.Show("Nenhum campo pode estar vazio!", "Atenção");
                        return false;

                    }

                }
            }


            foreach (Control controle in this.Controls)
            {
                // 1. Verificar se o controle atual é um TextBox
                if (controle is ComboBox)
                {
                    // 2. Fazer o casting (conversão) para o tipo TextBox
                    ComboBox cbo = (ComboBox)controle;

                    if (string.IsNullOrWhiteSpace(cbo.Text))

                    {
                        // A caixa de texto está vazia, nula ou contém apenas espaços em branco.
                        MessageBox.Show("Nenhum campo pode estar vazio!", "Atenção");
                        return false;

                    }

                }
            }





            return true;


        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            bool retorno = verificaSeAlgumaCaixaTextoVazia();

            if (!retorno)
            {
                return;

            }



            int verificaSeEstaCadastrado = AcessoDbSqlite.verificaSeEstaCadastrado(txtDispositivo.Text);

            if (verificaSeEstaCadastrado > 0)
            {
                MessageBox.Show("Equipamento já está cadastrado");
                return;
            }


            int ultimoCodigo = AcessoDbSqlite.obterUltimoCodigo();


            Dispositivo dispositivo = new Dispositivo();
            dispositivo.codigo = ultimoCodigo + 1;
            dispositivo.idControladora = cboIdControladora.Text;
            dispositivo.rele = Convert.ToInt16(cboRele.Text);
            dispositivo.direcao = (cboDirecao.Text);
            dispositivo.tempoARele = Convert.ToDouble(txtTempo.Text);
            dispositivo.ip = txtIpControladora.Text;           
            dispositivo.equipamentoAtrelado = txtDispositivo.Text.PadLeft(2,'0');

            AcessoDbSqlite.AdicionaDispositivo(dispositivo);
            carregaListaEquipamento();
            limpar();
            MessageBox.Show("Cadastro Efetuado");
            txtDispositivo.Focus();
        }
        private void limpar()
        {
            txtDispositivo.Clear();
            txtIpControladora.Clear();
            txtTempo.Clear();
            cboIdControladora.Text = "";
            cboDirecao.Text = "";
            cboRele.Text = "";

        }



        private void carregaListaEquipamento()
        {
            DataTable dataTable = AcessoDbSqlite.CarregaListaEquipamento();
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.DataSource = dataTable;
        }

        private void frmCadastroControladoraEquipamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Pula para o próximo controle na ordem de tabulação
                SelectNextControl(ActiveControl, true, true, true, true);
                e.Handled = true; // Impede que o Enter execute seu comportamento padrão (como pular linha em um TextBox)
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDispositivo.Text))
            {
                MessageBox.Show("Selecione um equipamento para deletar");
                return;
            }


            DialogResult resultado = MessageBox.Show(
   "Deseja realmente deletar equipamento cadastrado ?", // Mensagem
   "Confirmação de Exclusão",                                         // Título
   MessageBoxButtons.YesNo,                                     // Botões
   MessageBoxIcon.Warning                                             // Ícone
);

            // Você pode verificar a resposta do usuário:
            if (resultado == DialogResult.Yes)
            {
                AcessoDbSqlite.DeleteDispositivo(Convert.ToInt32(txtDispositivo.Text));

                MessageBox.Show("Equipamento Excluido");
                carregaListaEquipamento();
                limpar();


            }
            else if (resultado == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada.");
            }

            carregaListaEquipamento();

         

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Obter a linha selecionada (a linha clicada)
                DataGridViewRow linha = dgv.Rows[e.RowIndex];

                // 2. Extrair os dados da linha.
                //    Use o nome exato da coluna (ou o índice, se preferir).

                // Coluna 1: Nome (assumindo que a coluna 'Nome' é a segunda coluna, índice 1)
                string ipControladora = linha.Cells["Ip Controladora"].Value.ToString();
                string equipamento = linha.Cells["Equipamento"].Value.ToString();
                string idControladora = linha.Cells["Id Controladora"].Value.ToString();
                string Direcao = linha.Cells["Direcao"].Value.ToString();
                string rele = linha.Cells["Rele"].Value.ToString();
                string tempoARele = linha.Cells["tempo"].Value.ToString();

                // Coluna 2: Idade (assumindo que a coluna 'Idade' é a terceira coluna, índice 2)
                // Certifique-se de que o tipo seja compatível (ex: int, se a coluna for numérica)
                //string idade = linha.Cells["IdadeColunaGrid"].Value.ToString();

                // 3. Preencher as caixas de texto
                // txtDispositivo.Text = nome;
                txtIpControladora.Text = ipControladora;
                txtDispositivo.Text = equipamento;
                cboRele.Text = rele;
                cboDirecao.Text = Direcao;
                txtTempo.Text = tempoARele;
                cboIdControladora.Text = idControladora;

                // Observação: Substitua "NomeColunaGrid" e "IdadeColunaGrid" pelo
                // nome real das colunas no seu DataGridView.
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            bool retorno = verificaSeAlgumaCaixaTextoVazia();
            if (!retorno)
            {
                return;

            }

            int verificaSeEstaCadastrado = AcessoDbSqlite.verificaSeEstaCadastrado(txtDispositivo.Text);

            if (verificaSeEstaCadastrado == 0)
            {
                MessageBox.Show("Equipamento não está cadastrado");
                return;
            }




            Dispositivo dispositivo = new Dispositivo();
            dispositivo.idControladora = cboIdControladora.Text;
            dispositivo.ip = txtIpControladora.Text;
            dispositivo.rele = Convert.ToInt32(cboRele.Text);
            dispositivo.tempoARele = Convert.ToDouble(txtTempo.Text);
            dispositivo.direcao = cboDirecao.Text;
            dispositivo.equipamentoAtrelado = txtDispositivo.Text;

            AcessoDbSqlite.UpdateDispositivo(dispositivo);
            carregaListaEquipamento();
            limpar();

          
                MessageBox.Show("Atualização Efetuada");

            txtDispositivo.Focus();
               


        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limpar();
            txtDispositivo.Focus();

        }

        private void txtTempo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada é um número (0-9)
            if (Char.IsDigit(e.KeyChar))
            {

                //permite ação.
                return;

            }

            if (e.KeyChar == '.')
            {
                // Cancela a ação, impedindo a digitação de ponto
                e.Handled = true;
                // O 'return' é opcional aqui, mas interrompe o método
                return;
            }

            if (e.KeyChar == ',')
            {
                // Permite Acão.
                return;
            }

            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.')
            {
                // Se for Backspace ou Ponto, o 'return' permite a ação
                // e interrompe qualquer lógica de bloqueio que possa vir depois.
                return;
            }


            e.Handled = true;



            //' Cancela qualquer outra tecla, impedindo a digitação
            //e.Handled = True
            //}

        }

        private void txtDispositivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {

                //permite ação.
                return;

            }

            if (e.KeyChar == (char)Keys.Back)
            {
                // Se for Backspace ou Ponto, o 'return' permite a ação
                // e interrompe qualquer lógica de bloqueio que possa vir depois.
                return;
            }


            e.Handled = true;
        }

        private void txtIpControladora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada é um número (0-9)
            if (Char.IsDigit(e.KeyChar))
            {

                //permite ação.
                return;

            }

            if (e.KeyChar == ',')
            {
                // Cancela a ação, impedindo a digitação de virgula
                e.Handled = true;
                // O 'return' é opcional aqui, mas interrompe o método
                return;
            }

            if (e.KeyChar == '.')
            {
                // Permite Acão.
                return;
            }

            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.')
            {
                // Se for Backspace ou Ponto, o 'return' permite a ação
                // e interrompe qualquer lógica de bloqueio que possa vir depois.
                return;
            }


            e.Handled = true;


        }
    }


}
