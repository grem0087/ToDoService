using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Database.Entities
{
    [Table("Todos")]
    public class ToDoEntity
    {
        [Key]
        public int Id { get; set; }
        public string TableId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Estimation { get; set; }
        // maybe components in future
        //public string[] Tags { get; set; }
        public string Author { get; set; }
        public string Assignee { get; set; }
    }
}