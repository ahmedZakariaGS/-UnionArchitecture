using Application.Interfaces.Repositories.Base;
using Application.Shared.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Students.Commands
{

    public record DeleteStudent(int Id) : IRequest<Result>; //record immutable so we can use it here
    internal class DeleteStudentCommandHandler : IRequestHandler<DeleteStudent, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Student> _Repo;

        public DeleteStudentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Repo = _unitOfWork.Repository<Student>();
        }


        public async Task<Result> Handle(DeleteStudent request, CancellationToken cancellationToken)
        {
            var recordExist = await _Repo.FindByCondition(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            var result = Result.Failure(StatusResult.NotExists);

            if (recordExist != null)
            {
                //delete
                _Repo.Delete(recordExist);

                //complete
                result = await _unitOfWork.CompleteAsync(cancellationToken) >=
                    (int)StatusResult.Success ? Result.Success() : Result.Failure();

            }

            return result;
        }
    }
}
