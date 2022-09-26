using Microsoft.EntityFrameworkCore;
using todolist2.Models;

namespace todolist2.infrastructure
{
    public class todo_context:DbContext
    {
        public todo_context(DbContextOptions<todo_context> options)
            : base(options) 
        {

        }

        public DbSet<todo_list> todo_list { get; set; }
    }
}
