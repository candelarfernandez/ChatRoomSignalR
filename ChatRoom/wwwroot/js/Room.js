var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

connection.start().then(() => {
    let userName = document.getElementById("userName").value;
    connection.invoke("JoinSala", document.getElementById("salaId").value, userName) //de donde lo saco?
}).catch((e) => console.error(e));

document.getElementById("btnSend").addEventListener("click", (event) => {
    let salaId = document.getElementById("salaId").value;
    let userId = document.getElementById("user").value;
    let monto = document.getElementById("monto").value;

    connection.invoke("AddOferta", salaId, userId, monto).catch((err) =>
        console.error(err.toString()));

    document.getElementById("monto").value = "";
    document.getElementById("monto").focus();
    event.preventDefault();
});

connection.on("ReceiveOferta", (ofertas) => {
    var ofertasHtml = "";
    ofertas.forEach(oferta => {
        ofertasHtml += `<div><b>Monto:</b> ${oferta.monto}, <b>Usuario:</b> ${oferta.idComprador}</div>`;
    });
    document.getElementById("montos").innerHTML = ofertasHtml;
});

connection.on("ShowError", (message) => {
    var errorsHtml = `<div class="alert alert-danger"><b>${message}</b></div>`;

    const errorElement = document.createElement('div');
    errorElement.classList.add('alert', 'alert-danger');
    errorElement.innerHTML = message;

    const errorsContainer = document.getElementById("errors");
    errorsContainer.appendChild(errorElement);

    setTimeout(() => {
        errorElement.style.display = 'none';
    }, 3000);
});

connection.on("ShowWho", (message) => {
    var notificationHtml = `<div><b>${message}</b></div>`;
    document.getElementById("notifications").innerHTML = notificationHtml + document.getElementById("notifications").innerHTML;
});

connection.on("CloseAuction", () => {
    Toastify({
        text: "La subasta ha finalizado. Serás redirigido.",
        duration: 3000,
        close: true,
        gravity: "bottom",
        position: "center",
        backgroundColor: "#ff6f69",
        stopOnFocus: true,
    }).showToast();

    setTimeout(() => {
        window.location.href = "/Chat/Index";
    }, 3000);
});

connection.on("ReceiveNotification", (message) => {
    Toastify({
        text: message,
        duration: 6000,
        close: true,
        gravity: "bottom",
        position: "center",
        backgroundColor: "#88d8b0",
        stopOnFocus: true,
    }).showToast();
});

document.getElementById("btnFinalizarSubasta").addEventListener("click", function (event) {
    var numOfertas = document.getElementById("montos").children.length;
    let idSala = document.getElementById("idSala").value;

    if (numOfertas > 0) {
        event.preventDefault();
        Swal.fire({
            title: '¿Estás seguro?',
            text: "Al finalizar la subasta, la última oferta será la ganadora.",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, finalizar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                // Realizar solicitud fetch para finalizar la subasta
                fetch('/Chat/FinalizarSubasta', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ idSala: idSala, forceClose: false })
                })
            }
        });
    } else {
        event.preventDefault();
        Swal.fire({
            title: 'No hay ofertas',
            text: "No hay ofertas en la sala. ¿Desea cerrar la subasta de todos modos?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, cerrar subasta',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/Chat/FinalizarSubasta', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ idSala: idSala, forceClose: true })
                }).then(response => {
                    if (response.ok) {
                        Swal.fire(
                            'Cerrada!',
                            'La subasta ha sido cerrada.',
                            'success'
                        ).then(() => {
                            window.location.href = "/Chat/Index";
                        });
                    } else {
                        response.json().then(data => {
                            Swal.fire('Error', data.message, 'error');
                        });
                    }
                }).catch(error => {
                    Swal.fire('Error', 'Hubo un problema al cerrar la subasta.', 'error');
                });
            }
        });
    }
});
