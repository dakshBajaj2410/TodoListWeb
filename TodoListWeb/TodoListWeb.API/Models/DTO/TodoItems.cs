namespace TodoListWeb.API.Models.DTO
{
    public class TodoItems
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
