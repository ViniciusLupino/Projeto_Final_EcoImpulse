// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// script.js
const questions = [
    {
        question: "Qual é o principal objetivo dos ODS (Objetivos de Desenvolvimento Sustentável)?",
        options: ["Erradicar a pobreza", "Promover a justiça social", "Salvar as abelhas", "Reduzir o consumo de água"],
        answer: 0
    },
    {
        question: "Qual é a principal fonte de energia renovável?",
        options: ["Carvão", "Gás natural", "Solar", "Nuclear"],
        answer: 2
    },
    {
        question: "Qual é o método mais eficaz para reduzir a quantidade de resíduos sólidos?",
        options: ["Reciclagem", "Compostagem", "Redução e Reuso", "Queima"],
        answer: 2
    },
    {
        question: "Qual é o impacto do desmatamento na biodiversidade?",
        options: ["Aumenta a biodiversidade", "Não tem impacto", "Reduz a biodiversidade", "Melhora a qualidade do ar"],
        answer: 2
    },
    {
        question: "O que significa a sigla ESG?",
        options: ["Economia, Sustentabilidade e Gestão", "Energia, Segurança e Governança", "Environmental, Social and Governance", "Energia, Sociedade e Governo"],
        answer: 2
    },
    {
        question: "Qual é a vantagem da compostagem em relação ao descarte de resíduos orgânicos?",
        options: ["Reduz a poluição do ar", "Diminui o volume de resíduos enviados para aterros", "Aumenta a quantidade de resíduos tóxicos", "Não tem vantagem"],
        answer: 1
    },
    {
        question: "O que é o aquecimento global?",
        options: ["Aumento da temperatura média da Terra", "Resfriamento da Terra", "Aumento da umidade", "Mudança na rotação da Terra"],
        answer: 0
    },
    {
        question: "Qual é o principal gás de efeito estufa produzido pela queima de combustíveis fósseis?",
        options: ["Oxigênio", "Metano", "Dióxido de carbono", "Nitrogênio"],
        answer: 2
    },
    {
        question: "Qual é a importância da proteção dos oceanos para o clima global?",
        options: ["Aumenta a quantidade de oxigênio", "Regula a temperatura global", "Aumenta a acidez do solo", "Reduz o nível do mar"],
        answer: 1
    },
    {
        question: "Qual é a principal fonte de poluição dos oceanos?",
        options: ["Poluição por plástico", "Óxidos de nitrogênio", "Dióxido de enxofre", "Poluição por metais pesados"],
        answer: 0
    },
    {
        question: "Qual é o conceito de 'pegada ecológica'?",
        options: ["A quantidade de resíduos produzidos por uma pessoa", "A quantidade de água consumida por uma pessoa", "A área de terra necessária para sustentar um estilo de vida", "O impacto do uso de energia elétrica"],
        answer: 2
    },
    {
        question: "O que é a economia circular?",
        options: ["Economia que visa o crescimento contínuo", "Modelo de produção e consumo que visa a redução de resíduos", "Economia baseada em combustíveis fósseis", "Modelo econômico que incentiva o desperdício"],
        answer: 1
    },
    {
        question: "Qual é a função dos parques e reservas naturais?",
        options: ["Reduzir a biodiversidade", "Aumentar a urbanização", "Proteger habitats naturais e biodiversidade", "Desenvolver áreas para construção"],
        answer: 2
    },
    {
        question: "O que é o efeito estufa?",
        options: ["Processo natural que aquece a atmosfera da Terra", "Poluição causada por produtos químicos", "Destruição da camada de ozônio", "Redução da biodiversidade"],
        answer: 0
    },
    {
        question: "Qual é a principal estratégia para economizar água em casa?",
        options: ["Tomar banhos mais longos", "Deixar a torneira aberta enquanto escova os dentes", "Instalar dispositivos de economia de água", "Usar mais produtos de limpeza"],
        answer: 2
    }
];

let currentQuestionIndex = 0;
let score = 0;
let confettiInstance = null;

document.addEventListener("DOMContentLoaded", () => {
    loadQuestion();
    document.getElementById("next-button").addEventListener("click", () => {
        const selectedOption = document.querySelector('input[name="option"]:checked');
        if (selectedOption) {
            const answerIndex = parseInt(selectedOption.value);
            if (answerIndex === questions[currentQuestionIndex].answer) {
                score++;
            }
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.length) {
                loadQuestion();
            } else {
                showResult();
            }
        } else {
            alert("Por favor, selecione uma resposta.");
        }
    });

    document.getElementById("restart-button").addEventListener("click", () => {
        currentQuestionIndex = 0;
        score = 0;
        document.getElementById("result-container").classList.add("hidden");
        document.getElementById("quiz-container").classList.remove("hidden");
        hideConfetti(); // Hide confetti animation
        loadQuestion();
    });
});

function loadQuestion() {
    const question = questions[currentQuestionIndex];
    document.getElementById("question-container").innerHTML = `<h2>${question.question}</h2>`;
    const optionsContainer = document.getElementById("options-container");
    optionsContainer.innerHTML = question.options.map((option, index) =>
        `<label><input type="radio" name="option" value="${index}"> ${option}</label><br>`
    ).join('');
}
/*
document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById('modal');
    const closeModal = document.querySelector('.close');
    const canvas = document.getElementById('canvas');
    const confettiSettings = { target: canvas, max: 200, size: 1.5, animate: true };
    let confetti;

    function showModal() {
        // Exibir a modal e o canvas
        modal.style.display = 'block';
        canvas.style.display = 'block';

        // Inicializar os confetes
        confetti = new ConfettiGenerator(confettiSettings);
        confetti.render();
    }

    // Listener para fechar a modal
    closeModal.addEventListener('click', function () {
        if (confetti) {
            confetti.clear();
        }
        modal.style.display = 'none';
    });

    // Listener para mostrar o resultado (simulação)
    document.getElementById('next-button').addEventListener('click', function () {
        // Simulação de exibir o resultado final
        showModal();
    });

    // Listener para o botão 'Jogar Novamente'
    document.getElementById('restart-button').addEventListener('click', function () {
        if (confetti) {
            confetti.clear();
        }
        modal.style.display = 'none';
    });
});*/

function showResult() {
    document.getElementById("quiz-container").classList.add("hidden");
    document.getElementById("result-container").classList.remove("hidden");

    let resultText = "";
    if (score === questions.length) {
        resultText = "Parabéns! Você acertou todas as perguntas! 🌟";
    } else if (score >= 12) {
        resultText = "Ótimo trabalho! Você acertou a maioria das perguntas!";
    } else if (score >= 8) {
        resultText = "Bom trabalho! Você acertou mais da metade das perguntas.";
    } else if (score >= 4) {
        resultText = "Você tem um bom conhecimento, mas há espaço para melhorar.";
    } else {
        resultText = "Continue aprendendo sobre sustentabilidade! Você pode melhorar.";
    }

    document.getElementById("result-text").innerText = `Você acertou ${score} de ${questions.length} perguntas.\n${resultText}`;

    const canvas = document.getElementById('canvas');
    canvas.style.display = 'block'; // Ensure the canvas is visible
    confettiInstance = new ConfettiGenerator({ target: 'canvas' });
    confettiInstance.render();
}

function hideConfetti() {
    if (confettiInstance) {
        confettiInstance.clear(); 
        const canvas = document.getElementById('canvas');
        canvas.style.display = 'none';
    }
}
/*

function showResult() {
    document.getElementById("quiz-container").classList.add("hidden");
    document.getElementById("result-container").classList.remove("hidden");

    let resultText = "";
    if (score === questions.length) {
        resultText = "Parabéns! Você acertou todas as perguntas! 🌟";
    } else if (score >= 12) {
        resultText = "Ótimo trabalho! Você acertou a maioria das perguntas!";
    } else if (score >= 8) {
        resultText = "Bom trabalho! Você acertou mais da metade das perguntas.";
    } else if (score >= 4) {
        resultText = "Você tem um bom conhecimento, mas há espaço para melhorar.";
    } else {
        resultText = "Continue aprendendo sobre sustentabilidade! Você pode melhorar.";
    }

    document.getElementById("result-text").innerText = `Você acertou ${score} de ${questions.length} perguntas.\n${resultText}`;

    const canvas = document.getElementById('canvas');
    canvas.style.display = 'block'; // Certifique-se de que o canvas está visível
    confettiInstance = new ConfettiGenerator({ target: 'canvas' });
    confettiInstance.render();
}

function hideConfetti() {
    if (confettiInstance) {
        confettiInstance.clear();
        const canvas = document.getElementById('canvas');
        canvas.style.display = 'none'; // Esconde o canvas
    }
}
*/