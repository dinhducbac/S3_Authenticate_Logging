namespace Exercise2.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public EmployeeModel(int id, string name, string position)
        {
            Id = id;
            Name = name;
            Position = position;
        }
    }
}
