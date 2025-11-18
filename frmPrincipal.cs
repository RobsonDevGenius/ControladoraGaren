
using Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.CLASSES;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.VisualBasic;
using ControladoraGaren;
using ControladoraGaren.CLASSES;

namespace WinFormsApp1

{
    public partial class frmPrincipal : Form
    {
        //private static readonly HttpClient client = new HttpClient();
        private static HttpClient client;
        private DateTime dataHora = DateTime.MinValue;
        private static Negocio negocio = new Negocio();
        public static ListDispositivo<Dispositivo> list = [];
        private static string ipServidor;
        private static int portaServidor;
        private static int timeSpan;

        static frmPrincipal()
        {
            
            //CarregarListaEquipamento();
            //ControladoraGaren.CLASSES.AcessoDbSqlite.CriarTabelaSQlite(this);
            //CarregarListaEquipamentoSqLite();
            //CarregaConfigGaren();
            //iniciaComunicacaoComGarenEGerenciador();
        }

        public static void iniciaComunicacaoComGarenEGerenciador()
        {
            foreach (Dispositivo dispositivoParaEnviarMsg in list)
            {



                var controladora = new GarenGateway(dispositivoParaEnviarMsg.equipamentoAtrelado, dispositivoParaEnviarMsg.idControladora, dispositivoParaEnviarMsg.ip, ipServidor, portaServidor, timeSpan);
                _ = controladora.ConnectAndListenAsync(new CancellationToken());


            }

        }
        public void CriarPastaSeNaoExiste()
        {

            string caminhoDaPasta = "c:\\sistema\\controladoraGaren\\bancoSqLite";
           
            //string caminhoDaPasta= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bancoSqLite");
            // 1. Verificar se a pasta já existe
            if (!Directory.Exists(caminhoDaPasta))
            {
                // 2. Se a pasta NÃO existe, ela é criada
                try
                {
                    Directory.CreateDirectory(caminhoDaPasta);
                    MessageBox.Show($"Pasta criada com sucesso em: {caminhoDaPasta}");
                }
                catch (Exception ex)
                {
                    // Tratar possíveis erros (ex: permissão negada, caminho inválido)
                    MessageBox.Show($"Erro ao criar a pasta: {ex.Message}");
                }
            }
            else
            {
                //Console.WriteLine($"A pasta já existe em: {caminhoDaPasta}");
            }
        }


        public frmPrincipal()
        {
            InitializeComponent();
            CriarPastaSeNaoExiste();
            ControladoraGaren.CLASSES.AcessoDbSqlite.CriarTabelaSQlite(this);
            CarregarListaEquipamentoSqLite();
            CarregaConfigGaren();
            iniciaComunicacaoComGarenEGerenciador();






        }

        public static void CarregarListaEquipamento()
        {
            DataTable dt = negocio.CarregaListaEquipamento();
            foreach (DataRow row in dt.Rows)
            {
                Dispositivo disposito = new Dispositivo
                {


                    codigo = Convert.ToInt32(row["codigo"]),
                    ip = row["ip"].ToString(),
                    idControladora = row["idControladora"].ToString(),
                    equipamentoAtrelado = row["equipamentoAtrelado"].ToString(),
                    direcao = row["direcao"].ToString(),
                    rele = Convert.ToInt32(row["rele"]),
                    tempoARele = Convert.ToDouble(row["tempoARele"])

                };
                list.Add(disposito);

            }


        }

        public static void CarregarListaEquipamentoSqLite()
        {
            DataTable dt = AcessoDbSqlite.CarregaListaEquipamento();
            foreach (DataRow row in dt.Rows)
            {
                Dispositivo disposito = new Dispositivo
                {


                    codigo = Convert.ToInt32(row["codigo"]),
                    ip = row["Ip Controladora"].ToString(),
                    idControladora = row["Id Controladora"].ToString(),
                    equipamentoAtrelado = row["Equipamento"].ToString(),
                    direcao = row["Direcao"].ToString(),
                    rele = Convert.ToInt32(row["Rele"]),
                    tempoARele = Convert.ToDouble(row["Tempo"])

                };
                list.Add(disposito);

            }


        }

        public static void CarregaConfigGaren()
        {
            var appSettings = AppSettings.Settings.ApplicationSettings;
            //Text = appSettings.ApplicationName + " - " + appSettings.BackendIp;
            ipServidor = appSettings.BackendIp;
            portaServidor = appSettings.BackendPort;
            timeSpan = appSettings.TimeSpan;



            //DataTable dt = negocio.CarregaConfiguracaoGaren();
            //foreach (DataRow row in dt.Rows)
            //{

            //    timeSpan = Convert.ToInt32(row["timeSpan"]);
            //    ipServidor = row["ipServidor"].ToString();
            //    portaServidor = Convert.ToInt32(row["portaServidor"].ToString());

            //}

        }

        public static void carregaListAux(string mensagem)
        {
            if (lstMensagem.InvokeRequired)
            {
                lstMensagem.Invoke(new Action(() =>
                {
                    // Código para manipular o objeto de UI aqui
                    lstMensagem.Items.Add(mensagem);
                }));
            }
            else
            {
                // Código para manipular o objeto de UI aqui
                lstMensagem.Items.Add(mensagem);
            }

        }

        private void btnLimparLista_Click(object sender, EventArgs e)
        {
            lstMensagem.Items.Clear();
        }

        public static void carregaListDispConectados(string mensagem)
        {
            if (lstMensagemDispositivosConectados.InvokeRequired)
            {
                lstMensagemDispositivosConectados.Invoke(new Action(() =>
                {
                    // Código para manipular o objeto de UI aqui
                    lstMensagemDispositivosConectados.Items.Add(mensagem);
                }));
            }
            else
            {
                // Código para manipular o objeto de UI aqui
                lstMensagemDispositivosConectados.Items.Add(mensagem);
            }

        }
        private void abrirDialog()
        {
            string valorPadrao = "c1234";

            // Chama o InputBox. Ele retorna a string inserida pelo usuário.
            // Se o usuário clicar em "Cancelar", ele retorna uma string vazia ("").
            string valorDigitado = Interaction.InputBox(
                "Digite a Senha Para Sair", // Prompt (Texto dentro da caixa)
                "Senha c1234"      // Título da caixa
                                   // Valor padrão no campo
            );

            if (string.IsNullOrEmpty(valorDigitado))
            {
                MessageBox.Show("cancelado.");
                this.TopMost = true;
            }
            else
            {
                // Tente converter para inteiro (exemplo de conversão)
                if (valorDigitado == valorPadrao)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Senha incorreta.");
                }
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //var appSettings = AppSettings.Settings.ApplicationSettings;
            //Text = appSettings.ApplicationName + " - " + appSettings.BackendIp;
            //ipServidor = appSettings.BackendIp;
            //portaServidor = appSettings.BackendPort;
            //timeSpan = appSettings.TimeSpan;
            VerificarSeAplicativoJaEstaAberto("ControladoraGaren");

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Controladora";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Minimized;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.TopMost = true;

       



        }

        private void VerificarSeAplicativoJaEstaAberto(string nomeDoAplicativo)
        {


            // Obtém todos os processos com o nome "notepad"
            Process[] processos = Process.GetProcessesByName(nomeDoAplicativo);

            // Verifica se a lista de processos não está vazia
            if (processos.Length > 1)
            {
                // O aplicativo está aberto

                System.Windows.Forms.Application.Exit();
            }

        }
        private void btnLimparLists_Click(object sender, EventArgs e)
        {
            lstMensagem.Items.Clear();
            lstMensagemDispositivosConectados.Items.Clear();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            abrirDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmConfiguracao frm = new frmConfiguracao();
            frm.ShowDialog();
            this.TopMost = true;
            //Close(); 
        }

        private void mnuConfiguracao_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmConfiguracao frm = new frmConfiguracao();
            frm.ShowDialog();
            this.TopMost = true;
        }

        private void mnuMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void mnuSair_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            abrirDialog();
        }

        private void mnuCadastrarEquipamento_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            frmCadastroControladoraEquipamento frm = new frmCadastroControladoraEquipamento();
            frm.ShowDialog();
            this.TopMost = true;
        }
    }
}
