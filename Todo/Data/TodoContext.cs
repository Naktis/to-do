using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Web.Models;

namespace Todo.Web.Data
{
    public class AppContext : DbContext
    {
        public AppContext (DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItemTag>().HasKey(sc => new { sc.TodoItemID, sc.TagID });

            modelBuilder.Entity<TodoItemTag>()
                .HasOne<TodoItem>(ss => ss.TodoItem)
                .WithMany(s => s.TodoItemTags)
                .HasForeignKey(ss => ss.TodoItemID);

            modelBuilder.Entity<TodoItemTag>()
                .HasOne<Tag>(ss => ss.Tag)
                .WithMany(s => s.TodoItemTags)
                .HasForeignKey(ss => ss.TagID);
        }

        public DbSet<Todo.Web.Models.Category> Categories { get; set; }

        public DbSet<Todo.Web.Models.TodoItem> TodoItems { get; set; }

        public DbSet<Todo.Web.Models.Tag> Tags { get; set; }

        public DbSet<Todo.Web.Models.TodoItemTag> TodoItemTag { get; set; }
    }
}
