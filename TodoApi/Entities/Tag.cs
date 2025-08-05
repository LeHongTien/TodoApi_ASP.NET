namespace TodoApi.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}