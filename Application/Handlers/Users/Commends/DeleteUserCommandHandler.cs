 
using Application.Interfaces.Repositories.Base;
using Application.Shared.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
 using MediatR;
using Microsoft.EntityFrameworkCore;
 
namespace Application.Handlers.Users.Commends
{
    public record DeleteUser(int id ): IRequest<Result>;
  
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUser, Result>
    {

        private readonly IUnitOfWork unitOfWork;
  

        public DeleteUserCommandHandler(IUnitOfWork UnitOfWork)
        {
            this.unitOfWork = UnitOfWork;
   

        }
        public async Task<Result> Handle(DeleteUser request, CancellationToken cancellationToken)
        {

            var recordExist = await unitOfWork.Repository<User>().
                             FindByCondition(e => e.Id==request.id)
                             .FirstOrDefaultAsync();

            var result = Result.Failure(StatusResult.NotExists);
            if (recordExist != null)
            {
                 //1) Delete recordExist
                unitOfWork.Repository<User>().Delete(recordExist);

                //2) CompleteAsync
                result = await unitOfWork.CompleteAsync(cancellationToken) >= (int)StatusResult.Success ? Result.Success() : Result.Failure();
            }
            return result;
        }
    }
}
