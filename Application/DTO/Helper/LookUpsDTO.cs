using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
   

    public class LookUpsDTO
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string? Code { get; set; }
        public string? LatinName { get; set; }
        public string? ArabicName { get; set; }
    }
}
