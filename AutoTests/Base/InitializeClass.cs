using AutoTests.Params;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;


namespace AutoTests.Base
{
    public class InitializeClass
    {
        private static IWebDriver oDriver;

 
        public static void InitChromeBrowser(string url)
        {
            TryToInitChromedriverAndGoToUrl(url);
            oDriver.Url = url;
            Console.WriteLine("Go to url : " + url);

        }

        private static void TryToInitChromedriverAndGoToUrl(string url)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-dev-shm-usage");
                options.AddAdditionalCapability("useAutomationExtension", false);
                options.LeaveBrowserRunning = false;

                oDriver = new ChromeDriver(@"C:\ChromeDriver\", options);

                oDriver.Manage().Window.Maximize();
                oDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TestParams.timerInSec);
               
            }
            catch (Exception e)
            {
                throw new Exception("chromedriver initialize fail : " + e);
            }

           
        }

        public static IWebDriver getDriver
        {
            get { return oDriver; }
        }

        public static void Close()
        {
            oDriver.Quit();
        }


    }
}
