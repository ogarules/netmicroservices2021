using System.Threading.Tasks;
using Refit;
using Service2.Models;

namespace Service2.Services
{
    public interface IValuesSerice
    {
        [Get("/api/values")]
        Task<ValuesResult> GetValues();
    }
}