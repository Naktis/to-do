using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Data
{
    internal class DBInitializer
    {
        public static void Initialize(appContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            context.Categories.Add(new Category() { Name = "Work" });
            context.Categories.Add(new Category() { Name = "Personal" });

            context.SaveChanges();
        }
    }
}
