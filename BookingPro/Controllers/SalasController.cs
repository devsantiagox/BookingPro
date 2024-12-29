using BookingPro.Models;
using BookingPro.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

/**
 * Controlador de lógica para manejar las solicitudes relacionadas con las Salas.
 * Autor: Santiago Ruiz - santiago.develops@gmail.com
 * Copyright: Psycotick Software S.L.
 * Licencia: MIT
 */
namespace BookingPro.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SalasController : Controller
    {
        private readonly SalaService _salaService;
        private readonly ReservaService _reservaService;

        /**
         * Constructor con inyección de dependencias.
         * @param salaService Servicio de salas.
         * @param reservaService Servicio de reservas.
         */
        public SalasController(SalaService salaService, ReservaService reservaService)
        {
            _salaService = salaService ?? throw new ArgumentNullException(nameof(salaService));
            _reservaService = reservaService ?? throw new ArgumentNullException(nameof(reservaService));
        }

        /**
         * Acción para mostrar la lista de salas.
         * @return Vista con la lista de salas.
         */
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var salas = await _salaService.GetSalasAsync();
                return View(salas);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al obtener las salas: {ex.Message}";
                return RedirectToAction("Error");
            }
        }

        /**
         * Acción para mostrar el formulario de creación de una nueva sala.
         * @return Vista con el formulario de creación de sala.
         */
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
         * Acción para procesar el formulario de creación de una nueva sala.
         * @param sala Modelo de sala a crear.
         * @return Redirecciona a la vista de índice si tiene éxito, de lo contrario devuelve la vista con el modelo de sala.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Sala sala)
        {
            if (sala == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _salaService.AddSalaAsync(sala);
                    TempData["SuccessMessage"] = "Sala creada exitosamente.";
                    return RedirectToAction("Index");
                }
                return View(sala);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al crear la sala: {ex.Message}";
                return View(sala);
            }
        }

        /**
         * Acción para mostrar el formulario de edición de una sala.
         * @param id Identificador de la sala.
         * @return Vista con el formulario de edición de sala.
         */
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var sala = await _salaService.GetSalaByIdAsync(id.Value);
                if (sala == null)
                {
                    TempData["ErrorMessage"] = "Sala no encontrada.";
                    return RedirectToAction("Error");
                }
                return View(sala);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al obtener la sala: {ex.Message}";
                return RedirectToAction("Error");
            }
        }

        /**
         * Acción para procesar el formulario de edición de una sala.
         * @param sala Modelo de sala a editar.
         * @return Redirecciona a la vista de índice si tiene éxito, de lo contrario devuelve la vista con el modelo de sala.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Sala sala)
        {
            if (sala == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _salaService.UpdateSalaAsync(sala);
                    TempData["SuccessMessage"] = "Sala actualizada exitosamente.";
                    return RedirectToAction("Index");
                }
                return View(sala);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al actualizar la sala: {ex.Message}";
                return View(sala);
            }
        }

        /**
         * Acción para mostrar la confirmación de eliminación de una sala.
         * @param id Identificador de la sala.
         * @return Vista con el formulario de eliminación de sala.
         */
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var sala = await _salaService.GetSalaByIdAsync(id.Value);
                if (sala == null)
                {
                    TempData["ErrorMessage"] = "Sala no encontrada.";
                    return RedirectToAction("Error");
                }
                return View(sala);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al obtener la sala: {ex.Message}";
                return RedirectToAction("Error");
            }
        }

        /**
         * Acción para confirmar la eliminación de una sala.
         * @param id Identificador de la sala.
         * @return Redirecciona a la vista de índice si tiene éxito, de lo contrario devuelve la vista de error.
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var reservasAsociadas = await _reservaService.GetReservasBySalaIdAsync(id);
                if (reservasAsociadas.Any())
                {
                    return Json(new { success = false, message = "No se puede eliminar la sala porque tiene reservas asociadas." });
                }

                await _salaService.DeleteSalaAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al eliminar la sala: {ex.Message}" });
            }
        }
    }
}
