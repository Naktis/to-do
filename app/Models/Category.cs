using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class Category
{
        public Category()   // Automatic ID incremention and assignment
        {
            ID = maxID;
            maxID++;
        }
        public static void decreaseMaxID()
        {
            maxID--;
        }

        private static int maxID = 0;
        public int ID { get; }
        public string Name { get; set; }
    }
}
