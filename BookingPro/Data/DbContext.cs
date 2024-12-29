using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BookingPro.Data
{
    /**
     * Clase para manejar la conexión a la base de datos.
     * Autor: Santiago Ruiz - santiago.develops@gmail.com
     * Copyright: Psycotick Software S.L.
     * Licencia: MIT
     */
    public class DbContext
    {
        private readonly string connectionString;

        /**
         * Constructor que inicializa el contexto con la cadena de conexión.
         */
        public DbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        /**
         * Crea y devuelve una nueva conexión a la base de datos.
         * @return Una nueva instancia de IDbConnection.
         */
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
