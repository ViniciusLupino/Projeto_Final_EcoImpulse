// Variável para armazenar as tarefas
let tasks = [];
let currentTaskId = null;
let draggedTaskId = null;

// Função para abrir o modal
function openModal(mode, taskId) {
    const modal = document.getElementById('modal');
    modal.style.display = 'block';

    // Se o modo for editar
    if (mode === 2) {
        const task = tasks.find(t => t.id === taskId);
        document.getElementById('description').value = task.description;
        document.getElementById('priority').value = task.priority;
        document.getElementById('column').value = task.column;
        document.getElementById('datetime').value = task.dueDate;
        currentTaskId = taskId;

        document.getElementById('criarModeTitulo').style.display = 'none';
        document.getElementById('editaModeTitulo').style.display = 'block';
        document.getElementById('criarModeBotao').style.display = 'none';
        document.getElementById('editarModeBotao').style.display = 'block';
    } else {
        // Limpar os campos para criar uma nova tarefa
        document.getElementById('description').value = '';
        document.getElementById('priority').value = 'Baixa';
        document.getElementById('column').value = '1';
        document.getElementById('datetime').value = '';
        currentTaskId = null;

        document.getElementById('criarModeTitulo').style.display = 'block';
        document.getElementById('editaModeTitulo').style.display = 'none';
        document.getElementById('criarModeBotao').style.display = 'block';
        document.getElementById('editarModeBotao').style.display = 'none';
    }
}

// Função para fechar o modal
function closeModal() {
    const modal = document.getElementById('modal');
    modal.style.display = 'none';
}

// Função para criar uma nova tarefa
function createTask() {
    const task = {
        id: Date.now(),
        description: document.getElementById('description').value,
        priority: document.getElementById('priority').value,
        column: document.getElementById('column').value,
        dueDate: document.getElementById('datetime').value
    };

    tasks.push(task);
    closeModal();
    renderTasks();
}

// Função para editar uma tarefa existente
function editarTask() {
    const taskIndex = tasks.findIndex(t => t.id === currentTaskId);
    if (taskIndex !== -1) {
        tasks[taskIndex].description = document.getElementById('description').value;
        tasks[taskIndex].priority = document.getElementById('priority').value;
        tasks[taskIndex].column = document.getElementById('column').value;
        tasks[taskIndex].dueDate = document.getElementById('datetime').value;

        closeModal();
        renderTasks();
    }
}

// Função para renderizar as tarefas nas colunas
// Função para renderizar as tarefas nas colunas
function renderTasks() {
    const columns = {
        1: document.querySelector('[data_column="1"] .cards_list'),
        2: document.querySelector('[data_column="2"] .cards_list'),
        3: document.querySelector('[data_column="3"] .cards_list')
    };

    // Limpa as colunas antes de renderizar
    Object.values(columns).forEach(column => {
        column.innerHTML = '<button class="add_item" onclick="openModal(1)">Adicionar Item</button>';
    });

    // Adiciona as tarefas nas colunas apropriadas
    tasks.forEach(task => {
        const taskElement = document.createElement('div');
        taskElement.className = 'task';
        taskElement.setAttribute('draggable', 'true'); // Permite que a tarefa seja arrastável
        taskElement.setAttribute('data-task-id', task.id); // Atribui o ID da tarefa
        taskElement.innerHTML = `
            <div class="task-details">
                <p><strong>Descrição:</strong> ${task.description}</p>
                <p><strong>Prioridade:</strong> ${task.priority}</p>
                <p><strong>Prazo:</strong> ${task.dueDate}</p>
            </div>
            <div class="task-buttons">
                <button onclick="openModal(2, ${task.id})">Editar</button>
                <button onclick="deleteTask(${task.id})">Excluir</button>
            </div>
        `;

        // Eventos de drag and drop
        taskElement.addEventListener('dragstart', (event) => {
            draggedTaskId = task.id; // Armazena o ID da tarefa arrastada
            event.dataTransfer.effectAllowed = 'move';
        });

        taskElement.addEventListener('dragend', () => {
            draggedTaskId = null; // Limpa o ID após o arrasto
        });

        columns[task.column].appendChild(taskElement);
    });
}


// Função para excluir uma tarefa
function deleteTask(taskId) {
    tasks = tasks.filter(task => task.id !== taskId);
    renderTasks();
}

// Função para permitir o arrastar e soltar
function dragoverHandler(event) {
    event.preventDefault(); // Permite que o elemento seja solto
}

function dropHandler(event) {
    event.preventDefault();
    const columnId = event.target.closest('.column').getAttribute('data_column');

    // Atualiza a coluna da tarefa arrastada
    if (draggedTaskId) {
        const taskIndex = tasks.findIndex(t => t.id === draggedTaskId);
        if (taskIndex !== -1) {
            tasks[taskIndex].column = columnId; // Atualiza a coluna da tarefa
            renderTasks(); // Renderiza novamente para mostrar a nova posição
        }
    }
}

// Adicionar evento para fechar o modal ao clicar fora dele
window.onclick = function (event) {
    const modal = document.getElementById('modal');
    if (event.target === modal) {
        closeModal();
    }
}

// Adicionar eventos de drop e dragover para as colunas
document.querySelectorAll('.column').forEach(column => {
    column.addEventListener('dragover', dragoverHandler);
    column.addEventListener('drop', dropHandler);
});