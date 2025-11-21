using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.CLASSES
{
    public class Negocio
    {
        Acesso acesso = new ();
       public DataTable CarregaListaEquipamento()
        {
            string query = "select codigo,idcontroladora,DPS_NM_RAIA as equipamentoAtrelado, " +
                "case when DPS_DS_DIRECAO = 1 THEN 'ENTRADA' when " +
                "DPS_DS_DIRECAO = 2 THEN 'SAIDA' else 'INDEFINIDO'" +
                " end as direcao, dps_nm_apelido,ip, rele,tempoARele from  " +
                "T_EQUIPAMENTO_CONTROLADORA inner join " +
                "T_DISPOSITIVO_SOCKET on IdDispositivoEntradaSaida = DPS_CD_REGISTRO";







            DataTable dt;

            try
            {
                acesso.LimparParametros();


                dt = acesso.ExecutarConsulta(CommandType.Text, query);



            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar dados " + ex.Message);
            }

            return dt;

        }

        public DataTable CarregaConfiguracaoGaren()
        {
            string query = "select  * from T_CONFIG_GAREN ";

            DataTable dt;

            try
            {
                acesso.LimparParametros();


                dt = acesso.ExecutarConsulta(CommandType.Text, query);



            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar dados" + ex.Message);
            }

            return dt;

        }



    }
}
