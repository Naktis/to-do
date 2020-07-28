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
        public TodoItem()
        {
             CreationDate = DateTime.Now;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        #nullable enable
        public string? Description { get; set; }

        public DateTime? DeadLineDate { get; set; }
        #nullable disable

        public DateTime CreationDate { get; }

        [Range(1,5)]
        public int Priority { get; set; } = 3;

        enum Status
        {
            Backlog, //default value since it is the first enumerator
            Wip,
            Done,
            Archived
        }
    }
}
