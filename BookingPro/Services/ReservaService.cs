using BookingPro.Models;
using BookingPro.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/**
 * Servicio para manejar la lógica de negocio relacionada con las reservas.
 * Autor: Santiago Ruiz - santiago.develops@gmail.com
 * Copyright: Psycotick Software S.L.
 * Licencia: MIT
 */
namespace BookingPro.Services
{
    public class ReservaService
    {
        private readonly ReservaRepository _reservaRepository;

        /**
         * Constructor con inyección de dependencias.
         * @param reservaRepository Repositorio de reservas.
         */
        public ReservaService(ReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository ?? throw new ArgumentNullException(nameof(reservaRepository));
        }

        /**
         * Obtiene todas las reservas.
         * @return Una colección de reservas.
         * @throws Exception Si ocurre un error al obtener las reservas.
         */
        public async Task<IEnumerable<Reserva>> GetReservasAsync()
        {
            try
            {
                return await _reservaRepository.GetReservasAsync();
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al obtener las reservas", ex);
            }
        }

        /**
         * Obtiene una reserva por su ID.
         * @param id Identificador de la reserva.
         * @return La reserva con el ID especificado.
         * @throws Exception Si ocurre un error al obtener la reserva.
         */
        public async Task<Reserva> GetReservaByIdAsync(int id)
        {
            try
            {
                return await _reservaRepository.GetReservaByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al obtener la reserva", ex);
            }
        }

        /**
         * Agrega una nueva reserva.
         * @param reserva La reserva a agregar.
         * @throws Exception Si ocurre un error al agregar la reserva.
         */
        public async Task AddReservaAsync(Reserva reserva)
        {
            try
            {
                await _reservaRepository.AddReservaAsync(reserva);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Ya existe una reserva en esta fecha", ex);
            }
        }

        /**
         * Actualiza una reserva existente.
         * @param reserva La reserva a actualizar.
         * @throws Exception Si ocurre un error al actualizar la reserva.
         */
        public async Task UpdateReservaAsync(Reserva reserva)
        {
            try
            {
                await _reservaRepository.UpdateReservaAsync(reserva);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Ya existe una reserva en esta fecha", ex);
            }
        }

        /**
         * Elimina una reserva existente.
         * @param id Identificador de la reserva.
         * @throws Exception Si ocurre un error al eliminar la reserva.
         */
        public async Task DeleteReservaAsync(int id)
        {
            try
            {
                await _reservaRepository.DeleteReservaAsync(id);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al eliminar la reserva", ex);
            }
        }

        /**
         * Obtiene las reservas filtradas por rango de fechas y sala específica.
         * @param fechaInicio Fecha de inicio del filtro.
         * @param fechaFin Fecha de fin del filtro.
         * @param salaID Identificador de la sala (opcional).
         * @param usuario Nombre del usuario que realiza la consulta.
         * @return Una colección de reservas filtradas.
         * @throws Exception Si ocurre un error al obtener las reservas filtradas.
         */
        public async Task<IEnumerable<Reserva>> GetReservasFiltradasAsync(DateTime fechaInicio, DateTime fechaFin, int? salaID, string usuario)
        {
            try
            {
                return await _reservaRepository.GetReservasFiltradasAsync(fechaInicio, fechaFin, salaID, usuario);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al filtrar las reservas", ex);
            }
        }

        /**
         * Elimina todas las reservas asociadas a una sala específica.
         * @param salaId Identificador de la sala.
         * @throws Exception Si ocurre un error al eliminar las reservas.
         */
        public async Task<List<Reserva>> GetReservasBySalaIdAsync(int salaId)
        {
            try
            {
                return await _reservaRepository.GetReservasBySalaIdAsync(salaId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las reservas de la sala", ex);
            }
        }

    }
}
