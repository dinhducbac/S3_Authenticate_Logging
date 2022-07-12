namespace Exercise2.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public EmployeeViewModel(int id, string name, string position)
        {
            Id = id;
            Name = name;
            Position = position;
        }
    }
}
