using Ondato_WebApi.Models;
using Ondato_WebApi.Models.Dto;
using System.Collections.Generic;

namespace Ondato_WebApi.Mappers
{
    public static class CherryMapper
    {
        public static CherryItem MapToModel(CherryItemDto cherryItemDto)
        {
            return new CherryItem
            {
                ExpirationPeriod = cherryItemDto.ExpirationPeriod,
                Title = cherryItemDto.Title
            };
        }

        public static CherryItemDto MapToDtoModel(CherryItem cherryItemDto)
        {
            return new CherryItemDto { 
                ExpirationPeriod = cherryItemDto.ExpirationPeriod,
                Title = cherryItemDto.Title
            };
        }

        public static CherryDto MapToDtoModel(string key, CherryItem cherryItem)
        {
            var result = new CherryDto { 
                Cherries = new Dictionary<string, CherryItemDto>()
            };
            result.Cherries.Add(key, MapToDtoModel(cherryItem));

            return result;
        }
    }
}
