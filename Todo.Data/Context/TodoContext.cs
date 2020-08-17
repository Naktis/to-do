using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Business.Models;

namespace Todo.Business.Data
{
    public class AppContext : DbContext
    {
        public AppContext (DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItemTagDao>().HasKey(sc => new { sc.TodoItemID, sc.TagID });

            modelBuilder.Entity<TodoItemTagDao>()
                .HasOne<TodoItemDao>(ss => ss.TodoItem)
                .WithMany(s => s.TodoItemTags)
                .HasForeignKey(ss => ss.TodoItemID);

            modelBuilder.Entity<TodoItemTagDao>()
                .HasOne<TagDao>(ss => ss.Tag)
                .WithMany(s => s.TodoItemTags)
                .HasForeignKey(ss => ss.TagID);
        }

        public DbSet<Todo.Business.Models.CategoryDao> Categories { get; set; }

        public DbSet<Todo.Business.Models.TodoItemDao> TodoItems { get; set; }

        public DbSet<Todo.Business.Models.TagDao> Tags { get; set; }

        public DbSet<Todo.Business.Models.TodoItemTagDao> TodoItemTag { get; set; }
    }
}
