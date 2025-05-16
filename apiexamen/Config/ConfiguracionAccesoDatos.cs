using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Config
{
    public class ConfiguracionAccesoDatos
    {
        public bool UsarWebApi { get; set; } = true; 
        public string ConexionWebApi { get; set; } = "https://localhost:7227/";
        public string ConexionSql { get; set; } = "server=localhost\\SQLEXPRESS;database=BdiExamen;integrated security=true;TrustServerCertificate=True;";

    }
}
