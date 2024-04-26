using System.Diagnostics;

// Configura o tamanho máximo do ThreadPool com 10000 threads de trabalho e 10000 threads de conclusão
ThreadPool.SetMaxThreads(10000, 10000);

// Método assíncrono que simula uma operação demorada
async Task TarefaAsync(int item)
{
    // Imprime na tela o ID da thread atual e o número do item sendo processado
    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is processing item {item}");

    // Simula uma operação de espera de 1 segundo
    await Task.Delay(1000);
}

// Cria um cronômetro para medir o tempo de execução
Stopwatch stopwatch = Stopwatch.StartNew();

// Lista para armazenar as tarefas
List<Task> tasks = new List<Task>();

// Loop para criar e iniciar 1 milhão de tarefas
for (int i = 0; i < 1_000_000; i++)
{
    // Captura o valor do índice para cada iteração
    int currentItem = i;

    // Inicia uma nova tarefa para processar o item atual
    tasks.Add(Task.Run(() => TarefaAsync(currentItem)));
}

// Aguarda a conclusão de todas as tarefas
await Task.WhenAll(tasks);

// Para o cronômetro
stopwatch.Stop();

// Imprime na tela o tempo total decorrido
Console.WriteLine($"A operação levou {stopwatch.ElapsedMilliseconds} ms.");