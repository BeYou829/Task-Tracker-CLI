using System.Data;
using System.Text.Json;
using task_cli;

string fileName = "tasks.json";
string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
if (!File.Exists(path))
{
    File.WriteAllText(path, JsonSerializer.Serialize(new List<TaskTodo>()));
}
List<TaskTodo> tasks = JsonSerializer.Deserialize<List<TaskTodo>>(File.ReadAllText(path)) ?? [];

string action = args[0];
switch (action)
{
    case "add":
        string? taskName = args.ElementAtOrDefault(1);

        if (taskName is null)
        {
            Console.WriteLine($"Output: parameter description is missing.");
            return;
        }

        int nextId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

        TaskTodo newTask = new(nextId, taskName);
        SaveTask(newTask);

        Console.WriteLine($"Output: task added successfully (ID: {newTask.Id})");
        break;
    case "update":
        var taskToUpdate = tasks.FirstOrDefault(t => t.Id.ToString() == args.ElementAtOrDefault(1));

        if (taskToUpdate is null)
        {
            Console.WriteLine($"Output: task not found");
            return;
        }

        string? newTaskName = args.ElementAtOrDefault(2);

        if (string.IsNullOrWhiteSpace(newTaskName))
        {
            Console.WriteLine($"Output: parameter new description is missing");
            return;
        }

        taskToUpdate.Description = newTaskName;
        taskToUpdate.UpdatedAt = DateTime.Now;
        ReloadListTask();

        Console.WriteLine($"Output: task {taskToUpdate.Id} updated.");
        break;
    case "delete":
        var taskToDelete = tasks.FirstOrDefault(t => t.Id.ToString() == args.ElementAtOrDefault(1));

        if (taskToDelete is null)
        {
            Console.WriteLine($"Output: task not found");
            return;
        }

        tasks.Remove(taskToDelete);
        ReloadListTask();

        Console.WriteLine($"Output: task has been removed");
        break;
    case "mark-in-progress":
        // todo: Actualizar updatedAt campo
        var taskInProgress = tasks.FirstOrDefault(t => t.Id.ToString() == args.ElementAtOrDefault(1));

        if (taskInProgress is null)
        {
            Console.WriteLine($"Output: task not found");
            return;
        }

        taskInProgress.Status = Status.in_progress;
        taskInProgress.UpdatedAt = DateTime.Now;
        ReloadListTask();

        Console.WriteLine($"Output: task {taskInProgress.Description} changed its status to: {taskInProgress.Status}");
        break;
    case "mark-done":
        // todo: Actualizar updatedAt campo
        var taskDone = tasks.FirstOrDefault(t => t.Id.ToString() == args.ElementAtOrDefault(1));

        if (taskDone is null)
        {
            Console.WriteLine($"Output: task not found");
            return;
        }

        taskDone.Status = Status.done;
        taskDone.UpdatedAt = DateTime.Now;
        ReloadListTask();

        Console.WriteLine($"Output: task {taskDone.Description} changed its status to: {taskDone.Status}");
        break;
    case "list":
        var result = args.ElementAtOrDefault(1) switch
        {
            "done" => ListByStatus(Status.done),
            "in-progress" => ListByStatus(Status.in_progress),
            "todo" => ListByStatus(Status.todo),
            _ => tasks
        };

        PrintTasks(result);
        break;
    default:
        Console.WriteLine("Unknow command");
        break;
}

List<TaskTodo> ListByStatus(Status status) => tasks.Where(x => x.Status.Equals(status)).ToList();
void SaveTask(TaskTodo newTask)
{
    tasks.Add(newTask);

    ReloadListTask();
}
void ReloadListTask()
{
    string jsonString = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
    {
        WriteIndented = true
    });

    File.WriteAllText(path, jsonString);
}
void PrintTasks(List<TaskTodo> tasks)
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks found.");
        return;
    }
    int maxLength = tasks.Max(t => t.Description.Length);

    Console.WriteLine("ID  | Description    | Status      | Created At             | Updated At");
    System.Console.WriteLine("------------------------------------------------------------------------------------");
    foreach (var task in tasks)
    {
        Console.WriteLine(
            $"{task.Id,-3} | " +
            $"{task.Description.PadRight(maxLength)} | " +
            $"{task.Status,-11} | " +
            $"{task.CreatedAt,-22} | " +
            $"{task.UpdatedAt}");
    }

}
