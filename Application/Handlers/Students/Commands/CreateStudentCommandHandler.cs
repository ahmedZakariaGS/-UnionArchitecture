using Application.Core.Mappings;
using Application.Interfaces.Repositories.Base;
using Application.Shared.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Students.Commands
{
    //create used class
    public class CreateStudent : IRequest<Result>, IMapFrom<Student>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }


    //implemintation(Handler) class
    internal class CreateStudentCommandHandler : IRequestHandler<CreateStudent, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Student> _Repo;

        public CreateStudentCommandHandler(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
            _Repo = _unitOfWork.Repository<Student>();
        }

        public async Task<Result> Handle(CreateStudent request, CancellationToken cancellationToken)
        {
            var recordExists = await _Repo.
                FindByCondition(x => x.Name == request.Name).
                AnyAsync(); //check if student exist 

            var result = Result.Failure(StatusResult.Exist); //handle status result

            if (!recordExists)//if record does not exist create new
            {
                var newStd = _mapper.Map<Student>(request);

                _Repo.Create(newStd);

                //save async 
                result = await _unitOfWork.CompleteAsync(cancellationToken) >=
                    (int)StatusResult.Success ? Result.Success() : Result.Failure(); //result based on status
            }

            return result; //return result exist
        }
    }
}
