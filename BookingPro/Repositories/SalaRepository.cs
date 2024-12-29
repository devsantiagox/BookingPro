using BookingPro.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using BookingPro.Data;
using System;

/**
 * Repositorio para manejar las operaciones de acceso a datos de las salas.
 * @author Santiago Ruiz - santiago.develops@gmail.com
 * @copyright Psycotick Software S.L.
 * @license MIT
 */
namespace BookingPro.Repositories
{
    public class SalaRepository
    {
        private readonly DbContext _dbContext;

        /**
         * Constructor con inyección de dependencias.
         * @param dbContext Contexto de base de datos.
         */
        public SalaRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
                using (var connection = _dbContext.CreateConnection())
                {
                    return await connection.QueryAsync<Sala>("SP_OBTENER_SALAS", commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    return await connection.QuerySingleOrDefaultAsync<Sala>("SP_OBTENER_SALA_ID", new { Id = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al obtener la sala por ID", ex);
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
                using (var connection = _dbContext.CreateConnection())
                {
                    await connection.ExecuteAsync("SP_AGREGAR_SALA", new { sala.Nombre, sala.Capacidad, sala.Disponibilidad, Usuario = "admin" }, commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    await connection.ExecuteAsync("SP_ACTUALIZAR_SALA", new { Id = sala.IdSala, sala.Nombre, sala.Capacidad, sala.Disponibilidad, Usuario = "admin" }, commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    await connection.ExecuteAsync("SP_ELIMINAR_SALA", new { Id = id, Usuario = "admin" }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al eliminar la sala", ex);
            }
        }
    }
}
