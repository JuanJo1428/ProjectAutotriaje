const DocumentoUI = {
    txtDocumento: document.getElementById('txtDocumento'),
    ddlTipoDocumento: document.getElementById('ddlTipoDocumento'),

    pressNumber: function (num) {
        this.txtDocumento.value += num;
        this.ejecutarValidacionEnTiempoReal();
    },

    backspace: function () {
        const valorActual = this.txtDocumento.value;
        if (valorActual.length > 0) {
            this.txtDocumento.value = valorActual.substring(0, valorActual.length - 1);
            this.ejecutarValidacionEnTiempoReal();
        }
    },

    onTipoDocumentoChange: function () {
        this.ejecutarValidacionEnTiempoReal();
    },

    ejecutarValidacionEnTiempoReal: function () {
        if (typeof Page_ClientValidate === 'function') {
            ValidatorValidate(document.getElementById(cvDocumentoId));
        }
    }
};
