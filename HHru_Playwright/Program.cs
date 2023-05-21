using HeadHunterBrowserClient;
using Microsoft.Playwright;

namespace HHru_Playwright
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HH();
            Console.ReadKey();
        }

        public static async void HH()
        {
            HhUser user = new HhUser("_", "_");
            HhBrowserClient hhBrowserClient = new HhBrowserClient(user);
            await hhBrowserClient.Run();
        }
    }
}