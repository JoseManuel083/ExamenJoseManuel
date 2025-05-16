using apiexamen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WsApiexamen.Models;

namespace apiexamen.DataAccess
{
    public class WsApiexamenDataAccess:IDataAccess
    {
        private readonly HttpClient _httpClient;

        public WsApiexamenDataAccess(string apiUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
        }

        public async Task<List<TblExaman>> ObtenerExamenes()
        {
            return await _httpClient.GetFromJsonAsync<List<TblExaman>>("ConsultarExamenes");
        }
        public async Task<List<TblExaman>?> ConsultarExamen(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<TblExaman>>($"ConsultarExamen/{id}");
        }

        public async Task<bool> AgregarExamen(TblExaman examen)
        {
            var response = await _httpClient.PostAsJsonAsync("AgregarExamen", examen);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarExamen(TblExaman examen)
        {
            var response = await _httpClient.PatchAsJsonAsync("ActualizarExamen", examen);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarExamen(int id)
        {
            var response = await _httpClient.DeleteAsync($"EliminarExamen/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
