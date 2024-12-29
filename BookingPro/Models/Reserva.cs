using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPro.Models
{
    /**
     * Modelo que representa una reserva en el sistema.
     * Autor: Santiago Ruiz - santiago.develops@gmail.com
     * Copyright: Psycotick Software S.L.
     * Licencia: MIT
     */
    public class Reserva
    {
        /**
         * Identificador único de la reserva.
         */
        [Key]
        public int IdReserva { get; set; }

        /**
         * Identificador de la sala asociada a la reserva.
         * Es requerido y está relacionado con el modelo Sala.
         */
        [Required(ErrorMessage = "La Sala es obligatoria.")]
        [ForeignKey("Sala")]
        public int SalaID { get; set; }

        /**
         * Objeto de navegación para acceder a los detalles de la sala.
         */
        public virtual Sala Sala { get; set; }

        /**
         * Fecha en la que se realiza la reserva.
         * Es requerida y se muestra en la interfaz como "Fecha de Reserva".
         */
        [Required(ErrorMessage = "La Fecha de Reserva es obligatoria.")]
        [Display(Name = "Fecha de Reserva")]
        public DateTime FechaReserva { get; set; }

        /**
         * Fecha en la que se creó la reserva.
         * Se inicializa con la fecha y hora actual.
         */
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        /**
         * Nombre de la sala asociada a la reserva.
         * No está mapeado en la base de datos.
         */
        [NotMapped]
        [Display(Name = "Nombre de Sala")]
        public string NombreSala { get; set; }

        /**
         * Nombre del usuario que realiza la reserva.
         * No está mapeado en la base de datos.
         */
        [NotMapped]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }
    }
}
