using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Microsoft.Playwright;

namespace HeadHunterBrowserClient
{
    public class HhBrowserClient
    {
        private HhPages hh;
        private HhUser hhUser;

        public HhBrowserClient(HhUser hhUser)
        {
            hh = new HhPages("https://hh.ru");

            if (hhUser == null) { throw new ArgumentNullException(); }
            this.hhUser = hhUser;
        }

        public async Task Run()
        {
            using var playwright = await Playwright.CreateAsync();
            await using IBrowser browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1000,
                Timeout = 10_000,
                Env = new KeyValuePair<string, string>[] { new("DEBUG", "pw:browser,pw:api") }
            });

            //Прикидываемся хромом:
            BrowserNewContextOptions device = playwright.Devices["Desktop Chrome"];
            await using IBrowserContext context = await browser.NewContextAsync(device);
            IPage page = await context.NewPageAsync();
            
            await Login(page);

            await page.GotoAsync(hh.Search("asp.net core"));
            await page.ScreenshotAsync(new() { Path = "screenshot.png" });
        }

        private async Task Login(IPage page)
        {
            await page.GotoAsync(hh.Login);
            await page.ClickAsync("button[data-qa='expand-login-by-password']");
            await page.ClickAsync(hh.ButtonQa("expand-login-by-password"));
            await page.TypeAsync(hh.InputQa("login-input-username"), hhUser.Login);
            await page.TypeAsync(hh.InputQa("login-input-password"), hhUser.Password);
            await page.ClickAsync(hh.ButtonQa("account-login-submit"));
            var incorrectPassword = await page.IsVisibleAsync(hh.DivQa("account-login-error"));
            if (incorrectPassword)
            {
                throw new Exception("Incorrect password");
            }
        }

    }
}