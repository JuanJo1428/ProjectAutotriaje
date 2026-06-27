// Update Clock
function updateClock() {
    const el = document.getElementById('lblHora');
    const now = new Date();
    el.textContent = now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    el.setAttribute('datetime', now.toISOString().slice(0, 16));
}

function startClock() {
    updateClock();
    const now = new Date();
    const msToNextMinute = (60 - now.getSeconds()) * 1000 - now.getMilliseconds();
    setTimeout(function () {
        updateClock();
        setInterval(updateClock, 60000);
    }, msToNextMinute);
}

startClock();
