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
    public class UpdateStudent : IRequest<Result>, IMapFrom<Student>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    internal class UpdateStudentCommandHandler : IRequestHandler<UpdateStudent, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Student> _Repo;

        public UpdateStudentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Repo = _unitOfWork.Repository<Student>();
        }

        public async Task<Result> Handle(UpdateStudent request, CancellationToken cancellationToken)
        {
            var recordExist = await _Repo.FindByCondition(x => x.Id == request.Id).
                FirstOrDefaultAsync();

            var result = Result.Failure(StatusResult.NotExists);

            if (recordExist != null)
            {
                var UpdatedRecord = _mapper.Map<Student>(recordExist);

                _Repo.Update(UpdatedRecord);

                //complete
                result = await _unitOfWork.CompleteAsync(cancellationToken) <=
                    (int)StatusResult.Success ? Result.Success() : Result.Failure();
            }

            return result;

        }
    }
}
