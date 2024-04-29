using Application.Core.Mappings;
using Application.Interfaces.Repositories.Base;
using Application.Shared.Models;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Students.Commands
{
    //create used class
    public class CreateStudent : IRequest<Request>, IMapFrom<Student>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    internal class CreateStudentCommandHandler : IRequestHandler<CreateStudent, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateStudentCommandHandler(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
        }

        public Task<Result> Handle(CreateStudent request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
