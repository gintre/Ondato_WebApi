using Ondato_WebApi.Models.Dto;

namespace Ondato_WebApi.Logic.Interfaces
{
    public interface ICherryLogic
    {
        string CreateUpdate(CreateUpdateRequestDto createUpdateRequestDto);

        void Delete(string key);

        CherryDto Get(string key);
    }
}
