const DocumentoUI = {
    txtDocumento: document.getElementById('txtDocumento'),
    ddlTipoDocumento: document.getElementById('ddlTipoDocumento'),

    getReglaActual: function () {
        const tipoDoc = this.ddlTipoDocumento.value;
        if (CONFIG_DOCUMENTOS && CONFIG_DOCUMENTOS.TiposDocumento && CONFIG_DOCUMENTOS.TiposDocumento[tipoDoc]) {
            return CONFIG_DOCUMENTOS.TiposDocumento[tipoDoc];
        }
        return null;
    },

    pressNumber: function (num) {
        const regla = this.getReglaActual();

        if (regla && this.txtDocumento.value.length >= regla.MaxLength) {
            return;
        }

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

function validateDocumento(source, args) {
    const regla = DocumentoUI.getReglaActual();
    const valor = args.Value;

    if (!regla) {
        args.IsValid = true;
        return;
    }

    const longitudCorrecta = valor.length >= regla.MinLength && valor.length <= regla.MaxLength;
    args.IsValid = longitudCorrecta;
}
