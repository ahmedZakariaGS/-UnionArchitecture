 using Application.DTO;
 using Application.Interfaces.Repositories.Base;
 using Application.Shared.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
 
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
 
namespace Application.Handlers.Users.Commends
{
    public record GetAllUser() : IRequest<List<UserDTO>?>;
    internal class GetAllUserQueryHandler : IRequestHandler<GetAllUser,List<UserDTO>?>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
 
        public GetAllUserQueryHandler(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.unitOfWork = UnitOfWork;
            this.mapper = Mapper;
         }
        public async Task<List<UserDTO>?> Handle(GetAllUser request, CancellationToken cancellationToken)
        {
             var recordExist = await unitOfWork.Repository<User>()
                                .FindAll()
                                .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                                .ToListAsync();
            return recordExist;
        }
     }
}

