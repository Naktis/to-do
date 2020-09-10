using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Data.Context
{
    public class AppContext : DbContext
    {

        public AppContext() { }
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

        public virtual DbSet<CategoryDao> Categories { get; set; }

        public virtual DbSet<TodoItemDao> TodoItems { get; set; }

        public virtual DbSet<TagDao> Tags { get; set; }

        public virtual DbSet<TodoItemTagDao> TodoItemTag { get; set; }
    }
}
