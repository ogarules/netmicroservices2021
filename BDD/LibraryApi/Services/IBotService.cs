using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface IBotService
    {
        Task<string> GetBotMessage();
    }
}