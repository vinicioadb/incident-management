let selectedUserId = null;

$(document).ready(function () {
    $('#usersTable').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json'
        },
        responsive: true,
        columns: [
            { width: "5%" },  // ID
            { width: "20%" }, // Nombre
            { width: "20%" }, // Email
            { width: "15%" }, // Rol
            { width: "10%" }, // Estado
            { width: "20%" }, // Última Conexión
            { width: "10%" }  // Acciones
        ]
    });
});

function confirmDelete(userId) {
    selectedUserId = userId;
    var deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
    deleteModal.show();
}

function deleteUser() {
    // Aquí iría la lógica para eliminar el usuario
    console.log('Eliminando usuario:', selectedUserId);
    $('#deleteModal').modal('hide');
    // Recargar tabla o eliminar fila
}

function saveUser() {
    // Aquí iría la lógica para guardar el usuario
    console.log('Guardando usuario');
    $('#userModal').modal('hide');
    // Recargar tabla o agregar/actualizar fila
}

// Limpiar formulario cuando se abre el modal
$('#userModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var isEdit = button.hasClass('btn-info');

    $('#modalTitle').text(isEdit ? 'Editar Usuario' : 'Nuevo Usuario');
    if (!isEdit) {
        $('#userForm').trigger('reset');
    }
});