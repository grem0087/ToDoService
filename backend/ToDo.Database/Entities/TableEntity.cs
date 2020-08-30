using System.ComponentModel.DataAnnotations;

namespace ToDo.Database.Entities
{
    public class TableEntity
    {
        [Key]
        public int Id { get; set; }
        public string TableId { get; set; }
    }
}
