using Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControladoraGaren
{
    public partial class frmConfiguracao : Form
    {
        private IConfigurationRoot _configuration;
        public frmConfiguracao()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Configurações da Controladora";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void frmConfiguracao_Load(object sender, EventArgs e)
        {

            //// 1. Constrói o objeto de configuração para leitura
            //_configuration = new ConfigurationBuilder()
            //        .SetBasePath(Directory.GetCurrentDirectory())
            //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //         .Build();

            //// 2. Carrega as configurações para o modelo C#
            //var config = _configuration.GetSection("ApplicationSettings").Get<ApplicationSettings> ();
            var config = AppSettings.Settings.ApplicationSettings;

            //3.Preenche os campos do formulário
            if (config != null)
            {
                txtIp.Text = config.BackendIp;
                txtPorta.Text = config.BackendPort.ToString(); // Assumindo NumericUpDown
                txtStringConexao.Text = config.StringConexao;

               int gravarLogLoop = config.GravaLogLoop;
               int bancoDadosLocal = config.BancoDadosLocal;
               string stringConexao = config.StringConexao;


                if (gravarLogLoop == 0)
                {
                    chkGravarPacotesEmArquivo.Checked = false;  
                }
                else
                {
                    chkGravarPacotesEmArquivo.Checked = true;
                }

                if (bancoDadosLocal == 0)
                {
                    chkBancoDadosLocal.Checked = false;
                }
                else
                {
                    chkBancoDadosLocal.Checked = true;
                    txtStringConexao.Visible = false;   
                }

            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {


            DialogResult resultado = MessageBox.Show(
    "Deseja realmente alterar as informações ?", // Mensagem
    "Confirmação",                                         // Título
    MessageBoxButtons.YesNo,                                     // Botões
    MessageBoxIcon.Warning                                             // Ícone
);

            // Você pode verificar a resposta do usuário:
            if (resultado == DialogResult.Yes)
            {
                string json = File.ReadAllText("appsettings.json");
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                // Altera o valor da chave
                jsonObj["ApplicationSettings"]["BackendIp"] = txtIp.Text;
                jsonObj["ApplicationSettings"]["BackendPort"] = txtPorta.Text;
                jsonObj["ApplicationSettings"]["StringConexao"] = txtStringConexao.Text;

                if (chkGravarPacotesEmArquivo.Checked)
                {
                    jsonObj["ApplicationSettings"]["GravaLogLoop"] = 1;
                }
                else
                {
                    jsonObj["ApplicationSettings"]["GravaLogLoop"] = 0;
                }

                if (chkBancoDadosLocal.Checked)
                {
                    jsonObj["ApplicationSettings"]["BancoDadosLocal"] = 1;
                }
                else
                {
                    jsonObj["ApplicationSettings"]["BancoDadosLocal"] = 0;
                }






                // Opcional: Salvar em um objeto fortemente tipado antes de serializar
                // ...

                // Serializa de volta para string formatada
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

                // Escreve de volta no arquivo
                File.WriteAllText("appsettings.json", output);

                MessageBox.Show("Alteração Efetuada. Para que as alterações tenham efeito deve-se reiniciar a aplicação");
                Close();
            }
            else if (resultado == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada.");
            }


















        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
