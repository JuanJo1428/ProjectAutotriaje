function manejarErroresTemporales() {
    var errores = document.querySelectorAll('.validation-error');

    errores.forEach(function (error) {
        if (error.style.display !== 'none' && !error.classList.contains('fade-out-started')) {

            error.classList.add('fade-out-started');

            setTimeout(function () {
                error.style.transition = "opacity 0.5s ease";
                error.style.opacity = "0";

                setTimeout(function () {
                    error.style.display = 'none';
                    error.style.opacity = '1';
                    error.classList.remove('fade-out-started');
                }, 500);

            }, 4000);
        }
    });
}

document.addEventListener("keyup", manejarErroresTemporales);
document.addEventListener("click", manejarErroresTemporales);
