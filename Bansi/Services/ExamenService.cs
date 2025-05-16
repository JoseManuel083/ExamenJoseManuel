using apiexamen.Interfaces;
using WsApiexamen.Models;


namespace BansiFront.Services
{
    public class ExamenService
    {
        private readonly IDataAccess _dataAccess;

        public ExamenService(HttpClient httpClient, IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        
        public async Task<List<TblExaman>?> ObtenerExamen(int idExamen)
        {
            
            return await _dataAccess.ConsultarExamen(idExamen);
        }

        public async Task<List<TblExaman>?> ObtenerExamenes()
        {
            return await _dataAccess.ObtenerExamenes();
        }

        public async Task<bool> ActualizarExamenAsync(TblExaman examen)
        {
            return await _dataAccess.ActualizarExamen(examen);
        }

        public async Task<bool> AgregarExamenAsync(TblExaman examen)
        {
            return await _dataAccess.AgregarExamen(examen);
        }

        public async Task<bool> EliminarExamen(int idExamen)
        {
            return await _dataAccess.EliminarExamen(idExamen);
        }

    }
}
