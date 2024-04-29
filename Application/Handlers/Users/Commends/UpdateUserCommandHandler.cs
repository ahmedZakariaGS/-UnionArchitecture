
using Application.Interfaces.Repositories.Base;
using Application.Shared.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Handlers.Users.Commends
{
    public class UpdateUser : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUser, Result>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;




        public UpdateUserCommandHandler(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.unitOfWork = UnitOfWork;
            this.mapper = Mapper;
        }
        public async Task<Result> Handle(UpdateUser request, CancellationToken cancellationToken)
        {

            var recordExist = await unitOfWork.Repository<User>().
                            FindByCondition(e => e.Id == request.Id)
                            .FirstOrDefaultAsync();

            var result = Result.Failure(StatusResult.NotExists);
            if (recordExist != null)
            {
                //1) Delete recordExist
                recordExist.Name = request.Name;
                unitOfWork.Repository<User>().Update(recordExist);

                //2) CompleteAsync
                result = await unitOfWork.CompleteAsync(cancellationToken) >= (int)StatusResult.Success ? Result.Success() : Result.Failure();
            }



            return result;
        }

    }
}
