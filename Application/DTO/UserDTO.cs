using Application.Core.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UserDTO:IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
