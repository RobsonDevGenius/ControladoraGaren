using Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.CLASSES
{
    public class Acesso
    {

      

        private string StringConexaoInteira = AppSettings.Settings.ApplicationSettings.StringConexao;

        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;
        private SqlConnection CriarConexao()
        {
         
            
            return new SqlConnection(StringConexaoInteira);

        }

        public string AbrirConexao()
        {
            SqlConnection sqlConnection = CriarConexao();
            try
            {

                sqlConnection.Open();

            }
            catch (Exception e)
            {
                string mensagem = e.Message;
                return mensagem;
            }

            sqlConnection.Close();

            return "Conexão Efetuada com Sucesso";
        }

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }
        public object ExecutarManipulacaoTexto(CommandType commandType,
        string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (dentro da caixa que vai 
                //trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em Segundos

                ////Adicionar os parâmetros no comando
                //foreach (SqlParameter sqlParameter in sqlParameterCollection)
                //{
                //    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                //}
                var retorno = sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                return retorno;

              


            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);

            }

        }

        public string ExecutarManipulacaoTextoRetornoString(CommandType commandType,
       string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (dentro da caixa que vai 
                //trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em Segundos

                ////Adicionar os parâmetros no comando
                //foreach (SqlParameter sqlParameter in sqlParameterCollection)
                //{
                //    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                //}
                var retorno = sqlCommand.ExecuteScalar();
                sqlConnection.Close();                
                return (string)retorno;


            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);

            }

        }


        public object ExecutarManipulacao(CommandType commandType,
        string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (dentro da caixa que vai 
                //trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em Segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                }

                var retorno = sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                return retorno;

               

            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);

            }

        }

        public DataTable ExecutarConsulta(CommandType commandType,
string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (dentro da caixa que vai 
                //trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em Segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                }

                //Criar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //DataTable = Tabela Dados vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new DataTable();
                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher o datatable
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                sqlConnection.Close();
                return dataTable;


            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);

            }



        }
    }
}
