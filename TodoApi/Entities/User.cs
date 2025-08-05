using System.ComponentModel.DataAnnotations;

namespace TodoApi.Entities
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        // Quan hệ 1-nhiều
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
