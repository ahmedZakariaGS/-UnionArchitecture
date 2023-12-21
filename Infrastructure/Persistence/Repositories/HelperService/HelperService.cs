using Application.DTO;
 using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using Dapper;
 using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Application.Interfaces.OutSourceService;
using AutoMapper;
using Persistence.Contexts;
using Application.Interfaces.Repositories.Base;
using Application.DTO.Helper;
using Domain.Enums;
using Application.Shared.Models;

namespace Application.Interfaces.HelperService
{
    public class HelperService : IHelperService
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationSetup _applicationSetup;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;




        public HelperService(ApplicationDbContext context, IOptions<ApplicationSetup> options, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            this._applicationSetup = options.Value;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DropDownsDTO> FillDropDowns(List<int> PagesID)
        {

            DropDownsDTO dropdownsDataFromOutApi = new DropDownsDTO();

            IOutSourceApi IOutSourceApi = new OutSourceApi();
            var DropDownsURL = this._applicationSetup.LookupsURL + "api/Helpers/GetDropDowns";
            var apiResult = IOutSourceApi.Create(DropDownsURL, PagesID);
            if (apiResult != null)
            {
                dropdownsDataFromOutApi = JsonConvert.DeserializeObject<DropDownsDTO>(apiResult);
            }
            else
            {
                dropdownsDataFromOutApi = null;
            }

            DropDownsDTO dropdownsData = new DropDownsDTO();
            IDbConnection connection = _context.Database.GetDbConnection();
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {

                    _context.Database.UseTransaction(transaction as DbTransaction);
                    var tasks = new List<Task<IReadOnlyCollection<LookUpsDTO>>>();
                    if (PagesID.Contains((int)enumPagesID.Nationalities))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Nationalities = dropdownsDataFromOutApi.Nationalities;
                        }
                       
                    }

                    if (PagesID.Contains((int)enumPagesID.Countries))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Countries = dropdownsDataFromOutApi.Countries;
                        }

                    }
                    if (PagesID.Contains((int)enumPagesID.Stages))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Stages = dropdownsDataFromOutApi.Stages;
                        }
                      
                    }
                    if (PagesID.Contains((int)enumPagesID.StageType))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.StageType = dropdownsDataFromOutApi.StageType;
                        }
                      
                    }
                    if (PagesID.Contains((int)enumPagesID.Branchs))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Branchs = dropdownsDataFromOutApi.Branchs;
                        }
                      
                    }
                    if (PagesID.Contains((int)enumPagesID.BranchsBirth))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.BranchsBirth = dropdownsDataFromOutApi.BranchsBirth;
                        }

                    }
                    if (PagesID.Contains((int)enumPagesID.Edarat))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Edarat = dropdownsDataFromOutApi.Edarat;
                        }
                       
                    }
                    if (PagesID.Contains((int)enumPagesID.Related))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Related = dropdownsDataFromOutApi.Related;
                        }
                     
                    }
                    if (PagesID.Contains((int)enumPagesID.RelatedGov))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.RelatedGov = dropdownsDataFromOutApi.RelatedGov;
                        }

                    }

                

                    if (PagesID.Contains((int)enumPagesID.Sex))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Sex = dropdownsDataFromOutApi.Sex;
                        }
                    }

                    if (PagesID.Contains((int)enumPagesID.Religion))
                    {
                        if (dropdownsDataFromOutApi != null)
                        {
                            dropdownsData.Religion = dropdownsDataFromOutApi.Religion;
                        }
                    }

                  
                    //------------------------------------------------------------

                    if (PagesID.Contains((int)enumPagesID.EmpFlags))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.EmpFlags = (await connection.QueryAsync<LookUpsDTO>("select EMP_FLAG ID,EMP_FLAG Code , EmpFlagTypeID ParentID,EMP_FLAG_DESC LatinName,EMP_FLAG_DESC ArabicName from CDE_EMP_FLAG where EMP_FLAG not in ( 5 ,6)", null, transaction)).OrderBy(f=>f.ArabicName).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.EmpStatus))//**
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.EmpStatus = (await connection.QueryAsync<LookUpsDTO>("select STATUS_ID ID,STATUS_DESC Code,STATUS_DESC LatinName,STATUS_DESC ArabicName from EMP_STATUS", null, transaction)).AsList()));//new { BranchId = _ApplicationUser.BRANCH_ID }
                    }
                    if (PagesID.Contains((int)enumPagesID.LenderGeha))//**
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.LenderGeha = (await connection.QueryAsync<LookUpsDTO>("select GEHA_CODE ID,GEHA_CODE Code,GEHA LatinName,GEHA ArabicName from Lender_Geha", null, transaction)).AsList()));//new { BranchId = _ApplicationUser.BRANCH_ID }
                    }
                    if (PagesID.Contains((int)enumPagesID.Subjects))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.Subjects = (await connection.QueryAsync<LookUpsDTO>("select M_CODE ID,M_CODE Code ,OSTAGE ParentID ,MATIRIALS LatinName,MATIRIALS ArabicName from MADASTAGE  ", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.SuperVisorJobs))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.SuperVisorJobs = (await connection.QueryAsync<LookUpsDTO>("select JOB_CODE ID,JOB_CODE Code ,H_L_JOB_TYPE_ID ParentID ,JOB_DESC LatinName,JOB_DESC ArabicName from CDE_JOBS   where  H_L_JOB_TYPE_ID in (1,2,3) ", null, transaction)).AsList()));

                    }
                    if (PagesID.Contains((int)enumPagesID.JobTypes))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.JobTypes = (await connection.QueryAsync<LookUpsDTO>("select H_L_JOB_TYPE_CODE ID,H_L_JOB_TYPE_CODE Code,H_L_JOB_TYPE_DESC LatinName,H_L_JOB_TYPE_DESC ArabicName from CDE_H_L_JOB_TYPES", null, transaction)).AsList()));
                    }

                   if (PagesID.Contains((int)enumPagesID.JobsSetting))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.JobsSetting = (await connection.QueryAsync<LookUpsDTO>("select Super_JOB_CODE  ID, JOB_CODE ParentID from HighLevelJobsSetting", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.Qualifications))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.Qualifications = (await connection.QueryAsync<LookUpsDTO>(" select QLF_CODE  ID,  QLF_TYP  Code, QLF_DESC LatinName, QLF_DESC ArabicName  from CDE_QUALIFICATIONS", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.QualificationEntities))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.QualificationEntities = (await connection.QueryAsync<LookUpsDTO>("select QUAL_ENT_CODE  ID,QUAL_ENT_CODE Code,QUAL_ENT_TYPE ParentID, QUAL_ENT_DESC LatinName, QUAL_ENT_DESC ArabicName  from CDE_QUALIFICATION_ENT", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.QualificationTypes))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.QualificationTypes = (await connection.QueryAsync<LookUpsDTO>("select  FLAGE ID,QLF_TYP Code,QUAL_ENT_TYPE ParentID, QLF_TYP_DESC LatinName, QLF_TYP_DESC ArabicName  from CDE_QLF_TYPES", null, transaction)).AsList()));
                    }
                  
                    if (PagesID.Contains((int)enumPagesID.MartStat))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.MartStat = (await connection.QueryAsync<LookUpsDTO>("select MART_STAT_ID ID,MART_DESC ArabicName from MART_STAT", null, transaction)).AsList()));
                    }

                    if (PagesID.Contains((int)enumPagesID.Degrees))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.Degrees = (await connection.QueryAsync<LookUpsDTO>("select DEGREE_CODE  ID,  DEGREE_CODE Code, DEGREE_DESC LatinName, DEGREE_DESC ArabicName  from CDE_DEGREES", null, transaction)).AsList()));
                    }

                    if (PagesID.Contains((int)enumPagesID.HireLaws))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.HireLaws = (await connection.QueryAsync<LookUpsDTO>("select HireLawCode ID,  HireLawCode Code, Description LatinName, Description ArabicName  from HireLaw", null, transaction)).AsList()));
                    }

                    if (PagesID.Contains((int)enumPagesID.HireTypes))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.HireTypes = (await connection.QueryAsync<LookUpsDTO>("select TYPE_CODE  Code, TYPE_DESC LatinName, TYPE_DESC ArabicName from CDE_HIER_TYPE", null, transaction)).AsList()));
                    }

                    if (PagesID.Contains((int)enumPagesID.Jobs))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.Jobs = (await connection.QueryAsync<LookUpsDTO>("select  JOB_CODE ID, KADR_JOB_GROUP  Code , JOB_CLS_CODE ParentID,  M_FLAG LatinName, JOB_DESC ArabicName from CDE_JOBS", null, transaction)).AsList()));
                    } 
                    if (PagesID.Contains((int)enumPagesID.Grades))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.Grades = (await connection.QueryAsync<LookUpsDTO>("select  GRADE_CODE ID, GRADE_CODE  Code ,  GRADE_DESC LatinName, GRADE_DESC ArabicName from CDE_GRADES", null, transaction)).AsList()));
                    }


                    if (PagesID.Contains((int)enumPagesID.EstkakTarqee))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.EstkakTarqee = (await connection.QueryAsync<LookUpsDTO>("select NO_ESTKAK_CODE ID,NO_ESTKAK_CODE Code, NO_ESTKAK_DESC LatinName,NO_ESTKAK_DESC ArabicName from CDE_NO_ESTKAK_TARQEE", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.PrivateStages))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.PrivateStages = (await connection
                        .QueryAsync<LookUpsDTO>("select distinct  STAGE_CODE ID, STAGE_CODE Code, STAGE_DESC LatinName,STAGE_DESC ArabicName from employee.dbo.Special_schools_security_notReg", null, transaction)).AsList()));
                    }

                    if (PagesID.Contains((int)enumPagesID.PrivateRelated))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.PrivateRelated = (await connection
                        .QueryAsync<LookUpsDTO>("select distinct  REL_CODE ID, REL_CODE Code, REL_DESC LatinName,REL_DESC ArabicName from employee.dbo.Special_schools_security_notReg", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.TarqeeReasons))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.TarqeeReasons = (await connection
                        .QueryAsync<LookUpsDTO>("select  ESTKAK_TYPE_CODE ID, ESTKAK_TYPE_CODE Code,ESTKAK_TYPE_GROUP ParentID, ESTKAK_TYPE_DESC LatinName,ESTKAK_TYPE_DESC ArabicName from employee.dbo.CDE_ESTKAK_TARQEE_TYPES", null, transaction)).AsList()));
                    }
                    if (PagesID.Contains((int)enumPagesID.TarqeeCycles))
                    {
                        tasks.Add(Task.Run(async () => dropdownsData.TarqeeCycles = (await connection
                        .QueryAsync<LookUpsDTO>("select  CYCLE_CODE ID, CYCLE_CODE Code,CYCLE_YEAR ParentID, CYCLE_DESC LatinName,CYCLE_NAME ArabicName from employee.dbo.CDE_EMP_UPGRAD_CYCLE", null, transaction)).AsList()));
                    }



                    var data = await Task.WhenAll(tasks.ToArray());
                    return dropdownsData;


                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    dropdownsData = new DropDownsDTO();

                    return dropdownsData;
                }
                finally
                {

                    connection.Close();
                }
            }



        }

        

    }
}
