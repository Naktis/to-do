using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
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

        public DbSet<Todo.Models.Category> Categories { get; set; }

        public DbSet<Todo.Models.TodoItem> TodoItems { get; set; }

        public DbSet<Todo.Models.Tag> Tags { get; set; }

        public DbSet<Todo.Models.TodoItemTag> TodoItemTag { get; set; }
    }
}
