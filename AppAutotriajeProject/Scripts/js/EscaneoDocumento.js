const EscaneoUI = {

    buffer: "",

    hdLectura: null,
    btnEscaneo: null,
    iconElement: null,
    zoneElement: null,

    init: function () {

        this.hdLectura = document.getElementById("hdLectura");
        this.btnEscaneo = document.getElementById("btnEscaneoExitoso");
        this.iconElement = document.getElementById("scanIcon");
        this.zoneElement = document.getElementById("scanTargetZone");

        this.prepararEscaner();

        console.log("Esperando lectura...");
    },

    prepararEscaner: function () {

        document.addEventListener("keydown", (e) => {

            //-------------------------------------
            // Fin de lectura
            //-------------------------------------

            if (e.key === "Enter") {

                e.preventDefault();

                console.log("Lectura completa:");
                console.log(this.buffer);

                this.hdLectura.value = this.buffer;

                this.notificarEscaneoExitoso();

                this.buffer = "";

                return;
            }

            //-------------------------------------
            // Ignorar Shift
            //-------------------------------------

            if (e.key === "Shift") {
                return;
            }

            //-------------------------------------
            // Convertir TAB en \t
            //-------------------------------------

            if (e.key === "Tab") {

                e.preventDefault();

                this.buffer += "\t";

                return;
            }

            //-------------------------------------
            // Ignorar teclas largas
            //-------------------------------------

            if (e.key.length > 1)
                return;

            //-------------------------------------
            // Guardar carácter
            //-------------------------------------

            this.buffer += e.key;

        });

    },

    notificarEscaneoExitoso: function () {

        this.iconElement.className =
            "fa-solid fa-circle-check scan-success-icon";

        this.zoneElement.classList.add("scan-box-verified");

        setTimeout(() => {

            this.btnEscaneo.click();

        }, 700);

    }

};

document.addEventListener("DOMContentLoaded", () => {

    EscaneoUI.init();

});