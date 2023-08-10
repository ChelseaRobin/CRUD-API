using System.ComponentModel.DataAnnotations;

namespace CRUD_API.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateTimeStamp { get; set; }
    }
}
