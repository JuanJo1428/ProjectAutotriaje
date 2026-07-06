function manejarErroresTemporales() {
    // Buscamos todos los validadores con tu clase
    var errores = document.querySelectorAll('.validation-error');

    errores.forEach(function (error) {
        // Validamos si ASP.NET lo hizo visible (style.display != 'none')
        if (error.style.display !== 'none' && !error.classList.contains('fade-out-started')) {

            // Marcamos que ya iniciamos el temporizador para este error
            error.classList.add('fade-out-started');

            // Espera 4 segundos y lo desaparece suavemente
            setTimeout(function () {
                error.style.transition = "opacity 0.5s ease";
                error.style.opacity = "0";

                // Después de la animación, lo ocultamos por completo
                setTimeout(function () {
                    error.style.display = 'none';
                    error.style.opacity = '1'; // Reseteamos opacidad para la próxima validación
                    error.classList.remove('fade-out-started');
                }, 500);

            }, 200000); // 4000 milisegundos = 4 segundos
        }
    });
}

// Interceptamos el sistema de validación de ASP.NET
// Cada vez que el usuario escribe o hace clic, revisamos si aparecieron errores
document.addEventListener("keyup", manejarErroresTemporales);
document.addEventListener("click", manejarErroresTemporales);
