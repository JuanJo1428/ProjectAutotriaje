const MotivoConsultaUI = {

    txtSintomas: document.getElementById("txtSintomas"),
    btnHablar: document.getElementById("btnHablar"),
    btnContinuar: document.getElementById("btnContinuar"),
    estadoDictado: document.getElementById("estadoDictado"),

    reconocimiento: null,

    escuchando: false,
    reconocimientoActivo: false,

    // Cada resultado final queda guardado aquí.
    resultadosFinales: {},

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

            if (this.escuchando)
                this.detenerDictado();
            else
                this.iniciarDictado();

        });

        this.btnContinuar.addEventListener("click", () => {

            if (this.escuchando)
                this.detenerDictado();

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

                const texto = event.results[i][0].transcript.trim();

                if (event.results[i].isFinal) {

                    // SIEMPRE reemplaza el resultado del mismo índice.
                    // Nunca concatena.
                    this.resultadosFinales[i] = texto;

                }
                else {

                    textoTemporal += texto + " ";
                }
            }

            let textoFinal = "";

            Object.keys(this.resultadosFinales)
                .sort((a, b) => Number(a) - Number(b))
                .forEach(indice => {

                    textoFinal += this.resultadosFinales[indice] + " ";

                });

            let texto = (textoFinal + textoTemporal)
                .replace(/\s+/g, " ")
                .trim();

            if (texto.length > 0) {

                texto =
                    texto.charAt(0).toUpperCase() +
                    texto.slice(1);

            }

            this.txtSintomas.value = texto;
        };

        this.reconocimiento.onend = () => {

            this.reconocimientoActivo = false;

            if (this.escuchando) {

                try {

                    this.reconocimiento.start();

                }
                catch (e) {

                    console.log(e);

                }

            }
            else {

                this.estadoDictado.style.display = "none";

                this.btnHablar.disabled = false;
                this.btnHablar.value = "🎤 Iniciar dictado";
                this.btnHablar.classList.remove("dictando");
            }
        };

        this.reconocimiento.onerror = (event) => {

            console.log(event.error);

            if (event.error !== "aborted" &&
                event.error !== "no-speech") {

                this.detenerDictado();

            }

        };

    },

    iniciarDictado: function () {

        if (this.reconocimientoActivo)
            return;

        this.resultadosFinales = {};

        this.escuchando = true;

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

        this.txtSintomas.readOnly = false;

        this.btnHablar.disabled = true;
        this.btnHablar.value = "⏳ Deteniendo...";

        if (this.reconocimientoActivo)
            this.reconocimiento.stop();

    }

};

document.addEventListener("DOMContentLoaded", function () {

    MotivoConsultaUI.inicializar();

});