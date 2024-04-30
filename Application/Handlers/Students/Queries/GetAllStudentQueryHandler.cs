using Application.DTO;
using Application.Interfaces.Repositories.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Students.Queries
{

    public record GetAllStudents() : IRequest<List<StudentDTO>>;
    internal class GetAllStudentQueryHandler : IRequestHandler<GetAllStudents, List<StudentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Student> _Repo;

        public GetAllStudentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Repo = _unitOfWork.Repository<Student>();
        }


        public async Task<List<StudentDTO>> Handle(GetAllStudents request, CancellationToken cancellationToken)
        {
            var recordExist = await _Repo.FindAll().
                ProjectTo<StudentDTO>(_mapper.ConfigurationProvider).
                ToListAsync();

            return recordExist;
        }
    }
}
