namespace task_cli
{
    public class TaskTodo(int id, string description)
    {
        public int Id { get; set; } = id;
        public string Description { get; set; } = description;
        public Status Status { get; set; } = Status.todo;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}