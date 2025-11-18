

using System;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Windows.Forms;
using WinFormsApp1.CLASSES;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ControladoraGaren.CLASSES
{
    public class AcessoDbSqlite
    {
        private static SQLiteConnection sqliteConnection;
        public AcessoDbSqlite()
        { }
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=c:\\sistema\\controladoraGaren\\BancoSqLite\\Cadastro.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }
        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(@"c:\sistema\controladoraGaren\BancoSqLite\Cadastro.sqlite");
                //SQLiteConnection.CreateFile($"{AppDomain.CurrentDomain.BaseDirectory}\\BancoSqLite\\Cadastro.sqlite");
            
            }
            catch
            {
                throw;
            }
        }
        public static void CriarTabelaSQlite(WinFormsApp1.frmPrincipal frmPrincipal)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {

                   

                  string query = "CREATE TABLE IF NOT EXISTS T_EQUIPAMENTO_CONTROLADORA(" +
                        "    codigo INT IDENTITY(1,1) PRIMARY KEY, " +
                        "    ip VARCHAR(50)," +
                        "    idcontroladora VARCHAR(2) NOT NULL," +
                        "    rele INT NOT NULL," +
                        "    tempoARele FLOAT NOT NULL," +
                        "    direcao  VARCHAR(50) NOT NULL," +
                        "    idDispositivoEntradaSaida VARCHAR(2) NOT NULL)";

                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable CarregaListaEquipamento()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT codigo,idDispositivoEntradaSaida as Equipamento, idcontroladora as 'Id Controladora'," +
                        " ip as 'Ip Controladora', direcao as 'Direcao', rele as Rele, tempoARele as Tempo FROM T_EQUIPAMENTO_CONTROLADORA";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public static int obterUltimoCodigo()
        {
           
            int retorno = 0;
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT IFNULL(MAX(codigo), 0) FROM T_EQUIPAMENTO_CONTROLADORA";

                    var ret = cmd.ExecuteScalar();
                    retorno = Convert.ToInt32(ret);

                    return retorno;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetCliente(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Clientes Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AdicionaDispositivo(WinFormsApp1.CLASSES.Dispositivo dispositivo)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                 

                    string query = "INSERT INTO T_EQUIPAMENTO_CONTROLADORA (codigo,ip, idControladora, rele,tempoARele,idDispositivoEntradaSaida ,direcao" +
                  " ) values (@codigo,@ip, @idControladora, @rele,@tempoARele,@idDispositivoEntradaSaida,@direcao)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ip", dispositivo.ip);
                    cmd.Parameters.AddWithValue("@idControladora", dispositivo.idControladora);
                    cmd.Parameters.AddWithValue("@rele", dispositivo.rele);
                    cmd.Parameters.AddWithValue("@tempoARele", dispositivo.tempoARele);
                    cmd.Parameters.AddWithValue("@idDispositivoEntradaSaida", dispositivo.equipamentoAtrelado);                 
                    cmd.Parameters.AddWithValue("@direcao", dispositivo.direcao);
                    cmd.Parameters.AddWithValue("@codigo", dispositivo.codigo);



                    cmd.ExecuteNonQuery();
                    
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateDispositivo(Dispositivo dispositivo)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (dispositivo.equipamentoAtrelado != null)
                    { 
                        string query = "UPDATE T_EQUIPAMENTO_CONTROLADORA SET ip=@ip, " +
                            " idControladora=@idControladora," +
                            " rele = @rele," +
                            " tempoARele = @tempoARele, " +                           
                            " direcao = @direcao " +
                            " WHERE IdDispositivoEntradaSaida=@idDispositivoEntradaSaida";

                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@ip", dispositivo.ip);
                        cmd.Parameters.AddWithValue("@idControladora", dispositivo.idControladora);
                        cmd.Parameters.AddWithValue("@rele", dispositivo.rele);
                        cmd.Parameters.AddWithValue("@tempoARele", dispositivo.tempoARele);
                        cmd.Parameters.AddWithValue("@direcao", dispositivo.direcao);
                        cmd.Parameters.AddWithValue("@idDispositivoEntradaSaida", dispositivo.equipamentoAtrelado);

                        cmd.ExecuteNonQuery();
                    }
                }
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeleteDispositivo(int dispositivo)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM T_EQUIPAMENTO_CONTROLADORA Where IdDispositivoEntradaSaida =@IdDispositivoEntradaSaida";
                    cmd.Parameters.AddWithValue("@IdDispositivoEntradaSaida", dispositivo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int verificaSeEstaCadastrado(string dispositivo)
        {
            int retorno = 0;
            string query = "SELECT IFNULL(IdDispositivoEntradaSaida, 0) FROM T_EQUIPAMENTO_CONTROLADORA" +
                        " where IdDispositivoEntradaSaida = @IdDispositivoEntradaSaida";
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdDispositivoEntradaSaida", dispositivo);
                    var ret = cmd.ExecuteScalar();
                    retorno = Convert.ToInt32(ret);

                    return retorno;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
