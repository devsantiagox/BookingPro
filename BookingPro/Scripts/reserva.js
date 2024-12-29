// Evento que se dispara cuando el contenido del documento ha sido completamente cargado
document.addEventListener('DOMContentLoaded', function () {

    /**
     * Función para mostrar una alerta de confirmación antes de eliminar una reserva.
     * @param {number} id - El identificador de la reserva.
     * @param {string} name - El nombre de la sala asociada a la reserva.
     */
    function confirmDelete(id, name) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: `Estás a punto de eliminar la reserva para la sala ${name}. Esta acción no se puede deshacer.`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var token = $('#deleteReservaForm input[name="__RequestVerificationToken"]').val();

                // Realiza una solicitud AJAX para eliminar la reserva
                $.ajax({
                    url: 'https://localhost:44303/Reservas/Delete',
                    type: 'POST',
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (response) {
                        if (response.success) {
                            Swal.fire(
                                'Eliminado!',
                                'La reserva ha sido eliminada.',
                                'success'
                            ).then(() => {
                                location.reload();
                            });
                        } else {
                            Swal.fire(
                                'Error',
                                response.message,
                                'error'
                            );
                        }
                    },
                    error: function () {
                        Swal.fire(
                            'Error',
                            'Ocurrió un error al intentar eliminar la reserva.',
                            'error'
                        );
                    }
                });
            }
        });
    }

    window.confirmDelete = confirmDelete;
});
