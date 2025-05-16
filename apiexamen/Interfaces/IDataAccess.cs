using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WsApiexamen.Models;

namespace apiexamen.Interfaces
{
    public interface IDataAccess
    {
        Task<List<TblExaman>> ObtenerExamenes();
        Task<List<TblExaman>?> ConsultarExamen(int id);
        Task<bool> AgregarExamen(TblExaman examen);
        Task<bool> ActualizarExamen(TblExaman examen);
        Task<bool> EliminarExamen(int id);
    }

}
