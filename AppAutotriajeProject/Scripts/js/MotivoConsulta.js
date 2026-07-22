const MotivoConsultaUI = {

    txtSintomas: document.getElementById("txtSintomas"),
    btnHablar: document.getElementById("btnHablar"),
    btnContinuar: document.getElementById("btnContinuar"),
    estadoDictado: document.getElementById("estadoDictado"),

    reconocimiento: null,

    escuchando: false,
    reconocimientoActivo: false,

    textoBase: "",

    inicializar: function () {

        const SpeechRecognition =
            window.SpeechRecognition ||
            window.webkitSpeechRecognition;

        if (!SpeechRecognition) {

            this.btnHablar.disabled = true;
            this.btnHablar.value = "Micrófono no soportado";
            return;
        }

        this.reconocimiento = new SpeechRecognition();

        this.reconocimiento.lang = "es-CO";
        this.reconocimiento.continuous = true;
        this.reconocimiento.interimResults = true;
        this.reconocimiento.maxAlternatives = 1;

        this.btnHablar.addEventListener("click", () => {

            if (!this.escuchando) {
                this.iniciarDictado();
            }
            else {
                this.detenerDictado();
            }

        });

        this.btnContinuar.addEventListener("click", () => {

            if (this.escuchando) {
                this.detenerDictado();
            }

        });

        this.reconocimiento.onstart = () => {

            this.reconocimientoActivo = true;

            this.estadoDictado.style.display = "block";

            this.btnHablar.disabled = false;
            this.btnHablar.value = "🔴 Escuchando...";
            this.btnHablar.classList.add("dictando");

        };

        this.reconocimiento.onresult = (event) => {

            let textoTemporal = "";

            for (let i = event.resultIndex; i < event.results.length; i++) {

                const transcripcion = event.results[i][0].transcript;

                if (event.results[i].isFinal) {

                    this.textoBase += transcripcion + " ";

                }
                else {

                    textoTemporal += transcripcion;

                }

            }

            this.txtSintomas.value = this.textoBase + textoTemporal;

        };

        this.reconocimiento.onend = () => {

            this.reconocimientoActivo = false;

            if (this.escuchando) {

                try {

                    this.reconocimiento.start();

                }
                catch (e) {

                    console.log("Reinicio ignorado:", e);

                }

            }
            else {

                this.btnHablar.disabled = false;
                this.btnHablar.value = "🎤 Iniciar dictado";
                this.btnHablar.classList.remove("dictando");

            }

        };

        this.reconocimiento.onerror = (event) => {

            console.log("SpeechRecognition:", event.error);

            switch (event.error) {

                case "no-speech":
                    break;

                case "aborted":
                    break;

                default:
                    this.detenerDictado();
                    break;

            }

        };

    },

    iniciarDictado: function () {

        if (this.reconocimientoActivo)
            return;

        this.escuchando = true;

        this.textoBase = this.txtSintomas.value;

        this.txtSintomas.readOnly = true;

        this.btnHablar.disabled = true;
        this.btnHablar.value = "⏳ Iniciando...";

        try {

            this.reconocimiento.start();

        }
        catch (e) {

            console.log(e);

            this.btnHablar.disabled = false;

        }

    },

    detenerDictado: function () {

        if (!this.escuchando)
            return;

        this.escuchando = false;

        this.btnHablar.disabled = true;
        this.btnHablar.value = "⏳ Deteniendo...";

        this.estadoDictado.style.display = "none";

        // Conserva todo el texto reconocido
        this.textoBase = this.txtSintomas.value.trim();

        this.txtSintomas.readOnly = false;

        if (this.reconocimientoActivo) {

            this.reconocimiento.stop();

        }
        else {

            this.btnHablar.disabled = false;
            this.btnHablar.value = "🎤 Iniciar dictado";
            this.btnHablar.classList.remove("dictando");

        }

    }

};

document.addEventListener("DOMContentLoaded", function () {

    MotivoConsultaUI.inicializar();

});