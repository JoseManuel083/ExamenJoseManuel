using BansiFront.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WsApiexamen.Models;

namespace BansiFront.Pages
{
    public class ExamenesModel : PageModel
    {
        private readonly ExamenService _examenService;

        public List<TblExaman>? Examenes { get; set; }

        public ExamenesModel(ExamenService examenService)
        {
            _examenService = examenService;
        }

        public async Task OnGetAsync()
        {
            Examenes = await _examenService.ObtenerExamenes();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int idExamen)
        {
            bool eliminado = await _examenService.EliminarExamen(idExamen);
            if (eliminado) 
            {
                ModelState.Clear();
                Examenes = await _examenService.ObtenerExamenes();
            }
            return RedirectToPage("/Examenes");
        }

    }
}
