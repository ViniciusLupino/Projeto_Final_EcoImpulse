// Espera o documento estar completamente carregado
document.addEventListener("DOMContentLoaded", function () {
    // Inicializa o carrossel
    var carousel = new bootstrap.Carousel(document.querySelector('#carouselExampleIndicators'), {
        interval: 3000, // Intervalo entre os slides em milissegundos (ex: 3000ms = 3s)
        pause: 'hover' // Pausa o carrossel quando o mouse está sobre ele
    });

    // Adiciona eventos para os controles
    var prevButton = document.querySelector('.carousel-control-prev');
    var nextButton = document.querySelector('.carousel-control-next');

    prevButton.addEventListener('click', function () {
        carousel.prev();
    });

    nextButton.addEventListener('click', function () {
        carousel.next();
    });
});
