using Application.DTO.Helper;

namespace Application.Interfaces.HelperService
{
    public interface IHelperService
    {
        Task<DropDownsDTO> FillDropDowns(List<int> PagesID);
 

    }

}
