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
            document.getElementById(id).disabled = false;
        });

        document.getElementById("btnEditarPaciente").style.display = "none";
    }

};
