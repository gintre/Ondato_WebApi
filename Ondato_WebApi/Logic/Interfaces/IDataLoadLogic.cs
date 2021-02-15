using Ondato_WebApi.Models;
using System.Threading.Tasks;

namespace Ondato_WebApi.Logic.Interfaces
{
    public interface IDataLoadLogic
    {
        Task CreateUpdate(string key, CherryItem cherry);

        Task Delete(string key);

        Task<CherryItem> Get(string key);
    }
}
