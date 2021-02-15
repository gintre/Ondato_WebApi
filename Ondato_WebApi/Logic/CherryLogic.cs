using Microsoft.Extensions.Configuration;
using Ondato_WebApi.Exceptions;
using Ondato_WebApi.Helpers;
using Ondato_WebApi.Logic.Interfaces;
using Ondato_WebApi.Mappers;
using Ondato_WebApi.Models;
using Ondato_WebApi.Models.Dto;
using System;

namespace Ondato_WebApi.Logic
{
    public class CherryLogic : ICherryLogic
    {
        private readonly IConfiguration _config;
        public CherryLogic(IConfiguration config)
        {
            _config = config;
        }

        public string CreateUpdate(CreateUpdateRequestDto createUpdateRequestDto)
        {
            SetCherryItemExpirationDate(createUpdateRequestDto.CherryItem);

            var cherry = CherryMapper.MapToModel(createUpdateRequestDto.CherryItem);

            var result = DataStore.CreateUpdate(createUpdateRequestDto.Key, cherry);

            return result;
        }

        public void Delete(string key)
        {
            DataStore.Delete(key);
        }

        public CherryDto Get(string key)
        {
            var item = DataStore.Get(key);
            ResetExpirationDate(item);
            DataStore.CreateUpdate(key, item);

            var result = CherryMapper.MapToDtoModel(key, item);
            return result;
        }

        private void SetCherryItemExpirationDate(CherryItemDto cherryItem)
        {
            if (!cherryItem.ExpirationPeriod.HasValue || cherryItem.ExpirationPeriod.Value == DateTime.MinValue)
            {
                cherryItem.ExpirationPeriod = _config.GetValue<DateTime>("BusinessConstants:DefaultObjectExpirationDate");
            }
            else if((cherryItem.ExpirationPeriod.Value - DateTime.Now).TotalHours > _config.GetValue<int>("BusinessConstants:MaxExpirationPeriodInHours"))
            {
                throw new ExpirationDateException();
            }
        }

        private void ResetExpirationDate(CherryItem cherryItem)
        {
            if (cherryItem.ExpirationPeriod.HasValue)
            {
                cherryItem.ExpirationPeriod = null;
            }
        }
    }
}
