using BookingPro.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using BookingPro.Data;
using System;
using System.Linq;

/**
 * Repositorio para manejar las operaciones de acceso a datos de las reservas.
 * Autor: Santiago Ruiz - santiago.develops@gmail.com
 * Copyright: Psycotick Software S.L.
 * Licencia: MIT
 */
namespace BookingPro.Repositories
{
    public class ReservaRepository
    {
        private readonly DbContext _dbContext;

        /**
         * Constructor con inyección de dependencias.
         * @param dbContext Contexto de base de datos.
         */
        public ReservaRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
                using (var connection = _dbContext.CreateConnection())
                {
                    return await connection.QueryAsync<Reserva>("SP_OBTENER_RESERVAS", commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    return await connection.QuerySingleOrDefaultAsync<Reserva>("SP_OBTENER_RESERVA_ID", new { Id = id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al obtener la reserva por ID", ex);
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
                using (var connection = _dbContext.CreateConnection())
                {
                    await connection.ExecuteAsync("SP_AGREGAR_RESERVA", new
                    {
                        SalaID = reserva.SalaID,
                        FechaReserva = reserva.FechaReserva,
                        UsuarioID = 1,
                        Usuario = "admin"
                    }, commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    await connection.ExecuteAsync("SP_ACTUALIZAR_RESERVA", new
                    {
                        Id = reserva.IdReserva,
                        SalaID = reserva.SalaID,
                        FechaReserva = reserva.FechaReserva,
                        UsuarioID = 1
                    }, commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    await connection.ExecuteAsync("SP_ELIMINAR_RESERVA", new { Id = id, Usuario = "admin" }, commandType: CommandType.StoredProcedure);
                }
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
                using (var connection = _dbContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@FechaInicio", fechaInicio);
                    parameters.Add("@FechaFin", fechaFin);
                    parameters.Add("@SalaID", salaID.HasValue ? (object)salaID.Value : DBNull.Value);
                    parameters.Add("@Usuario", "admin");

                    return await connection.QueryAsync<Reserva>("SP_CONSULTAR_RESERVAS", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (p. ej., registrar el error)
                throw new Exception("Error al filtrar las reservas", ex);
            }
        }

        /**
         * Obtiene las reservas por el ID de la sala.
         * @param salaId Identificador de la sala.
         * @return Una lista de reservas asociadas a la sala.
         */
        public async Task<List<Reserva>> GetReservasBySalaIdAsync(int salaId)
        {
            var query = "SELECT * FROM TB_RESERVAS WHERE SalaID = @SalaId";
            using (var connection = _dbContext.CreateConnection())
            {
                try
                {
                    return (await connection.QueryAsync<Reserva>(query, new { SalaId = salaId })).ToList();
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    throw new Exception("Error al obtener las reservas de la sala", ex);
                }
            }
        }
    }
}
