using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration;

public class AppSettings
{
    public ApplicationSettings ApplicationSettings { get; set; } = new();

    public static AppSettings Settings { get; set; } = new();

    public static void LoadConfiguration()
    {
        IConfiguration Configuration;

        // Configurar o carregamento do appsettings.json
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = configurationBuilder.Build();

        Configuration.Bind(Settings);

    }

   
}
