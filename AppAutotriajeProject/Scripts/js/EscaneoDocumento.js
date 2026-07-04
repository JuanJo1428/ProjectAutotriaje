const EscaneoUI = {
    iconElement: document.getElementById('scanIcon'),
    zoneElement: document.getElementById('scanTargetZone'),

    init: function () {
        console.log("API del Escáner iniciada: Esperando lectura de código de barras...");

        setTimeout(() => {
            this.procesarEscaneoExitoso();
        }, 4000);
    },

    procesarEscaneoExitoso: function () {
        if (this.iconElement) {
            this.iconElement.className = "fa-solid fa-circle-check scan-success-icon";
        }

        if (this.zoneElement) {
            this.zoneElement.classList.add('scan-box-verified');
        }

        const pacienteSimulado = {
            idTipoDocumento: "CC",
            nroDocumento: "1020304050",
            primerNombre: "Carlos",
            segundoNombre: "Alberto",
            primerApellido: "Gómez",
            segundoApellido: "Restrepo",
            sexoBiologico: "Masculino",
            fechaNacimiento: "1992-05-14",
            editado: false,
            existeEnBaseDatos: true
        };

        sessionStorage.setItem('paciente_triage', JSON.stringify(pacienteSimulado));

        setTimeout(() => {
            window.location.href = "InformacionPaciente.aspx";
        }, 1500);
    }
};

document.addEventListener("DOMContentLoaded", () => {
    EscaneoUI.init();
});
