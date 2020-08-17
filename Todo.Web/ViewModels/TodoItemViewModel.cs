using Microsoft.CodeAnalysis.Differencing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Models;

namespace Todo.Web.ViewModels
{
    public class TodoItemViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeadLineDate { get; set; }

        public DateTime CreationDate { get; set; }

        [Range(1,5)]
        [Required]
        public int Priority { get; set; } = 3;

        public TodoItemStatusDao Status { get; set; }

        public int? CategoryID { get; set; }
        public CategoryDao Category { get; set; }

        public IList<TodoItemTagDao> TodoItemTags { get; set; }
    }
}
