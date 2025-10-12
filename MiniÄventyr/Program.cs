using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Threading.Channels;

namespace MiniÄventyr
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
       
    }
}
