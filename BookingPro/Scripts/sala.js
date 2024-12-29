// Evento que se dispara cuando el contenido del documento ha sido completamente cargado
document.addEventListener('DOMContentLoaded', function () {

    /**
     * Función para mostrar una alerta de confirmación antes de eliminar una sala.
     * @param {number} id - El identificador de la sala.
     * @param {string} name - El nombre de la sala.
     */
    function confirmDelete(id, name) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: `Estás a punto de eliminar la sala ${name}. Esta acción no se puede deshacer.`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                var token = $('#deleteSalaForm input[name="__RequestVerificationToken"]').val();

                // Realiza una solicitud AJAX para eliminar la sala
                $.ajax({
                    url: 'https://localhost:44303/Salas/Delete',
                    type: 'POST',
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (response) {
                        if (response.success) {
                            Swal.fire(
                                'Eliminado!',
                                'La sala ha sido eliminada.',
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
                            'Ocurrió un error al intentar eliminar la sala.',
                            'error'
                        );
                    }
                });
            }
        });
    }

    window.confirmDelete = confirmDelete;
});
