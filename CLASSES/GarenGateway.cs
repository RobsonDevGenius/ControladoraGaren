using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.CLASSES;

namespace WinFormsApp1.CLASSES;

public class GarenGateway
{
    public event Func<string, Task>? OnMessageReceived;
    private TcpClient _clientGerenciador;
    private HttpClient _clientGaren;
    private readonly string _idEquipamento;
    private readonly string _idControladora;
    private readonly string _ipControladora;
    private readonly string _ipGerenciador;
    private readonly int _portGerenciador;

    public GarenGateway()
    {

    }

    private string UrlControladora
    {
        get
        {
            return $"http://{_ipControladora}:5000";
        }
    }

    private readonly int _timeToReconnectInMs;

    private const int BufferSize = (1024 * 4); // 4KB
    
    public GarenGateway(string idEquipamento, string idControladora, string ipControladora, string ipGerenciador, int portGerenciador, int timeToReconnectInSeconds )
    {
        _clientGerenciador = new TcpClient();
        _clientGaren = new HttpClient();
        _clientGaren.DefaultRequestHeaders.Add("Authorization", "senha");  // Adiciona o cabeçalho uma vez
        _clientGaren.Timeout = TimeSpan.FromSeconds(timeToReconnectInSeconds);
        _idEquipamento = idEquipamento;
        _idControladora = idControladora;
        _timeToReconnectInMs = timeToReconnectInSeconds * 1000;        
        _ipControladora = ipControladora;
        _ipGerenciador = ipGerenciador;
        _portGerenciador = portGerenciador;
    }


    private async Task abreLeituraParaGerenciador()
    {
        frmPrincipal.carregaListAux("Entrada No Loop Abre Leitura Gerenciador");
        using (var stream = _clientGerenciador.GetStream())
        {

            // Buffer para receber dados
            byte[] buffer = new byte[1024];
            int bytesRead;


            // Loop para receber mensagens continuamente
            while (true)
            {
               


                try
                {

                    try
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            // Conexão fechada pelo servidor

                            break;
                        }
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        await ProcessReceivedMessage(receivedMessage);
                    }
                    catch (IOException ex)

                    {
                        frmPrincipal.carregaListAux("Saida Do Loop Abre Leitura Gerenciador: " + ex.Message);

                        fecharSocket();


                    }

                }
                catch (IOException ex)
                {

                    frmPrincipal.carregaListAux("Saida");


                }
            }

            escreveMensagemListDispostivosConectados("Saiu no Loop");
        }

       

    }
   
    private async Task EnviarComandoConexao(CancellationToken cancellationToken)
    {
        ////string mensagemEnvio = "|1001|" + dp.equipamentoAtrelado + "|";
        string mensagemEnvio = "|1001|" + _idEquipamento + "|";
        await EnviaMensagem(mensagemEnvio);
        await Task.Delay(1000, cancellationToken);

    }

  
    
        private async Task<bool> VerificarConexaoGaren()
    {
        //var json = JsonConvert.SerializeObject("");
        //var content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            var response = await _clientGaren.GetAsync($"{UrlControladora}/resposta");

            await Task.Delay(100);

            if (!response.IsSuccessStatusCode)
            {
              
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Erro ao conectar na controladora: {ex.Message}");
            return false;
        }

        return true;
    }

    private  async Task ProcessaComandoAcionamentoRemoto(int porta, double tempo, string ipControladora)
    {
        try
        {


            string ip = UrlControladora;
            string url = ip + "/api/comando/acionamento_remoto";

            var comando = new
            {
                porta = porta,
                auxiliar = 0,
                tempo = tempo
            };

            var json = JsonConvert.SerializeObject(comando);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _clientGaren.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();
                await DeletarLimite1();

            }
        }
        catch (Exception ex)
        {

        }
    }

    private  async Task DeletarLimite1()
    {
        try
        {


            string ip = UrlControladora;
            string url = ip + "/api/eventos";
            var comando = new
            {
                limite = 1
            };


            var json = JsonConvert.SerializeObject(comando);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _clientGaren.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();

            }
        }
        catch (Exception ex)
        {
            //carregaListAux(ex.Message);
        }
    }

    public  async Task ProcessReceivedMessage(string message)
    {
        //Aqui você pode implementar a lógica para processar a mensagem recebida
        //MsgRecebida(0, message);
        await RecebeMensagemGerenciador(message);
        


        //Exemplo: Se a mensagem for "exit", encerra a conexão
        if (message.Trim().ToLower() == "exit")
        {

            //Aqui você pode implementar o código para fechar a conexão
            //Exemplo: return;
        }
    }

 

    private static Dispositivo PegaControladoraReferenteEquipamento(string id)
    {
        Dispositivo dispositivo = new();
        foreach (Dispositivo dispositivoParaEnviaMsg in frmPrincipal.list)
        {
            if (dispositivoParaEnviaMsg.equipamentoAtrelado == id)
            {
                dispositivo = dispositivoParaEnviaMsg;
                break;

            }
        }

        return dispositivo;
    }

    private static Dispositivo PegaEquipamentoReferenteId(string id)
    {
        Dispositivo dispositivo = new();
        foreach (Dispositivo dispositivoParaEnviaMsg in frmPrincipal.list)
        {
            if (dispositivoParaEnviaMsg.equipamentoAtrelado == id)
            {
                dispositivo = dispositivoParaEnviaMsg;

                break;
            }
        }

        return dispositivo;
    }
    public async Task  RecebeMensagemGerenciador(string mensagem)
    {
        string[] words = mensagem.Split("|");
        string pacote = words[1];
        string dispositivo = words[2];

        Dispositivo dispositivoRetorno = PegaEquipamentoReferenteId(dispositivo);

        if (pacote == "3002")
        {

                await ProcessaComandoAcionamentoRemoto(dispositivoRetorno.rele, dispositivoRetorno.tempoARele, dispositivoRetorno.ip);

        }

        if (pacote == "3003")
        {

                await ProcessaComandoAcionamentoRemoto(dispositivoRetorno.rele, dispositivoRetorno.tempoARele, dispositivoRetorno.ip);

        }


        if (pacote == "1000")
        {
            await EnviaMensagem($"|1001|{_idEquipamento}|");

        }

        if (pacote == "1002")
        {
            dispositivoRetorno = PegaControladoraReferenteEquipamento(dispositivo);
           
            await ProcessaComandoAcionamentoRemoto(dispositivoRetorno.rele, dispositivoRetorno.tempoARele, dispositivoRetorno.ip);

        }



    }
    private void escreveMensagemListDispostivosConectados(string msg)
    {
        frmPrincipal.carregaListDispConectados(msg);
    }

    public async Task ConnectAndListenAsync(CancellationToken cancellationToken)
    {
        Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Iniciando serviço de Socket com o Gerenciador.");
        DeletarLimite1();
      

        // Loop infinito para garantir a reconexão
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (! await VerificarConexaoGaren())
                {
                    
                    continue;
                }

               // Form1.carregaListAux("Garen Conectada Equipamento: " + _idEquipamento + " idControladora " + _idControladora);

                Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Conectado na Controladora");
                escreveMensagemListDispostivosConectados($"[Equipamento {_idEquipamento} / Controladora {_idControladora} ip {_ipControladora}] Conectado na Controladora");
                escreveMensagemListDispostivosConectados("");

                await _clientGerenciador.ConnectAsync(_ipGerenciador, _portGerenciador, cancellationToken);

                Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Conectado no Gerenciador.");
                escreveMensagemListDispostivosConectados($"[Equipamento {_idEquipamento} / Controladora {_idControladora} ip {_ipControladora}] Conectado no GERENCIADOR.");
                escreveMensagemListDispostivosConectados("");

                // Form1.carregaListAux("Servidor Conectado Equipamento: " + _idEquipamento + " idControladora " + _idControladora);

                await EnviarComandoConexao(cancellationToken);

                //nao pode colocar await pq senão trava a aplicacão.
                 abreLeituraParaGerenciador();
                //ReceberMensagem();

                await ListenForMessagesAsync(cancellationToken);
                Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Terminou Listen.");
            }
            catch (SocketException ex)
            {
                Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Erro de Socket: {ex.Message}. Tentando reconectar em { _timeToReconnectInMs / 1000} segundos...");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Erro inesperado: {ex.Message}. Tentando reconectar em {_timeToReconnectInMs / 1000} segundos...");
            }
            finally
            {
                // Se a conexão foi fechada ou perdida, garantimos que o estado está limpo para a próxima tentativa


                if (!_clientGerenciador.Connected)
                {
                    _clientGerenciador.Close();
                }
                _clientGerenciador.Dispose();

                // Recria a instância para a próxima tentativa de conexão


                _clientGerenciador = new TcpClient();
                Debug.WriteLine($"[Equipamento {_idEquipamento} / Controladora {_idControladora}] Client Gerenciador recriado.");


            }

            await Task.Delay(_timeToReconnectInMs, cancellationToken); // Espera antes de tentar reconectar
        }
    }

 

    private static Dispositivo PegaEquipamentoReferenteIdControladora(string id)
    {
        Dispositivo dispositivo = new();
        foreach (Dispositivo dispositivoParaEnviaMsg in frmPrincipal.list)
        {
            if (dispositivoParaEnviaMsg.idControladora == id)
            {
                dispositivo = dispositivoParaEnviaMsg;
                break;

            }
        }

        return dispositivo;
    }

    public async Task EnviaMensagem(string msg)
    {

        try
        {
       

            var stream = _clientGerenciador.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            await stream.WriteAsync(data, 0, data.Length);

            Debug.WriteLine($"Equipamento {_idEquipamento} / Controladora {_idControladora}. Mensagem enviada: {msg}");

        }
        catch (Exception ex)
        {

            fecharSocket();

        }

    }

    public async Task<string> ReceberMensagem()
    {
        // Buffer para receber dados
        byte[] buffer = new byte[1024];
        int bytesRead = 0;
        CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(1));

        // Loop para receber mensagens continuamente
        while (true)
        {

            try
            {
                var stream = _clientGerenciador.GetStream();
                if (stream == null)
                    return "";

                Debug.WriteLine("*************************** Antes do ReadAsync");
                try
                {
                    bytesRead = await stream.ReadAsync(buffer, cancellationToken.Token);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("*************************** Caiu por timeout");
                    bytesRead = 0;
                }
                Debug.WriteLine("*************************** Depois do ReadAsync");
                if (bytesRead == 0)
                {
                    return "";
                }

                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                await ProcessReceivedMessage(receivedMessage);

                return receivedMessage;
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Controladora {_idControladora}. Erro comunicação gerenciador: {ex.Message}");
                fecharSocket();
            }
        }

    }

    public void fecharSocket()
    {
        try
        {


           _clientGerenciador.Close();

        }
        catch (Exception ex)
        {


        }

    }



    private void VerificarConexaoGerenciador()
    {
        bool houveAlteracaoStatus = _clientGerenciador.Client.Poll(1000, SelectMode.SelectRead);
        bool estaIndisponivel = (_clientGerenciador.Client.Available == 0);
        if (houveAlteracaoStatus && estaIndisponivel)
        {
           
            Debug.WriteLine($"Equipamento {_idEquipamento} / Controladora {_idControladora}. Conexão com o Gerenciador fechada");
            throw new Exception("Conexão encerrada pelo Gerenciador");
        }
    }



    private async Task ListenForMessagesAsync(CancellationToken cancellationToken)
    {
        escreveMensagemListDispostivosConectados("Entrou no ListenForMessagesAsync");
        while (!cancellationToken.IsCancellationRequested && _clientGerenciador.Connected)
        {
            try
            {
                if (!await VerificarConexaoGaren())
                {
                    break;
                }

                VerificarConexaoGerenciador();

                //await VerificarMensagemStatusGerenciador();

                await ProcessarMensagemGaren();


                await Task.Delay(1000, cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Equipamento {_idEquipamento} / Controladora {_idControladora}. Erro ao escutar mensagens: {ex.Message}");
                throw;
            }
            if (cancellationToken.IsCancellationRequested) 
                Debug.WriteLine($"Equipamento {_idEquipamento} / Controladora {_idControladora}. CancelationToken finalizado");
                
            if (! _clientGerenciador.Connected)
                Debug.WriteLine($"Equipamento {_idEquipamento} / Controladora {_idControladora}. Servidor desconectado");
        }
        escreveMensagemListDispostivosConectados("Saiu no ListenForMessagesAsync");

    }


    private async Task ProcessarMensagemGaren()
    {


        string ip = UrlControladora;
        string url = ip + "/api/eventos?limite=1";
        int dataHora = 0;

        try
        {


         

            var response = await _clientGaren.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic dados = JsonConvert.DeserializeObject(jsonResponse);

            var data = dados.detalhes[0].data;

            if (dados.status == "sucesso" && data != dataHora)
            {
                DateTime dataUtc = DateTimeOffset.FromUnixTimeSeconds((long)data).DateTime;
                TimeZoneInfo saopauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                DateTime dataSP = TimeZoneInfo.ConvertTimeFromUtc(dataUtc, saopauloTimeZone);
                string dataFormatada = dataSP.ToString("dd-MM-yyyy HH:mm:ss");

                dynamic descricao_obj = dados.detalhes[0].descricao_obj;
                string door = descricao_obj.door;
                if (door != _idControladora)
                {
                    Debug.WriteLine($"Equipamento {_idEquipamento} / controladora {_idControladora} Pacote da porta {door} não é meu");
                    return;
                }
                Debug.WriteLine($"Equipamento {_idEquipamento} / controladora {_idControladora} Pacote da minha porta {door}");

                string descricao = dados.detalhes[0].descricao;
                dynamic conteudo = JsonConvert.DeserializeObject(descricao);
                string tipo = conteudo.type;

                if (tipo == "READER")
                {
                    //Debug.WriteLine($"Conversando com eq {_idEquipamento} / controladora {_idControladora} {jsonResponse}");

                    string cartao = conteudo.code;
                    var (fac, id) = ConverterWiegand(int.Parse(cartao));
                    string leitor = conteudo.door;                           

                    Dispositivo dispostivoRetorno = PegaEquipamentoReferenteIdControladora(leitor);

                    if (dispostivoRetorno.direcao == "ENTRADA")
                    {

                        string mensagemEnvio = "|3000|" + dispostivoRetorno.equipamentoAtrelado+ "|10|" + cartao.ToString().PadLeft(10,'0') + "|";
                        await EnviaMensagem(mensagemEnvio);

//                        var resposta = await ReceberMensagem();
//Debug.WriteLine($"Equipamento {_idEquipamento} / controladora {_idControladora} Resposta do gerenciador: {resposta}");
//                        await ProcessReceivedMessage(resposta);

                    }

                    if (dispostivoRetorno.direcao == "SAIDA")
                    {

                        string mensagemEnvio = "|3001|" + dispostivoRetorno.equipamentoAtrelado + "|10|" + cartao + "|";
                        await EnviaMensagem(mensagemEnvio);

//                        var resposta = await ReceberMensagem();
//Debug.WriteLine($"Equipamento {_idEquipamento} / controladora {_idControladora} Resposta do gerenciador: {resposta}");
//                        await ProcessReceivedMessage(resposta);



                    }


                    dataHora = data;

var comandoDelete = new
{
    porta = _idControladora,
    auxiliar = 0
};

var json = JsonConvert.SerializeObject(comandoDelete);
var content = new StringContent(json, Encoding.UTF8, "application/json");


var request = new HttpRequestMessage
{
    Method = HttpMethod.Delete,
    RequestUri = new Uri(url),
    Content = content
};


                    response = await _clientGaren.SendAsync(request);

jsonResponse = await response.Content.ReadAsStringAsync();

                }
            }
        }
        catch (Exception ex)
        {

            string erro = ex.Message;

        }

        //await Task.Delay(100);

        return;
    }



    private static (int, int) ConverterWiegand(int dadoWiegand)
    {
        int facilityCode = (dadoWiegand >> 16) & 0xFF;  // Máscara para pegar os primeiros 8 bits
        int idCode = dadoWiegand & 0xFFFF;  // Máscara para pegar os últimos 16 bits

        return (facilityCode, idCode);
    }

   
}

