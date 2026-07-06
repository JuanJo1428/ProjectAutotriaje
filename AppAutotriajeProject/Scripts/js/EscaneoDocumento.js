const EscaneoUI = {

    iconElement: null,
    zoneElement: null,

    init: function () {

        this.iconElement = document.getElementById("scanIcon");
        this.zoneElement = document.getElementById("scanTargetZone");

        console.log("Escáner iniciado. Esperando documento...");

        // SIMULACIÓN
        // Cuando llegue el SDK esta llamada desaparecerá
        setTimeout(() => {

            this.notificarEscaneoExitoso();

        }, 4000);

    },

    notificarEscaneoExitoso: function () {

        // Cambia el icono por el check
        if (this.iconElement) {
            this.iconElement.className = "fa-solid fa-circle-check scan-success-icon";
        }

        // Cambia el estilo de la caja
        if (this.zoneElement) {
            this.zoneElement.classList.add("scan-box-verified");
        }

        // Espera un momento para que el usuario vea la confirmación
        setTimeout(() => {

            document.getElementById("btnEscaneoExitoso").click();

        }, 1200);

    }

};

document.addEventListener("DOMContentLoaded", () => {

    EscaneoUI.init();

});