using Microsoft.CodeAnalysis.Differencing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class TodoItem
    {
        public TodoItem()   // Automatic ID incremention and assignment
        {
            ID = maxID;
            maxID++;
        }

        public TodoItem Copy(TodoItem newData)  // Copy assignment without changing ID for editing
        {
            Name = newData.Name;
            Description = newData.Description;
            priority = newData.Priority;
            return this;
        }

        private static int maxID = 0;
        public int ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        private int priority = 3;
        public int Priority
        {
            get 
            { 
                return priority; 
            }
            set
            {
                if (value >= 1 && value <= 5)
                    priority = value;
            }
        }
    }
}
