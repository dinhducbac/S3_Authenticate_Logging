using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise2.Entity
{
    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
