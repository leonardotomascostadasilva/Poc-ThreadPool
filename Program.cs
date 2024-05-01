using System.Diagnostics;

ThreadPool.SetMinThreads(50, 50); // Define o número mínimo de threads no ThreadPool como 50.

async Task TestAsync(int item) // Declara uma função assíncrona chamada TestAsync que recebe um parâmetro item.
{
    // Abre um arquivo para escrita de forma assíncrona. O caminho do arquivo é construído com base no valor do parâmetro item.
    using (var stream = new StreamWriter($"./files/file_{item}.txt"))
    {
        // Escreve no arquivo de forma assíncrona. O conteúdo é uma string que inclui o valor do parâmetro item.
        await stream.WriteAsync($"Conteúdo do arquivo {item}");
    }
}

Stopwatch stopwatch = Stopwatch.StartNew(); // Cria e inicia um cronômetro para medir o tempo de execução.

List<Task> tasks = new List<Task>(); // Cria uma lista para armazenar as tarefas assíncronas.

for (int i = 0; i < 1000; i++) // Loop de 0 a 999.
{
    int currentItem = i; // Armazena o valor do índice atual em uma variável temporária.

    // Adiciona uma tarefa assíncrona à lista de tarefas. Cada tarefa chama a função TestAsync com o valor do índice atual como argumento.
    tasks.Add(TestAsync(currentItem));
}

// Aguarda a conclusão de todas as tarefas na lista.
await Task.WhenAll(tasks);

stopwatch.Stop(); // Para o cronômetro.

// Exibe o tempo total decorrido durante a execução das tarefas.
Console.WriteLine($"A operação levou {stopwatch.ElapsedMilliseconds} ms.");