using Application.DTO;
using Application.Handlers.Students.Commands;
using Application.Interfaces.Repositories.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Students.Queries
{
    public record GetStudentById(int Id) : IRequest<StudentDTO>;
    internal class GetStudentByIdQueryHandler : IRequestHandler<GetStudentById, StudentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Student> _Repo;

        public GetStudentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IEnumerable<IValidator<CreateStudent>> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Repo = _unitOfWork.Repository<Student>();
        }

        public async Task<StudentDTO> Handle(GetStudentById request, CancellationToken cancellationToken)
        {
            var recordExist = await _Repo.FindByCondition(x => x.Id == request.Id).
                ProjectTo<StudentDTO>(_mapper.ConfigurationProvider).
                FirstOrDefaultAsync();

            return recordExist;
        }
    }
}
