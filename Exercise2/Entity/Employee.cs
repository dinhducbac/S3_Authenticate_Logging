using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise2.Entity
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositionID { get; set; }
        public Position Position { get; set; }
    }
}
