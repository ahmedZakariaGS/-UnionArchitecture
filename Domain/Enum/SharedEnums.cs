using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{


    public enum enumPagesID
    {
        Nationalities,
        EmpStatus,
        Stages,
        StageType,
        Branchs,
        Edarat,
        Related,
        RelatedGov,
        EmpFlags,
        Countries,
        LenderGeha,
        Subjects,
        SuperVisorJobs,
        JobTypes,
        JobsSetting,
        Qualifications,
        QualificationEntities,
        QualificationTypes,
        Sex,
        Religion,
        MartStat,
        //---------------------------
        Degrees,
        HireLaws,
        HireTypes,
        Jobs,
        Grades,
        BranchsBirth,
        EstkakTarqee,
        PrivateStages,
        PrivateRelated,
        TarqeeReasons,
        TarqeeCycles





    }
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
