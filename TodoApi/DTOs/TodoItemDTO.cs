namespace TodoApi.DTOs
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        public long UserId { get; set; }
    }
}
    