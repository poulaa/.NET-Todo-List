using System.ComponentModel.DataAnnotations;

namespace todolist2.Models
{
    public class todo_list
    {
        public int id { get; set; }
        [Required]
        public string content { get; set; }
    }
}
