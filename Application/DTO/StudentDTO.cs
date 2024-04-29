using Application.Core.Mappings;
using Domain.Entities;

namespace Application.DTO
{
    public class StudentDTO : IMapFrom<Student> //mapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
