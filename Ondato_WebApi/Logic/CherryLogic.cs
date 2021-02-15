using Microsoft.Extensions.Configuration;
using Ondato_WebApi.Exceptions;
using Ondato_WebApi.Logic.Interfaces;
using Ondato_WebApi.Mappers;
using Ondato_WebApi.Models;
using Ondato_WebApi.Models.Dto;
using System;
using System.Threading.Tasks;

namespace Ondato_WebApi.Logic
{
    public class CherryLogic : ICherryLogic
    {
        private readonly IConfiguration _config;
        private readonly IDataLoadLogic _dataLoadLogic;

        public CherryLogic(IConfiguration config, IDataLoadLogic dataLoadLogic)
        {
            _config = config;
            _dataLoadLogic = dataLoadLogic;
        }

        public async Task CreateUpdate(CreateUpdateRequestDto createUpdateRequestDto)
        {
            SetCherryItemExpirationDate(createUpdateRequestDto.CherryItem);

            var cherry = CherryMapper.MapToModel(createUpdateRequestDto.CherryItem);

            await _dataLoadLogic.CreateUpdate(createUpdateRequestDto.Key, cherry);
        }

        public async Task Delete(string key)
        {
            await _dataLoadLogic.Delete(key);
        }

        public async Task<CherryDto> Get(string key)
        {
            var item = await _dataLoadLogic.Get(key);
            ResetExpirationDate(item);
            await _dataLoadLogic.CreateUpdate(key, item);

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
