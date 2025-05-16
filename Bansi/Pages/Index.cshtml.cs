using apiexamen;
using apiexamen.Interfaces;
using BansiFront.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WsApiexamen.Models;

namespace Bansi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ExamenService _examenService;
        private readonly clsExamen _dataAccess;
        public string FuenteSeleccionada { get; set; } = "api";
        public IndexModel(ILogger<IndexModel> logger, ExamenService examenService, clsExamen dataAccess)
        {
            _logger = logger;
            _examenService = examenService;
            _dataAccess = dataAccess;
        }

        [BindProperty]
        public TblExaman NuevoExamen { get; set; } = new TblExaman();

        public async Task<IActionResult> OnPostAsync()
        {
            NuevoExamen.IdExamen =  new Random().Next();
            bool resultado = await _examenService.AgregarExamenAsync(NuevoExamen);
            if (resultado)
            {
                return RedirectToPage("/Examenes"); 
            }

            ModelState.AddModelError(string.Empty, "Error al guardar el examen.");
            return Page();
        }

        public void OnPostSeleccionarFuente(string fuenteDatos) 
        {
            FuenteSeleccionada = fuenteDatos;
            _dataAccess.CambiarMetodoAcceso(FuenteSeleccionada == "api"?true:false);
        }
    }
}
