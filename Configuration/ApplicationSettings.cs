using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration;

public class ApplicationSettings
{
    public const string DefaultBackendIp = "127.0.0.1";
    public const int DefaultBackendPort = 9877;
    public const string DefaultStringConexao = "Data Source =.\\sqlexpress;Initial Catalog=Automacao;Persist Security Info=True; User ID = sa ;Password=325014;";

    public const int DefaultGravaLogLoop = 0;
    public const int DefaultBancoDadosLocal = 0;    

    public const int DefaultTimeSpan = 2;


    public string ApplicationName {  get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public string BackendIp { get; set; } = DefaultBackendIp;
    public int BackendPort { get; set; } = DefaultBackendPort;

    public string StringConexao { get; set; } = DefaultStringConexao;

    public int TimeSpan { get; set; } = DefaultTimeSpan;

    public int GravaLogLoop { get; set; } = DefaultGravaLogLoop;

    public int BancoDadosLocal { get; set; } = DefaultBancoDadosLocal;
}
