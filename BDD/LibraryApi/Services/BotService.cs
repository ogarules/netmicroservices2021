using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class BotService : IBotService
    {
        public virtual async Task<string> GetBotMessage()
        {
            return "Hola desde la implementacion";
        }
    }
}