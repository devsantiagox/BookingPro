using BookingPro.Models;
using BookingPro.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/**
 * Servicio para manejar la lógica de negocio relacionada con las salas.
 * @author Santiago Ruiz - santiago.develops@gmail.com
 * @copyright Psycotick Software S.L.
 * @license MIT
 */
namespace BookingPro.Services
{
    public class SalaService
    {
        private readonly SalaRepository _salaRepository;

        /**
         * Constructor con inyección de dependencias.
         * @param salaRepository Repositorio de salas.
         */
        public SalaService(SalaRepository salaRepository)
        {
            _salaRepository = salaRepository ?? throw new ArgumentNullException(nameof(salaRepository));
        }

        /**
         * Obtiene todas las salas.
         * @return Una colección de salas.
         * @throws Exception Si ocurre un error al obtener las salas.
         */
        public async Task<IEnumerable<Sala>> GetSalasAsync()
        {
            try
            {
                return await _salaRepository.GetSalasAsync();
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al obtener las salas", ex);
            }
        }

        /**
         * Obtiene una sala por su ID.
         * @param id Identificador de la sala.
         * @return La sala con el ID especificado.
         * @throws Exception Si ocurre un error al obtener la sala.
         */
        public async Task<Sala> GetSalaByIdAsync(int id)
        {
            try
            {
                return await _salaRepository.GetSalaByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al obtener la sala", ex);
            }
        }

        /**
         * Agrega una nueva sala.
         * @param sala La sala a agregar.
         * @throws Exception Si ocurre un error al agregar la sala.
         */
        public async Task AddSalaAsync(Sala sala)
        {
            try
            {
                await _salaRepository.AddSalaAsync(sala);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Ya existe un nombre con ese nombre", ex);
            }
        }

        /**
         * Actualiza una sala existente.
         * @param sala La sala a actualizar.
         * @throws Exception Si ocurre un error al actualizar la sala.
         */
        public async Task UpdateSalaAsync(Sala sala)
        {
            try
            {
                await _salaRepository.UpdateSalaAsync(sala);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Ya existe un nombre con ese nombre", ex);
            }
        }

        /**
         * Elimina una sala existente.
         * @param id Identificador de la sala.
         * @throws Exception Si ocurre un error al eliminar la sala.
         */
        public async Task DeleteSalaAsync(int id)
        {
            try
            {
                await _salaRepository.DeleteSalaAsync(id);
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al eliminar la sala", ex);
            }
        }
    }
}
