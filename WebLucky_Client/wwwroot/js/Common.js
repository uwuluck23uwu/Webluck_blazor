﻿window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 2000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 2000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification!',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification!',
            message,
            'error'
        )
    }
}

function ShowDeleteConfirmationModal() {
    $('#DeleteModal').modal('show');
}
function HideDeleteConfirmationModal() {
    $('#DeleteModal').modal('hide');
}

document.addEventListener('DOMContentLoaded', function () {
    AOS.init();
});