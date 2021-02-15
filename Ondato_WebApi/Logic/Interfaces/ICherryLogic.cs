using Ondato_WebApi.Models.Dto;
using System.Threading.Tasks;

namespace Ondato_WebApi.Logic.Interfaces
{
    public interface ICherryLogic
    {
        Task CreateUpdate(CreateUpdateRequestDto createUpdateRequestDto);

        Task Delete(string key);

        Task<CherryDto> Get(string key);
    }
}
