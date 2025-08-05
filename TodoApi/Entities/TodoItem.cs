namespace TodoApi.Entities
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        // Khóa ngoại đến User
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
