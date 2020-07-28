using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Data
{
    internal class DBInitializer
    {
        public static void Initialize(AppContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            context.Categories.Add(new Category() { Name = "Work" });
            context.Categories.Add(new Category() { Name = "Personal" });

            context.SaveChanges();

            context.TodoItems.Add(new TodoItem() { Name = "Exercise", Description = "Catch a pigeon" });
            context.TodoItems.Add(new TodoItem() { Name = "Sweet dreams", Description = "Are made of this" });

            context.SaveChanges();
        }
    }
}
