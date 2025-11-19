
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
        private GarenGateway garenGateway = new GarenGateway();
        private static int CONTADORMSGLOOP = 0;
        private static string caminhoArquivoLogLoop;
        private static int gravaLogLoop = 0;
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


                // Acessamos a variável global/membro CONTADORMSGLOOP
                CONTADORMSGLOOP = CONTADORMSGLOOP + 1; // Pode ser escrito como CONTADORMSGLOOP++;

            // if (CONTADORMSGLOOP > 10000)
            if (CONTADORMSGLOOP > 2)
            {

                // Chamada ao método global/membro
                CriaOutroArquivoTxt();
                CONTADORMSGLOOP = 0;
            }

            sr.Close();

            // O VB.NET usa If gravaLogLoop = "1" Then. Em C#, usamos if (gravaLogLoop == "1")
           
                // Variável não é realmente usada na lógica Try/Catch original, mas mantida por fidelidade
                int numeroResultado;

                try
                {
                    // O código VB.NET original está usando uma variável 'sr' (StreamWriter) globalmente 
                    // e gerenciando o fechamento/abertura manualmente. Isso pode ser problemático.
                    // A melhor prática em C# (e VB.NET) para escrever em arquivos é usar a classe File estática 
                    // ou o comando 'using' para garantir que o Stream seja fechado.

                    // Usando File.AppendAllText() simplifica o processo (melhor prática):
                    // File.AppendAllText(caminhoAux, mensagem + Environment.NewLine); 

                    // Para ser mais fiel à lógica original de usar um StreamWriter (StreamWriter sr = ...):
                    // OBS: Não é necessário fazer sr.Close() e reabrir com File.AppendText(). 
                    // O File.AppendText já cria/abre o StreamWriter no modo append.

                    using (StreamWriter sw = File.AppendText(caminhoArquivoLogLoop))
                    {
                        sw.WriteLine(mensagem);
                    } // O 'using' garante que sw.Close() seja chamado automaticamente, mesmo em caso de erro.

                    numeroResultado = 0; // Supondo sucesso
                }
                catch (Exception ex)
                {
                    // Em C#, a variável 'numeroResultado' deve ser inicializada ou definida antes de ser usada.
                    // Aqui, apenas a definimos dentro do catch, como na lógica VB.NET.
                    numeroResultado = 1;

                    // Em uma aplicação real, você deve logar o 'ex' para saber o que aconteceu!
                    // Console.WriteLine($"Erro ao escrever no arquivo: {ex.Message}");
                }

                //// Acessamos a variável global/membro CONTADORMSGLOOP
                //CONTADORMSGLOOP = CONTADORMSGLOOP + 1; // Pode ser escrito como CONTADORMSGLOOP++;

                //// if (CONTADORMSGLOOP > 10000)
                //if (CONTADORMSGLOOP > 2)
                //{
                   
                //    // Chamada ao método global/membro
                //    CriaOutroArquivoTxt();
                //    CONTADORMSGLOOP = 0;
                //}
            }
        }

        public static void CriaOutroArquivoTxt()
        {
            // 1. Formatar a data e hora atual.
            // Em C#, usamos DateTime.Now e o método ToString() com o formato desejado.
            string dataformatada = DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
            

            // 2. Definir o novo caminho do arquivo.
            // string novoCaminhoAux = @"C:\SISTEMA\LOGLOOP\logLoop" + dataformatada + ".txt";

            string novoCaminhoAux = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogLoop\\" + dataformatada + ".txt");



            // 3. Estrutura de Verificação e Criação.
            // O código VB.NET original verifica se 'caminho' existe e, se não, faz a mesma coisa.
            // Isso pode ser drasticamente simplificado em C# (e VB.NET) eliminando a verificação
            // e apenas criando o arquivo diretamente no novo caminho 'novoCaminhoAux'.

            // O VB.NET original:
            // If File.Exists(caminho) Then ... End If
            // If Not File.Exists(caminho) Then ... End If

            // Simplificado em C# (e mais eficiente):

            // Atualiza a variável global/membro 'caminhoAux' para o novo arquivo
            caminhoArquivoLogLoop = novoCaminhoAux;

            // File.Create(caminhoAux) cria o arquivo. O 'using' garante que o FileStream seja liberado.
            using (FileStream fs = File.Create(caminhoArquivoLogLoop))
            {
                // Este bloco fica vazio pois o objetivo é apenas criar e fechar o arquivo.
            }

            // 4. Atribuir o novo StreamWriter ao objeto 'sr'.
            // O VB.NET usava sr = File.AppendText(caminhoAux)
            // Em C#, 'sr' é provavelmente um System.IO.StreamWriter.
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
                //Console.WriteLine($"A pasta já existe em: {caminhoDaPasta}");
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
            CriarPastaBancoDeDadosSeNaoExiste();            
            ControladoraGaren.CLASSES.AcessoDbSqlite.CriarTabelaSQlite(this);
            CarregarListaEquipamentoSqLite();
            CarregaConfigGaren();

            if (gravaLogLoop == 1)
            {
                CriarPastaLogLoopSeNaoExiste();
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
            //Text = appSettings.ApplicationName + " - " + appSettings.BackendIp;
            ipServidor = appSettings.BackendIp;
            portaServidor = appSettings.BackendPort;
            timeSpan = appSettings.TimeSpan;
            gravaLogLoop = appSettings.GravaLogLoop;

           



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
