namespace ToDo.Service.Controllers.Dtos
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string TableId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Estimation { get; set; }
        public string[] Tags { get; set; }
        public string Author { get; set; }
        public string Assignee { get; set; }
    }
}
