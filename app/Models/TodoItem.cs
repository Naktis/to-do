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
        public int ID { get; set;  }
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
                else if (value > 5)
                    priority = 5;
                // If neither of the conditions are met,
                // the value is kept default (3)
            }
        }
        public TodoItem Copy(TodoItem newData)  // Copy assignment without changing ID
        {
            Name = newData.Name;
            Description = newData.Description;
            priority = newData.Priority;
            return this;
        }
    }
}
