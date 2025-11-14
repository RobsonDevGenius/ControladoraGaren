using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.CLASSES
{
    public class Dispositivo
    {
       

        public int codigo;
        public string ip = "";       
        public string idControladora = "";
        public string equipamentoAtrelado = "";       
        public string direcao = "";      
        public int rele;
        public double tempoARele;
    }

    public  class ListDispositivo<Dispositivo>:List<Dispositivo>;
   

}
