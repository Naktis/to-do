using app.Services;
using Microsoft.CodeAnalysis.Differencing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class TodoItem : IID
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        #nullable enable
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeadLineDate { get; set; }
        #nullable disable

        public DateTime CreationDate { get; set; }

        [Range(1,5)]
        [Required]
        public int Priority { get; set; } = 3;

        public TodoItemStatus Status { get; set; }

        public int? CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
