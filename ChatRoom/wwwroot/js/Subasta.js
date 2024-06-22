var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById("nuevaSubastaBtn").addEventListener("click", function () {
        var isAuthenticated = document.getElementById("nuevaSubastaBtn").dataset.isAuthenticated;
        if (isAuthenticated === 'true') {
            document.getElementById("nuevaSubastaFormContainer").classList.remove("d-none");
        } else {
            alert("Debes iniciar sesión para crear una subasta.");
            //redirigir a la página de login o mostrar un mensaje adicional
        }
    });

    document.getElementById("cancelarSubastaBtn").addEventListener("click", cancelarSubasta());
    function cancelarSubasta() {
        document.getElementById("nuevaSubastaFormContainer").classList.add("d-none");
        document.getElementById("createSalaForm").reset();
    }
    function mensajeExito() {
        // Mostrar un mensaje de éxito
        alert("Nueva subasta creada con éxito")
        cancelarSubasta();

    }

    document.getElementById("createSalaForm").addEventListener("submit", function (event) {
        event.preventDefault();
        var nombre = document.getElementById("nombre").value;
        var fotoProductoNombre = document.getElementById("fotoProductoNombre").files[0];
        var idVendedor = document.getElementById("idVendedor").value;

        if (fotoProductoNombre) {
            if (fotoProductoNombre.size > 2 * 1024 * 1024) {
                alert("La imagen es demasiado grande. Por favor, selecciona una imagen de menos de 2MB.");
                return;
            }
            const reader = new FileReader();
            reader.readAsDataURL(fotoProductoNombre);
            reader.onload = async function () {
                const base64Image = reader.result.split(',')[1];

                try {
                    await connection.invoke("CreateSala", nombre, base64Image, idVendedor);
                    mensajeExito();
                    

                } catch (err) {
                    console.error('Error:', err);
                    alert('Ocurrió un error al crear la sala: ' + err.message);
                }
            };
            reader.onerror = function (error) {
                console.log('Error:', error);
            };
        } else {
            alert("Debe subir una imagen del producto.");
        }
    });

    connection.start().then(function () {
        connection.invoke("GetSalas");
    }).catch(function (err) {
        return console.error(err.toString());
    });
    connection.on("ReceiveSalas", function (salas) {
        const salasList = document.getElementById("salasList");
        const mensajeSubastasNull = document.getElementById("mensajeSubastasNull");

        // Limpiar la lista de subastas existentes
        salasList.innerHTML = "";

        if (salas.length === 0) {
            mensajeSubastasNull.style.display = "block"; // Mostrar el mensaje de "No hay subastas activas"
        } else {

            salas.forEach(function (sala) {
                const salaItem = document.createElement("li");
                salaItem.className = "list-group-item list-group-item-action py-3 lh-sm mb-3 m-lg-2 rounded-2";
                salaItem.innerHTML = `<a href="/Chat/Room?id=${sala.id}">${sala.nombre}</a>`;
                salasList.appendChild(salaItem);
            });
            mensajeSubastasNull.style.display = "none"; // Ocultar el mensaje de "No hay subastas activas"
        }
    });

});
