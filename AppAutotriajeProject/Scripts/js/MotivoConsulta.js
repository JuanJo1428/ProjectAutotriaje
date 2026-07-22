const VozUI = {

    txtSintomas: document.getElementById("txtSintomas"),
    btnHablar: document.getElementById("btnHablar"),

    recognition: null,
    escuchando: false,

    iniciar: function () {

        const SpeechRecognition =
            window.SpeechRecognition || window.webkitSpeechRecognition;

        if (!SpeechRecognition) {
            alert("Este navegador no soporta reconocimiento de voz.");
            return;
        }

        this.recognition = new SpeechRecognition();

        this.recognition.lang = "es-CO";
        this.recognition.continuous = true;
        this.recognition.interimResults = true;

        this.recognition.onstart = () => {

            this.escuchando = true;
            this.btnHablar.value = "Detener";

        };

        this.recognition.onend = () => {

            this.escuchando = false;
            this.btnHablar.value = "Hablar (Voz)";

        };

        this.recognition.onresult = (event) => {

            let texto = "";

            for (let i = event.resultIndex; i < event.results.length; i++) {

                texto += event.results[i][0].transcript;

            }

            this.txtSintomas.value = texto;

        };

        this.btnHablar.addEventListener("click", () => {

            if (this.escuchando) {

                this.recognition.stop();

            } else {

                this.recognition.start();

            }

        });

    }

};

document.addEventListener("DOMContentLoaded", function () {

    VozUI.iniciar();

});