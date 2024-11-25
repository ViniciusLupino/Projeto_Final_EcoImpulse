function calcularImpactoAmbientalAvancado() {
    var emissaoCO2 = parseFloat(document.getElementById("emissaoCO2").value);
    var consumoAgua = parseFloat(document.getElementById("consumoAgua").value);
    var geracaoLixo = parseFloat(document.getElementById("geracaoLixo").value);
    var energiaRenovavel = parseFloat(document.getElementById("energiaRenovavel").value);
    var eficienciaEnergia = parseFloat(document.getElementById("eficienciaEnergia").value);
    var reducaoEmissao = parseFloat(document.getElementById("reducaoEmissao").value);

    var impactoAmbientalBasico = (emissaoCO2 + consumoAgua + geracaoLixo) / 3;
    var reducaoImpacto = (impactoAmbientalBasico * reducaoEmissao) / 100;
    var eficienciaImpacto = (impactoAmbientalBasico * eficienciaEnergia) / 100;
    var energiaRenovavelImpacto = (impactoAmbientalBasico * energiaRenovavel) / 100;
    var impactoAmbientalFinal = impactoAmbientalBasico - reducaoImpacto - eficienciaImpacto + energiaRenovavelImpacto;

    // Exibir resultado em uma modal
    var modal = document.getElementById("modal");
    var resultadoModal = document.getElementById("resultado-modal");
    resultadoModal.innerHTML = "Impacto Ambiental: " + impactoAmbientalFinal.toFixed(2);
    modal.style.display = "block";

    // Fechar modal quando clicar no botão fechar
    var close = document.getElementsByClassName("close")[0];
    close.onclick = function () {
        modal.style.display = "none";
    }
}
// Exibir a modal

