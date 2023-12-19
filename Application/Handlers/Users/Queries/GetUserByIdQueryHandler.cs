 using Application.DTO;
 using Application.Interfaces.Repositories.Base;
 
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
 using MediatR;
using Microsoft.EntityFrameworkCore;
  
namespace Application.Handlers.Users.Commends
{
    public record GetUserById(int  Id) : IRequest<UserDTO?>;
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserById,UserDTO?>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
 
        public GetUserByIdQueryHandler(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.unitOfWork = UnitOfWork;
            this.mapper = Mapper;
         }
        public async Task<UserDTO?> Handle(GetUserById request, CancellationToken cancellationToken)
        {
             var recordExist = await unitOfWork.Repository<User>().
                            FindByCondition(e => e.Id==request.Id)
                            .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();


            return recordExist;
        }
     }
}

