// Función para inicializar los eventos y comportamientos
function initPreguntasSeguimiento() {

    // 1. Ocultar el spinner al iniciar o cuando termine una petición AJAX
    const loading = document.getElementById("loadingPregunta");
    if (loading) {
        loading.style.display = "none";
    }

}

// Delegación de eventos en el document (Escucha clics incluso en elementos nuevos del UpdatePanel)
document.addEventListener("click", function (e) {

    const opcionLista = e.target.closest(".opcion-card");
    const opcionSiNo = e.target.closest(".btn-opcion-sino");

    if (!opcionLista && !opcionSiNo) return;

    // Si es lista
    if (opcionLista) {
        document.querySelectorAll(".opcion-card").forEach(function (b) {
            b.disabled = true;
        });
        opcionLista.classList.add("procesando");
    }

    // Si es Sí / No
    if (opcionSiNo) {
        opcionSiNo.classList.add("procesando");
        // Pequeño retardo para asegurar que ASP.NET procese el postback
        setTimeout(function () {
            document.querySelectorAll(".btn-opcion-sino").forEach(function (b) {
                b.disabled = true;
            });
        }, 10);
    }

    // Mostrar el spinner de carga
    const loading = document.getElementById("loadingPregunta");
    if (loading) {
        loading.style.display = "flex";
    }
});

// Registrar eventos del ciclo de vida del UpdatePanel
document.addEventListener("DOMContentLoaded", function () {

    // Se ejecuta en la carga inicial
    initPreguntasSeguimiento();

    // Se ejecuta CADA VEZ que el UpdatePanel termina de actualizar el HTML
    if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initPreguntasSeguimiento();
        });
    }
});