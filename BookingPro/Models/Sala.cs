using System;
using System.ComponentModel.DataAnnotations;

namespace BookingPro.Models
{
    /**
     * Modelo que representa una sala en el sistema.
     * Autor: Santiago Ruiz - santiago.develops@gmail.com
     * Copyright: Psycotick Software S.L.
     * Licencia: MIT
     */
    public class Sala
    {
        /**
         * Identificador único de la sala.
         */
        [Key]
        public int IdSala { get; set; }

        /**
         * Nombre de la sala.
         * Es requerido y no puede tener más de 100 caracteres.
         */
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [Display(Name = "Nombre de la Sala")]
        public string Nombre { get; set; }

        /**
         * Capacidad de la sala.
         * Es requerida y debe ser un número positivo.
         */
        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser un número positivo.")]
        [Display(Name = "Capacidad de la Sala")]
        public int Capacidad { get; set; }

        /**
         * Disponibilidad de la sala.
         * Es requerida.
         */
        [Required(ErrorMessage = "La disponibilidad es obligatoria.")]
        [Display(Name = "Disponibilidad")]
        public bool Disponibilidad { get; set; }

        /**
         * Fecha en la que se creó la sala.
         */
        [Display(Name = "Fecha de Creación")]
        public DateTime? FechaCreacion { get; set; }
    }
}
