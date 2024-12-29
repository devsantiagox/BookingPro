using BookingPro.Models;
using BookingPro.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

/**
 * Controlador de lógica para manejar las solicitudes relacionadas con las Reservas.
 * Autor: Santiago Ruiz - santiago.develops@gmail.com
 * Copyright: Psycotick Software S.L.
 * Licencia: MIT
 */
namespace BookingPro.Controllers
{
    [EnableCors(origins: "http://localhost:44303", headers: "*", methods: "*")]
    public class ReservasController : Controller
    {
        private readonly ReservaService _reservaService;
        private readonly SalaService _salaService;

        /**
         * Constructor con inyección de dependencias.
         * @param reservaService Servicio de reservas.
         * @param salaService Servicio de salas.
         */
        public ReservasController(ReservaService reservaService, SalaService salaService)
        {
            _reservaService = reservaService ?? throw new ArgumentNullException(nameof(reservaService));
            _salaService = salaService ?? throw new ArgumentNullException(nameof(salaService));
        }

        /**
         * Acción para manejar la solicitud GET de la lista de reservas.
         * @return La vista correspondiente a la lista de reservas.
         */
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var reservas = await _reservaService.GetReservasAsync();
                ViewBag.Salas = new SelectList(await _salaService.GetSalasAsync(), "IdSala", "Nombre");
                return View(reservas);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener las reservas: {ex.Message}";
                return View("Error");
            }
        }

        /**
         * Acción para manejar la solicitud GET de creación de una nueva reserva.
         * @return La vista correspondiente a la creación de una nueva reserva.
         */
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                var salasDisponibles = await _salaService.GetSalasAsync();
                salasDisponibles = salasDisponibles.Where(s => s.Disponibilidad == true).ToList();
                ViewBag.Salas = new SelectList(salasDisponibles, "IdSala", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener las salas: {ex.Message}";
                return View("Error");
            }
        }

        /**
         * Acción para manejar la solicitud POST de creación de una nueva reserva.
         * @param reserva La reserva a crear.
         * @return Redirección a la lista de reservas o vista de creación en caso de error.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Reserva reserva)
        {
            if (reserva == null)
            {
                TempData["ErrorMessage"] = "Datos de la reserva no válidos.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _reservaService.AddReservaAsync(reserva);
                    TempData["SuccessMessage"] = "Reserva creada exitosamente.";
                    return RedirectToAction("Index");
                }

                var salasDisponibles = await _salaService.GetSalasAsync();
                salasDisponibles = salasDisponibles.Where(s => s.Disponibilidad == true).ToList();
                ViewBag.Salas = new SelectList(salasDisponibles, "IdSala", "Nombre");
                return View(reserva);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al crear la reserva: {ex.Message}";
                return RedirectToAction("Create");
            }
        }

        /**
         * Acción para manejar la solicitud GET de edición de una reserva.
         * @param id Identificador de la reserva a editar.
         * @return La vista correspondiente a la edición de la reserva.
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
                var reserva = await _reservaService.GetReservaByIdAsync(id.Value);
                if (reserva == null)
                {
                    ViewBag.ErrorMessage = "Reserva no encontrada.";
                    return View("Error");
                }

                var salasDisponibles = await _salaService.GetSalasAsync();
                salasDisponibles = salasDisponibles.Where(s => s.Disponibilidad == true).ToList();
                ViewBag.Salas = new SelectList(salasDisponibles, "IdSala", "Nombre", reserva.SalaID);
                return View(reserva);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener la reserva: {ex.Message}";
                return View("Error");
            }
        }

        /**
         * Acción para manejar la solicitud POST de edición de una reserva.
         * @param reserva La reserva a editar.
         * @return Redirección a la lista de reservas o vista de edición en caso de error.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Reserva reserva)
        {
            if (reserva == null)
            {
                TempData["ErrorMessage"] = "Datos de la reserva no válidos.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _reservaService.UpdateReservaAsync(reserva);
                    TempData["SuccessMessage"] = "Reserva actualizada exitosamente.";
                    return RedirectToAction("Index");
                }

                var salasDisponibles = await _salaService.GetSalasAsync();
                salasDisponibles = salasDisponibles.Where(s => s.Disponibilidad == true).ToList();
                ViewBag.Salas = new SelectList(salasDisponibles, "IdSala", "Nombre", reserva.SalaID);
                return View(reserva);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al actualizar la reserva: {ex.Message}";
                return RedirectToAction("Edit", new { id = reserva.IdReserva });
            }
        }

        /**
         * Acción para manejar la solicitud GET de eliminación de una reserva.
         * @param id Identificador de la reserva a eliminar.
         * @return La vista correspondiente a la eliminación de la reserva.
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
                var reserva = await _reservaService.GetReservaByIdAsync(id.Value);
                if (reserva == null)
                {
                    ViewBag.ErrorMessage = "Reserva no encontrada.";
                    return View("Error");
                }
                return View(reserva);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener la reserva: {ex.Message}";
                return View("Error");
            }
        }

        /**
         * Acción para manejar la solicitud POST de confirmación de eliminación de una reserva.
         * @param id Identificador de la reserva a eliminar.
         * @return Respuesta en formato JSON indicando el éxito o fallo de la eliminación.
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _reservaService.DeleteReservaAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al eliminar la reserva: {ex.Message}" });
            }
        }

        /**
         * Acción para manejar la solicitud GET de filtrado de reservas.
         * @param fechaInicio Fecha de inicio del filtro.
         * @param fechaFin Fecha de fin del filtro.
         * @param salaID Identificador de la sala (opcional).
         * @return La vista correspondiente a la lista de reservas filtradas.
         */
        [HttpGet]
        public async Task<ActionResult> FiltrarReservas(DateTime? fechaInicio, DateTime? fechaFin, int? salaID)
        {
            try
            {
                if (!fechaInicio.HasValue || !fechaFin.HasValue)
                {
                    TempData["ErrorMessage"] = "Por favor, proporciona un rango de fechas válido.";
                    return View("Error");
                }

                var usuario = "admin";
                var reservas = await _reservaService.GetReservasFiltradasAsync(fechaInicio.Value, fechaFin.Value, salaID, usuario);
                ViewBag.Salas = new SelectList(await _salaService.GetSalasAsync(), "IdSala", "Nombre");
                return View("Index", reservas);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al filtrar las reservas: {ex.Message}";
                return View("Error");
            }
        }
    }
}
