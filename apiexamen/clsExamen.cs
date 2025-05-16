using apiexamen.Config;
using apiexamen.DataAccess;
using apiexamen.Interfaces;

namespace apiexamen
{
    public class clsExamen
    {
        private readonly ConfiguracionAccesoDatos _config;

        public clsExamen(ConfiguracionAccesoDatos config)
        {
            _config = config;
        }

        public IDataAccess CrearAccesoDatos()
        {
            return _config.UsarWebApi
                ? new WsApiexamenDataAccess(_config.ConexionWebApi)
                : new AdoDataAccess(_config.ConexionSql);
        }

        public void CambiarMetodoAcceso(bool usarWebApi)
        {
            _config.UsarWebApi = usarWebApi;
        }


    }
}
