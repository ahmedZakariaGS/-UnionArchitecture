using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{ 

   
   
    public enum StatusResult
    {
        Falid,
        Success,
        RelatedData,
        Exist,
        ExistInChild,
        NotExists,
        ApplicationException
 
    }
  

     
     public enum LanguageType
    {
        Ar = 1,
        En = 2
    }
    
}
