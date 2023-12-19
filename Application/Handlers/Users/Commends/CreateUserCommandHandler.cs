
using Application.Core.Mappings;
using Application.Interfaces.Repositories.Base;
using Application.Shared.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
 using MediatR;
using Microsoft.EntityFrameworkCore;
 

namespace Application.Handlers.Users.Commends
{
    public class CreateUser:IRequest<Result>,IMapFrom<User>
    {
        public string Name { get; set; }


    }
    internal class CreateUserCommandHandler : IRequestHandler<CreateUser, Result>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
 

        public CreateUserCommandHandler(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.unitOfWork = UnitOfWork;
            this.mapper = Mapper;
 

        }

        public async Task<Result> Handle(CreateUser request, CancellationToken cancellationToken)
        {

            var recordExist = await unitOfWork.Repository<User>().
                              FindByCondition(e =>e.Name==request.Name)
                              .AnyAsync();   
            
            var result = Result.Failure(StatusResult.Exist);

            if (!recordExist)
            {

                var newUser = mapper.Map<User>(request);
                 unitOfWork.Repository<User>().Create(newUser);

                //4) CompleteAsync
                result = await unitOfWork.CompleteAsync(cancellationToken) >= (int)StatusResult.Success ? Result.Success() : Result.Failure();


            }


            return result;
        }
    }
}
