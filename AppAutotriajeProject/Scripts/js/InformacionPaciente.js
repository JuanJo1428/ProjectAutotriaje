const InformacionPacienteUI = {

    habilitarEdicion: function () {

        [
            "txtPrimerNombre",
            "txtSegundoNombre",
            "txtPrimerApellido",
            "txtSegundoApellido",
            "txtFechaNacimiento",
            "ddlSexoBiologico"
        ].forEach(id => {

            const control = document.getElementById(id);

            if (control)
                control.disabled = false;

        });

        document.getElementById("btnEditarPaciente").style.display = "none";
    }

};

document.addEventListener("DOMContentLoaded", function () {

    const txtFecha = document.getElementById("txtFechaNacimiento");

    if (!txtFecha)
        return;

    const ayer = new Date();
    ayer.setDate(ayer.getDate() - 1);

    const year = ayer.getFullYear();
    const month = String(ayer.getMonth() + 1).padStart(2, "0");
    const day = String(ayer.getDate()).padStart(2, "0");

    txtFecha.max = `${year}-${month}-${day}`;
});