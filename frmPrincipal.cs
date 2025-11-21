
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
        private static string ipServidor ;
        private static int portaServidor;
        private static int timeSpan;
        private GarenGateway garenGateway = new GarenGateway();
        private static int CONTADORMSGLOOP = 0;
        private static string caminhoArquivoLogLoop;
        private static int gravaLogLoop = 0;
        private static int bancoDadosLocal = 0;
        private static StreamWriter sr = new StreamWriter("Robson");


        
        // static frmPrincipal()
        //{

        //CarregarListaEquipamento();
        //ControladoraGaren.CLASSES.AcessoDbSqlite.CriarTabelaSQlite(this);
        //CarregarListaEquipamentoSqLite();
        //CarregaConfigGaren();
        //iniciaComunicacaoComGarenEGerenciador();
        // }

        //garenGateway.escreveNoList += MensagemSemPararAutorizationSaida;

        // ...
        // O método (manipulador) deve ser declarado na sua classe com a assinatura correta
        //private void MensagemSemPararAutorizationSaida(object sender, EventArgs e)
        //        {
        //            // Lógica para tratar o evento, por exemplo:
        //            // Console.WriteLine("Recebi a autorização de saída!");
        //        }


        public static void EscreverTextoTxt(string mensagem)
        {

            if (gravaLogLoop == 1)
            {


                
            CONTADORMSGLOOP = CONTADORMSGLOOP + 1; 

            if (CONTADORMSGLOOP > 10000)
            {

               
                CriaOutroArquivoTxt();
                CONTADORMSGLOOP = 0;
            }

            sr.Close();

           
                int numeroResultado;

                try
                {
                  

                    using (StreamWriter sw = File.AppendText(caminhoArquivoLogLoop))
                    {
                        sw.WriteLine(mensagem);
                    } 

                    numeroResultado = 0; 
                }
                catch (Exception ex)
                {
                  
                    numeroResultado = 1;

                }

               
            }
        }

        public static void CriaOutroArquivoTxt()
        {
          
            string dataformatada = DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
            

         

            string novoCaminhoAux = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogLoop\\" + dataformatada + ".txt");



        
            caminhoArquivoLogLoop = novoCaminhoAux;

          
            using (FileStream fs = File.Create(caminhoArquivoLogLoop))
            {
                // Este bloco fica vazio pois o objetivo é apenas criar e fechar o arquivo.
            }

          
            sr = File.AppendText(caminhoArquivoLogLoop);
        }

        public static void iniciaComunicacaoComGarenEGerenciador()
        {
            foreach (Dispositivo dispositivoParaEnviarMsg in list)
            {



                var controladora = new GarenGateway(dispositivoParaEnviarMsg.equipamentoAtrelado, dispositivoParaEnviarMsg.idControladora, dispositivoParaEnviarMsg.ip, ipServidor, portaServidor, timeSpan);
                _ = controladora.ConnectAndListenAsync(new CancellationToken());


            }

        }
        public void CriarPastaBancoDeDadosSeNaoExiste()
        {

           // string caminhoDaPasta = "c:\\sistema\\controladoraGaren\\bancoSqLite";

            string caminhoDaPasta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bancoSqLite");

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
               
            }
        }

        public void CriarPastaLogLoopSeNaoExiste()
        {

            caminhoArquivoLogLoop = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogLoop");

            if (!Directory.Exists(caminhoArquivoLogLoop))
            {
                Directory.CreateDirectory(caminhoArquivoLogLoop);
            }

            string dataformatada = DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");


            // 2. Definir o novo caminho do arquivo.
            // string novoCaminhoAux = @"C:\SISTEMA\LOGLOOP\logLoop" + dataformatada + ".txt";

            caminhoArquivoLogLoop = Path.Combine(caminhoArquivoLogLoop,  dataformatada + ".txt");




        }

        public frmPrincipal()
        {
            InitializeComponent();
            
            CarregaConfigGaren();

            if (gravaLogLoop == 1)
            {
                CriarPastaLogLoopSeNaoExiste();
            }

            if (bancoDadosLocal == 1)
            {
                CriarPastaBancoDeDadosSeNaoExiste();
                ControladoraGaren.CLASSES.AcessoDbSqlite.CriarTabelaSQlite(this);
                CarregarListaEquipamentoSqLite();


            }
            else
            {
                mnuCadastrarEquipamento.Visible = false;
                try
                {
                    CarregarListaEquipamento();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }




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
            ipServidor = appSettings.BackendIp;
            portaServidor = appSettings.BackendPort;
            timeSpan = appSettings.TimeSpan;
            gravaLogLoop = appSettings.GravaLogLoop;
            bancoDadosLocal = appSettings.BancoDadosLocal;


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

        public static void escreveNoListBox(ListBox lst, string mensagem)
        {
            if (lst.InvokeRequired)
            {
                lst.Invoke(new Action(() =>
                {
                    // Código para manipular o objeto de UI aqui
                    lst.Items.Add(mensagem);
                    lst.SelectedIndex = lst.Items.Count - 1;
                    EscreverTextoTxt(mensagem);
                }));
            }
            else
            {
                // Código para manipular o objeto de UI aqui
                lst.Items.Add(mensagem);
                lst.SelectedIndex = lst.Items.Count - 1;
                EscreverTextoTxt(mensagem);
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
           
            VerificarSeAplicativoJaEstaAberto("ControladoraGaren");

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Controladora";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Minimized;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.tabControl.SelectedIndex = 1;  
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

        private void lstMensagensTrocadas_DrawItem(object sender, DrawItemEventArgs e)
        {
            // O controle que disparou o evento (ListBox, ComboBox, etc.)
            ListBox controle = sender as ListBox;

            // Ignora o desenho se o índice for inválido (como -1)
            if (e.Index < 0)
            {
                return;
            }

            try
            {
                // 1. Desenha o fundo (necessário para manter a cor de seleção)
                e.DrawBackground();

                // 2. Obtém o texto do item
                string text = controle.Items[e.Index].ToString();
                Brush brushAux = System.Drawing.Brushes.Black; // Define uma cor padrão!

                // 3. Lógica Condicional
                if (text.Contains("Envia"))
                {
                    brushAux = System.Drawing.Brushes.Red;
                }
                else if (text.Contains("Recebe"))
                {
                    brushAux = System.Drawing.Brushes.Blue;
                }
                // else: mantém a cor Black (preto) definida acima

                // 4. Desenha o texto com a cor definida
                e.Graphics.DrawString(
                    text,
                    e.Font, // Usa a fonte padrão do evento
                    brushAux,
                    e.Bounds.X,
                    e.Bounds.Y
                );

                // Desenha o foco se o item estiver selecionado
                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                // Tratar exceção (bom para debug)
                Console.WriteLine(ex.Message);
            }
        }

        private void btnLimparHistorico_Click(object sender, EventArgs e)
        {
            lstMensagensTrocadas.Items.Clear();
        }
        private void CopiarListBoxParaClipboard(ListBox lst)
        {
            // Assumindo que sua ListBox se chama 'listBoxEnvioRecebimento'

            try
            {
                // 1. Cria um StringBuilder para construir o texto eficientemente
                StringBuilder buffer = new StringBuilder();

                // 2. Itera por todos os itens da ListBox
                for (int i = 0; i < lst.Items.Count; i++)
                {
                    // Adiciona o item ao buffer
                    buffer.Append(lst.Items[i].ToString());

                    // Adiciona uma quebra de linha (vbCrLf em VB.NET é "\r\n" em C#)
                    buffer.Append("\r\n");
                }

                // 3. Copia o texto final para a Área de Transferência
                Clipboard.SetText(buffer.ToString());

                // Opcional: Feedback ao usuário
                // MessageBox.Show("Conteúdo copiado para a área de transferência!", "Sucesso");
            }
            catch (Exception ex)
            {
                // Trate a exceção se necessário. No seu VB.NET original, a exceção é ignorada.
                // Console.WriteLine("Erro ao copiar para o clipboard: " + ex.Message);
            }
        }

        private void btnCopiarHistorico_Click(object sender, EventArgs e)
        {
            CopiarListBoxParaClipboard(lstMensagensTrocadas);
        }
    }
}
